using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BeehooeDataService.Infrastructure
{
    /// <summary>
    /// CallContext已经不适用于 .NETStandard 或 .NET Core 。
    /// 但是可以使用 Asynclocal<T> 来模仿 CallContext
    /// </summary>
    public static class CallContext
    {
        static readonly ConcurrentDictionary<string, AsyncLocal<object>> State = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static void SetData(string name, object data) =>
            State.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        public static object GetData(string name) =>
            State.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;
    }
}
