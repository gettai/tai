using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tai.UI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Services;
using Tai.Tests;
using System.Diagnostics;

namespace Tai.UI.DataAccess.Tests
{
    [TestClass()]
    public class AppRepositoryTests
    {
        [TestMethod()]
        public void GetActiveTotalDurationTest()
        {
            var dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var dayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var appRep = DIHost.GetService<IAppRepository>();
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
            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 10,
                Start = start,
                End = end
            });
            //  持久化
            appData.Persist();

            var res = appRep.GetActiveTotalDuration(resApp.Id, start, end);
            Assert.AreEqual(10, res);

            //  测试2
            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 2,
                Start = start.AddDays(-1),
                End = end.AddDays(-1)
            });
            //  持久化
            appData.Persist();
            res = appRep.GetActiveTotalDuration(resApp.Id, start, end);
            Assert.AreEqual(10, res);

            //  测试3
            res = appRep.GetActiveTotalDuration(resApp.Id, dayStart, dayEnd);
            Assert.AreEqual(10, res);

            //  测试4
            res = appRep.GetActiveTotalDuration(resApp.Id, dayStart.AddDays(-1), dayEnd.AddDays(-1));
            Assert.AreEqual(2, res);

            //  测试5
            res = appRep.GetActiveTotalDuration(resApp.Id, dayStart.AddDays(-1), dayEnd);
            Assert.AreEqual(12, res);
        }

        [TestMethod()]
        public void GetTopListTest()
        {
            var dayStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var dayEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var appRep = DIHost.GetService<IAppRepository>();
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
            var resApp2 = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "Test2" + guid,
                Path = "Path"
            });
            Assert.IsNotNull(resApp2);
            var resApp3 = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "Test3" + guid,
                Path = "Path"
            });
            Assert.IsNotNull(resApp3);
            //  添加记录
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(10);
            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 10,
                Start = start,
                End = end
            });
            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = resApp2.Id,
                Duration = 1,
                Start = start,
                End = end
            });
            appData.AddActiveLog(new Core.Data.Models.AppActiveTimeLog()
            {
                AppId = resApp3.Id,
                Duration = 20,
                Start = start,
                End = end
            });
            //  持久化
            appData.Persist();

            var res = appRep.GetTopList(dayStart, dayEnd, 10);

            Assert.AreEqual(3, res.Count);
            Assert.AreEqual(31, res.Sum(s => s.ActiveDuration));

            res = appRep.GetTopList(dayStart, dayEnd, 1);
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(20, res.Sum(s => s.ActiveDuration));
        }
    }
}