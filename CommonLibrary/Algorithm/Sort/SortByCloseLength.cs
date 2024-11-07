using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary.Algorithm.Sort
{
    /// <summary> 自己写的排序 </summary>
    public static class SortByCloseLength
    {
        /// <summary>
        /// 对一个字符串集合，按内容的Length距离的绝对值，升序排列
        /// </summary>
        /// <param name="list"> </param>
        /// <param name="length"> 要比较的长度 </param>
        public static List<T[]> FromCloseToFarAbs<T>(this List<T[]> list, int length)
        {
            int[] ints = list.Select(n => Math.Abs(n.Count() - length)).ToArray();
            List<(int, T[])> tuples = new List<(int, T[])>();
            for (int i = 0; i < ints.Length; i++)
            {
                tuples.Add((ints[i], list[i]));
            }
            for (int j = 0; j < list.Count; j++)
            {
                for (int k = 0; k < list.Count - 1; k++)
                {
                    if (tuples[k].Item1 > tuples[k + 1].Item1)
                    {
                        var temp = tuples[k + 1];
                        tuples[k + 1] = tuples[k];
                        tuples[k] = temp;
                    }
                }
            }
            return tuples.Select(n => n.Item2).ToList();
        }
    }
}
