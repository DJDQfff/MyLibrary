using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;

using static Windows.Storage.AccessCache.StorageApplicationPermissions;

namespace MyLibrary.UWP
{
    /// <summary>
    /// Windows系统可访问列表帮助类
    /// </summary>
    public static class AccestListHelper
    {
        /// <summary>
        /// 查找FutureAccestList中依然存在的可用StorageFolder
        /// </summary>
        public static async Task<List<StorageFolder>> GetAvailableFutureFolder ()
        {
            List<StorageFolder> result = new List<StorageFolder>();

            foreach (var item in FutureAccessList.Entries)
            {
                try
                {
                    StorageFolder storageFolder = await FutureAccessList.GetFolderAsync(item.Token);
                    result.Add(storageFolder);
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