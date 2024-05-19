using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Constants;
using Tai.Core.Data.Models;

namespace Tai.Core.Services
{
    public struct QueueItem
    {
        public Website Site { get; set; }
        public string IconUrl { get; set; }
    }
    public class WebsiteIconHandler : IWebsiteIconHandler
    {
        private readonly IPolling _polling;
        private readonly IWebData _webData;

        private readonly ConcurrentQueue<QueueItem> _queue = new ConcurrentQueue<QueueItem>();

        public WebsiteIconHandler(IPolling polling_, IWebData webData_)
        {
            _polling = polling_;
            _webData = webData_;
        }
        public void Start()
        {
            _polling.PollingTimeElapsed += _polling_PollingTimeElapsed;
        }

        public void Stop()
        {
            _polling.PollingTimeElapsed -= _polling_PollingTimeElapsed;
        }

        public void UpdateIcon(Website site_, string iconUrl_)
        {
            if (string.IsNullOrEmpty(UrlHelper.GetValidIconName(iconUrl_))) return;
            if (string.IsNullOrEmpty(iconUrl_)) return;
            if (!string.IsNullOrEmpty(site_.Icon)) return;
            _queue.Enqueue(new QueueItem()
            {
                Site = site_,
                IconUrl = iconUrl_
            });
        }

        private void _polling_PollingTimeElapsed()
        {
            HandleAsync();
        }

        private async void HandleAsync()
        {
            if (!_queue.IsEmpty)
            {
                QueueItem item;
                if (_queue.TryDequeue(out item))
                {
                    string fileName = GetIconName(item.Site.Domain);
                    string savePath = Path.Combine(FileHelper.GetAppRunDirectory(), AppConstants.WebIconFolder, fileName);
                    string dres = await HttpHelper.DownloadAsync(item.IconUrl, savePath);
                    if (!string.IsNullOrEmpty(dres))
                    {
                        _webData.UpdateIcon(new Website()
                        {
                            Id = item.Site.Id,
                            Icon = Path.Combine(AppConstants.WebIconFolder, fileName),
                            Domain = item.Site.Domain
                        });
                    }
                }
            }
        }

        private string GetIconName(string domain)
        {
            return $"{CryptographyHelper.MD5(domain, 6)}.png";
        }
    }
}
