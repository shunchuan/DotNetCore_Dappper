using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Dappper.Model.Authentication
{
    public class JwtDto
    {
        public string Type { get; set; }

        public string AccessToken { get; set; }

        public DateTime Expires { get; set; }
    }
}
