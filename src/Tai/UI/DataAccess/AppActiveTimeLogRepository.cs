using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Services;

namespace Tai.UI.DataAccess
{
    public class AppActiveTimeLogRepository : IAppActiveTimeLogRepository
    {
        private readonly IDataBase _db;
        public AppActiveTimeLogRepository(IDataBase db_)
        {
            _db = db_;
        }

        public int Delete()
        {
            int res = 0;
            _db.Write((ctx) =>
            {
                res = ctx.Database.ExecuteSqlCommand("DELETE FROM AppActiveTimeLogs");
            });
            return res;
        }

        public int Delete(DateTime start_, DateTime end_)
        {
            int res = 0;
            _db.Write((ctx) =>
            {
                res = ctx.Database.ExecuteSqlCommand("DELETE FROM AppActiveTimeLogs WHERE S");
            });
            return res;
        }

        public int GetFollowedTotalDuration(DateTime start_, DateTime end_)
        {
            int res = 0;

            start_ = DateTimeHelper.Fromat(start_);
            end_ = DateTimeHelper.Fromat(end_);

            _db.Read((ctx) =>
            {
                var query = from log in ctx.AppActiveTimeLogs
                            join app in ctx.Apps on log.AppId equals app.Id
                            where log.Start >= start_ && log.End <= end_ && app.IsFollowed
                            select log.Duration;
                res = query.Any() ? query.Sum() : 0;
            });

            return res;
        }

        public int GetTotalDuration(DateTime start_, DateTime end_)
        {
            int res = 0;

            start_ = DateTimeHelper.Fromat(start_);
            end_ = DateTimeHelper.Fromat(end_);

            _db.Read((ctx) =>
            {
                var query = from log in ctx.AppActiveTimeLogs
                            where log.Start >= start_ && log.End <= end_
                            select log.Duration;
                res = query.Any() ? query.Sum() : 0;
            });

            return res;
        }
    }
}
