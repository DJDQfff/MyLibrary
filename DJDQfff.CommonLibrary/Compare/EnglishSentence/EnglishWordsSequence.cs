using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using MyLibrary.Algorithm.Sort;

namespace MyLibrary.Algorithm.Compare.EnglishSentence
{
    /// <summary> 按单词出现的顺序进行比较，需要调参，没弄，麻烦 </summary>
    public static class EnglishWordsSequence
    {
        /// <summary>
        /// 从一堆字符串中找出最符合要求的一个，如果找不到合适的，则返回null
        /// </summary>
        /// <param name="source"> 规则字符串，以他为标准 </param>
        /// <param name="stringpool">
        /// 字符串池子,每一项都必须是用空格分开的单词
        /// </param>
        /// <param name="sentencesense"> 句子灵敏度 </param>
        /// <param name="wordsense"> 单词灵敏度 </param>
        /// <returns> </returns>
        public static string SelectByWordsSequence (this string source, IEnumerable<string> stringpool, CompareSense sentencesense, CompareSense wordsense)
        {
            var sentencePool = stringpool.SplitIntoWords();
            var sourceSentence = source.Split(' ');

            Func<string[], bool> func = (n) => Math.Abs(sourceSentence.Length - n.Length) <= sentencesense.LengthDifference;

            var filteredSentences = sentencePool.Where(n => func(n)).ToList();
            var SortedSentences = filteredSentences.FromCloseToFarAbs(sourceSentence.Length);              // 按目标长度排序

            foreach (var Sentence in SortedSentences)
            {
                if (SentenceSimilar(sourceSentence, Sentence, sentencesense, wordsense))
                {
                    return string.Join(" ", Sentence);
                }
            }

            return null;
        }

        /// <summary> 句子灵敏度，将字符串视为一句话，按空格分开，比较相似度 </summary>
        /// <param name="vs1"> 要进行比较的字符串 </param>
        /// <param name="vs2"> 被比较的字符串 </param>
        /// <param name="sentencesense"> 句子灵敏度 </param>
        /// <param name="wordsense"> 单词灵敏度 </param>
        /// <returns> </returns>
        public static bool SentenceSimilar (IList<string> vs1, IList<string> vs2, CompareSense sentencesense, CompareSense wordsense)
        {
            Func<string, string, bool> wordCompare = (a, b) => WordSimilar(a, b, wordsense);
            Func<string, string, bool> test = (a, b) => a == b;
            bool c = CompareEachItem(vs1, vs2, sentencesense, test);
            return c;
        }

        /// <summary> 判断一个单词是否相等的方法 </summary>
        /// <param name="word1"> </param>
        /// <param name="word2"> </param>
        /// <param name="compareSense"> 单词灵敏度 </param>
        /// <returns> </returns>
        public static bool WordSimilar (string word1, string word2, CompareSense compareSense)
        {
            Func<char, char, bool> func = (a, b) => a == b;

            bool c = CompareEachItem(word1.ToArray(), word2.ToArray(), compareSense, func);

            return c;
        }

        /// <summary>
        /// 调参麻烦，参数设计是个大难题
        /// 顺序递进比较两个集合，累计其不同项的个数，到一定阈值后返回false
        /// 如asdf，asdsdfg作为参数1、2，以字符相等为委托，则在
        /// ts1中的元素必须按相应顺序在ts2中全部出现（一个都不能漏）
        /// ts2中可以出现ts1以外的元素，限定阈值范围内个数
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="ts1"> 比较的目标，以他为排序准则进行比较 </param>
        /// <param name="ts2">
        /// 要检查的集合，统计它上面不符合目标元素顺序的个数
        /// </param>
        /// <param name="sense"> 元素灵敏度比较 </param>
        /// <param name="compareFunc"> 元素相等性比较的方法 </param>
        /// <returns> </returns>
        public static bool CompareEachItem<T> (IList<T> ts1, IList<T> ts2, CompareSense sense, Func<T, T, bool> compareFunc)
        {
            int item1ignore = sense.IgnoreThreshold;
            int sense2 = sense.SkipThreshold;

            for (int j = 0, k = 0; (j < ts1.Count) && (k < ts2.Count); j++, k++)   // j是ts1的位置
            {                                                                      // k是ts2的位置
                int delay = sense.Delay;                                     // 忽略1还是2的临界值
                int tempk = k;                                                   // k的临时位置
                while (sense2 > 0 && tempk < ts2.Count)              // 循环条件：k值没到顶，且灵敏度未到阈值
                {
                    if (delay >= 0)
                    {
                        if (compareFunc(ts1[j], ts2[tempk]))
                        {
                            k = tempk;
                            break;
                        }
                        else
                        {
                            tempk++;
                            delay--;
                        }
                    }
                    else
                    {
                        item1ignore--;
                        break;
                    }
                }

                if (sense2 < 0 || (item1ignore < 0))
                {
                    return false;                       // 灵敏度耗尽
                }
            }
            return true;
        }
    }

    /// <summary> 比较算法灵敏度参数类 </summary>
    public class CompareSense
    {
        /// <summary> 元素1允许被忽略个个数阈值， </summary>
        public int IgnoreThreshold { set; get; }

        /// <summary>
        /// 在元素2集合中查找特定元素1，连续这么多个都找不到，视为该特定元素1无效，跳过这个元素1，IgnoreThreshold阈值减少
        /// 若在连续跳了几个元素2后，找到了该特定元素1，且跳过的个数小于忽略延迟，则元素2的Skip减少
        /// </summary>
        public int Delay { set; get; }

        /// <summary> Item2元素可以被跳过的个数阈值 </summary>
        public int SkipThreshold { set; get; }

        /// <summary> 允许两个比较的长度相差范围 </summary>
        public int LengthDifference { set; get; }

        #region 类静态方法

        /// <summary> 按距离和百分比自动创建灵敏度 </summary>
        /// <param name="ts"> </param>
        /// <param name="percent"> </param>
        /// <returns> </returns>
        public static CompareSense ByLengthAuto (string ts, int percent)
        {
            int length = ts.Split(' ').Length;
            double f = percent / 100.0 * length;
            var c = Math.Ceiling(f);
            int d = Convert.ToInt32(c);
            return new CompareSense()
            {
                Delay = d,
                IgnoreThreshold = d,
                SkipThreshold = d,
                LengthDifference = d
            };
        }

        /// <summary> 创建灵敏度 </summary>
        /// <param name="t1"> 元素1忽略上线 </param>
        /// <param name="t2"> Delay </param>
        /// <param name="t3"> 元素2跳过上限 </param>
        /// <param name="t4"> 长度最大差值 </param>
        /// <returns> </returns>
        public static CompareSense Creat (int t1, int t2, int t3, int t4)
        {
            return new CompareSense()
            {
                Delay = t2,
                IgnoreThreshold = t1,
                SkipThreshold = t3,
                LengthDifference = t4
            };
        }

        #endregion 类静态方法
    }
}