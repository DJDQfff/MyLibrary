using System;
using System.Collections.Generic;
using System.Text;
namespace DJDQfff.Weather.Core.Request.Free
{
    public static class GetMethodUri
    {
        public static string FreeDayUri (string cityid = CityIDs.湖北咸宁赤壁)
        {
            string uri = $"https://www.tianqiapi.com/free/day?appid={Configuration.APPID}&appsecret={Configuration.APPSecret}&cityid={cityid}";
            return uri;
        }
    }
}
