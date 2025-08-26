namespace CommonLibrary
{
    /// <summary>
    /// 文件操作封装
    /// </summary>
    public static class FileWrap
    {
        /// <summary>
        /// 确保存在文件，如果没有则生成一个
        /// </summary>
        /// <param name="path"></param>
        public static void EnsureFileExist(string path)
        {
            FileInfo fileInfo = new(path);
            if (!fileInfo.Exists)
            {
                fileInfo.Create();
            }
        }
    }
}