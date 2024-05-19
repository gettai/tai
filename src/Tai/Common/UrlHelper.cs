using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tai.Common
{
    public class UrlHelper
    {
        /// <summary>
        /// 获取链接中的域名部分
        /// </summary>
        /// <param name="url_">链接</param>
        /// <param name="isRemovePH_">是否移除协议头(默认是)</param>
        /// <returns></returns>
        public static string GetDomain(string url_, bool isRemovePH_ = true)
        {
            if (string.IsNullOrEmpty(url_)) { return url_; }

            var ph = Regex.Match(url_, @"((https|http|ftp|rtsp|mms)?:\/\/)").Value;
            if (!string.IsNullOrEmpty(ph))
            {
                url_ = url_.Replace(ph, string.Empty);
            }

            int index = url_.IndexOf("/");
            if (index != -1)
            {
                url_ = url_.Substring(0, index);
            }

            return !isRemovePH_ ? ph + url_ : url_;
        }
        /// <summary>
        /// 判断链接是否是域名主页
        /// </summary>
        /// <param name="url_"></param>
        /// <returns></returns>
        public static bool IsIndexUrl(string url_)
        {
            url_ = url_.Replace("http://", string.Empty);
            url_ = url_.Replace("https://", string.Empty);
            return url_.IndexOf("/") == -1 || url_.Last() == '/';
        }
        /// <summary>
        /// 从URL中获取支持的Icon名称（包括后缀）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetValidIconName(string url)
        {
            Regex regex = new Regex(@"\/([^\/?#]+)\.(?:jpg|jpeg|png|gif|ico)");
            Match match = regex.Match(url);
            if (match.Success)
            {
                return match.Value.Substring(1);
            }
            else
            {
                return null; // 或者抛出异常，视情况而定
            }
        }
    }
}
