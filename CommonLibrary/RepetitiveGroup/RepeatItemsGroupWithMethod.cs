using System.Threading;

namespace CommonLibrary.RepetitiveGroup;

public class RepeatItemsGroupWithMethod<TKey, TElement, TGroup> : GroupsViewModel<TKey, TElement, TGroup>
        where TGroup : Group<TKey, TElement>, new()
{
    public List<TElement> Source { set; get; }

    public event Action<TElement> AddToResult;

    public event Action<TGroup> AddGroup;

    protected async Task ParseAll_FindOut(IList<TElement> elements, Func<IEnumerable<TElement>, IEnumerable<TKey>> parse, Func<TElement, TKey, bool> elementgetkey, Func<TKey, bool> filtkey)
    {
        var keys = await Task.Run(() => parse(elements).Where(x => filtkey(x)));

        foreach (var key in keys)
        {
            var group = new TGroup();
            group.Key = key;
            await Task.Run(() =>
            {
                for (var index = elements.Count - 1; index >= 0; index--)
                {
                    if (elementgetkey(elements[index], key))
                    {
                        group.AddElement(elements[index]);
                        elements.RemoveAt(index);
                    }
                }
            });
            if (group.Collections.Count > 1)
            {
                RepeatPairs.Add(group);
                AddGroup?.Invoke(group);
            }
        }
    }

    protected async Task StartCompareSequence(IList<TElement> elements, Func<TElement, TElement, TKey> compare, Func<TKey, bool> filt, CancellationTokenSource cancellationToken)
    {
        while (elements.Count > 1)
        {
            if (cancellationToken.Token.IsCancellationRequested)
            {
                return;
            }

            var group = new TGroup();

            await Task.Run(() =>
             {
                 for (int index = elements.Count - 2; index >= 0; index--)
                 {
                     if (cancellationToken.Token.IsCancellationRequested)
                     {
                         return;
                     }
                     var key = compare(elements[^1], elements[index]);
                     if (filt(key))
                     {
                         if (group.Key is null)
                         {
                             group.Initial(key);
                             group.AddElement(elements[^1]);
                         }
                         if (key.Equals(group.Key))
                         {
                             group.AddElement(elements[index]);
                             elements.Remove(elements[index]);
                         }
                     }
                 }
                 elements.Remove(elements[^1]);
             }, cancellationToken.Token);

            if (group.Collections.Count >= 2)
            {
                RepeatPairs.Add(group);
                AddGroup?.Invoke(group);
                foreach (var manga in group.Collections)
                {
                    AddToResult?.Invoke(manga);
                }
            }
        }
    }

    protected async Task ByEachKey(IEnumerable<TElement> elements, Func<TElement, TKey> getkey,
        Func<TGroup, bool> filt)
    {
        var items = new List<TGroup>();
        //var array = elements.Select(x => getkey(x));
        IEnumerable<IGrouping<TKey, TElement>> a = null;

        await Task.Run(() =>
        {
            a = elements
        .GroupBy(getkey)
        .SkipWhile(x => x.Key is null);
        });
        foreach (var cc in a)
        {
            if (cc.Count() > 1)
            {
                var group = new TGroup();
                group.Initial(cc);
                var can = filt?.Invoke(group);
                if (can.Value)
                {
                    items.Add(group);
                    RepeatPairs.Add(group);
                    AddGroup?.Invoke(group);
                }
            }
        }

        foreach (var item in items)
        {
            //foreach (var manga in item.Collections)
            //{
            //    AddToResult?.Invoke(manga);
            //}
        }
    }
}