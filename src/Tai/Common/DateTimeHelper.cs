using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 格式化时间与数据库匹配
        /// </summary>
        /// <param name="datetime_"></param>
        /// <returns></returns>
        public static DateTime Fromat(DateTime datetime_)
        {
            return DateTime.Parse($"{datetime_:yyyy-MM-dd HH:mm:ss}");
        }
        /// <summary>
        /// 格式化时间为数据库格式的字符串
        /// </summary>
        /// <param name="datetime_"></param>
        /// <returns></returns>
        public static string FromatToString(DateTime datetime_)
        {
            return $"{datetime_:yyyy-MM-dd HH:mm:ss}";
        }

        /// <summary>
        /// 获取今日的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        public static DateTime[] GetTodayStartToEndTimes()
        {
            return GetStartToEndTimes(DateTime.Now);
        }

        /// <summary>
        /// 获取指定日期的开始时间和结束时间
        /// </summary>
        /// <param name="datetime_"></param>
        /// <returns></returns>
        public static DateTime[] GetStartToEndTimes(DateTime datetime_)
        {
            return new DateTime[] { DateTime.Parse($"{datetime_:yyyy-MM-dd 00:00:00}"), DateTime.Parse($"{datetime_:yyyy-MM-dd 23:59:59}") };
        }
        /// <summary>
        /// 将秒转换为时间数组
        /// </summary>
        /// <param name="seconds_"></param>
        /// <returns>[小时,分钟,秒]</returns>
        public static int[] ConvertSecondsToTimeArray(int seconds_)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds_);
            int[] result = new int[] { 0, 0, 0 };
            if (timeSpan.TotalMinutes < 1)
            {
                result[2] = seconds_;
            }
            else if (timeSpan.TotalHours < 1)
            {
                result[1] = (int)timeSpan.TotalMinutes;
                if (timeSpan.Seconds > 0)
                {
                    result[2] = timeSpan.Seconds;
                }
            }
            else
            {
                result[0] = (int)timeSpan.TotalHours;
                result[1] = timeSpan.Minutes;
                result[2] = timeSpan.Seconds;
            }

            return result;
        }

        /// <summary>
        /// 将秒转换为时分秒字符串，当有小时位时忽略秒位
        /// </summary>
        /// <param name="seconds_">秒</param>
        /// <param name="hoursText_">拼接小时文本</param>
        /// <param name="minutesText_">拼接分钟文本</param>
        /// <param name="secondsText_">拼接秒文本</param>
        /// <returns></returns>
        public static string ConvertSecondsToTimeString(int seconds_, string hoursText_ = "小时", string minutesText_ = "分钟", string secondsText_ = "秒")
        {
            var times = ConvertSecondsToTimeArray(seconds_);
            string result = string.Empty;

            if (times[0] > 0)
            {
                result += times[0] + hoursText_;
                if (times[1] > 0)
                {
                    result += times[1] + minutesText_;
                }
            }
            else if (times[1] > 0)
            {
                result += times[1] + minutesText_;
                if (times[2] > 0)
                {
                    result += times[2] + secondsText_;
                }
            }
            else
            {
                result += times[2] + secondsText_;
            }

            return result;
        }
        /// <summary>
        /// 获取本周的开始和结束时间
        /// </summary>
        /// <returns>[Start,End]</returns>
        public static DateTime[] GetWeekTimes(DayOfWeek weekFirst_ = DayOfWeek.Monday)
        {
            return GetWeekTimes(DateTime.Now, weekFirst_);
        }
        /// <summary>
        /// 获取指定日期一周的开始和结束时间
        /// </summary>
        /// <param name="date_">日期</param>
        /// <param name="weekFirst_">开始日（默认周一）</param>
        /// <returns>[Start,End]</returns>
        public static DateTime[] GetWeekTimes(DateTime date_, DayOfWeek weekFirst_ = DayOfWeek.Monday)
        {

            var diff = (date_.DayOfWeek == 0 ? 7 : (int)date_.DayOfWeek) - (weekFirst_ == 0 ? 7 : (int)weekFirst_);
            var start = DateTime.Parse($"{date_.Date.AddDays(-diff):yyyy-MM-dd 00:00:00}");
            var end = DateTime.Parse($"{start.AddDays(6):yyyy-MM-dd 23:59:59}");

            return new DateTime[] { start, end };
        }
    }
}
