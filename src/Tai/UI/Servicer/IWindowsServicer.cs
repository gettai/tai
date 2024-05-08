using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.UI.Servicer
{
    /// <summary>
    /// 窗口服务
    /// </summary>
    public interface IWindowsServicer
    {
        /// <summary>
        /// 显示主窗口
        /// </summary>
        void ShowMainWindow();
        /// <summary>
        /// 关闭主窗口
        /// </summary>
        void CloseMainWindow();
    }
}
