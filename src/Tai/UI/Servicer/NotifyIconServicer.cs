using LancerUI.Controls.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tai.UI.Servicer
{
    public class NotifyIconServicer : INotifyIconServicer
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private readonly string _defaultIcon = "pack://application:,,,/Tai;component/Resources/Icon/tai32.ico";

        private readonly IWindowsServicer _windowsServicer;
        public NotifyIconServicer(IWindowsServicer windowsServicer)
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.MouseDoubleClick += _notifyIcon_MouseDoubleClick;
            _windowsServicer = windowsServicer;
        }

        private void _notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _windowsServicer.ShowMainWindow();
        }

        public void SetIcon(string path_ = null)
        {
            if (string.IsNullOrEmpty(path_))
            {
                path_ = _defaultIcon;
            }
            Stream iconStream = Application.GetResourceStream(new Uri(path_)).Stream;
            _notifyIcon.Icon = new System.Drawing.Icon(iconStream);
        }

        public void Start()
        {
            _notifyIcon.Visible = true;
            SetIcon();
        }

        public void Stop()
        {
            _notifyIcon.Visible = false;
        }
    }
}
