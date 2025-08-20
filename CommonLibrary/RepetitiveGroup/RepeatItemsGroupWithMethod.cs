

namespace CommonLibrary.RepetitiveGroup;
public class RepeatItemsGroupWithMethod<TKey, TElement, TRepeatGroup> : GroupsViewModel<TKey , TElement , TRepeatGroup>
        where TRepeatGroup : Group<TKey , TElement>, new()
{

    public List<TElement> Source { set; get; }
    public event Action<TElement> AddToResult;

    protected async Task StartCompareSequence (IList<TElement> elements , Func<TElement , TElement , TKey> compare , Func<TKey , bool> filt)
    {
        RepeatPairs.Clear();
        while (elements.Count > 1)
        {
            var group = new TRepeatGroup();

            await Task.Run(() =>
             {
                 for (int index = elements.Count - 2 ; index >= 0 ; index--)
                 {
                     var key = compare(elements[^1] , elements[index]);
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

             });

            if (group.Collections.Count >= 2)
            {
                RepeatPairs.Add(group);
                foreach (var manga in group.Collections)
                {
                    AddToResult?.Invoke(manga);
                }

            }
        }
    }

    protected async Task ByEachKey (IEnumerable<TElement> elements , Func<TElement , TKey> getkey ,
        Func<TRepeatGroup , bool> filt)
    {
        RepeatPairs.Clear();
        var items = new List<TRepeatGroup>();
        //var array = elements.Select(x => getkey(x));
        IEnumerable<IGrouping<TKey , TElement>> a = null;

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
                var item = new TRepeatGroup();
                item.Initial(cc);
                var can = filt?.Invoke(item);
                if (can.Value)
                {
                    items.Add(item);
                    RepeatPairs.Add(item);

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
