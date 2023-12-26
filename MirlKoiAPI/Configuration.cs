using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

using static DJDQfff. MirlKoiAPI.Configuration.SortType;
namespace DJDQfff. MirlKoiAPI
{
    public  class Configuration
    {
        private int serverindex = 0;
        string[] Servers { get; } = new string[]
        {
            "iw233.cn",
            "api.iw233.cn",
            "ap1.iw233.cn",
            "dev.iw233.cn"
        };
        internal string LoopTryServer () 
        {
            serverindex = serverindex % Servers.Length;
            return Servers[serverindex++];
        }

       internal string this[SortType sortType]
        {
            get
            {

                if(DisplayNames.TryGetValue(sortType,out string value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }

       internal  Dictionary<SortType, string> DisplayNames { get; } = new Dictionary<SortType, string>()

        {
            {random,"随机壁纸（全部图）"  },
            {iw233, "随机壁纸（无色图"},
            { top,"壁纸推荐"},
            {yin,"银发" },
            {cat,"兽耳"},
            {xing,"星空" },
            {mp,"竖屏壁纸"},
        };

        public enum SortType
        {
            random,
            iw233,
            top,
            yin,
            cat,
            xing,
                mp
        }
    }
}
