using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using DJDQfff.BaiduTranslateAPI.Models.RequestMessage;
using DJDQfff.BaiduTranslateAPI.Models.ResponseJSON;

namespace DJDQfff.BaiduTranslateAPI
{
    /// <summary> 简单翻译api封装， </summary>
    public class SimpleTranslator : IDisposable
    {
        /// <summary>
        /// 百度翻译开发平台开发者APPID
        /// </summary>
        public string APPID { get; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string KEY { get; }

        private string HttpsAPI { get; } = "https://fanyi-api.baidu.com/api/trans/vip/translate";

        /// <summary>
        /// 百度翻译api简单封装
        /// </summary>
        /// <param name="appid">注册成为开发者，获取 APPID</param>
        /// <param name="key">开通通用翻译API服务，获取 Key</param>
        public SimpleTranslator (string appid , string key)
        {
            APPID = appid;
            KEY = key;
        }

        private readonly HttpClient httpClient = new HttpClient();

        /// <summary> 单个通用文本翻译</summary>
        /// <param name="query">翻译内容 </param>
        /// <param name="to">目标语种，默认简中 </param>
        /// <param name="from">原语种，默认自动识别 </param>
        /// <returns> 翻译结果</returns>
        public async Task<string> CommonTranslateAsync (string query , string to = "zh" , string from = "auto")
        {
            var requestMessage = new CommonTranslate_RequestMessage(HttpsAPI , query , to , from , APPID , KEY);
            try
            {
                var httpResponseMessage = await httpClient.SendAsync(requestMessage);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();

                var common = JsonSerializer.Deserialize<CommonTranslate_ResponseMessage>(response);
                return common.trans_result.Single().dst;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 批量通用文本翻译l </summary>
        /// <param name="queryList"> 翻译内容</param>
        /// <param name="to">目标语种，默认简中 </param>
        /// <param name="from"> 原语种，默认自动识别</param>
        /// <returns>翻译结果 </returns>
        public async Task<List<trans_result>> CommonTranslateAsync (IEnumerable<string> queryList , string to = "zh" , string from = "auto")
        {
            var requestMessage = new CommonTranslate_RequestMessage(queryList , HttpsAPI , to , from , APPID , KEY);

            try
            {
                var httpResponseMessage = await httpClient.SendAsync(requestMessage);
                string response = await httpResponseMessage.Content.ReadAsStringAsync();
                var common = JsonSerializer.Deserialize<CommonTranslate_ResponseMessage>(response);
                return common.trans_result.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        ///// <summary> 使用Get方式翻译单个 </summary>
        ///// <param name="query"> 翻译内容 </param>
        ///// <param name="to"> 目标语种，默认简中 </param>
        ///// <param name="from"> 原语种 </param>
        ///// <returns> 翻译后的句子 </returns>
        //[Obsolete("不用Get" , true)]
        //public async Task<string> CommonTranslateGetAsync (string query , string to = "zh" , string from = "auto")
        //{
        //    string salt = Helper.Salt();
        //    string sign = Helper.Sign(APPID , query , salt , KEY);
        //    string requestUri = HttpsAPI + "?q=" + HttpUtility.UrlEncode(query) + "&from=" + from + "&to=" + to + "&appid=" + APPID + "&salt=" + salt + "&sign=" + sign;
        //    string response = await httpClient.GetStringAsync(requestUri);

        //    return response;
        //}

        /// <summary> 释放网络资源 </summary>
        public void Dispose ()
        {
            httpClient.Dispose();
        }
    }
}