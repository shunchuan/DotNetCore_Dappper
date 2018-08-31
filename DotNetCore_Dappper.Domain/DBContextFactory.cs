using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
            if (null == model.Type)
            {
                throw new SqlNullValueException();
            }
            switch (model.Type.ToUpper())
            {
                case "MYSQL":
                    model.Dbtype = DBTYPE.MySql;
                    break;
                case "MSSQL":
                    model.Dbtype = DBTYPE.SqlServer;
                    break;
                default:
                    throw new NullReferenceException();
            }

            return GetConn(model);
        }

        public IDbConnection GetOpenConnection(string connectStr, string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException();
            }

            DBTYPE dbtype ;
            

            switch (type.ToUpper())
            {
                case "MYSQL":
                    dbtype = DBTYPE.MySql;
                    break;
                case "MSSQL":
                    dbtype = DBTYPE.SqlServer;
                    break;
                default:
                    throw new NullReferenceException();
            }
            return GetConn(new DatabaseModel() { Dbtype = dbtype, ConnectStr = connectStr });
        }

        public IDbConnection GetOpenConnection(string connectStr, DBTYPE type)
        {
            return GetConn(new DatabaseModel(connectStr, type));
        }

        private IDbConnection GetConn(DatabaseModel model)
        {
            if (string.IsNullOrEmpty(model.ConnectStr))
            {
                throw new NullReferenceException();
            }
            switch (model.Dbtype)
            {
                case DBTYPE.MySql:
                    return new MySql.Data.MySqlClient.MySqlConnection(model.ConnectStr);
                case DBTYPE.SqlServer:
                    return new System.Data.SqlClient.SqlConnection(model.ConnectStr);
                default:
                    throw new NullReferenceException();
            }
        }
    }
}
