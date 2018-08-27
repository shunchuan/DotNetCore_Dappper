using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Dappper.Model.Authentication
{
    public class JwtClaimModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
