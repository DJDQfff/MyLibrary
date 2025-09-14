namespace CommonLibrary.CollectionFindRepeat;

/// <summary>
/// 对一个较长字符串的集合，查找重复出现的短字符串
/// </summary>
public class StringCollection<TSource, TAtom>
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
    public IEnumerable<TSource> Sources { get; set; } = [];

    public Func<TSource, IEnumerable<TAtom>> Action { set; get; }
    public List<RepeatItem<TAtom>> RepeatList { get; } = [];

    /// <summary>
    /// 增字查找
    /// </summary>
    public void Run()
    {
        var checkList = Sources
            .Select(x => new CheckTarget<TSource, TAtom>(x) { ParserString = Action(x) })
           .OrderBy(x => x.ParserString.Count())
           .ToList();

        var repeatitems = new List<string>();
        for (int index = 0; index < checkList.Count; index++)
        {
            var currentCheckItem = checkList[index];
            //var behindFdj = checkList.GetRange(index + 1, checkList.Count - index);
            var maxLength = currentCheckItem.ParserString.Count();
            for (int start = 0; start < maxLength; start++)
            {
                for (int length = MinItemLength; start + length <= maxLength; length++)
                {
                    var item = currentCheckItem.ParserString.Skip(start).Take(length);
                    CountBehind(checkList, RepeatList, index, item);
                }
            }
        }
    }

    /// <summary>
    /// 减字查找
    /// </summary>
    public void Run2()
    {
        var checkList = Sources
            .Select(x => new CheckTarget<TSource, TAtom>(x) { ParserString = Action(x) })
           .OrderBy(x => x.ParserString.Count())
           .ToList();

        for (int index = 0; index < checkList.Count; index++)
        {
            var currentCheckString = checkList[index];
            //var behindFdj = checkList.GetRange(index + 1, checkList.Count - index);
            var currentStringLength = currentCheckString.ParserString.Count();
            for (int start = 0; start < currentStringLength; start++)
            {
                for (
                    int length = currentStringLength;
                    MinItemLength <= length && start + length <= currentStringLength;
                    length--
                )
                {
                    var item = currentCheckString.ParserString.Skip(start).Take(length);

                    if (CountBehind(checkList, RepeatList, index, item))
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
    private bool CountBehind(
        List<CheckTarget<TSource, TAtom>> checkList,
        List<RepeatItem<TAtom>> repeatList,
        int index,
        IEnumerable<TAtom> item
    )
    {
        // TODO 可以再一个参数最低出现次数，返回值修改为bool（是否达到最低值）
        // TODO 还可以做一个版本，不统计所有次数，出现一定次数后停止然后返回bool
        if (repeatList.SingleOrDefault(x => object.Equals(x.Content, item)) is null)
        {
            var repeatitem = new RepeatItem<TAtom>(item);
            for (var behindIndex = index + 1; behindIndex < checkList.Count; behindIndex++)
            {
                var checktarget = checkList[behindIndex];
                var checkstring = checktarget.ParserString;

                var count = StringSearch.CountRepeat<TAtom>(checkstring.ToArray(), item.ToArray());
                if (count > 0)
                {
                    repeatitem.Count += count;
                    repeatitem.CheckStrings.Add(checkstring);
                }
            }
            if (repeatitem.Count >= MinOccurTimes)
            {
                repeatList.Add(repeatitem);
                //WriteLine(repeatitem.Content);
                return true;
            }
        }
        return false;
    }

    private void CheckResult()
    { }
}

public class RepeatItem<TAtom>(IEnumerable<TAtom> content)
{
    public IEnumerable<TAtom> Content { set; get; } = content;
    public int Count { set; get; } = 1;
    public List<IEnumerable<TAtom>> CheckStrings { get; } = [];
}

public class CheckTarget<T, TAtom>(T fullString)
{
    public T Source => fullString;
    public IEnumerable<TAtom> ParserString { get; set; }
}