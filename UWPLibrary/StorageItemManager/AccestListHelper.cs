using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;

using static Windows.Storage.AccessCache.StorageApplicationPermissions;

namespace UWPLibrary
{
    /// <summary>
    /// Windows系统可访问列表帮助类
    /// </summary>
    public static class AccestListHelper
    {
        public static async Task<(string, StorageFolder)> GetStorageFolder(string folderpath)
        {
            StorageFolder folder = null;
            string token = null;
            var entries = FutureAccessList.Entries;
            foreach (var entry in entries)
            {
                var temptoken = entry.Token;
                try
                {
                    StorageFolder storageFolder = await FutureAccessList.GetFolderAsync(temptoken);
                    if (storageFolder?.Path == folderpath)
                    {
                        folder = storageFolder;
                        token = temptoken;
                        break;
                    }
                }
                catch { }
            }

            return (token, folder);
        }

        public static async Task<StorageFile> GetStorageFile(string filepath)
        {
            string folderpath = Path.GetDirectoryName(filepath);
            string filename = Path.GetFileName(filepath);

            var folder = await GetStorageFolder(folderpath);

            if (folder.Item1 != null)
            {
                var item = await folder.Item2.TryGetItemAsync(filename);
                return item as StorageFile;
            }
            return null;
        }

        public static async Task RenameFile(string path, string newName)
        {
            var folder = await GetStorageFile(path);

            if (folder != null)
            {
                await folder.RenameAsync(newName);
            }
        }

        public static async Task RemoveFolder(string folderPath)
        {
            var folder = await GetStorageFolder(folderPath);
            if (folder.Item1 != null)
            {
                FutureAccessList.Remove(folder.Item1);
            }
        }

        public static async Task DeleteStorageFile(
            string path,
            StorageDeleteOption storageDeleteOption
        )
        {
            var storagefile = await GetStorageFile(path);
            if (storagefile != null)
            {
                await storagefile.DeleteAsync(storageDeleteOption);
            }
        }

        /// <summary>
        /// 查找FutureAccestList中依然存在的可用StorageFolder
        /// </summary>
        public static async Task<Dictionary<string, StorageFolder>> GetAvailableFutureFolder()
        {
            Dictionary<string, StorageFolder> result = new Dictionary<string, StorageFolder>();

            foreach (var item in FutureAccessList.Entries)
            {
                try
                {
                    StorageFolder storageFolder = await FutureAccessList.GetFolderAsync(item.Token);

                    if (storageFolder != null)
                    {
                        result.Add(item.Token, storageFolder);
                    }
                }
                catch (Exception)
                {
                    // 文件夹被移除，找不到，
                }
            }

            return result;
        }
    }
}
