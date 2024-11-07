using System.Text.RegularExpressions;

namespace DJDQfff.CommonLibrary;

/// <summary>
/// 对字符串集合进行操作
/// </summary>
public static class StringCollectionOperation
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static bool ContainRepeat(this string[] items)
    {
        for (int index = 0; index < items.Count() - 1; index++)
        {
            for (int j = index + 1; j < items.Count(); j++)
            {
                if (items[index] == items[j])
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 字符串集合中是否存在一项包含指定字符串
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    public static bool AnyLineContains(this IEnumerable<string> str, string sub)
    {
        if (string.IsNullOrEmpty(sub))
        {
            throw new ArgumentException();
        }

        foreach (var s in str)
        {
            if (s.Contains(sub))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 对每行搜索正则匹配索引，若不存在，则索引为-1
    /// </summary>
    /// <param name="list"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static List<int> EachLineRegexIndex(this IList<string> list, string regex)
    {
        List<int> vs = new List<int>();
        foreach (var l in list)
        {
            var match = Regex.Match(l, regex);
            int index = match.Success ? match.Index : -1;
            vs.Add(index);
        }
        return vs;
    }

    /// <summary>
    /// 从字符串集合中移除null、空字符串、全空白符的字符串
    /// </summary>
    /// <param name="list"></param>
    public static void RemoveEmptyLine(this IList<string> list)
    {
        for (int index = list.Count - 1; index >= 0; index--)
        {
            if (string.IsNullOrWhiteSpace(list[index]))
            {
                list.RemoveAt(index);
            }
        }
    }

    /// <summary>
    /// 返回字符串中为null或empty的项的索引
    /// </summary>
    /// <param name="lines"></param>
    /// <returns></returns>
    public static List<int> GetEmptyIndex(this IList<string> lines)
    {
        List<string> newlines = new List<string>(lines); // lines必须是可以添加项的集合
        newlines.Insert(0, string.Empty);
        newlines.Add(string.Empty);

        List<int> emptyIndex = new List<int>();

        for (int index = 0; index < newlines.Count; index++)
        {
            string line = newlines[index];

            if (string.IsNullOrEmpty(line))
            {
                emptyIndex.Add(index);
            }
        }

        return emptyIndex;
    }

    /// <summary>
    /// 从字符串集合中移除符合正则条件的项
    /// </summary>
    /// <param name="list"></param>
    /// <param name="regex"></param>
    public static void RemoveItemMatchRegex(this IList<string> list, string regex)
    {
        for (int index = list.Count - 1; index >= 0; index--)
        {
            if (Regex.Match(list[index], regex).Success)
            {
                list.RemoveAt(index);
            }
        }
    }
}
