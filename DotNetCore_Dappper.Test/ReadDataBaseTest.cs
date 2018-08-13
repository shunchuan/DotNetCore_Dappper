using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Infrastructure;
using DotNetCore_Dappper.Model;
using DotNetCore_Dappper.Model.Enmu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCore_Dappper.Test
{
    [TestClass]
    public class ReadDataBaseTest
    {
        [TestMethod]
        public void ReaDatabaseConfigTest()
        {
            var model = new DatabaseModel
            {
                Dbtype = DBTYPE.MySql,
                ConnectStr =
                    "Server=192.168.3.5;Database=Test;User ID=test;Password=123456789;SslMode=none;Charset=utf8;"
            };
            Assert.AreEqual(ReadDatabase.CreateInstance.ReaDatabaseConfig(), model);
        }
    }
}
