using System.Threading;

namespace CommonLibrary
{
    /// <summary>
    /// 最简单的入门练习
    /// </summary>
    public static class SimpleAlgorithm
    {
        /// <summary>
        /// 提取整数各位上的数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static List<int> Tiqu (this int number)
        {
            List<int> list = [];
            for (int i = 0 ; number != 0 ; i++) // i：第多少位，0为个位
            {
                int a = number % 10;
                number /= 10;
                list.Add(a);
            }
            return list;
        }

        /// <summary>
        /// 奇偶个数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Jiougeshu (this IList<int> list)
        {
            int count = default;
            foreach (int a in list)
            {
                if (a % 2 == 0)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 分解质因数
        /// </summary>
        /// <param name="source"></param>
        //public static void FenJie (this int source) { }

        /// <summary>
        /// 逆序输出
        /// </summary>
        /// <param name="abc"></param>
        /// <returns></returns>
        public static int Nixu (this int abc)
        {
            int a = abc / 100;
            int bc = abc % 100;
            int b = bc / 10;
            int c = bc % 10;
            int cba = 100 * c + 10 * b + a;
            return cba;
        }

        /// <summary>
        /// 对一个Ienumerable循环不停
        /// </summary>
        private static void LoopArray ()
        {
            int[] ints = new int[] { 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 , };

            var index = 0;
            var temp = 0;
            while (true)
            {
                temp = temp % ints.Length;
                Console.WriteLine(ints[temp++] + "\tindex:" + index + "\ttemp:" + temp);
                Thread.Sleep(1000);
            }
        }
    }
}
