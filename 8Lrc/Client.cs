using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

using HtmlAgilityPack;
namespace _8Lrc
{
    public class Client
    {
        private static string website = @"https://www.8lrc.com";
        private static string searchsite = website + "/search/?key=";

        private string[] extensions = { ".mp3", ".flac" };

        public void Run(string soucefolder, string outputfolder) 
        {

            var namelist = Directory.GetFiles(soucefolder)
                .Where(x => extensions.Contains(Path.GetExtension(x).ToLower()))
                .Select(x => Path.GetFileNameWithoutExtension(x))
                .ToList();

            foreach (var name in namelist)
            {
                string song = default;
                string singer = default;
                if (name.Contains('-'))
                {
                    var parts = name.Split('-').Select(x => x.Trim()).ToList();
                    song = parts[1];
                    singer = parts[0];
                }
                else
                {
                    song = name;
                }

                try
                {
                    Output(song, singer, outputfolder);

                }
                catch
                {

                }
            }

        }
        public  void Output(string song,string singer,string outputfolder)
        {

            if (song is null)
                return;
           string key=HttpUtility.HtmlEncode(song);
            string get=searchsite+key;

            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlWeb();
            var docu=htmlWeb.Load(get);

            var nodes = docu.DocumentNode.SelectNodes("//div[@class=\"cicont\"]");

            if(nodes is null)
            {
                return;
            }

            foreach (var child in nodes)
            {

                var gequnode = child.SelectSingleNode(".//a[@class=\"tGequ\"]");
                var title = gequnode.InnerText.Trim(Path.GetInvalidPathChars());

                if (singer != null)
                {
                    if (!title.Contains(singer))
                    {
                        continue;
                    }

                    if (!title.Contains(song))
                    {
                        continue;
                    }
                }



                var contentnode = child.SelectSingleNode(".//p");

                var content = contentnode.InnerText;

                var filepath = outputfolder+"/" + title + ".txt";

                    using (var file = File.CreateText(filepath))
                    {
                        file.Write(content);
                    }
            }


        }

    }
}
