using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehooeDataService.Domain
{
    public static class DbHelper
    {
        /// <summary>
        /// 查询数据-集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                return con.Query(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 查询数据-集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                return con.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 执行异步查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                return con.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 执行异步查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition commandDefinition)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                return con.QueryAsync<T>(commandDefinition);
            }
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="totalCount"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryMultiple<T>(string sql, out int totalCount, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                var multi = con.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

                totalCount = int.Parse(multi.Read<long>().Single().ToString());
                return multi.Read<T>();
            }
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static T QueryOne<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) where T : class
        {
            var dataResult = Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            return dataResult != null && dataResult.Count() > 0 ? dataResult.ToList<T>()[0] : null;
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {

            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                return con.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(string sql, object param = null)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                con.Open();
                return con.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static T Execute<T>(string command, Dictionary<string, object> paras)
        {
            using (IDbConnection con = DbConnectionFactory.Instance.GetOpenConnection())
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = command;
                com.CommandType = CommandType.StoredProcedure;

                if (paras != null)
                {
                    foreach (var item in paras.Keys)
                    {
                        IDbDataParameter para = com.CreateParameter();
                        para.Value = paras[item];
                        para.ParameterName = item;
                        com.Parameters.Add(para);
                    }
                }

                con.Open();
                return (T)com.ExecuteScalar();
            }
        }

        /// <summary>
        /// 读取blob字段
        /// </summary>
        /// <returns></returns>
        public static byte[] ReadBlob(string command, object paras)
        {
            return QueryOne<byte[]>(command, paras, null, false, null, CommandType.Text);
        }

        /// <summary>
        /// 更新blob
        /// </summary>
        /// <param name="command"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool WriteBlob(string command, object param)
        {
            int effactRows = Execute(command, param, null, null, CommandType.Text);

            return effactRows > 0;
        }
    }
}
