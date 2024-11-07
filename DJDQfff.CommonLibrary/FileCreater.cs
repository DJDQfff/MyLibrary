using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyLibrary.Creater
{
    public class FileCreater
    {
        /// <summary>
        /// 未做保证
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="filecount"></param>
        /// <param name="extension"></param>
        public static void CreateFiles (string folder, string extension, int filecount=5  )
        {

           while(filecount> 0)
            {
                string name=Path.Combine(folder,filecount-- + extension);
                File.Create(name).Dispose();
             
            }
        }
    }
}
