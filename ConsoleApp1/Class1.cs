global using CommonLibrary;

global using ConsoleApp1;

namespace ConsoleApp1;

internal class Class1
{
    internal static void Run()
    {
        var path = @"D:\本子\本子二审";
        var list = Directory
            .GetFiles(path)
            .Select(x => Path.GetFileNameWithoutExtension(x))
            .ToList();
        var counter = new RepeatSubStringInList();
        counter.Add(list);
        counter.Run();
    }
}
