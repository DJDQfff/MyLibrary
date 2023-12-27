// See https://aka.ms/new-console-template for more information
using DJDQfff.BaiduTranslateAPI;

var tranlator = new SimpleTranslator("20210219000701366" , "VkerV4o1qG1TK6mUlbr_");
while (true)
{
    var show = Console.ReadLine();

    if (show != null)
    {
        var b = show.Split(' ');
        var a = await tranlator.CommonTextTranslateAsync(b);
        foreach (var c in a)
        {
            Console.WriteLine(c.dst);
        }
    }
}