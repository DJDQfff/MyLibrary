using System.Collections.Generic;

namespace MyLibrary.Standard20
{
    /// <summary>
    /// 对数字集合进行操作
    /// </summary>
    public static class NumberCollectionOperation
    {
        #region int 整形数字

        /// <summary>
        /// 返回指定值的全部索引
        /// </summary>
        /// <param name="list"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<int> SearchValueIndex(this IList<int> list, int n)
        {
            List<int> vs = new List<int>();
            for (int index = 0; index < list.Count; index++)
            {
                if (list[index] == n)
                {
                    vs.Add(index);
                }
            }
            return vs;
        }

        /// <summary>
        /// 如果集合中每个元素不相同，则返回false
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        public static bool IsSame(this IList<int> ints)
        {
            int index = 0;
            while (index < ints.Count - 1)
            {
                if (ints[index] != ints[++index])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion int 整形数字
    }
}