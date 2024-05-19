using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;

namespace Tai.Core.Services
{
    public class WebTimeTracker : IWebTimeTracker
    {
        private readonly IWebBrowserListener _webBrowserListener;
        private readonly IWebData _webData;
        private readonly IWebsiteIconHandler _websiteIconHandler;

        public WebTimeTracker(
            IWebBrowserListener webBrowserListener_,
            IWebData webData_,
            IWebsiteIconHandler websiteIconHandler_)
        {
            _webBrowserListener = webBrowserListener_;
            _webData = webData_;
            _websiteIconHandler = websiteIconHandler_;
        }

        public void Start()
        {
            _webBrowserListener.OnActive += _webBrowserListener_OnActive;
            _webBrowserListener.OnInactive += _webBrowserListener_OnInactive;
            _webBrowserListener.OnClosed += _webBrowserListener_OnClosed;
            _webBrowserListener.OnExit += _webBrowserListener_OnExit;
        }

        public void Stop()
        {
            _webBrowserListener.OnActive -= _webBrowserListener_OnActive;
            _webBrowserListener.OnInactive -= _webBrowserListener_OnInactive;
            _webBrowserListener.OnClosed -= _webBrowserListener_OnClosed;
            _webBrowserListener.OnExit -= _webBrowserListener_OnExit;
        }

        private void TrackActiveTime(Message.TaiWebBrowserActiveMessage msg_)
        {
            if (msg_.Data.Duration <= 0) return;

            var data = msg_.Data;
            string domain = UrlHelper.GetDomain(data.Url);
            var site = _webData.GetWebsiteByDomain(domain);
            if (site == null)
            {
                site = new Data.Models.Website()
                {
                    Domain = domain,
                    Name = domain,
                };
                site = _webData.AddWebsite(site);
            }

            if (site != null)
            {
                //  添加记录
                _webData.AddActiveLog(site.Id, msg_);

                //  获取图标
                if (site.Icon == null && !string.IsNullOrEmpty(data.Icon))
                {
                    _websiteIconHandler.UpdateIcon(site, data.Icon);
                }
            }
        }

        private void _webBrowserListener_OnActive(string browserId_, Message.TaiWebBrowserActiveMessage msg_)
        {
            TrackActiveTime(msg_);
        }
        private void _webBrowserListener_OnInactive(string browserId_, Message.TaiWebBrowserInactiveMessage msg_)
        {
            //throw new NotImplementedException();
        }

        private void _webBrowserListener_OnClosed(string browserId_, int id_)
        {
            //throw new NotImplementedException();
        }
        private void _webBrowserListener_OnExit(string browserId_)
        {
            //throw new NotImplementedException();
        }




    }
}
