using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using MyLibrary.Algorithm.Compare.EnglishSentence;

using static System.Console;

namespace MyLibrary.Algorithm.Compare.EnglishSentence
{
    /// <summary> 按句子的出现百分比进行选择 </summary>
    public static class EnglishWordsOccurrencePercent
    {
        /// <summary>
        /// 从众多字符串中，选出目标句子单词在其中出现的百分比进行选择
        /// </summary>
        /// <param name="source"> 目标英语句子 </param>
        /// <param name="stringpool"> 英语句子池 </param>
        /// <param name="lengthdiff"> 句子长度最大差距 </param>
        /// <param name="least">
        /// 必须满足的概率，低于此概率，返回null
        /// </param>
        /// <returns> </returns>
        public static string SelectByWordsFrequency (this string source, IEnumerable<string> stringpool, int lengthdiff, double least)
        {
            var sentencePool = stringpool.SplitIntoWords();
            var sourceSentence = source.Split(' ');

            Func<string[], bool> func = (n) => Math.Abs(sourceSentence.Length - n.Length) <= lengthdiff;

            var filteredSentences = sentencePool.Where(n => func(n)).ToList();

            if (filteredSentences.Count == 0)// 有可能筛选出来的个数为0
            {
                lengthdiff += 100;
                filteredSentences = sentencePool.Where(n => func(n)).ToList();
            }

            List<int> vs = new List<int>();                       // 词频集合
            foreach (var sentence in filteredSentences)                 // 每个Key
            {
                int count = 0;

                List<string> sourceList = new List<string>(sourceSentence);
                List<string> sentenceList = new List<string>(sentence);

                int j = sourceList.Count() - 1;

                while (j >= 0)
                {
                    int k = sentenceList.Count() - 1;
                    while (j >= 0 && k >= 0)
                    {
                        if ((sourceList[j] == sentenceList[k]))
                        {
                            count++;

                            sourceList.RemoveAt(j);
                            sentenceList.RemoveAt(k);

                            j--;
                            k--;
                        }
                        else
                        {
                            k--;
                        }
                    }
                    if (k < 0)
                    {
                        j--;
                    }
                }

                vs.Add(count);
            }
            if (vs.Count == 0)                                              // 一个符合的都没有，返回null
            {
                return null;
            }
            int p = 0;
            if (vs.Count > 1)
            {
                for (int j = 1; j < vs.Count; j++)                           // 选出频率最高的
                {
                    if (vs[p] < vs[j])
                    {
                        p = j;
                    }
                }
            }
            else
            {
                p = 0;
            }

            int bigcount = vs[p];
            double percent = 1.0 * bigcount / sourceSentence.Length;
            string hithest = string.Join(" ", filteredSentences[p]);

            //WriteLine(percent);
            //WriteLine(bigcount + "/" + sourceSentence.Length);
            //WriteLine(hithest);
            //WriteLine(source);

            if (percent >= least)                               // 必须大于最小值
            {
                return hithest;
            }
            else
            {
                return null;
            }
        }
    }
}