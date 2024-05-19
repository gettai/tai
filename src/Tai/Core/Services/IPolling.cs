using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Event;

namespace Tai.Core.Services
{
    /// <summary>
    /// 主程序统一轮询服务
    /// </summary>
    public interface IPolling
    {
        event PollingEventHandler PollingTimeElapsed;
        void Start();
        void Stop();
    }
}
