using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DJDQfff.MirlKoiAPI;

namespace ConsoleApp1
{
    internal static class TestMirlKoiAPI
    {
        static internal void RunTest ()
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
}
