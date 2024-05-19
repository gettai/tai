using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Common
{
    public static class CryptographyHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input_"></param>
        /// <returns></returns>
        public static string MD5(string input_)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input_);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input_">内容</param>
        /// <param name="length_">返回指定长度，负数表示从末端截取，非0正数从第一位开始取，默认为0返回全部</param>
        /// <returns></returns>
        public static string MD5(string input_, int length_ = 0)
        {
            string res = MD5(input_);
            if (length_ > 0)
            {
                return res.Substring(0, length_);
            }
            else if (length_ < 0)
            {
                return res.Substring(res.Length - Math.Abs(length_), Math.Abs(length_));
            }
            else
            {
                return res;
            }
        }
    }
}
