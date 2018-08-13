using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DotNetCore_Dappper.Infrastructure;
using DotNetCore_Dappper.Model;
using DotNetCore_Dappper.Model.Enmu;

namespace DotNetCore_Dappper.Domain
{
    public sealed class DbConnectionFactory
    {
        private static DbConnectionFactory _instance;
        private DbConnectionFactory()
        { }

        public static DbConnectionFactory Instance => _instance ?? (_instance = new DbConnectionFactory());

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            IDbConnection con = null;
            //DatabaseModel model = ReadDatabase.CreateInstance.ReaDatabaseConfig();
            //switch (model.Dbtype)
            //{
            //    case DBTYPE.MySql:
            //        con = new MySql.Data.MySqlClient.MySqlConnection(model.ConnectStr);
            //        break;
            //    case DBTYPE.SqlServer:
            //        con = new System.Data.SqlClient.SqlConnection(model.ConnectStr);
            //        break;
            //    default:
            //        break;
            //}
            DatabaseModel model = ReadDatabase.CreateInstance.DatabaseConfig();
            switch (model.Type.ToUpper())
            {
                case "MYSQL":
                    con = new MySql.Data.MySqlClient.MySqlConnection(model.ConnectStr);
                    break;
                case "MSSQL":
                    con = new System.Data.SqlClient.SqlConnection(model.ConnectStr);
                    break;
                default:
                    break;
            }

            if (con == null)
            {
                throw new Exception("数据库连接配置不正确。");
            }

            return con;
        }
    }
}
