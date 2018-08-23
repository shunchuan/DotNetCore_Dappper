using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DotNetCore_Dappper.Domain.RepositoryBase
{
    public interface IDapperRepositoryBase
    {
        T Get<T>(object id) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        long Insert<T>(T obj) where T : class;
        long Insert<T>(List<T> list);
        bool Update<T>(T obj) where T : class;
        bool Update<T>(List<T> list);
        bool Delete<T>(T obj) where T : class;
        bool Delete<T>(List<T> list);
        bool DeleteAll<T>() where T : class;
        IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition commandDefinition);

        IEnumerable<T> QueryMultiple<T>(string sql, out int totalCount, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null,
            CommandType? commandType = null);

        T QueryOne<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null) where T : class;

        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null);

        T ExecuteScalar<T>(string sql, object param = null);

        T Execute<T>(string command, Dictionary<string, object> paras);

        byte[] ReadBlob(string command, object paras);

        bool WriteBlob(string command, object param);
    }
}
