using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Data.Models
{
    /// <summary>
    /// 网页活跃时长记录
    /// </summary>
    public class WebActiveTimeLog
    {
        public int Id { get; set; }
        /// <summary>
        /// 站点ID
        /// </summary>
        public int WebsiteId { get; set; }
        public virtual Website Site { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// 持续时长（秒）
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
    }
}
