﻿using System;

using Windows.UI;
using Windows.UI.Xaml.Media;

namespace DJDQfff.UWP
{
    /// <summary>
    /// WindowsColor帮助类
    /// </summary>
    public class WindowsUIColorHelper
    {
        /// <summary>
        /// 获取随机颜色SolidColorBrush
        /// </summary>
        /// <returns></returns>
        public static SolidColorBrush GetRandomSolidColorBrush()
        {
            //Type type = typeof(Windows.UI.Colors);
            //var a = type.GetProperties();
            //Random random = new Random();
            //var b = random.Next(0, a.Length);
            //var c = a[b];
            //var d = (Windows.UI.Color) c.GetValue(b);

            var random2 = new Random();

            byte[] ragb = new byte[4];

            random2.NextBytes(ragb);

            Color color = Color.FromArgb(ragb[0], ragb[1], ragb[2], ragb[3]);
            SolidColorBrush solidColorBrush = new SolidColorBrush(color);

            return solidColorBrush;
        }
    }
}
