using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using LogLevel = NLog.LogLevel;

namespace Tai.Common
{
    public class Logger
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Init()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // 创建一个Debug目标，用于输出日志到调试输出窗口
            var debugTarget = new DebugSystemTarget("debug")
            {
                Layout = "\r\n<(●'◡'●)>↘ [${level:uppercase=true}] [${longdate}]\r\n\r\n${message}\r\n\r\nEND ----------- - ↑ - ----------- END" // 日志格式
            };

            // 将Debug目标添加到配置中
            config.AddTarget(debugTarget);

            // 创建一个规则，将所有日志记录到Debug目标
            config.AddRuleForAllLevels(debugTarget);

            // 创建一个文件目标，用于保存日志
            var fileTarget = new FileTarget("file")
            {
                FileName = $"logs/log_{DateTime.Now:yyyyMMdd}.txt", // 日志文件路径
                Layout = "${longdate} ${level:uppercase=true} ${callsite:includeSourcePath=true:skipFrames=2} ${message}" // 日志格式
            };
            var asyncTarget = new AsyncTargetWrapper(fileTarget, 50, AsyncTargetWrapperOverflowAction.Discard)
            {
                Name = "async"
            };
            // 将文件目标添加到配置中
            config.AddTarget(asyncTarget);
            config.AddRule(LogLevel.Info, LogLevel.Error, asyncTarget);
            NLog.LogManager.Configuration = config;
        }

        public static void Error(Exception ex_)
        {
            _logger.Error(ex_);
        }
        public static void Error(Exception ex_, string msg_)
        {
            _logger.Error(ex_, msg_);
        }
        public static void Error(object obj_)
        {
            _logger.Error(obj_);
        }
        public static void Error(string msg_, object obj_)
        {
            _logger.Error(msg_, obj_);
        }

        public static void Warn(Exception ex_)
        {
            _logger.Warn(ex_);
        }
        public static void Warn(Exception ex_, string msg_)
        {
            _logger.Warn(ex_, msg_);
        }
        public static void Warn(object message_)
        {
            _logger.Warn(message_);
        }
        public static void Warn(string msg_, object obj_)
        {
            _logger.Warn(msg_, obj_);
        }

        public static void Info(Exception ex_)
        {
            _logger.Info(ex_);
        }
        public static void Info(Exception ex_, string msg_)
        {
            _logger.Info(ex_, msg_);
        }
        public static void Info(object message_)
        {
            _logger.Info(message_);
        }
        public static void Info(string msg_, object obj_)
        {
            _logger.Info(msg_, obj_);
        }

        public static void Debug(Exception ex_)
        {
            _logger.Debug(ex_);
        }
        public static void Debug(Exception ex_, string msg_)
        {
            _logger.Debug(ex_, msg_);
        }
        public static void Debug(object message_)
        {
            _logger.Debug(message_);
        }
        public static void Debug(string msg_, object obj_)
        {
            _logger.Debug(msg_, obj_);
        }
    }
}
