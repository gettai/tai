using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tai.Core.Event;

namespace Tai.Core.Services
{
    public class Polling : IPolling
    {
        public event PollingEventHandler PollingTimeElapsed;

        private Timer _timer;
        private readonly int _pollingInterval = 5000;
        public void Start()
        {
            Stop();
            _timer = new Timer(OnTimeElapsed, null, 0, _pollingInterval);
        }

        private void OnTimeElapsed(object state)
        {
            Debug.WriteLine("轮询时间到！！！！！！！！！");
            PollingTimeElapsed?.Invoke();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
