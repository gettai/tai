using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Tai.Core.Data.Models;
using System.Drawing;
using Tai.Common;
using Tai.Constants;

namespace Tai.Core.Services
{
    public class AppIconHandler : IAppIconHandler
    {
        private readonly IPolling _polling;
        private readonly IAppData _appData;

        private readonly ConcurrentQueue<Data.Models.App> _appQueue = new ConcurrentQueue<Data.Models.App>();

        public AppIconHandler(IPolling polling_, IAppData appData_)
        {
            _polling = polling_;
            _appData = appData_;
        }

        public void Start()
        {
            _polling.PollingTimeElapsed += _polling_PollingTimeElapsed;
        }

        private void _polling_PollingTimeElapsed()
        {
            Handle();
        }

        public void Stop()
        {
            _polling.PollingTimeElapsed -= _polling_PollingTimeElapsed;
        }

        public void UpdateIcon(Data.Models.App app_)
        {
            _appQueue.Enqueue(app_);
        }

        private void Handle()
        {
            if (!_appQueue.IsEmpty)
            {
                Data.Models.App app;
                if (_appQueue.TryDequeue(out app))
                {
                    string icon = ExtractFromFile(app);
                    if (!string.IsNullOrEmpty(icon))
                    {
                        app.Icon = icon;
                        _appData.UpdateAppIcon(app);
                    }
                }
            }
        }

        /// <summary>
        /// 提取icon为Png格式并保存到程序目录下
        /// </summary>
        /// <param name="file"></param>
        /// <param name="processname"></param>
        /// <param name="desc"></param>
        /// <param name="isRelativePath">是否返回相对路径</param>
        /// <returns>返回提取到程序目录下的路径</returns>
        private string ExtractFromFile(Data.Models.App app_)
        {
            try
            {
                string iconName = $"{CryptographyHelper.MD5(app_.Name, 6)}.png";
                string iconPath = Path.Combine(FileHelper.GetAppRunDirectory(), AppConstants.AppIconFolder, iconName);
                string relativePath = Path.Combine(AppConstants.AppIconFolder, iconName);
                string dir = Path.GetDirectoryName(iconPath);
                string file = app_.Path;

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //  uwp app icon handle
                if (file.IndexOf("WindowsApps") != -1)
                {
                    //  只有在包含此关键字时才去处理

                    //  继续判断是否是uwp程序
                    string appdir = file.Substring(0, file.Length - file.Split('\\').Last().Length);
                    string appxManifestPath = appdir + "AppxManifest.xml";
                    if (File.Exists(appxManifestPath))
                    {
                        //  是uwp程序
                        Debug.WriteLine("is uwp!" + appxManifestPath);
                        //  读取描述文件
                        string manifestText = File.ReadAllText(appxManifestPath);
                        var match = Regex.Match(manifestText, @"<Logo>(.*?)</Logo>");
                        string logoName = match.Groups[1].Value;

                        string iconFile = string.Empty;

                        string logo100 = logoName.Replace(".png", ".scale-100.png");
                        string logo125 = logoName.Replace(".png", ".scale-125.png");
                        string logo150 = logoName.Replace(".png", ".scale-150.png");
                        string logo200 = logoName.Replace(".png", ".scale-200.png");
                        string logo400 = logoName.Replace(".png", ".scale-400.png");

                        if (File.Exists(appdir + logo100))
                        {
                            iconFile = appdir + logo100;
                        }
                        else if (File.Exists(appdir + logo125))
                        {
                            iconFile = appdir + logo125;
                        }
                        else if (File.Exists(appdir + logo150))
                        {
                            iconFile = appdir + logo150;
                        }
                        else if (File.Exists(appdir + logo200))
                        {
                            iconFile = appdir + logo200;
                        }
                        else if (File.Exists(appdir + logo400))
                        {
                            iconFile = appdir + logo400;
                        }
                        else
                        {
                            return string.Empty;
                        }

                        if (!string.IsNullOrEmpty(iconFile) && File.Exists(iconFile))
                        {
                            //  copy to tai dir
                            File.Copy(iconFile, iconPath);
                            return relativePath;
                        }
                        return string.Empty;
                    }
                }



                //  exe app icon handle

                Icon ico = Icon.ExtractAssociatedIcon(file);

                using (var fileStream = new FileStream(iconPath, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(ToImageSource(ico) as BitmapSource));
                    encoder.Save(fileStream);
                }
                return relativePath;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        private ImageSource ToImageSource(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
    }
}
