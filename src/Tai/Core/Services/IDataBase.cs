using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Data;

namespace Tai.Core.Services
{
    /// <summary>
    /// 数据库上下文操作服务
    /// </summary>
    public interface IDataBase
    {
        /// <summary>
        /// 获取执行读操作的数据库上下文（允许多个读操作同时进行），已包含释放资源的操作
        /// </summary>
        /// <param name="action_"></param>
        void Read(Action<TaiDbContext> action_);
        /// <summary>
        /// 获取执行写操作的数据库上下文（只允许一个写操作进行），已包含释放资源的操作
        /// </summary>
        /// <param name="action_"></param>
        void Write(Action<TaiDbContext> action_);
    }
}
