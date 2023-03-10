using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using static MyLibrary.Algorithm.Sort.SortByCloseLength;

namespace MyLibrary.Algorithm.Compare.EnglishSentence
{
    /// <summary> 英语词句比较算法用到的通用方法 </summary>
    public static class Common
    {
        /// <summary> 把一个字符串按空格切分。 </summary>
        /// <param name="vs"> 需要是英语句子 </param>
        /// <returns> 按空格切分的单词 </returns>
        public static List<string[]> SplitIntoWords (this IEnumerable<string> vs)
        {
            List<string[]> list = new List<string[]>();
            foreach (var str in vs)
            {
                list.Add(str.Split(' '));
            }
            return list;
        }
    }
}