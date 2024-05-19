using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Common
{
    /// <summary>
    /// 文件系统帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取应用运行目录
        /// </summary>
        /// <returns></returns>
        public static string GetAppRunDirectory()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
