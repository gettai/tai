using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message.Types
{
    /// <summary>
    /// 浏览器消息类型
    /// </summary>
    public enum TaiWebBrowserMessageType
    {
        /// <summary>
        /// Ping
        /// </summary>
        Ping,
        /// <summary>
        /// 焦点计时统计数据
        /// </summary>
        Active,
        /// <summary>
        /// 失去焦点数据
        /// </summary>
        Inactive,
        /// <summary>
        /// 关闭页面
        /// </summary>
        Closed
    }
}
