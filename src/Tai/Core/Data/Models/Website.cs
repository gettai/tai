using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Data.Models
{
    /// <summary>
    /// 网站信息
    /// </summary>
    public class Website
    {
        public int Id { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 累计浏览总时长（秒）
        /// </summary>
        public int ActiveDuration { get; set; }
        /// <summary>
        /// 累计闲置总时长（秒）
        /// </summary>
        public int InactiveDuration { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsFollowed { get; set; } = false;
    }
}
