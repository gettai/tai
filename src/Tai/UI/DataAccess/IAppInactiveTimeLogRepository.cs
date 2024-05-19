using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.UI.DataAccess
{
    public interface IAppInactiveTimeLogRepository
    {
        /// <summary>
        /// 获取指定时间范围内的后台总时长
        /// </summary>
        /// <param name="start_">开始时间</param>
        /// <param name="end_">结束时间</param>
        /// <returns>时长秒</returns>
        int GetTotalDuration(DateTime start_, DateTime end_);
    }
}
