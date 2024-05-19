using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tai.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Tests;
using System.Diagnostics;

namespace Tai.Core.Services.Tests
{
    [TestClass()]
    public class AppDataTests
    {
        #region AddAppTest
        [TestMethod()]
        public void AddAppTest()
        {
            var appData = DIHost.GetService<IAppData>();
            string guid = new Random().Next(10000, 99999).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");

            //  正常添加
            var res = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "Test" + guid,
                Path = "Path"
            });
            Assert.IsNotNull(res);

            //  不允许进程名称重复添加测试
            res = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "Test" + guid,
                Path = "Path"
            });
            Assert.IsNull(res);

            //  空值测试   
            res = appData.AddApp(null);
            Assert.IsNull(res);
        }
        #endregion

        #region GetAppTest
        [TestMethod()]
        public void GetAppTest()
        {
            var appData = DIHost.GetService<IAppData>();
            string guid = new Random().Next(10000, 99999).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");

            //  获取不存在的数据
            var res = appData.GetApp(string.Empty);
            Assert.IsNull(res);

            //  添加一条数据
            res = appData.AddApp(new Data.Models.App()
            {
                Name = "Test" + guid
            });
            Assert.IsNotNull(res);

            //  获取一条存在的数据
            var res2 = appData.GetApp(res.Name);
            Assert.IsNotNull(res2);
        }
        #endregion

        #region AddActiveLogTest
        [TestMethod()]
        public void AddActiveLogTest()
        {
            var appData = DIHost.GetService<IAppData>();
            string guid = new Random().Next(10000, 99999).ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");

            //  正常添加
            var resApp = appData.AddApp(new Tai.Core.Data.Models.App()
            {
                Name = "Test" + guid,
                Path = "Path"
            });
            Assert.IsNotNull(resApp);

            //  1. 通过数据模型添加
            //  添加正确数据
            appData.AddActiveLog(new Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 1,
                Start = DateTime.Now,
                End = DateTime.Now.AddSeconds(1),
            });

            //  添加空时长
            appData.AddActiveLog(new Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 0,
                Start = DateTime.Now,
                End = DateTime.Now.AddSeconds(1),
            });

            //  添加错误时间范围
            appData.AddActiveLog(new Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 1,
                Start = DateTime.Now,
                End = DateTime.Now,
            });

            //  添加错误时间范围2
            appData.AddActiveLog(new Data.Models.AppActiveTimeLog()
            {
                AppId = resApp.Id,
                Duration = 1,
                Start = DateTime.Now.AddDays(1),
                End = DateTime.Now,
            });

            int res = appData.Persist();

            Assert.AreEqual(2, res);

            //  2.通过消息数据添加
            //appData.AddActiveLog(new Message.TaiSentryActiveAppTimeMessage()
            //{
            //    CreateTime = 0,
            //    Msg = new Message.SentrActiveAppTimeData()
            //    {
            //        ActiveTime = DateTime.Now,
            //        App = new Message.TaiSentryAppInfo()
            //        {
            //            Process = resApp.Name,
            //            ExecutablePath = resApp.Path,
            //        },
            //        Duration = 1,
            //        EndTime = DateTime.Now.AddSeconds(1),
            //    },
            //    Type = Message.Types.TaiSentryMessageType.ActiveAppTime
            //});


            //  错误数据1
            //appData.AddActiveLog(new Message.TaiSentryActiveAppTimeMessage()
            //{
            //    CreateTime = 0,
            //    Msg = new Message.SentrActiveAppTimeData()
            //    {
            //        ActiveTime = DateTime.Now,
            //        Duration = 1,
            //        EndTime = DateTime.Now.AddSeconds(1),
            //        App = new Message.TaiSentryAppInfo()
            //        {
            //            Process = string.Empty,
            //        },
            //    },
            //    Type = Message.Types.TaiSentryMessageType.ActiveAppTime
            //});


            //  错误数据2
            //appData.AddActiveLog(new Message.TaiSentryActiveAppTimeMessage()
            //{
            //    CreateTime = 0,
            //    Msg = new Message.SentrActiveAppTimeData()
            //    {
            //        ActiveTime = DateTime.Now,
            //        Duration = 0,
            //        EndTime = DateTime.Now.AddSeconds(1),
            //        App = new Message.TaiSentryAppInfo()
            //        {
            //            Process = resApp.Name,
            //        },
            //    },
            //    Type = Message.Types.TaiSentryMessageType.ActiveAppTime
            //});

            //  错误数据3
            //appData.AddActiveLog(new Message.TaiSentryActiveAppTimeMessage()
            //{
            //    CreateTime = 0,
            //    Msg = new Message.SentrActiveAppTimeData()
            //    {
            //        ActiveTime = DateTime.Now,
            //        Duration = 0,
            //        EndTime = DateTime.Now.AddSeconds(1),
            //        App = new Message.TaiSentryAppInfo()
            //        {
            //            Process = "TEST_NULL",
            //        },
            //    },
            //    Type = Message.Types.TaiSentryMessageType.ActiveAppTime
            //});
            //res = appData.Persist();
            //Assert.AreEqual(2, res);
        }
        #endregion
    }
}