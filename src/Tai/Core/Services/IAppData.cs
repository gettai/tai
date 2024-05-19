using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Data.Models;
using Tai.Core.Message;

namespace Tai.Core.Services
{
    public interface IAppData
    {
        /// <summary>
        /// 通过 TaiSentryActiveAppTimeMessage 添加一条前台APP活动记录，如果App不存在于数据库中时不会添加
        /// </summary>
        /// <param name="appDataMsg_"></param>
        //void AddActiveLog(TaiSentryActiveAppTimeMessage appDataMsg_);
        /// <summary>
        /// 通过Db数据模型添加一条APP活动记录，不校验APPID是否正确
        /// </summary>
        /// <param name="dbModel_">记录数据模型</param>
        void AddActiveLog(AppActiveTimeLog dbModel_);
        /// <summary>
        /// 通过数据模型添加一条后台APP时长记录，如果APP不存在于数据库时不添加
        /// </summary>
        /// <param name="log_"></param>
        void AddInactiveLog(AppInactiveTimeLog log_);
        /// <summary>
        /// 通过进程名称获取App
        /// </summary>
        /// <param name="processName_">进程名称</param>
        /// <returns>不存在时返回null</returns>
        Data.Models.App GetApp(string processName_);
        /// <summary>
        /// 从当前缓存中获取APP（可能为null）
        /// </summary>
        /// <param name="processName_">进程名称</param>
        /// <returns></returns>
        Data.Models.App GetAppCache(string processName_);
        /// <summary>
        /// 添加一个APP
        /// </summary>
        /// <param name="app_"></param>
        /// <returns>添加失败时返回null</returns>
        Data.Models.App AddApp(Data.Models.App app_);
        /// <summary>
        /// 更新APP图标路径
        /// </summary>
        /// <param name="app_"></param>
        /// <returns>成功返回true</returns>
        bool UpdateAppIcon(Data.Models.App app_);
        /// <summary>
        /// 持久化缓存数据
        /// </summary>
        /// <returns>返回影响行数</returns>
        int Persist();
    }
}
