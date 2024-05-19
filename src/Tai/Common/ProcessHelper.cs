using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Common
{
    /// <summary>
    /// 进程操作帮助类
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 指定进程是否正在运行
        /// </summary>
        /// <param name="processName_">进程名称</param>
        /// <returns></returns>
        public static bool IsProcessRuning(string processName_)
        {
            processName_ = processName_.Replace(".exe", string.Empty);
            Process[] process = Process.GetProcessesByName(processName_);
            return process != null && process.Length > 0;
        }

        /// <summary>
        /// 启动一个进程
        /// </summary>
        /// <param name="file_">可执行文件路径</param>
        /// <param name="isRunAsAdministator_">是否以管理员身份启动</param>
        /// <returns></returns>
        public static bool Start(string file_, bool isRunAsAdministator_ = false)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = file_;
            proc.StartInfo.UseShellExecute = true;
            if (isRunAsAdministator_)
            {
                proc.StartInfo.Verb = "runas";
            }
            return proc.Start();
        }

        /// <summary>
        /// 终止指定进程
        /// </summary>
        /// <param name="processName_">进程名称</param>
        public static void Kill(string processName_)
        {
            processName_ = processName_.Replace(".exe", string.Empty);
            try
            {
                // 根据进程名称获取进程对象
                Process[] processes = Process.GetProcessesByName(processName_);

                // 如果存在指定名称的进程，则终止它
                if (processes.Length > 0)
                {
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
