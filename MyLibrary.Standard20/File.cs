using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyLibrary.Standard20
{
    /// <summary>
    /// 文件操作封装
    /// </summary>
    public static class File
    {
        /// <summary>
        /// 确保存在文件，如果没有则生成一个
        /// </summary>
        /// <param name="path"></param>
        public static void EnsureFileExist(string path)
        {
             FileInfo fileInfo = new FileInfo(path);
            if(!fileInfo.Exists)
            {
                fileInfo.Create();
            }
        }
    }
}
