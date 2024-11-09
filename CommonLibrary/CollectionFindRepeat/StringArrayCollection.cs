namespace CommonLibrary.CollectionFindRepeat;

public class StringArrayCollection
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="arrays"></param>
    /// <returns></returns>
    public static Dictionary<string, int> Run(IEnumerable<IEnumerable<string>> arrays)
    {
        Dictionary<string, int> Numbers = new();
        foreach (var array in arrays)
        {
            foreach (var item in array)
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
