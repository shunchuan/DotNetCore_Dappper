using System.Data;

namespace DotNetCore_Dappper.Domain.RepositoryBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using DapperExtensions;
    public interface IDapperExtensionsRepositoryBase<T>
    {
        IEnumerable<T> GetList();

        T Get(object id);

        bool Update(T t);

        T Insert(T apply);

        bool Delete(T t);

        IEnumerable<T> Find(Expression<Func<T, object>> expression, Operator op, object param);

        void Insert(IEnumerable<T> entities);
        bool Delete(object predicate);
        IEnumerable<T> GetList(object predicate);
        IEnumerable<T> GetList(object predicate, List<ISort> sorts);

        IEnumerable<T> GetListPaged(object predicate, List<ISort> sorts,
            int pageNumber, int itemsPerPage);

        IEnumerable<T> GetSet(object predicate, List<ISort> sorts,
            int firstResult, int maxResults);

        int Count(object predicate);
        IEnumerable<T> Query(string query);
        IEnumerable<T> Query(string query, object parameters);
        IEnumerable<T> Query(string query, object parameters, CommandType commandType);
        IEnumerable<TAny> Query<TAny>(string query);
        IEnumerable<TAny> Query<TAny>(string query, object parameters);
        IEnumerable<TAny> Query<TAny>(string query, object parameters, CommandType commandType);
        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>> Query<TFirst, TSecond>(string query);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>> Query<TFirst, TSecond>(string query, object parameters);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>
            Query<TFirst, TSecond>(string query, object parameters, CommandType commandType);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>>
            Query<TFirst, TSecond, TThird>(string query);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>>
            Query<TFirst, TSecond, TThird>(string query, object parameters);
    }
}
