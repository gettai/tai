using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Services;

namespace Tai.UI.DataAccess
{
    public class AppRepository : IAppRepository
    {
        private readonly IDataBase _db;
        public AppRepository(IDataBase db_)
        {
            _db = db_;
        }

        public int GetActiveTotalDuration(int appId_, DateTime start_, DateTime end_)
        {
            int res = 0;

            start_ = DateTimeHelper.Fromat(start_);
            end_ = DateTimeHelper.Fromat(end_);

            _db.Read((ctx) =>
            {
                var query = from log in ctx.AppActiveTimeLogs
                            where log.Start >= start_ && log.End <= end_ && log.AppId == appId_
                            select log.Duration;
                res = query.Any() ? query.Sum() : 0;
            });

            return res;
        }

        public IList<Core.Data.Models.App> GetTopList(DateTime start_, DateTime end_, int num_)
        {
            var result = new List<Core.Data.Models.App>();

            _db.Read((ctx) =>
            {
                var query = from log in ctx.AppActiveTimeLogs
                            join app in ctx.Apps on log.AppId equals app.Id
                            where log.Start >= start_ && log.End <= end_
                            group log by app into g
                            orderby g.Sum(log => log.Duration) descending
                            select new { App = g.Key, TotalDuration = g.Sum(log => log.Duration) };

                // 获取前 num_ 个使用时间最长的应用程序
                result = query.Take(num_)
                .Select(item => new { item.App, item.TotalDuration })
                .ToList()
                .Select(item =>
                {
                    item.App.ActiveDuration = item.TotalDuration;
                    return item.App;
                })
                .ToList();
            });

            return result;
        }
    }
}
