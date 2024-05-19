using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.UI.DataAccess
{
    public interface IAppActiveTimeLogRepository
    {
        /// <summary>
        /// 获取指定时间范围内的前台使用总时长
        /// </summary>
        /// <param name="start_">开始时间</param>
        /// <param name="end_">结束时间</param>
        /// <returns>时长秒</returns>
        int GetTotalDuration(DateTime start_, DateTime end_);
        /// <summary>
        /// 删除所有APP前台时长统计记录
        /// </summary>
        /// <returns>删除成功的条数</returns>
        int Delete();
        /// <summary>
        /// 删除指定范围内的前台时长统计记录
        /// </summary>
        /// <param name="start_">开始时间</param>
        /// <param name="end_">结束时间</param>
        /// <returns>删除成功的条数</returns>
        int Delete(DateTime start_, DateTime end_);
        /// <summary>
        /// 获取关注应用时间范围内的前台使用总时长
        /// </summary>
        /// <param name="start_">开始时间</param>
        /// <param name="end_">结束时间</param>
        /// <returns></returns>
        int GetFollowedTotalDuration(DateTime start_, DateTime end_);
    }
}
