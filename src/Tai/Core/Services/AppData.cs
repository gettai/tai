using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Data.Models;
using Tai.Core.Message;

namespace Tai.Core.Services
{
    public class AppData : IAppData
    {
        private readonly IDataBase _db;
        private readonly IPolling _polling;

        private ThreadSafeDictionary<string, Data.Models.App> _appCacheByProcess = new ThreadSafeDictionary<string, Data.Models.App>();
        private ConcurrentQueue<AppActiveTimeLog> _activeTimeLogQueue = new ConcurrentQueue<AppActiveTimeLog>();
        private ConcurrentQueue<AppInactiveTimeLog> _inactiveTimeLogQueue = new ConcurrentQueue<AppInactiveTimeLog>();
        public AppData(IDataBase db_, IPolling polling_)
        {
            _db = db_;
            _polling = polling_;
            _polling.PollingTimeElapsed += _polling_PollingTimeElapsed;
        }



        //public void AddActiveLog(TaiSentryActiveAppTimeMessage appDataMsg_)
        //{
        //    if (appDataMsg_ == null) return;

        //    var appData = appDataMsg_.Msg;
        //    var app = GetApp(appData.App.Process);
        //    if (app == null) return;

        //    AddActiveLog(new AppActiveTimeLog()
        //    {
        //        AppId = app.Id,
        //        Duration = appData.Duration,
        //        Start = appData.ActiveTime,
        //        End = appData.EndTime,
        //    });
        //}

        public void AddActiveLog(AppActiveTimeLog dbModel_)
        {
            if (dbModel_ == null) return;
            if (dbModel_.Duration <= 0) return;
            if (dbModel_.Start >= dbModel_.End) return;
            if (dbModel_.AppId <= 0) return;

            _activeTimeLogQueue.Enqueue(dbModel_);
            //int res = 0;
            //_db.Write((ctx) =>
            //{
            //    ctx.AppActiveTimeLogs.Add(dbModel_);
            //    res = ctx.SaveChanges();
            //});
            //return res > 0;
        }

        public Data.Models.App AddApp(Data.Models.App app_)
        {
            if (app_ is null) return null;
            //  先查询是否存在，存在则不添加
            var app = GetApp(app_.Name);
            if (app != null) return null;

            int res = 0;
            _db.Write((ctx) =>
            {
                ctx.Apps.Add(app_);
                res = ctx.SaveChanges();
            });

            //  添加到缓存
            if (res > 0)
            {
                //var appCacheByProcess = _appCacheByProcess.Get(app_.Name);
                _appCacheByProcess.Set(app_.Name, app_);
                return app_;
            }
            return null;
        }

        #region 通过进程名称获取APP
        public Data.Models.App GetApp(string processName_)
        {
            var appCache = _appCacheByProcess.Get(processName_);
            if (appCache != null)
            {
                return appCache;
            }
            Data.Models.App res = null;
            _db.Read((ctx) =>
            {
                res = (from s in ctx.Apps where s.Name == processName_ select s).FirstOrDefault();
            });

            if (res != null)
            {
                //  设置缓存
                _appCacheByProcess.Set(processName_, res);
            }
            return res;
        }
        public Data.Models.App GetAppCache(string processName_)
        {
            return _appCacheByProcess.Get(processName_);
        }
        #endregion

        #region 更新APP图标路径
        public bool UpdateAppIcon(Data.Models.App app_)
        {
            var app = new Data.Models.App()
            {
                Id = app_.Id,
                Icon = app_.Icon
            };
            var appCache = _appCacheByProcess.Get(app_.Name);

            int sr = 0;
            _db.Write((ctx) =>
            {
                ctx.Apps.Attach(app);
                ctx.Entry(app).Property(p => p.Icon).IsModified = true;
                sr = ctx.SaveChanges();
            });

            if (sr > 0)
            {
                //  尝试更新缓存
                if (appCache != null)
                {
                    appCache.Icon = app_.Icon;
                }
                return true;
            }
            return false;
        }
        #endregion


        private void _polling_PollingTimeElapsed()
        {
            Persist();
            PersistInactiveTimeLog();
        }

        #region 将队列中的数据持久化
        public int Persist()
        {
            AppActiveTimeLog item;
            string sqlStr = "INSERT INTO AppActiveTimeLogs (AppId, Start, End, Duration) VALUES ";
            string sqlTotalStr = string.Empty;
            string values = string.Empty;
            while (_activeTimeLogQueue.TryDequeue(out item))
            {
                values += $"({item.AppId}, '{DateTimeHelper.FromatToString(item.Start)}', '{DateTimeHelper.FromatToString(item.End)}', {item.Duration}), ";
                sqlTotalStr += $"UPDATE Apps SET ActiveDuration = ActiveDuration + {item.Duration} WHERE Id = {item.AppId};";

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


        #endregion

        public void AddInactiveLog(AppInactiveTimeLog log_)
        {
            if (log_ == null) return;
            if (log_.Start >= log_.End || log_.Duration <= 0) return;
            if (log_.AppId <= 0) return;

            _inactiveTimeLogQueue.Enqueue(log_);
        }

        public int PersistInactiveTimeLog()
        {
            AppInactiveTimeLog item;
            string sqlStr = "INSERT INTO AppInactiveTimeLogs (AppId, Start, End, Duration) VALUES ";
            string sqlTotalStr = string.Empty;
            string values = string.Empty;
            while (_inactiveTimeLogQueue.TryDequeue(out item))
            {
                values += $"({item.AppId}, '{DateTimeHelper.FromatToString(item.Start)}', '{DateTimeHelper.FromatToString(item.End)}', {item.Duration}), ";
                sqlTotalStr += $"UPDATE Apps SET InactiveDuration = InactiveDuration + {item.Duration} WHERE Id = {item.AppId};";
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
    }
}
