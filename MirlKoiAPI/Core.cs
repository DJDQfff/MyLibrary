using System;
using System.IO;
using System.Net.Http;

using static MirlKoiAPI.Configuration;

namespace MirlKoiAPI
{
    public class Core
    {
        Configuration configuration = new Configuration();

        HttpClient httpClient = new HttpClient();

        public string GetUri(SortType e)
        {
            string server = configuration.LoopTryServer();

            string url = "https://" + server + "/api.php?sort=" + e.ToString();
            return url;
        }

        public Stream GetImage(SortType sortType)
        {
            string str = GetUri(sortType);
            Uri uri = new Uri(str);
            var content = httpClient.GetAsync(uri).Result.Content;
            Stream stream = content.ReadAsStreamAsync().Result;
            return stream;
        }
    }
}
