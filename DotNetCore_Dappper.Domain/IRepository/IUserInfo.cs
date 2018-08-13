using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_Dappper.Domain.IRepository
{
    public interface IUserInfo
    {
        IEnumerable<string> GetUserName();
    }
}
