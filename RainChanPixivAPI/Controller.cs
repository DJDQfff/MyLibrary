using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Net.Http;
using DJDQfff. RainChanPixivAPI;
using System.Threading.Tasks;

namespace DJDQfff. RainChanPixivAPI
{
    public class Controller
    {
        private HttpClient httpClient = new HttpClient();
        private string host;

        public Controller ()
        {
            foreach (var str in HostAddress.address)
            {
                var address = httpClient.GetAsync(str).Result;
                if (address.IsSuccessStatusCode)
                {
                    host = str;
                    break;
                }
            }
        }

        /// <summary> 随即返回一张比较小的图片 </summary>
        /// <returns> </returns>
        public async Task<Stream> GetRandom ()
        {
            string get = GetUriHelper.GetUri(host);
            var response = await httpClient.GetStreamAsync(get);

            return response;
        }

        /// <summary> 得到一张适合Web环境使用的图片 </summary>
        public async Task<Stream> GetWebImage (string imageID, bool web)
        {
            string get = GetUriHelper.GetUri(host, imageID, web);
            var response = await httpClient.GetStreamAsync(get);

            return response;
        }

        /// <summary> 获取某张图片的原始文件 </summary>
        public async Task<Stream> GetSourceImage (string imageID)
        {
            string get = GetUriHelper.GetUri(host);
            var response = await httpClient.GetStreamAsync(get);

            return response;
        }
    }
}