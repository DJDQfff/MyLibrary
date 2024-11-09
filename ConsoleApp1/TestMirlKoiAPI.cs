using MirlKoiAPI;

namespace ConsoleApp1;

internal static class TestMirlKoiAPI
{
    internal static void RunTest()
    {
        Console.WriteLine("Hello, World!");
        Core core = new Core();
        var image = core.GetImage(Configuration.SortType.random);
        FileStream fileStream = new FileStream("test.jpg", FileMode.OpenOrCreate, FileAccess.Write);

        image.CopyTo(fileStream);
        image.Dispose();
        fileStream.Dispose();
    }
}
