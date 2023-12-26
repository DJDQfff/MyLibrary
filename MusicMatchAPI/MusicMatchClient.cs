using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;
using System.Web;

namespace MusicMatchAPI
{
    /// <summary>
    /// 这个api免费版只提供一首歌的前30%的歌词，没鸟用
    /// </summary>
    public class MusicMatchClient:IDisposable
    {

        private HttpClient httpClient = new HttpClient();
        private bool disposedValue;
        private const string apikey = @"ce14fde2d24f2b7466b3d64d562e9853";

        private const string rootURI = @"https://api.musixmatch.com/ws/1.1/";

        public   string GetLyric(string songname,string singername)
        {
           
            string uri = rootURI + "matcher.lyrics.get" +
                "?apikey=" + apikey + 
                "&q_track=" +HttpUtility.UrlEncode( songname) + 
                "&q_artist=" + HttpUtility.UrlEncode( singername);


            string result = httpClient.GetStringAsync(uri).Result;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(result);
            var utf8 = new Utf8JsonReader(bytes);
            var json = JsonSerializer.Deserialize<Rootobject>(ref utf8);

            return json.message.body.lyrics.lyrics_body;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                 
                    httpClient.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~MusicMatchClient()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
