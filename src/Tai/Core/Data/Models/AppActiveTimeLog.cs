using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Data.Models
{
    /// <summary>
    /// 应用活跃时长记录
    /// </summary>
    public class AppActiveTimeLog
    {
        public int Id { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }
        public virtual App App { get; set; }
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
    }
}
