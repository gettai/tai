using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Services
{
    public class CoreMain : ICoreMain
    {
        private readonly ITaiSentry _taiSentry;
        private readonly IAppTimeTracker _appTimeTracker;
        private readonly IPolling _polling;
        private readonly IAppIconHandler _appIconHandler;
        private readonly IWebBrowserListener _webBrowserListener;
        private readonly IWebTimeTracker _webTimeTracker;
        private readonly IWebsiteIconHandler _websiteIconHandler;
        public CoreMain(
            ITaiSentry taiSentry_,
            IAppTimeTracker appTimeTracker_,
            IPolling polling_,
            IAppIconHandler appIconHandler_,
            IWebBrowserListener webBrowserListener_,
            IWebTimeTracker webTimeTracker_,
            IWebsiteIconHandler websiteIconHandler_)
        {
            _taiSentry = taiSentry_;
            _appTimeTracker = appTimeTracker_;
            _polling = polling_;
            _appIconHandler = appIconHandler_;
            _webBrowserListener = webBrowserListener_;
            _webTimeTracker = webTimeTracker_;
            _websiteIconHandler = websiteIconHandler_;
        }

        public void Start()
        {
            //  轮询服务必须先启动
            _polling.Start();
            _taiSentry.Run();
            _appTimeTracker.Start();
            _appIconHandler.Start();
            _webBrowserListener.Start();
            _webTimeTracker.Start();
            _websiteIconHandler.Start();
        }

        public void Stop()
        {
            _polling.Stop();
            _taiSentry.Shutdown();
            _appTimeTracker.Stop();
            _appIconHandler.Stop();
            _webBrowserListener.Stop();
            _webTimeTracker.Stop();
            _websiteIconHandler.Stop();
        }
    }
}
