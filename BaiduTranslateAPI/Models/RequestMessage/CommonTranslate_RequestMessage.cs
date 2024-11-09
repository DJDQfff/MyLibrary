using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using BaiduTranslateAPI.Helpers;

namespace BaiduTranslateAPI.Models.RequestMessage
{
    /// <summary> 通用翻译 请求头 </summary>
    internal class CommonTranslate_RequestMessage : HttpRequestMessage
    {
        internal CommonTranslate_RequestMessage(
            IEnumerable<string> queryList,
            string httpuri,
            string to,
            string from,
            string appid,
            string key
        )
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(httpuri);

            string q = queryList.ToMultiplyQ();

            string salt = Helper.Salt();
            string sign = Helper.Sign(appid, q, salt, key);

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>(6)
            {
                { "q", q },
                { "from", from },
                { "to", to },
                { "appid", appid },
                { "salt", salt },
                { "sign", sign }
            };
            HttpContent formUrlEncodedContent = new FormUrlEncodedContent(keyValuePairs);
            Content = formUrlEncodedContent;

            Content.Headers.ContentType = new MediaTypeHeaderValue(
                "application/x-www-form-urlencoded"
            );
        }

        internal CommonTranslate_RequestMessage(
            string httpapi,
            string q,
            string to,
            string from,
            string appid,
            string key
        )
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri(httpapi);

            string salt = Helper.Salt();
            string sign = Helper.Sign(appid, q, salt, key);

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>(6)
            {
                { "q", q },
                { "from", from },
                { "to", to },
                { "appid", appid },
                { "salt", salt },
                { "sign", sign }
            };
            HttpContent formUrlEncodedContent = new FormUrlEncodedContent(keyValuePairs);
            Content = formUrlEncodedContent;

            Content.Headers.ContentType = new MediaTypeHeaderValue(
                "application/x-www-form-urlencoded"
            );
        }
    }
}
