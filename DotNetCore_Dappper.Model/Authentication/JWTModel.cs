using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Dappper.Model.Authentication
{
    public class JwtModel
    {
        public string JwtKey { set; get; }

        public string JwtIssuer { set; get; }

        public string JwtAudience { set; get; }

        public double JwtExpireDays { set; get; } = 30;
    }
}
