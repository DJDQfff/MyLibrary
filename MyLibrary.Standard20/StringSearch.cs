using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyLibrary.Standard20
{
    /// <summary>
    /// 字符串内查找
    /// </summary>
    public static class StringSearch
    {
        /// <summary>
        /// 查找字符串的正则，不存在则返回-1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static int SearchOrderListIndex(this string str, string regex)
        {
            var match = Regex.Match(str, regex);
            return match.Success ? match.Index : -1;
        }

        /// <summary>
        /// 返回字符串的缩进（第一个非空白字符的索引）
        /// </summary>
        /// <param name="str">字符串不能是null、空字符串、全空白符，否则报错</param>
        /// <returns>缩进</returns>
        public static int FirstNotWhiteSpaceCharacterIndex(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException();
            }
            string regex = @"[^\s]";
            return Regex.Match(str, regex).Index;
        }

        /// <summary>
        /// 遍历查找字符串数组各项，查找出所有位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="words">被查找字符串数组</param>
        /// <returns>Key为被查找字符串，Values为该字符串在目标中的所有位置</returns>
        public static Dictionary<string, List<int>> IndexOfAny(this string str, params string[] words)
        {
            Dictionary<string, List<int>> keyValuePairs = new Dictionary<string, List<int>>();

            foreach (var word in words)
            {
                List<int> vs = new List<int>();
                int index;
                while ((index = str.IndexOf(word)) != -1)
                {
                    vs.Add(index);
                }
                keyValuePairs.Add(word, vs);
            }

            return keyValuePairs;
        }
    }
}