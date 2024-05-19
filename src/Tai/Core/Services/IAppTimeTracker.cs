using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Services
{
    /// <summary>
    /// APP时间记录服务，负责处理活跃时间和后台时间的记录
    /// </summary>
    public interface IAppTimeTracker
    {
        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();
        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();
    }
}
