namespace CommonLibrary;

/// <summary>
/// 对ObservableCollection集合操作
/// </summary>
public static class ObservableCollectionOperation
{
    /// <summary>
    /// 添加一个集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="values"></param>
    public static void AddRange<T>(this ObservableCollection<T> target, IEnumerable<T> values)
    {
        foreach (var item in values)
        {
            target.Add(item);
        }
    }
}
