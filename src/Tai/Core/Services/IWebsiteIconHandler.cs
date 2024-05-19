using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Data.Models;

namespace Tai.Core.Services
{
    /// <summary>
    /// Website图标处理服务
    /// </summary>
    public interface IWebsiteIconHandler
    {
        void Start();
        void Stop();
        /// <summary>
        /// 获取并更新图标（使用队列更新，并不是实时）
        /// </summary>
        /// <param name="site_"></param>
        /// <param name="iconUrl_"></param>
        void UpdateIcon(Website site_, string iconUrl_);
    }
}
