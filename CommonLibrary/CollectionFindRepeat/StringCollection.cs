namespace CommonLibrary.CollectionFindRepeat;

/// <summary>
/// 对一个较长字符串的集合，查找重复出现的短字符串
/// </summary>
public class StringCollection<T>
{
    /// <summary>
    /// 短字符串最短长度
    /// </summary>
    public int MinItemLength { set; get; } = 3;

    /// <summary>
    /// 最少出现次数
    /// </summary>
    public int MinOccurTimes { set; get; } = 2;

    /// <summary>
    /// 字符串集合
    /// </summary>
    public List<T> StringsList { get; } = [];

    public Func<T , string> Action { set; get; }

    /// <summary>
    /// 增字查找
    /// </summary>
    public void Run ()
    {
        var checkList = new List<CheckTarget<T>>();
        var repeatList = new List<RepeatItem>();
        var a = StringsList.Select(x => new CheckTarget<T>(x)).ToList();
        a.ForEach(x => x.SetParserContent(Action));
        a = a.OrderBy(x => x.ParserString.Length).ToList();
        checkList.AddRange(a);
        var repeatitems = new List<string>();
        for (int index = 0 ; index < checkList.Count ; index++)
        {
            var currentCheckItem = checkList[index];
            //var behindFdj = checkList.GetRange(index + 1, checkList.Count - index);
            var maxLength = currentCheckItem.ParserString.Length;
            for (int start = 0 ; start < maxLength ; start++)
            {
                for (int length = MinItemLength ; start + length <= maxLength ; length++)
                {
                    var item = currentCheckItem.ParserString.Substring(start , length);
                    CountBehind(checkList , repeatList , index , item);
                }
            }
        }
    }

    /// <summary>
    /// 减字查找
    /// </summary>
    public void Run2 ()
    {
        var checkList = new List<CheckTarget<T>>();
        var repeatList = new List<RepeatItem>();
        var a = StringsList.Select(x => new CheckTarget<T>(x)).ToList();
        a.ForEach(x => x.SetParserContent(Action));
        a = a.OrderBy(x => x.ParserString.Length).ToList();
        checkList.AddRange(a);

        var repeatitems = new List<string>();
        for (int index = 0 ; index < checkList.Count ; index++)
        {
            var currentCheckString = checkList[index];
            //var behindFdj = checkList.GetRange(index + 1, checkList.Count - index);
            var currentStringLength = currentCheckString.ParserString.Length;
            for (int start = 0 ; start < currentStringLength ; start++)
            {
                for (
                    int length = currentStringLength ;
                    MinItemLength <= length && start + length <= currentStringLength ;
                    length--
                )
                {
                    var item = currentCheckString.ParserString.Substring(start , length);

                    if (CountBehind(checkList , repeatList , index , item))
                    {
                        goto JDKJFK;
                    }
                }
            JDKJFK:
                break;
            }
        }
    }

    /// <summary>
    /// 检索后面的string，统计item所有出现次数
    /// </summary>
    /// <param name="checkList"></param>
    /// <param name="repeatList"></param>
    /// <param name="index"></param>
    /// <param name="item"></param>
    private bool CountBehind (
        List<CheckTarget<T>> checkList ,
        List<RepeatItem> repeatList ,
        int index ,
        string item
    )
    {
        // TODO 可以再一个参数最低出现次数，返回值修改为bool（是否达到最低值）
        // TODO 还可以做一个版本，不统计所有次数，出现一定次数后停止然后返回bool
        if (repeatList.SingleOrDefault(x => x.Content == item) is null)
        {
            var repeatitem = new RepeatItem(item);
            for (var behindIndex = index + 1 ; behindIndex < checkList.Count ; behindIndex++)
            {
                var checktarget = checkList[behindIndex];
                var checkstring = checktarget.ParserString;

                var count = StringSearch.CountRepeat(checkstring , item);
                if (count > 0)
                {
                    repeatitem.Count += count;
                    repeatitem.CheckStrings.Add(checkstring);
                }
            }
            if (repeatitem.Count >= MinOccurTimes)
            {
                repeatList.Add(repeatitem);
                WriteLine(repeatitem.Content);
                return true;
            }
        }
        return false;
    }

    private void CheckResult () { }
}

internal class RepeatItem (string content)
{
    public string Content { set; get; } = content;
    public int Count { set; get; } = 1;
    public List<string> CheckStrings { get; } = [];
}

public class CheckTarget<T> (T fullString)
{
    public T Source => fullString;
    public string ParserString { get; private set; }

    public void SetParserContent (Func<T , string> action)
    {
        ParserString = action(Source);
    }
}
