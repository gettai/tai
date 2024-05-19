using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.UI.Main;

namespace Tai.UI.Services
{
    public class UIMainServicer : IUIMainServicer
    {
        private readonly INotifyIconServicer _notifyIconServicer;
        public UIMainServicer(INotifyIconServicer notifyIconServicer_)
        {
            _notifyIconServicer = notifyIconServicer_;
        }


        public void Start()
        {
            _notifyIconServicer.Start();
        }

        public void Stop()
        {
            _notifyIconServicer.Stop();
        }
    }
}
