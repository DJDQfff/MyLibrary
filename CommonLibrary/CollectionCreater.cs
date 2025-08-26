using System.Text;

namespace CommonLibrary;

/// <summary> 随机数据创建机 </summary>
public static class CollectionCreater
{
    public static List<string> GetSequenceNumberList(
        int startnumber,
        int endnumber,
        int repeatcount
    )
    {
        List<string> strings = [];
        while (startnumber <= endnumber)
        {
            string str = startnumber++.ToString().Repeat(repeatcount);
            strings.Add(str);
        }
        return strings;
    }

    /// <summary> 创建随机ASCII可见字符串 </summary>
    /// <param name="minLength"> 字符串最小长度 </param>
    /// <param name="maxLength"> 字符串最大长度 </param>
    /// <param name="count"> 字符串个数 </param>
    /// <returns> </returns>
    public static List<string> GetAsciiStringList(
        int minLength = 5,
        int maxLength = 20,
        int count = 10
    )
    {
        Random random = new();

        List<string> list = [];
        for (int j = 0; j < count; j++)
        {
            StringBuilder stringBuilder = new();

            int length = random.Next(minLength, maxLength);

            while (length-- > 0)
            {
                int r = random.Next(33, 126);
                char c = (char)r;
                stringBuilder.Append(c);
            }

            list.Add(stringBuilder.ToString());
        }

        return list;
    }

    /// <summary> 获得一组随机整数 TODO 逻辑有点 乱，以后修改 </summary>
    /// <param name="mincount"> </param>
    /// <param name="maxcount"> </param>
    /// <param name="range"> </param>
    /// <returns> </returns>
    public static List<int> GetIntList(int mincount = 5, int maxcount = 20, int range = 1000)
    {
        Random random = new();
        int r = random.Next(mincount, maxcount);
        List<int> list = [];
        for (int i = 0; i < r; i++)
        {
            Random random1 = new();
            int r1 = random1.Next(range);
            list.Add(r1);
        }
        return list;
    }
}