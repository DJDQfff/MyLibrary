using Weather.Core;
using Weather.Core.ResponseJson;

namespace ConsoleApp1;

internal class TestWeatherAPI
{
    internal static async Task RunTest()
    {
        Controller controller = new Controller();
        FreeDayJson json = await controller.GetFreeDay();
        foreach (System.Reflection.PropertyInfo property in json.GetType().GetProperties())
        {
            Console.WriteLine(property.GetValue(json).ToString());
        }
        System.Console.ReadKey();
    }
}
