using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message.Types
{
    public enum TaiSentryAppType
    {
        /// <summary>
        /// 传统应用
        /// </summary>
        Win32,
        /// <summary>
        /// UWP
        /// </summary>
        UWP,
        /// <summary>
        /// 系统组件，如任务栏、开始菜单、状态栏、系统登录界面等
        /// </summary>
        SystemComponent
    }
}
