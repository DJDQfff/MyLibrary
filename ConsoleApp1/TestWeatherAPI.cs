using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DJDQfff.Weather.Core.ResponseJson;
using DJDQfff.Weather.Core;

namespace ConsoleApp1
{
     internal class TestWeatherAPI
    {
      internal  static async Task RunTest ()
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
}
