using System;
using System.Collections.Generic;
using System.Text;
using BeehooeDataService.Model.Enmu;

namespace BeehooeDataService.Model
{
    public class DatabaseModel
    {
        public DBTYPE Dbtype { set; get; }

        public string DbConnectStr { set; get; }
    }
}
