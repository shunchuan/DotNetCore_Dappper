using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Domain;
using DotNetCore_Dappper.Domain.IRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCore_Dappper.Test
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void GetDataAccess()
        {
            Assert.IsInstanceOfType(DataAccess<IUserInfo>.CreateObject("UserInfo"), typeof(IUserInfo));
        }
    }
}
