using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;

namespace MyLibrary.UWP
{
    /// <summary>
    /// 应用程序数据文件夹帮助类
    /// </summary>
    public static class StorageFolderHelper
    {
        /// <summary> 在app临时文件夹获取子文件夹 </summary>
        /// <param name="foldername"> 子文件夹的名称 </param>
        /// <returns> </returns>
        public static async Task<StorageFolder> GetChildTemporaryFolder (string foldername)
        {
            StorageFolder LocalCacheFolder = ApplicationData.Current.TemporaryFolder;

            StorageFolder CoversFolder = await LocalCacheFolder.GetFolderAsync(foldername);

            return CoversFolder;
        }

        /// <summary>
        /// 确保在app临时文件夹存在文件夹，如果不存在，则创建文件夹
        /// </summary>
        /// <param name="foldernames"> 子文件夹名称 </param>
        /// <returns> </returns>
        public static async Task EnsureChildTemporaryFolders (params string[] foldernames)
        {
            StorageFolder LocalCacheFolder = ApplicationData.Current.TemporaryFolder;

            foreach (var name in foldernames)
            {
                _ = await LocalCacheFolder.CreateFolderAsync(name , CreationCollisionOption.OpenIfExists);
            }
        }

        /// <summary>
        /// 查询指定StorageItems集合中是否存在相同路径的StorageItem
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="check"> </param>
        /// <returns> </returns>
        public static bool Contain (this IEnumerable<IStorageItem> Items , StorageFolder check)
        {
            foreach (var item in Items)
            {
                string path = item.Path;
                string target = check.Path;
                if (path == target)
                {
                    return true;
                }
            }
            return false;
        }
    }
}