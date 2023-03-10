using System;
using System.Collections.Generic;

namespace MyLibrary.Standard20
{
    /// <summary> 封装在控制台上的显示操作 </summary>
    public static class ConsoleShow
    {
        /// <summary> 显示错误信息 </summary>
        /// <param name="str"> </param>
        public static void ShowErrorLine (string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        /// <summary> </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="list"> </param>
        /// <param name="front"> </param>
        /// <param name="behind">
        /// 默认 \n 以换行 ，输入 "" 不换行
        /// </param>
        /// <param name="beforemessage"> </param>
        /// <param name="aftermessage"> </param>
        public static void ShowList<T> (this IList<T> list , string front = "\t" , string behind = "\n" , string beforemessage = "数组为:" , string aftermessage = "")
        {
            System.Console.WriteLine(beforemessage);
            foreach (var t in list)
            {
                System.Console.Write(front + t + behind);
            }
            System.Console.WriteLine(aftermessage);
        }

        /// <summary> 单个对象显示在控制台上 </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="t"> </param>
        public static void Show<T> (this T t)
        {
            System.Console.WriteLine(t);
        }

        /// <summary> 在控制台上显示异常的信息 </summary>
        /// <param name="exception"> </param>
        public static void Show (this Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }

        /// <summary> 在控制台上显示二维数组 </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="array"> 要操作的二维数组 </param>
        public static void Show<T> (this T[,] array)
        {
            for (int i = 0 ; i < array.GetLength(0) ; i++)
            {
                for (int j = 0 ; j < array.GetLength(1) ; j++)
                {
                    Console.Write(array[i , j]);
                }
                Console.Write('\n');
            }
        }
    }
}