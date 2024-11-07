using CommonLibrary;

namespace CommonLibrary.Algorithm.Sort
{
    /// <summary> 基础排序算法 </summary>
    public static class Basic
    {
        /// <summary> 冒泡排序 </summary>
        /// <param name="list"> </param>
        /// <returns> </returns>
        public static int BubbleSort(this IList<int> list)
        {
            int count = default;
            for (int i = 0; i < list.Count; i++) //i：一共i个元素，所以共count-1次
            {
                for (int j = 0; j < list.Count - 1; j++) //j：当前元素位置，每次都从0开始
                //因为设计的是向后冒泡，所以要从零开始
                {
                    if (list[j] > list[j + 1]) //当前位置与后一位比较
                    {
                        int temp = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = temp;

                        count++;
                    }
                }
                list.ShowList(behind: "", beforemessage: "调整后：");
            }
            return count;
        }

        /// <summary> 选择排序 </summary>
        /// <param name="list"> </param>
        /// <returns> </returns>
        public static int SelectionSort(this IList<int> list)
        {
            int count = default;

            for (int i = 0; i < list.Count; i++)
            {
                int index = i;
                for (int j = i; j < list.Count; j++)
                {
                    if (list[index] > list[j])
                    {
                        index = j;
                    }
                    count++;
                }
                int k = list[index];
                list[index] = list[i];
                list[i] = k;

                list.ShowList(behind: "", beforemessage: "调整后：");
            }
            return count;
        }
    }
}
