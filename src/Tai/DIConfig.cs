using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Services;
using Tai.UI.DataAccess;
using Tai.UI.Main;
using Tai.UI.Services;
using Tai.UI.ViewModels;
using Tai.UI.Views;

namespace Tai
{
    public class DIConfig
    {
        public static void InstallServices(IServiceCollection services_)
        {
            //  注册核心服务
            services_.AddSingleton<IDataBase, DataBase>();
            services_.AddSingleton<ITaiSentry, TaiSentry>();
            services_.AddSingleton<IAppTimeTracker, AppTimeTracker>();
            services_.AddSingleton<ICoreMain, CoreMain>();
            services_.AddSingleton<IAppData, AppData>();
            services_.AddSingleton<IPolling, Polling>();
            services_.AddSingleton<IAppIconHandler, AppIconHandler>();
            services_.AddSingleton<IWebBrowserListener, WebBrowserListener>();
            services_.AddSingleton<IWebData, WebData>();
            services_.AddSingleton<IWebTimeTracker, WebTimeTracker>();
            services_.AddSingleton<IWebsiteIconHandler, WebsiteIconHandler>();


            //  注册UI服务
            services_.AddSingleton<INotifyIconServicer, NotifyIconServicer>();
            services_.AddSingleton<IWindowsServicer, WindowsServicer>();
            services_.AddSingleton<IUIMainServicer, UIMainServicer>();
            services_.AddSingleton<IRouter, Router>();

            services_.AddTransient<MainWindow>();
            services_.AddTransient<MainVM>();

            services_.AddTransient<Overview>();
            services_.AddTransient<OverviewVM>();
            services_.AddTransient<Statistics>();
            services_.AddTransient<StatisticsVM>();
            services_.AddTransient<Page1>();
            services_.AddTransient<Page2>();
            services_.AddTransient<Page2VM>();

            //  数据库操作
            services_.AddTransient<IAppRepository, AppRepository>();
            services_.AddTransient<IAppActiveTimeLogRepository, AppActiveTimeLogRepository>();
            services_.AddTransient<IWebActiveTimeLogRepository, WebActiveTimeLogRepository>();
            services_.AddTransient<IWebRepository, WebRepositroy>();
            services_.AddTransient<IAppInactiveTimeLogRepository, AppInactiveTimeLogRepository>();
        }
    }
}
