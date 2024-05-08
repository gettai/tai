using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.UI.Servicer
{
    /// <summary>
    /// 通知栏图标服务接口
    /// </summary>
    public interface INotifyIconServicer : IServicer
    {
        void SetIcon(string path_ = null);
    }
}
