using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Tai.Common;
using Tai.Core.Data;
using Tai.Core.Services;
using Tai.UI.Main;
using Tai.UI.Services;
using Tai.UI.ViewModels;
using Tai.UI.Views;

namespace Tai
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }
        private System.Threading.Mutex _mutex;
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                DIConfig.InstallServices(services);
            }).Build();
        }



        protected override async void OnStartup(StartupEventArgs e)
        {
            if (IsAppRuning())
            {
                Current.Shutdown();
                return;
            }
            await AppHost.StartAsync();
            Logger.Init();
            var coreMain = AppHost.Services.GetRequiredService<ICoreMain>();
            var uiMain = AppHost.Services.GetRequiredService<IUIMainServicer>();

            //  启动核心服务
            coreMain.Start();

            //  启动UI服务
            uiMain.Start();

            //  创建保活窗口
            MainWindow = new Window();
            base.OnStartup(e);
        }

        #region 获取当前程序是否已运行
        /// <summary>
        /// 获取当前程序是否已运行
        /// </summary>
        private bool IsAppRuning()
        {
            bool ret;
            _mutex = new System.Threading.Mutex(true, System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name, out ret);
            if (!ret)
            {
#if !DEBUG
                return true;
#endif
            }
            return false;
        }
        #endregion

        protected override async void OnExit(ExitEventArgs e)
        {
            NLog.LogManager.Shutdown();
            var uiMainServicer = AppHost.Services.GetRequiredService<IUIMainServicer>();
            uiMainServicer.Stop();
            await AppHost.StopAsync();
            base.OnExit(e);
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Logger.Error(e.Exception);
            NLog.LogManager.Shutdown();
        }
    }
}
