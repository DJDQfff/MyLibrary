using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using static System.Net.Mime.MediaTypeNames;

namespace DJDQfff. RainChanPixivAPI
{
    public class GetUriHelper
    {
        /// <summary> 随即返回一张比较小的图片 </summary>
        /// <param name="host"> </param>
        /// <returns> </returns>
        public static string GetUri (string host)
        {
            return host + "/img";
        }

        /// <summary> 获取某张图片的原始文件 </summary>
        /// <param name="host"> </param>
        /// <param name="imageID"> </param>
        /// <returns> </returns>
        public static string GetUri (string host, string imageID)
        {
            return GetUri(host) + "?img_id=" + imageID;
        }

        /// <summary> 得到一张适合Web环境使用的图片 </summary>
        /// <param name="host"> </param>
        /// <param name="ImageID"> </param>
        /// <param name="web"> </param>
        /// <returns> </returns>
        public static string GetUri (string host, string ImageID, bool web)
        {
            return GetUri(host, ImageID) + "&web=" + web;
        }
    }
}