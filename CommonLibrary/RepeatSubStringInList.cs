using static CommonLibrary.StringParser.BracketBasedStringParser;

namespace CommonLibrary;

public class RepeatSubStringInList
{
    public int minLength = 3;
    public List<string> StringsPool { get; } = new();

    public void Add(string str) => StringsPool.Add(str);

    public void Add(IEnumerable<string> strs) => StringsPool.AddRange(strs);

    public void Clear() => StringsPool.Clear();

    /// <summary>
    // 增字查找
    /// </summary>
    public void Run()
    {
        var checkList = new List<CheckString>();
        var repeatList = new List<RepeatItem>();
        foreach (var file in StringsPool)
        {
            var fdj = new CheckString(file);
            checkList.Add(fdj);
        }
        checkList = checkList.OrderBy(x => x.ParserContent.Length).ToList();

        var repeatitems = new List<string>();
        for (int index = 0; index < checkList.Count; index++)
        {
            var currentCheckItem = checkList[index];
            //var behindFdj = checkList.GetRange(index + 1, checkList.Count - index);
            var maxLength = currentCheckItem.ParserContent.Length;
            for (int start = 0; start < maxLength; start++)
            {
                for (int length = minLength; start + length <= maxLength; length++)
                {
                    var item = currentCheckItem.ParserContent.Substring(start, length);

                    if (repeatList.SingleOrDefault(x => x.Content == item) is null)
                    {
                        var repeatitem = new RepeatItem(item);
                        for (
                            var behindIndex = index + 1;
                            behindIndex < checkList.Count;
                            behindIndex++
                        )
                        {
                            var checkitem = checkList[behindIndex];
                            var checkstring = checkitem.ParserContent;
                            var count = StringSearch.CountRepeat(checkstring, item);
                            if (count > 0)
                            {
                                repeatitem.Count += count;
                                repeatitem.CheckStrings.Add(checkitem);
                            }
                        }
                        if (repeatitem.Count != 1)
                        {
                            repeatList.Add(repeatitem);
                            WriteLine(repeatitem.Content);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 减字查找
    /// </summary>
    public void Run2()
    {
        // TODO
    }
}

file class RepeatItem
{
    public string Content { set; get; }
    public int Count { set; get; } = 1;
    public List<CheckString> CheckStrings { get; } = new();

    public RepeatItem(string content)
    {
        Content = content;
    }
}

file class CheckString
{
    public string fullString;
    public string ParserContent { get; }

    public CheckString(string fullString)
    {
        this.fullString = fullString;
        ParserContent = string.Join(" ", Get_OutsideContent(fullString));
    }
}
