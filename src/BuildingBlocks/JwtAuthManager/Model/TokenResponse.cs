using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthManager.Model
{
    public class TokenResponse
    {
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
        //public string Email { get; set; }
        //public string UserName { get; set; }
        public DateTime JWTExpires { get; set; }
        public DateTime RefreshExpires { get; set; }
        //public bool IsPasswordUpdatedByUser { get; set; }
    }
}
