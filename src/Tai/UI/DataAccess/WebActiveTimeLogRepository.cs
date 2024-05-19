using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Services;

namespace Tai.UI.DataAccess
{
    public class WebActiveTimeLogRepository : IWebActiveTimeLogRepository
    {
        private readonly IDataBase _db;
        public WebActiveTimeLogRepository(IDataBase db_)
        {
            _db = db_;
        }

        public int GetTotalDuration(DateTime start_, DateTime end_)
        {
            int res = 0;

            start_ = DateTimeHelper.Fromat(start_);
            end_ = DateTimeHelper.Fromat(end_);

            _db.Read((ctx) =>
            {
                var query = from log in ctx.WebActiveTimeLogs
                            where log.Start >= start_ && log.End <= end_
                            select log.Duration;
                res = query.Any() ? query.Sum() : 0;
            });

            return res;
        }
    }
}
