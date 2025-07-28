using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GroupedItemsLibrary
{
    /// <summary>
    /// 重复项组合ViewModel
    /// </summary>
    /// <typeparam name="TKey">重复项分组依据</typeparam>
    /// <typeparam name="TElement">重复项 Model</typeparam>
    /// <typeparam name="TRepeatGroup">重复项组合</typeparam>
    public class ItemsGroupsViewModel<TKey, TElement, TRepeatGroup>
        where TRepeatGroup : ItemsGroup<TKey , TElement>, new()
    {
        /// <summary>
        /// 重复项组合的集合
        /// </summary>
        public ObservableCollection<TRepeatGroup> RepeatPairs { set; get; } = [];

        /// <summary>
        /// 组合个数
        /// </summary>
        public int Count => RepeatPairs.Count;

        /// <summary>
        /// 获取所有元素项
        /// </summary>
        public IEnumerable<TElement> AllElements
        {
            get
            {
                var list = new List<TElement>();
                foreach (var item in RepeatPairs)
                {
                    var em = item.Collections.ToArray();
                    list.AddRange(em);
                }
                return list;
            }
        }
        public void StartCompareSequence (IList<TElement> elements , Func<TElement , TElement , TKey> compare)
        {
            RepeatPairs.Clear();
            while (elements.Count > 1)
            {
                var group = new TRepeatGroup();

                for (int index = elements.Count - 2 ; index >= 0 ; index--)
                {
                    var key = compare(elements[^1] , elements[index]);
                    if (key is not null)
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
                if (group.Collections.Count >= 2)
                {
                    RepeatPairs.Add(group);

                }
            }
        }

        public void StartGroup (IEnumerable<TElement> elements , Func<TElement , TKey> getkey ,
            Func<TRepeatGroup , bool> filt)
        {
            RepeatPairs.Clear();
            var array = elements.Select(x => getkey(x));

            var a = elements.GroupBy(getkey);
            foreach (var cc in a)
            {
                if (cc.Count() > 1)
                {
                    var item = new TRepeatGroup();
                    item.Initial(cc);
                    var can = filt.Invoke(item);
                    if (can)
                    {
                        RepeatPairs.Add(item);
                    }
                }
            }

        }


        /// <summary>
        /// 删除一个项，在集合中检测删除
        /// </summary>
        /// <param name="elment"></param>
        public void DeleteStorageFileInRootObservable (TElement elment)
        {
            for (int index = Count - 1 ; index >= 0 ; index--)
            {
                var group = RepeatPairs[index];

                var count = group.TryRemoveItem(elment);
                // 没有重复项后，会自动从集合中移除此集合
                if (count == 1)
                {
                    RepeatPairs.RemoveAt(index);
                }
            }
        }
    }
}
