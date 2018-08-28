using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Infrastructure.Jwt;
using DotNetCore_Dappper.Model.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCore_Dappper.Test
{
    [TestClass]
    public class JwtTest
    {
        [TestMethod]
        public void GenerateToken()
        {
            var token= new JwtManager().GenerateToken(new JwtClaimModel() {RoleId = "TheRoleID", RoleName = "TheRoleName"});
        }
    }
}
