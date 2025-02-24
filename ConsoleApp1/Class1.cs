global using CommonLibrary.CollectionFindRepeat;

global using ConsoleApp1;

using static CommonLibrary.StringParser.BracketBasedStringParser;

namespace ConsoleApp1;

internal class Class1
{
    static string path = @"D:\本子\本子二审";

    internal static void Run2()
    {
        var list = Directory
            .GetFiles(path)
            .Select(x => Path.GetFileNameWithoutExtension(x))
            .Select(x => Get_OutsideContent(x))
            .SelectMany(x => x)
            .Select(x => x.Split(' ', '-', '+'));
        var result = StringArrayCollection.Run(list, x => x).OrderBy(x => x.Value);
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Value}\t{item.Key}");
        }
    }

    internal static void Run1()
    {
        var list = Directory.GetFiles(path).Select(x => Path.GetFileNameWithoutExtension(x));
        var counter = new StringCollection<string>();
        counter.StringsList.AddRange(list);
        counter.Action = (x) =>
        {
            return string.Join(" ", Get_OutsideContent(x));
        };
        counter.Run2();
    }
}
