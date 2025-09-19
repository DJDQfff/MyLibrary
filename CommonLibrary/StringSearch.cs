using System.Linq;
using System.Text.RegularExpressions;

namespace CommonLibrary;

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
        ArgumentException.ThrowIfNullOrWhiteSpace(str, nameof(str));
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
        Dictionary<string, List<int>> keyValuePairs = [];

        foreach (var word in words)
        {
            List<int> vs = [];
            int index;
            while ((index = str.IndexOf(word)) != -1)
            {
                vs.Add(index);
            }
            keyValuePairs.Add(word, vs);
        }

        return keyValuePairs;
    }

    /// <summary>
    /// 一个短数组在长数组中重复出现的次数
    /// </summary>
    /// <typeparam name="Item"></typeparam>
    /// <param name="longArray"></param>
    /// <param name="shortArray"></param>
    /// <param name="start"></param>
    /// <returns></returns>
    public static int CountRepeat<Item>(Item[] longArray, Item[] shortArray)
    {
        var count = 0;
        for (int index = 0; index + shortArray.Length <= longArray.Length; index++)
        {
            var comparor = longArray[index..(index + shortArray.Length)];
            if (shortArray.SequenceEqual(comparor))
            {
                // TODO 这里比较相同数组，还可以升级下，比较类似数组，比如相差一两个字符的那种，用下面的
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// 按顺序统计两个数组差异的量
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="array1"></param>
    /// <param name="array2"></param>
    /// <returns></returns>
    public static int SequenceDifferenceDelta<T>(T[] array1, T[] array2, int maxdiff = 3)
    {
        // TODO

        int diff = 0;
        T[] longarray;
        T[] shortarray;
        if (array1.Length > array2.Length)
        {
            longarray = array1;
            shortarray = array2;
        }
        else
        {
            longarray = array2;
            shortarray = array1;
        }

        for (int index = 0; index < longarray.Length; index++)
        {
            if (Equals(array1[index], array2[index]))
            {
                continue;
            }
            else
            {
                for (int i = 0; i < maxdiff && index + i < longarray.Length; i++)
                {
                    var sameposition = array1[index..(index + maxdiff)];
                }
            }
        }
        return diff;
    }
}