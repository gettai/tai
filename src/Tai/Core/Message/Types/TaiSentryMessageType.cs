using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message.Types
{
    public enum TaiSentryMessageType
    {
        /// <summary>
        /// 系统消息，如：服务启动、服务停止、连接成功、服务异常等
        /// </summary>
        System,
        /// <summary>
        /// 用户状态消息
        /// </summary>
        Status,
        /// <summary>
        /// APP使用计时更新
        /// </summary>
        ActiveAppTime,
        /// <summary>
        /// 当前焦点APP
        /// </summary>
        Focused,
        /// <summary>
        /// APP非活跃计时数据
        /// </summary>
        InactiveAppTime
    }
}
