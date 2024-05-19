using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tai.UI.Main;

namespace Tai.UI.Services
{
    public class WindowsServicer : IWindowsServicer
    {
        private MainWindow _mainWindow;
        public WindowsServicer()
        {
        }
        public void CloseMainWindow()
        {
        }

        public void ShowMainWindow()
        {
            if (_mainWindow == null)
            {
                _mainWindow = App.AppHost.Services.GetRequiredService<MainWindow>();
                _mainWindow.Closed += (s, e) =>
                {
                    _mainWindow.DataContext = null;
                    _mainWindow = null;
                };
            }

            _mainWindow.Show();
            _mainWindow.Activate();
        }
    }
}
