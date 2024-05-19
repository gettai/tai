using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Event;

namespace Tai.Core.Services
{
    public interface ITaiSentry
    {
        /// <summary>
        /// 应用前台时长统计数据更新事件
        /// </summary>
        event TaiSentryActiveAppDataEventHandler OnActiveAppDataUpdated;
        /// <summary>
        /// 应用后台时长统计数据更新事件
        /// </summary>
        event TaiSentryInactiveAppDataEventHandler OnInactiveAppDataUpdated;
        /// <summary>
        /// 用户状态变更事件
        /// </summary>
        event TaiSentryStatusEventHandler OnStatusChanged;
        void Run();
        void Shutdown();
    }
}
