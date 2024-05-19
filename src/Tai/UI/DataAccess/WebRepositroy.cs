using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Data.Models;
using Tai.Core.Services;

namespace Tai.UI.DataAccess
{
    public class WebRepositroy : IWebRepository
    {
        private readonly IDataBase _db;
        public WebRepositroy(IDataBase db_)
        {
            _db = db_;
        }
        public IList<Core.Data.Models.Website> GetTopList(DateTime start_, DateTime end_, int num_)
        {
            var result = new List<Core.Data.Models.Website>();

            _db.Read((ctx) =>
            {
                var query = from log in ctx.WebActiveTimeLogs
                            join site in ctx.WebSites on log.WebsiteId equals site.Id
                            where log.Start >= start_ && log.End <= end_ && log.Duration > 0
                            group log by site into g
                            orderby g.Sum(log => log.Duration) descending
                            select new { Site = g.Key, TotalDuration = g.Sum(log => log.Duration) };

                // 获取前 num_ 个使用时间最长的应用程序
                result = query.Take(num_)
                .Select(item => new { item.Site, item.TotalDuration })
                .ToList()
                .Select(item =>
                {
                    item.Site.ActiveDuration = item.TotalDuration;
                    return item.Site;
                })
                .ToList();
            });

            return result;
        }
    }
}
