using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Data.Models
{
    /// <summary>
    /// 应用信息
    /// </summary>
    public class App
    {
        public int Id { get; set; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 进程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 应用说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图标（相对路径）
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 可执行文件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 累计使用时长（秒）
        /// </summary>
        public int ActiveDuration { get; set; } = 0;
        /// <summary>
        /// 累计后台时长（秒）
        /// </summary>
        public int InactiveDuration { get; set; } = 0;
        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// 是否关注
        /// </summary>
        public bool IsFollowed { get; set; } = false;
    }
}
