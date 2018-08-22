namespace DotNetCore_Dappper.Domain.RepositoryBase
{
    using DapperExtensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using Dapper;

    public class DapperExtensionsRepositoryBase<T> : IDapperExtensionsRepositoryBase<T> where T :class
    {
        public IDbConnection Connection => DbConnectionFactory.Instance.GetOpenConnection();

        #region MyRegion

        public virtual IEnumerable<T> GetList()
        {
            using (Connection)
            {
                return Connection.GetList<T>();
            }

        }

        public virtual T Get(object id)
        {
            using (Connection)
            {
                return Connection.Get<T>(id);
            }
        }

        public virtual bool Update(T t)
        {
            using (Connection)
            {
                return Connection.Update(t);
            }
        }

        public virtual T Insert(T apply)
        {
            using (Connection)
            {
                Connection.Insert(apply);
                return apply;
            }
        }

        public virtual bool Delete(T t)
        {
            using (Connection)
            {
                return Connection.Delete(t);
            }
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, object>> expression, Operator op, object param)
        {
            using (Connection)
            {
                return Connection.GetList<T>(Predicates.Field(expression, op, param));
            }
        }


        #endregion

        /// <summary>
        /// 批量插入指定实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            using (Connection)
            {
                Connection.Insert(entities);
            }
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns>删除结果</returns>
        public virtual bool Delete(object predicate)
        {
            using (Connection)
            {
                return Connection.Delete<T>(predicate);
            }
        }

        /// <summary>
        /// 根据查询条件获取实体列表
        /// （查询使用谓词或匿名对象）
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        public virtual IEnumerable<T> GetList(object predicate)
        {
            using (Connection)
            {
                return Connection.GetList<T>(predicate);
            }
        }

        /// <summary>
        /// 根据查询条件和排序条件获取实体列表
        /// （查询使用谓词或匿名对象，排序使用Sort或匿名对象）
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sorts">排序条件</param>
        /// <returns>实体列表</returns>
        public virtual IEnumerable<T> GetList(object predicate, List<ISort> sorts)
        {
            using (Connection)
            {
                return Connection.GetList<T>(predicate, sorts);
            }
        }

        /// <summary>
        /// 根据查询条件和排序条件获取实体分页列表
        /// （查询使用谓词或匿名对象，排序使用Sort或匿名对象）
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sorts">排序条件</param>
        /// <param name="pageNumber">页号，从1起始</param>
        /// <param name="itemsPerPage">每页条数</param>
        /// <returns>实体分页列表</returns>
        public virtual IEnumerable<T> GetListPaged(object predicate, List<ISort> sorts,
            int pageNumber, int itemsPerPage)
        {
            using (Connection)
            {
                return Connection.GetPage<T>(predicate,
                    sorts, pageNumber - 1, itemsPerPage).ToList();
            }
        }


        /// <summary>
        /// 根据查询条件和排序条件获取实体区间列表
        /// （查询使用谓词或匿名对象，排序使用Sort或匿名对象）
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sorts">排序条件</param>
        /// <param name="firstResult">起始行数</param>
        /// <param name="maxResults">最大条数</param>
        /// <returns>实体区间列表</returns>
        public virtual IEnumerable<T> GetSet(object predicate, List<ISort> sorts,
            int firstResult, int maxResults)
        {
            using (Connection)
            {
                return Connection.GetSet<T>(predicate, sorts,
                    firstResult, maxResults).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取实体条数
        /// （条件使用谓词或匿名对象）
        /// </summary>
        /// <param name="predicate">条件，使用谓词或匿名对象</param>
        /// <returns>实体条数</returns>
        public virtual int Count(object predicate)
        {
            using (Connection)
            {
                return Connection.Count<T>(predicate);
            }
        }

        /// <summary>
        /// 使用SQL语句获取实体集合
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <returns>实体集合</returns>
        public virtual IEnumerable<T> Query(string query)
        {
            using (Connection)
            {
                return Connection.Query<T>(query);
            }
        }

        /// <summary>
        /// 使用SQL语句获取实体集合
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>实体集合</returns>
        public virtual IEnumerable<T> Query(string query, object parameters)
        {
            using (Connection)
            {
                return Connection.Query<T>(query, parameters);
            }
        }

        /// <summary>
        /// 使用SQL语句获取实体集合
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>实体集合</returns>
        public virtual IEnumerable<T> Query(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                return Connection.Query<T>(query, parameters, commandType: commandType);
            }
        }

        /// <summary>
        /// 使用SQL语句获取指定实体集合
        /// </summary>
        /// <typeparam name="TAny">返回实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>实体集合</returns>
        public virtual IEnumerable<TAny> Query<TAny>(string query)
        {
            using (Connection)
            {
                return Connection.Query<TAny>(query);
            }
        }

        /// <summary>
        /// 使用SQL语句获取指定实体集合
        /// </summary>
        /// <typeparam name="TAny">返回实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>实体集合</returns>
        public virtual IEnumerable<TAny> Query<TAny>(string query, object parameters)
        {
            using (Connection)
            {
                return Connection.Query<TAny>(query, parameters);
            }
        }

        /// <summary>
        /// 使用SQL语句获取指定实体集合
        /// </summary>
        /// <typeparam name="TAny">返回实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>实体集合</returns>
        public virtual IEnumerable<TAny> Query<TAny>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                return Connection.Query<TAny>(query, parameters, commandType: commandType);
            }
        }

        #region 使用SQL语句获取多个指定实体集合

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>
            Query<TFirst, TSecond>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>
            Query<TFirst, TSecond>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>
            Query<TFirst, TSecond>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>>
            Query<TFirst, TSecond, TThird>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>>
            Query<TFirst, TSecond, TThird>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>>
            Query<TFirst, TSecond, TThird>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>>
            Query<TFirst, TSecond, TThird, TFourth>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>>
            Query<TFirst, TSecond, TThird, TFourth>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>>
            Query<TFirst, TSecond, TThird, TFourth>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <typeparam name="TSeventh">第七个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>, IEnumerable<TSeventh>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList(),
                        (IEnumerable<TSeventh>)gridReader.Read<TSeventh>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <typeparam name="TSeventh">第七个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>, IEnumerable<TSeventh>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList(),
                        (IEnumerable<TSeventh>)gridReader.Read<TSeventh>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <typeparam name="TSeventh">第七个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>, IEnumerable<TSeventh>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList(),
                        (IEnumerable<TSeventh>)gridReader.Read<TSeventh>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <typeparam name="TSeventh">第七个实体类型</typeparam>
        /// <typeparam name="TEighth">第八个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>, IEnumerable<TSeventh>, Tuple<IEnumerable<TEighth>>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TEighth>(string query)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList(),
                        (IEnumerable<TSeventh>)gridReader.Read<TSeventh>().ToList(),
                        (IEnumerable<TEighth>)gridReader.Read<TEighth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <typeparam name="TSeventh">第七个实体类型</typeparam>
        /// <typeparam name="TEighth">第八个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>, IEnumerable<TSeventh>, Tuple<IEnumerable<TEighth>>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TEighth>(string query, object parameters)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList(),
                        (IEnumerable<TSeventh>)gridReader.Read<TSeventh>().ToList(),
                        (IEnumerable<TEighth>)gridReader.Read<TEighth>().ToList());
                }
            }
        }

        /// <summary>
        /// 使用SQL语句获取多个指定实体集合
        /// </summary>
        /// <typeparam name="TFirst">第一个实体类型</typeparam>
        /// <typeparam name="TSecond">第二个实体类型</typeparam>
        /// <typeparam name="TThird">第三个实体类型</typeparam>
        /// <typeparam name="TFourth">第四个实体类型</typeparam>
        /// <typeparam name="TFifth">第五个实体类型</typeparam>
        /// <typeparam name="TSixth">第六个实体类型</typeparam>
        /// <typeparam name="TSeventh">第七个实体类型</typeparam>
        /// <typeparam name="TEighth">第八个实体类型</typeparam>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">SQL参数</param>
        /// <param name="commandType">SQL语句命令类型</param>
        /// <returns>多个实体集合</returns>
        public virtual Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFourth>, IEnumerable<TFifth>, IEnumerable<TSixth>, IEnumerable<TSeventh>, Tuple<IEnumerable<TEighth>>>
            Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TEighth>(string query, object parameters, CommandType commandType)
        {
            using (Connection)
            {
                using (var gridReader = Connection.QueryMultiple(query, parameters, commandType: commandType))
                {
                    return Tuple.Create((IEnumerable<TFirst>)gridReader.Read<TFirst>().ToList(),
                        (IEnumerable<TSecond>)gridReader.Read<TSecond>().ToList(),
                        (IEnumerable<TThird>)gridReader.Read<TThird>().ToList(),
                        (IEnumerable<TFourth>)gridReader.Read<TFourth>().ToList(),
                        (IEnumerable<TFifth>)gridReader.Read<TFifth>().ToList(),
                        (IEnumerable<TSixth>)gridReader.Read<TSixth>().ToList(),
                        (IEnumerable<TSeventh>)gridReader.Read<TSeventh>().ToList(),
                        (IEnumerable<TEighth>)gridReader.Read<TEighth>().ToList());
                }
            }
        }

        #endregion


    }
}
