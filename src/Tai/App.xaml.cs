using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tai.UI.Main;
using Tai.UI.Servicer;
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
        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                InstallServices(services);
            }).Build();
        }

        private void InstallServices(IServiceCollection services_)
        {
            services_.AddSingleton<INotifyIconServicer, NotifyIconServicer>();
            services_.AddSingleton<IWindowsServicer, WindowsServicer>();
            services_.AddSingleton<IUIMainServicer, UIMainServicer>();
            services_.AddSingleton<IRouter, Router>();

            services_.AddTransient<MainWindow>();
            services_.AddTransient<MainVM>();
            
            services_.AddTransient<Overview>();
            services_.AddTransient<OverviewVM>();
            services_.AddTransient<Statistics>();
            services_.AddTransient<Page1>();
            services_.AddTransient<Page2>();
            services_.AddTransient<Page2VM>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            var uiMainServicer = AppHost.Services.GetRequiredService<IUIMainServicer>();
            uiMainServicer.Start();
            MainWindow = new Window();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var uiMainServicer = AppHost.Services.GetRequiredService<IUIMainServicer>();
            uiMainServicer.Stop();
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}
