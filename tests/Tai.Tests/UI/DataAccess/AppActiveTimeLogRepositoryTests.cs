using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tai.UI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Services;
using Tai.Tests;

namespace Tai.UI.DataAccess.Tests
{
    [TestClass()]
    public class AppActiveTimeLogRepositoryTests
    {
        [TestMethod()]
        public void GetTotalDurationTest()
        {
            var dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var dayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var appData = DIHost.GetService<IAppData>();
            var aatlRep = DIHost.GetService<IAppActiveTimeLogRepository>();

            string guid = new Random().Next(10000, 99999).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");

            //  清空记录
            aatlRep.Delete();

            var resApp = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "Test" + guid,
                Path = "Path"
            });
            Assert.IsNotNull(resApp);
            //  添加记录
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(10);
            for (int i = 0; i < 10; i++)
            {
                appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
                {
                    AppId = resApp.Id,
                    Duration = 10,
                    Start = start.AddSeconds(i + 10),
                    End = end.AddSeconds(i + 10)
                });
            }

            //  持久化
            appData.Persist();

            var res = aatlRep.GetTotalDuration(dayStart, dayEnd);
            Assert.AreEqual(100, res);

            res = aatlRep.GetTotalDuration(start.AddSeconds(10), end.AddSeconds(10));
            Assert.AreEqual(10, res);

            res = aatlRep.GetTotalDuration(start.AddSeconds(10), start.AddSeconds(10));
            Assert.AreEqual(0, res);

            res = aatlRep.GetTotalDuration(start.AddSeconds(10), end.AddSeconds(11));
            Assert.AreEqual(20, res);
        }

        [TestMethod()]
        public void GetFollowedTotalDurationTest()
        {
            var dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var dayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var appData = DIHost.GetService<IAppData>();
            var aatlRep = DIHost.GetService<IAppActiveTimeLogRepository>();

            string guid = new Random().Next(10000, 99999).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");

            //  清空记录
            aatlRep.Delete();

            var followedApp = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "followedApp" + guid,
                Path = "Path",
                IsFollowed = true
            });
            Assert.IsNotNull(followedApp);
            var unfollowedApp = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "unfollowedApp" + guid,
                Path = "Path",
            });
            Assert.IsNotNull(unfollowedApp);
            //  添加记录
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(10);

            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = followedApp.Id,
                Duration = 3,
                Start = start,
                End = end
            });
            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = unfollowedApp.Id,
                Duration = 5,
                Start = start,
                End = end
            });

            //  持久化
            appData.Persist();

            var res = aatlRep.GetFollowedTotalDuration(dayStart, dayEnd);
            Assert.AreEqual(3, res);


        }
    }
}