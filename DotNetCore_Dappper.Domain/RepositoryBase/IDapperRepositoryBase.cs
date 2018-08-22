using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DotNetCore_Dappper.Domain.RepositoryBase
{
    public interface IDapperRepositoryBase
    {
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
