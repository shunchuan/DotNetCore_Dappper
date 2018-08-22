using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore_Dappper.Domain.RepositoryBase;
using DotNetCore_Dappper.Model.Entity;

namespace DotNetCore_Dappper.Domain.IRepository
{
    public interface IUserInfoRepository: IDapperExtensionsRepositoryBase<UserInfo>
    {
        IEnumerable<string> GetUserName();
        IEnumerable<UserInfo> GetUserInfos();
    }
}
