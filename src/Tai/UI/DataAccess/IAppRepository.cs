using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Data.Models;

namespace Tai.UI.DataAccess
{
    public interface IAppRepository
    {
        /// <summary>
        /// 获取指定应用前台总时长
        /// </summary>
        /// <param name="appId_">应用ID</param>
        /// <param name="start_">开始时间</param>
        /// <param name="end_">结束时间</param>
        /// <returns></returns>
        int GetActiveTotalDuration(int appId_, DateTime start_, DateTime end_);

        /// <summary>
        /// 获取在指定时间范围内使用时间最长的应用程序列表
        /// </summary>
        /// <param name="start_">开始时间</param>
        /// <param name="end_">结束时间</param>
        /// <param name="num_">获取条数</param>
        /// <returns> IList<Core.Data.Models.App> </returns>
        IList<Core.Data.Models.App> GetTopList(DateTime start_, DateTime end_, int num_);
    }
}
