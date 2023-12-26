using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DJDQfff.BaiduTranslateAPI.Helpers
{
    /// <summary>
    /// 帮助类
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// 把一个
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static string ToMultiplyQ (this IEnumerable<string> list)
        {
            StringBuilder multiplyq = new StringBuilder();
            foreach (string str in list)
            {
                multiplyq.Append(str);
                multiplyq.Append('\n');
            }
            return multiplyq.ToString();
        }

        internal static string Decode (string str)
        {
            string decode = HttpUtility.HtmlDecode(str);
            return decode;
        }

        internal static string Salt ()
        {
            string salt = new Random().Next(100).ToString();
            return salt;
        }

        internal static string Sign (string appid , string q , string salt , string key)
        {
            string str = appid + q + salt + key;

            MD5 md5 = MD5.Create();//MD5类不能实例化
            byte[] vs = Encoding.UTF8.GetBytes(str);
            byte[] vs1 = md5.ComputeHash(vs);//获得一个128位的字节数组，一字节8位，共16字节

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte v in vs1)
            {
                stringBuilder.Append(v.ToString("x2"));//每个字节8位，对应两个16进制数（每个16进制数占4位，两个16进制数表一个字节）
            }
            return stringBuilder.ToString();
        }
    }
}