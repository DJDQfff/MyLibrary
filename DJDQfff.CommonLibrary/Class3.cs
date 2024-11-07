namespace ConsoleApp1;

internal class Class3
{
    internal static int minLength = 3;

    internal static void Run(List<string> File_ersheng)
    {
        var checkList = new List<CheckString>();
        var repeatList = new List<RepeatItem>();
        foreach (var file in File_ersheng)
        {
            var fdj = new CheckString(file);
            checkList.Add(fdj);
        }
        checkList = checkList.OrderBy(x => x.ParserContent.Length).ToList();
        //checkList.ForEach(x =>
        //{
        //    WriteLine(x.ParserContent);
        //    WriteLine(x.fullString);
        //    WriteLine();
        //});

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
                            var count = CountRepeat(checkstring, item);
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

    public static void TestGetCount()
    {
        var count = CountRepeat("ふたりのひみつ", "ひみつ");
        WriteLine(count);
    }

    //TODO
    public static int CountRepeat(string parserContent, string item, int start = 0)
    {
        var count = 0;
        for (int index = start; index + item.Length <= parserContent.Length; index++)
        {
            var comparor = parserContent[index..(index + item.Length)];
            if (item == comparor)
            {
                count++;
            }
        }
        return count;
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
