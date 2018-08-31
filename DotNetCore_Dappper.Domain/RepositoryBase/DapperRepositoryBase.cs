using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using DotNetCore_Dappper.Model.Enmu;

namespace DotNetCore_Dappper.Domain.RepositoryBase
{
    public class DapperRepositoryBase: IDapperRepositoryBase
    {
        private readonly IDbConnection _con = null;

        #region 构造函数

        public DapperRepositoryBase()
        {
            _con = DbConnectionFactory.Instance.GetOpenConnection();
        }

        public DapperRepositoryBase(string connectStr, string type)
        {
            _con = DbConnectionFactory.Instance.GetOpenConnection(connectStr, type);
        }

        public DapperRepositoryBase(string connectStr, DBTYPE type)
        {
            _con = DbConnectionFactory.Instance.GetOpenConnection(connectStr, type);
        }

        #endregion

        public T Get<T>(object id) where T : class
        {
            using (_con)
            {
                return _con.Get<T>(id);
            }
        }

        public  Task<T> GetAsync<T>(object id) where T : class
        {
            using (_con)
            {
                return _con.GetAsync<T>(id);
            }
        }
        public IEnumerable<T> GetAll<T>() where T:class 
        {
            using (_con)
            {
                return _con.GetAll<T>();
            }
        }



        public long Insert<T>(T obj) where T : class
        {
            using (_con)
            {
                return _con.Insert(obj);
            }
        }

        public long Insert<T>(List<T> list) 
        {
            using (_con)
            {
                return _con.Insert(list);
            }
        }

        public bool Update<T>(T obj) where T : class
        {
            using (_con)
            {
                return _con.Update(obj);
            }
        }

        public bool Update<T>(List<T> list)
        {
            using (_con)
            {
                return _con.Update(list);
            }
        }

        public bool Delete<T>(T obj) where T : class
        {
            using (_con)
            {
                return _con.Delete(obj);
            }
        }

        public bool Delete<T>(List<T> list)
        {
            using (_con)
            {
                return _con.Delete(list);
            }
        }

        public bool DeleteAll<T>() where T : class
        {
            using (_con)
            {
                return _con.DeleteAll<T>();
            }
        }

        /// <summary>
        /// 查询数据-集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public  IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (_con)
            {
                return _con.Query(sql, param, transaction, buffered, commandTimeout, commandType);
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
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (_con)
            {
                return _con.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
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
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (_con)
            {
                return _con.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 执行异步查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition commandDefinition)
        {
            using (_con)
            {
                _con.Open();
                return _con.QueryAsync<T>(commandDefinition);
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
        public IEnumerable<T> QueryMultiple<T>(string sql, out int totalCount, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (_con)
            {
                var multi = _con.QueryMultiple(sql, param, transaction, commandTimeout, commandType);

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
        public T QueryOne<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) where T : class
        {
            var dataResult = Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            return dataResult != null && dataResult.Any()? dataResult.ToList()[0] : null;
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
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {

            using (_con)
            {
                return _con.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object param = null)
        {
            using (_con)
            {
                return _con.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public T Execute<T>(string command, Dictionary<string, object> paras)
        {
            using (_con)
            {
                var com = _con.CreateCommand();
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
                
                return (T)com.ExecuteScalar();
            }
        }

        /// <summary>
        /// 读取blob字段
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBlob(string command, object paras)
        {
            return QueryOne<byte[]>(command, paras, null, false, null, CommandType.Text);
        }

        /// <summary>
        /// 更新blob
        /// </summary>
        /// <param name="command"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool WriteBlob(string command, object param)
        {
            var effactRows = Execute(command, param, null, null, CommandType.Text);

            return effactRows > 0;
        }
    }
}
