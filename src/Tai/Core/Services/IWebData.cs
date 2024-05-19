using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Data.Models;
using Tai.Core.Message;

namespace Tai.Core.Services
{
    public interface IWebData
    {
        void AddActiveLog(int siteId_, TaiWebBrowserActiveMessage msg_);
        /// <summary>
        /// 持久化缓存数据
        /// </summary>
        /// <returns>返回影响行数</returns>
        int Persist();
        Website GetWebsiteByUrl(string url_);
        Website GetWebsiteByDomain(string domain_);
        Website AddWebsite(Website site_);
        bool UpdateIcon(Website site_);
    }
}
