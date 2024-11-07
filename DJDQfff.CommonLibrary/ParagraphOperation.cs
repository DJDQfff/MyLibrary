using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyLibrary.Standard20
{
    /// <summary> 对一个字符串集合，以空白行为分离点，中间内容视为一个个段落 </summary>
    public static class ParagraphOperation
    {
        /// <summary> 当前段落至下一个同缩进段落之前的段落 </summary>
        /// <param name="paragraphs"> 源段落集合 </param>
        /// <param name="offset">
        /// 开始的段落，至下一个缩进相同段落。如果下一个不存在，则至最后一个
        /// </param>
        /// <returns> </returns>
        public static List<T> UntilSameIndent<T> (this IList<T> paragraphs , int offset) where T : IParagraph
        {
            List<T> ts = new List<T>();
            var currentParagraph = paragraphs[offset];                 // 当前段落
            int minIndent = currentParagraph.Indent;               // 获取最大缩进

            ts.Add(paragraphs[offset++]);                             // 添加 起始段落，记得偏移段落

            while (offset < paragraphs.Count)                   // 依次将索引小的段落添加的集合中，直到遇见统计索引或全部遍历完
            {
                var para = paragraphs[offset];
                if (para.Indent > minIndent)                         // 索引大则添加段落
                {
                    ts.Add(para);
                }
                else                                                // 索引小或相等则停止遍历
                {
                    break;
                }

                offset++;               // 下一个段落
            }

            return ts;
        }

        /// <summary> 返回段落中符合正则的行的索引 </summary>
        /// <param name="paragraph"> </param>
        /// <param name="regex"> </param>
        /// <returns> </returns>
        public static List<int> RegexIndexList (this IParagraph paragraph , string regex)
        {
            List<int> vs = new List<int>();
            for (int index = 0 ; index < paragraph.Lines.Count ; index++)
            {
                string line = paragraph.Lines[index];
                Match match = Regex.Match(line , regex);
                if (match.Success)
                {
                    vs.Add(index);
                }
            }
            return vs;
        }

        /// <summary> 是否所有行都匹配正则 </summary>
        /// <param name="paragraph"> </param>
        /// <param name="regex"> </param>
        /// <returns> </returns>
        public static bool IsAllLinesMatchRegex (this IParagraph paragraph , string regex)
        {
            foreach (string line in paragraph.Lines)
            {
                if (!Regex.Match(line , regex).Success)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary> 判断最后一行是否符合正则 </summary>
        /// <param name="paragraph"> 要匹配的段落 </param>
        /// <param name="regex"> 要匹配的正则 </param>
        /// <returns> 匹配存在，则返回索引；不存在，则返回-1 </returns>
        public static int LastLineRegexMatchIndex (this IParagraph paragraph , string regex)
        {
            string line = paragraph.Lines.LastItem();
            var match = Regex.Match(line , regex);
            return match.Success ? match.Index : -1;
        }

        /// <summary> 统计段落内有多少行符合正则 </summary>
        /// <param name="paragraph"> </param>
        /// <param name="regex"> </param>
        /// <returns> </returns>
        public static int LinesMatchRegexAmount (this IParagraph paragraph , string regex)
        {
            int count = 0;
            foreach (var line in paragraph.Lines)
            {
                if (Regex.Match(line , regex).Success)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary> 段落是否存在任意行符合正则匹配 </summary>
        /// <param name="paragraph"> </param>
        /// <param name="regex"> </param>
        /// <returns> </returns>
        public static bool AnyLineMatchRegex (this IParagraph paragraph , string regex)
        {
            foreach (var line in paragraph.Lines)
            {
                if (Regex.Match(line , regex).Success)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary> 判断段落首行匹配正则的索引 </summary>
        /// <param name="paragraph"> 要查找的段落 </param>
        /// <param name="regex"> 要匹配的正则 </param>
        /// <returns> 匹配存在，则返回索引；不存在，则返回-1 </returns>
        public static int FirstLineRegexMatchIndex (this IParagraph paragraph , string regex)
        {
            var match = Regex.Match(paragraph.Lines[0] , regex);
            return match.Success ? match.Index : -1;
        }

        /// <summary> 拼接各个字符串集合，返回一个新的字符串列表 </summary>
        /// <param name="contents"> 要拼接的内容 </param>
        /// <returns> 新的List </returns>
        public static List<string> ConnectList (params IList<string>[] contents)
        {
            List<string> vs = new List<string>();
            foreach (var list in contents)
            {
                vs.AddRange(list);
            }
            return vs;
        }
    }

    /// <summary> Paragraph的工厂类 </summary>
    public static class ParagraphFactory
    {
        /// <summary>
        /// 按索引对字符串集合进行分离 这些索引会作为子段落的首行
        /// 如：一个10行的段落，按2.8.进行分割，将得到2个段落，2至7一个段落，8至9一个段落
        /// </summary>
        /// <param name="lines"> </param>
        /// <param name="ints"> 必须依次递增 </param>
        /// <returns> </returns>
        public static List<Paragraph> SplitParagraphByStartIndex (this IList<string> lines , params int[] ints)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
            List<int> vs = new List<int>(ints)
            {
                lines.Count
            };

            for (int index = 0 ; index < vs.Count - 1 ; index++)
            {
                List<string> vs1 = lines.SubList(vs[index] , vs[index + 1] - 1);
                Paragraph paragraph = new Paragraph(vs1);
                paragraphs.Add(paragraph);
            }
            return paragraphs;
        }

        /// <summary>
        /// 按索引对字符串集合进行分离 这些索引本身是会被丢弃的
        /// 如：一个10行的段落，按2,8进行分离，将得到3个段落，0至1一个段落，3至7一个段落，9一个段落
        /// </summary>
        /// <param name="lines"> </param>
        /// <param name="Indexes"> </param>
        /// <returns> </returns>
        public static List<Paragraph> SplitParagraphBySperateIndex (this IList<string> lines , params int[] Indexes)
        {
            List<string> newlines = new List<string>(lines);            // lines必须是可以添加项的集合
            newlines.Insert(0 , string.Empty);
            newlines.Add(string.Empty);

            List<Paragraph> paragraphs = new List<Paragraph>();

            for (int index = 0 ; index < Indexes.Length - 1 ; index++)
            {
                int emptystart = Indexes[index];             // 首空行
                int emptyend = Indexes[index + 1];           // 尾空行
                int lcount = emptyend - emptystart;

                if (lcount > 1)               // 中间存在内容
                {
                    var vs = newlines.SubList(emptystart + 1 , emptyend - 1);

                    Paragraph paragraph = new Paragraph(vs);

                    paragraphs.Add(paragraph);
                }
            }
            return paragraphs;
        }

        /// <summary> 将可枚举字符串视为分离行，切分为各个段落 </summary>
        /// <param name="lines"> 源字符串 </param>
        /// <returns> 段落 </returns>
        public static List<Paragraph> SplitParagraphByEmptyLines (this IList<string> lines)
        {
            var emptyIndex = lines.GetEmptyIndex().ToArray();

            var pa = lines.SplitParagraphBySperateIndex(emptyIndex);

            return pa;
        }
    }

    /// <summary> 段落 </summary>
    public class Paragraph : IParagraph
    {
        /// <summary> 段落缩进，目前无用 </summary>
        public int Indent { get; }

        /// <summary> 在源可枚举字符串中的起始索引位置，最小为0 </summary>
        public int StartLineIndex { get; }

        /// <summary> 段落结束行索引 </summary>
        public int EndLineIndex { get; }

        /// <summary> 内容 </summary>
        public IList<string> Lines { get; }

        /// <summary> 各行缩进集合 </summary>
        public List<int> Indents { get; } = new List<int>();

        /// <summary> 构造函数 </summary>
        /// <param name="_content"> 内容 </param>
        public Paragraph (IList<string> _content)
        {
            Lines = _content;

            foreach (var line in Lines)
            {
                Indents.Add(line.FirstNotWhiteSpaceCharacterIndex());
            }
        }
    }

    /// <summary> 段落接口 </summary>
    public interface IParagraph
    {
        /// <summary> 段落的每一行 </summary>
        IList<string> Lines { get; }

        /// <summary> 段落缩进 </summary>
        int Indent { get; }
    }
}