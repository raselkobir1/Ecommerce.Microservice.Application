using Common;
using JwtAuthManager.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthManager
{
    public class JwtTokenHandler
    {
        private readonly JwtTokenConfiguration _jwtTokenConfiguration;

        public JwtTokenHandler(IOptionsSnapshot<JwtTokenConfiguration> _tokenAppsettingConfig)
        {
            _jwtTokenConfiguration = _tokenAppsettingConfig.Value;
        }

        public TokenResponse GenerateJWTTokensAsync(UserForClaimDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtTokenConfiguration.SigningKey);
            var expiryTime = CommonMethods.GetCurrentTime().AddMinutes(Convert.ToDouble(_jwtTokenConfiguration.JWTTokenExpirationMinutes));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("IsSuperAdmin", user.IsSuperAdmin.ToString()),
                    new Claim("UserName", user.UserName.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),

                Expires = expiryTime,
                Issuer = _jwtTokenConfiguration.Issuer,
                Audience = _jwtTokenConfiguration.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new TokenResponse
            {
                JWTToken = accessToken,
                JWTExpires = (DateTime)tokenDescriptor.Expires,
            };
        }

        private TokenResponse GenerateRefreshToken()
        {
            var expiryTime = CommonMethods.GetCurrentTime().AddMinutes(Convert.ToDouble(_jwtTokenConfiguration.RefreshTokenExpirationMinutes));
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new TokenResponse
            {
                RefreshToken = Convert.ToBase64String(randomNumber),
                RefreshExpires = expiryTime
            };
        }
    }
}
