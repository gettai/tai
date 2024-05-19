using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url_">文件远程地址</param>
        /// <param name="savePath_">本地保存路径</param>
        public static async Task<string> DownloadAsync(string url_, string savePath_)
        {
            if (string.IsNullOrEmpty(url_)) return string.Empty;

            string dir = Path.GetDirectoryName(savePath_);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url_, HttpCompletionOption.ResponseHeadersRead);

                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream contentStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (FileStream fileStream = File.Create(savePath_))
                            {
                                await contentStream.CopyToAsync(fileStream);
                                return savePath_;
                            }
                        }
                    }
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "下载失败");
                return string.Empty;
            }
        }
    }
}
