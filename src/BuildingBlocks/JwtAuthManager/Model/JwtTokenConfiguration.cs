using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthManager.Model
{
    public class JwtTokenConfiguration
    {
        [Required(AllowEmptyStrings = false)]
        public string Issuer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Audience { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SigningKey { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string JWTTokenExpirationMinutes { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string RefreshTokenExpirationMinutes { get; set; }
    }
}
