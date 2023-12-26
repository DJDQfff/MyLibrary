using System;
using System.Collections.Generic;
using System.Text;

namespace DJDQfff.Weather.Core.ResponseJson
{
    public class FreeDayJson
    {
        public string cityid { get; set; }
        public string city { get; set; }
        public string update_time { get; set; }
        public string wea { get; set; }
        public string wea_img { get; set; }
        public string tem { get; set; }
        public string tem_day { get; set; }
        public string tem_night { get; set; }
        public string win { get; set; }
        public string win_speed { get; set; }
        public string win_meter { get; set; }
        public string air { get; set; }
    }

}
