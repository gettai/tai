using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tai.UI.Common;

namespace Tai.UI.Services
{
    public interface IRouter
    {
        /// <summary>
        /// 初始化完成事件
        /// </summary>
        event CommonEventHandler InitCompleted;
        /// <summary>
        /// 跳转页面事件
        /// </summary>
        event CommonEventHandler Navigated;
        /// <summary>
        /// 当前页面
        /// </summary>
        Type CurrentPage { get; }
        /// <summary>
        /// 当前页面的额外数据
        /// </summary>
        object ExtraData { get; }
        /// <summary>
        /// 初始化路由器
        /// </summary>
        /// <param name="frame_"></param>
        void Init(Frame frame_);
        void Navigate(Type page_, bool isCache_ = false);
        void Navigate(Type page_, object extraData_, bool isCache_ = false);
        void GoBack();
        void Dispose();
    }
}
