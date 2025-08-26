using System.Text;

namespace CommonLibrary;

/// <summary>
/// 对泛型集合进行操作
/// </summary>
public static class GenericCollectionOperation
{
    /// <summary>
    /// 返回集合的最后一个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ts"></param>
    /// <returns></returns>
    public static T LastItem<T>(this IList<T> ts)
    {
        int count = ts.Count;

        return ts[count - 1];
    }

    /// <summary>
    /// 拼接字符串，返回一个新字符串
    /// </summary>
    /// <param name="vs"></param>
    /// <returns></returns>
    public static string ConnectString(this IEnumerable<string> vs)
    {
        StringBuilder stringBuilder = new();
        foreach (var str in vs)
        {
            stringBuilder.Append(str);
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// 把一个集合的部分元素加到另一个集合
    /// </summary>
    /// <typeparam name="T">任意类型</typeparam>
    /// <param name="sourcelist">源集合</param>
    /// <param name="targetList">接收元素的集合</param>
    /// <param name="startIndex">发送元素的起始索引</param>
    /// <param name="endIndex">发送元素的结束索引</param>
    public static void SelectRange<T>(
        this IList<T> sourcelist,
        IList<T> targetList,
        int startIndex,
        int endIndex
    )
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            targetList.Add(sourcelist[i]);
        }
    }

    /// <summary>
    /// 返回一个集合的子集合
    /// 起始索引必须小于或等于结束索引，若相等，则返回一个元素
    /// </summary>
    /// <typeparam name="T">任意类型</typeparam>
    /// <param name="sourceList">源集合</param>
    /// <param name="startIndex">起始索引</param>
    /// <param name="endIndex">结束索引</param>
    /// <returns></returns>
    public static List<T> SubList<T>(this IList<T> sourceList, int startIndex, int endIndex)
    {
        if (startIndex > endIndex)
        {
            throw new InvalidOperationException("起始索引必须不大于结束索引！");
        }
        List<T> ts = [];
        for (int i = startIndex; i <= endIndex; i++)
        {
            ts.Add(sourceList[i]);
        }
        return ts;
    }
}