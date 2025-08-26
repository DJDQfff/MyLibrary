namespace CommonLibrary;

/// <summary> 字符串比较 </summary>
public static class StringCompare
{
    /// <summary> 两个字符串进行比较，返回不第一个不相同字符的位置 </summary>
    /// <param name="str1"> </param>
    /// <param name="str2"> </param>
    /// <returns> </returns>
    public static int DifferentIndexOf(this string str1, string str2)
    {
        int length = Math.Max(str1.Length, str2.Length);
        for (int index = 0; index < length; index++)
        {
            if (str1[index] != str2[index])
            {
                return index;
            }
        }
        return -1;
    }

    /// <summary> 比较一个字符串集合和指定字符串值相等性 </summary>
    /// <param name="list"> </param>
    /// <param name="str"> </param>
    /// <returns> </returns>
    public static bool IsListEqualString(this IEnumerable<string> list, string str)
    {
        string newstring = list.ConnectString();
        return newstring == str;
    }

    /// <summary> 判断一个字符串是不是由重复字符构成 </summary>
    /// <param name="str"> 源字符串 </param>
    /// <param name="chars"> 是否是重复的指定元素 </param>
    /// <returns> 是否重复 </returns>
    public static bool IsRepeatCharLine(this string str, params char[] chars)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(str, nameof(str));

        char c = str[0];
        for (int i = 1; i < str.Length - 1; i++)
        {
            if (c != str[i])
                return false;
        }

        if (chars.Length == 0)
        {
            return true;
        }
        else
        {
            return chars.Contains(c);
        }
    }
}