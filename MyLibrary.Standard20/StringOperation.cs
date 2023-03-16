using System.Text;

namespace MyLibrary.Standard20
{
    /// <summary> 对字符串的操作 </summary>
    public static class StringOperation
    {
        /// <summary> 返回一个字符串重复count遍后的结果 </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="str"> 要重复的内容 </param>
        /// <param name="count"> 重复次数 </param>
        /// <returns> </returns>
        public static string Repeat<T>(this T str, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(str.ToString());
            }

            return sb.ToString();
        }
    }
}