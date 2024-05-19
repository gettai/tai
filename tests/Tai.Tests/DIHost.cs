using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai;

namespace Tai.Tests
{
    public class DIHost
    {
        private static IHost AppHost { get; set; }

        private static void Init()
        {
            if (AppHost != null) return;
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                DIConfig.InstallServices(services);
            }).Build();
            AppHost.Start();
        }

        public static T GetService<T>()
        {
            if (AppHost == null)
            {
                Init();
            }
            return AppHost.Services.GetRequiredService<T>();
        }
    }
}
