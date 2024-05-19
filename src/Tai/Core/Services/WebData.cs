using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Data.Models;
using Tai.Core.Message;

namespace Tai.Core.Services
{
    public class WebData : IWebData
    {
        private readonly IDataBase _db;
        private readonly IPolling _polling;

        private ThreadSafeDictionary<string, Website> _webSiteCacheByDomain = new ThreadSafeDictionary<string, Website>();
        private ConcurrentQueue<WebActiveTimeLog> _activeTimeLogQueue = new ConcurrentQueue<WebActiveTimeLog>();

        public WebData(IDataBase db_, IPolling polling_)
        {
            _db = db_;
            _polling = polling_;
            _polling.PollingTimeElapsed += _polling_PollingTimeElapsed;
        }

        public void AddActiveLog(int siteId_, TaiWebBrowserActiveMessage msg_)
        {
            var data = msg_.Data;
            var log = new WebActiveTimeLog()
            {
                WebsiteId = siteId_,
                Duration = data.Duration,
                Start = data.StartDateTime,
                End = data.EndDateTime,
                Title = data.Title,
                Url = data.Url
            };
            _activeTimeLogQueue.Enqueue(log);
        }

        public Website GetWebsiteByUrl(string url_)
        {
            string domain = UrlHelper.GetDomain(url_);
            return GetWebsiteByDomain(domain);
        }

        public Website GetWebsiteByDomain(string domain_)
        {
            if (string.IsNullOrEmpty(domain_)) return null;
            if (_webSiteCacheByDomain.ContainsKey(domain_))
            {
                return _webSiteCacheByDomain.Get(domain_);
            }
            Website res = null;
            _db.Read((ctx) =>
            {
                res = (from s in ctx.WebSites where s.Domain == domain_ select s).FirstOrDefault();
            });

            if (res != null)
            {
                //  设置缓存
                _webSiteCacheByDomain.Set(domain_, res);
            }
            return res;
        }

        public Website AddWebsite(Website site_)
        {
            if (site_ is null) return null;
            //  先查询是否存在，存在则不添加
            var site = GetWebsiteByDomain(site_.Domain);
            if (site != null) return null;

            int res = 0;
            _db.Write((ctx) =>
            {
                ctx.WebSites.Add(site_);
                res = ctx.SaveChanges();
            });

            //  添加到缓存
            if (res > 0)
            {
                _webSiteCacheByDomain.Set(site_.Domain, site_);
                return site_;
            }
            return null;
        }

        public int Persist()
        {
            WebActiveTimeLog item;
            string sqlStr = "INSERT INTO WebActiveTimeLogs (WebsiteId, Start, End, Duration, Title, Url) VALUES ";
            string sqlTotalStr = string.Empty;
            string values = string.Empty;
            while (_activeTimeLogQueue.TryDequeue(out item))
            {
                values += $"({item.WebsiteId}, '{DateTimeHelper.FromatToString(item.Start)}', '{DateTimeHelper.FromatToString(item.End)}', {item.Duration}, '{item.Title}', '{item.Url}'), ";
                sqlTotalStr += $"UPDATE Websites SET ActiveDuration = ActiveDuration + {item.Duration} WHERE Id = {item.WebsiteId};";
            }

            int res = 0;

            if (!string.IsNullOrEmpty(values))
            {
                sqlStr += $"{values.TrimEnd(' ', ',')};{sqlTotalStr}";
                _db.Write((ctx) =>
                {
                    res = ctx.Database.ExecuteSqlCommand(sqlStr);
                });
            }

            return res;
        }

        private void _polling_PollingTimeElapsed()
        {
            Persist();
        }

        public bool UpdateIcon(Website site_)
        {
            var site = new Website()
            {
                Id = site_.Id,
                Icon = site_.Icon
            };
            var cache= _webSiteCacheByDomain.Get(site_.Domain);

            int sr = 0;
            _db.Write((ctx) =>
            {
                ctx.WebSites.Attach(site);
                ctx.Entry(site).Property(p => p.Icon).IsModified = true;
                sr = ctx.SaveChanges();
            });

            if (sr > 0)
            {
                //  尝试更新缓存
                if (cache != null)
                {
                    cache.Icon = site_.Icon;
                }
                return true;
            }
            return false;
        }
    }
}
