using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthManager.Model
{
    public class UserForClaimDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }
    }
}
