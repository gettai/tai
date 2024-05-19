using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Services
{
    /// <summary>
    /// 网页时间记录服务
    /// </summary>
    public interface IWebTimeTracker
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
