namespace CommonLibrary.CollectionFindRepeat;

public class StringArrayCollection
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="arrays"></param>
    /// <returns></returns>
    public static Dictionary<string , int> Run<T> (
        IEnumerable<T> arrays ,
        Func<T , IEnumerable<string>> func
    )
    {
        Dictionary<string , int> Numbers = [];

        foreach (var t in arrays)
        {
            foreach (var item in func(t))
            {
                if (Numbers.TryGetValue(item , out int value))
                {
                    Numbers[item] = ++value;
                }
                else
                {
                    Numbers.Add(item , 1);
                }
            }
        }
        return Numbers;
    }
}