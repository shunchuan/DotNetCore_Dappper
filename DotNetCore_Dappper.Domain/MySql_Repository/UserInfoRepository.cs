using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Domain.IRepository;
using DotNetCore_Dappper.Domain.RepositoryBase;
using DotNetCore_Dappper.Model.Enmu;
using DotNetCore_Dappper.Model.Entity;

namespace DotNetCore_Dappper.Domain.MySql_Repository
{
    public class UserInfoRepository: DapperRepositoryBase, IUserInfoRepository
    {
        public UserInfoRepository()
        {
        }

        public UserInfoRepository(string connectStr, string type) : base(connectStr, type)
        {
        }

        public UserInfoRepository(string connectStr, DBTYPE type) : base(connectStr, type)
        {
        }
        public IEnumerable<string> GetUserName()
        {
            string sql = "select  name from userInfo";
            return Query<string>(sql);
        }
    }
}
