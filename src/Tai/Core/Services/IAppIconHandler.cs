using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Services
{
    /// <summary>
    /// APP图标处理服务
    /// </summary>
    public interface IAppIconHandler
    {
        void Start();
        void Stop();
        /// <summary>
        /// 获取并更新APP图标（使用队列更新，并不是实时）
        /// </summary>
        /// <param name="app_"></param>
        void UpdateIcon(Data.Models.App app_);
    }
}
