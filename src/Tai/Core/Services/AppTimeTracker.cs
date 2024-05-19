using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Common;
using Tai.Core.Types;

namespace Tai.Core.Services
{
    public class AppTimeTracker : IAppTimeTracker
    {
        private readonly ITaiSentry _taiSentry;
        private readonly IAppData _appData;
        private readonly IAppIconHandler _appIconHandler;

        public AppTimeTracker(
            ITaiSentry taiSentry_,
            IAppData appData_,
            IAppIconHandler appIconHandler_)
        {
            _taiSentry = taiSentry_;
            _appData = appData_;
            _appIconHandler = appIconHandler_;
        }

        public void Start()
        {
            _taiSentry.OnActiveAppDataUpdated += _taiSentry_OnActiveAppDataUpdated;
            _taiSentry.OnInactiveAppDataUpdated += _taiSentry_OnInactiveAppDataUpdated;
        }

        private void _taiSentry_OnInactiveAppDataUpdated(object sender_, Message.TaiSentryInactiveAppTimeMessage e_)
        {
            //  统计后台时长
            TrackInactiveTime(e_);
        }

        private void _taiSentry_OnActiveAppDataUpdated(object sender_, Message.TaiSentryActiveAppTimeMessage e_)
        {
            //  统计前台时长
            TrackActiveTime(e_);
        }

        private void TrackActiveTime(Message.TaiSentryActiveAppTimeMessage msg_)
        {
            var log = msg_.Msg;
            var app = _appData.GetApp(log.App.Process);
            if (app == null)
            {
                //  创建APP数据
                app = new Data.Models.App()
                {
                    Name = log.App.Process,
                    Path = log.App.ExecutablePath,
                    Description = log.App.Description,
                };
                app = _appData.AddApp(app);
            }

            if (app != null)
            {
                //  记录
                _appData.AddActiveLog(new Data.Models.AppActiveTimeLog()
                {
                    AppId = app.Id,
                    Duration = log.Duration,
                    Start = log.Start,
                    End = log.End,
                });

                //  更新图标（如果为空）
                if (string.IsNullOrEmpty(app.Icon))
                {
                    _appIconHandler.UpdateIcon(app);
                }
            }
        }

        private void TrackInactiveTime(Message.TaiSentryInactiveAppTimeMessage msg_)
        {
            //  统计后台时长表示已经添加过APP，所以不再处理
            foreach (var item in msg_.Msg)
            {
                //  只从当前缓存中获取
                var app = _appData.GetAppCache(item.App.Process);
                if (app != null)
                {
                    _appData.AddInactiveLog(new Data.Models.AppInactiveTimeLog()
                    {
                        AppId = app.Id,
                        Duration = item.Duration,
                        Start = item.Start,
                        End = item.End,
                    });
                }
            }
        }

        public void Stop()
        {
            _taiSentry.OnActiveAppDataUpdated -= _taiSentry_OnActiveAppDataUpdated;
            _taiSentry.OnInactiveAppDataUpdated -= _taiSentry_OnInactiveAppDataUpdated;
        }

    }
}
