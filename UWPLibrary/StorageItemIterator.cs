﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;

namespace UWPLibrary
{
    /// <summary>
    /// 存储项遍历工具类
    /// </summary>
    public static class StorageItemIterator
    {
        /// <summary>
        /// 只遍历子文件夹，不遍历子文件
        /// </summary>
        /// <param name="rootFolder">要遍历的根路径</param>
        /// <returns>子StorageFolder集合</returns>
        public static async Task<List<StorageFolder>> GetAllStorageFolder(
            this StorageFolder rootFolder
        )
        {
            List<StorageFolder> targetFolders = new List<StorageFolder>();

            await Loop(rootFolder);

            return targetFolders;

            async Task Loop(StorageFolder storageFolder)
            {
                var childFolders = await storageFolder.GetFoldersAsync();

                targetFolders.AddRange(childFolders);

                foreach (var folder in childFolders)
                {
                    await Loop(folder);
                }
            }
        }

        /// <summary>
        /// 遍历这个文件夹下所有文件及子文件，有点慢
        /// </summary>
        /// <param name="rootFolder">根文件夹</param>
        /// <returns></returns>
        [Obsolete("请选择具体要遍历的类型，不要用这个，这个把所有的都遍历了，性能不好。")]
        public static async Task<(List<StorageFolder>, List<StorageFile>)> GetAllStorageItems(
            this StorageFolder rootFolder
        )
        {
            List<StorageFolder> targetFolders = new List<StorageFolder>();
            List<StorageFile> targetFiles = new List<StorageFile>();

            await Loop(rootFolder);

            return (targetFolders, targetFiles);

            async Task Loop(StorageFolder storageFolder)
            {
                var childFolders = await storageFolder.GetFoldersAsync();

                var childFiles = await storageFolder.GetFilesAsync();

                targetFolders.AddRange(childFolders);
                targetFiles.AddRange(childFiles);

                foreach (var folder in childFolders)
                {
                    await Loop(folder);
                }
            }
        }
    }
}
