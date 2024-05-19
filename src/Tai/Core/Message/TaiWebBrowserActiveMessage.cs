using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message
{
    public struct TaiWebBrowserActiveData
    {
        /// <summary>
        /// 网页Id（来自于浏览器TabId）
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 网页标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 网页图标（可能为空）
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public int Duration { get; set; }
        public DateTime StartDateTime
        {
            get
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dateTime.AddSeconds(Start).ToLocalTime();
            }
        }
        public DateTime EndDateTime
        {
            get
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dateTime.AddSeconds(End).ToLocalTime();
            }
        }
    }
    public class TaiWebBrowserActiveMessage : TaiWebBrowserMessageBase
    {
        public TaiWebBrowserActiveData Data { get; set; }
    }
}
