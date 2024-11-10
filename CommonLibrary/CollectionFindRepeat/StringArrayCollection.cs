namespace CommonLibrary.CollectionFindRepeat;

public class StringArrayCollection
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="arrays"></param>
    /// <returns></returns>
    public static Dictionary<string, int> Run<T>(
        IEnumerable<T> arrays,
        Func<T, IEnumerable<string>> func
    )
    {
        Dictionary<string, int> Numbers = new();

        foreach (var t in arrays)
        {
            foreach (var item in func(t))
            {
                if (Numbers.ContainsKey(item))
                {
                    Numbers[item]++;
                }
                else
                {
                    Numbers.Add(item, 1);
                }
            }
        }
        return Numbers;
    }
}
