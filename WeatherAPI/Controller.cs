using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Weather.Core.ResponseJson;
using Weather.Core.Request.Free;
using System.Threading.Tasks;

namespace Weather.Core
{
    public class Controller
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<FreeDayJson> GetFreeDay()
        {
            string response = await httpClient.GetStringAsync(GetMethodUri.FreeDayUri());
            FreeDayJson freeDayJson = JsonSerializer.Deserialize<FreeDayJson>(response);
            return freeDayJson;
        }
    }
}
