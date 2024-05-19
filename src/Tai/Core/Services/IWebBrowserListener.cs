using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Event;

namespace Tai.Core.Services
{
    public interface IWebBrowserListener
    {
        /// <summary>
        /// 焦点计时数据更新事件
        /// </summary>
        event WebBrowserListenerActiveEventHandler OnActive;
        /// <summary>
        /// 失去焦点数据更新事件
        /// </summary>
        event WebBrowserListenerInactiveEventHandler OnInactive;
        /// <summary>
        /// 关闭网页事件
        /// </summary>
        event WebBrowserListenerClosedEventHandler OnClosed;
        /// <summary>
        /// 浏览器关闭事件
        /// </summary>
        event WebBrowserListenerExitEventHandler OnExit;
        void Start();
        void Stop();
    }
}
