using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Model.Enmu;

namespace DotNetCore_Dappper.Model
{
    public class DatabaseModel
    {
        public DBTYPE Dbtype { set; get; }

        public string Type { set; get; }

        public string ConnectStr { set; get; }
    }
}
