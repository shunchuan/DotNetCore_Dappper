using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BeehooeDataService.Infrastructure;
using BeehooeDataService.Model;
using BeehooeDataService.Model.Enmu;

namespace BeehooeDataService.Domain
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
            DatabaseModel model = ReadDatabase.CreateInstance.ReaDatabaseConfig();
            IDbConnection con = null;
            switch (model.Dbtype)
            {
                case DBTYPE.MySql :
                    con = new MySql.Data.MySqlClient.MySqlConnection(model.DbConnectStr);
                    break;
                case DBTYPE.SqlServer:
                    con = new System.Data.SqlClient.SqlConnection(model.DbConnectStr);
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
