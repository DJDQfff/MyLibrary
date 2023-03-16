using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.AccessCache;

using static Windows.Storage.AccessCache.StorageApplicationPermissions;
namespace MyLibrary.UWP.StorageItemManager
{
    public class StorageItemManager
    {
        public Dictionary<string, StorageFolder> AccessDictionary { get; private set; }
        public ObservableCollection<StorageFolder> Folders = new ObservableCollection<StorageFolder>();
        public void InitialFolders(Dictionary<string, StorageFolder> folders)
        {
            AccessDictionary = folders;

            Folders.Clear();
            foreach (var folder in folders.Values)
            {
                Folders.Add(folder);
            }
        }

        public void AddToken(StorageFolder folder)
        {
            var token = StorageApplicationPermissions.FutureAccessList.Add(folder);
            AccessDictionary.Add(token, folder);
        }
        public void RemoveToken(string folder)
        {
            var pair = AccessDictionary.Single(x => x.Value.Path == folder);
            var token = pair.Key;
            FutureAccessList.Remove(token); // 从系统未来访问列表里删除
            AccessDictionary.Remove(token);
        }
        public StorageFolder GetStorageFolder(string folderpath)
        {
            return Folders.SingleOrDefault(f => f.Path == folderpath);

        }
        public async Task<StorageFile> GetStorageFile(string filepath)
        {
            string folderpath = Path.GetDirectoryName(filepath);
            string filename = Path.GetFileName(filepath);

            var folder = Folders.SingleOrDefault(f => f.Path == folderpath);
            if (folder == null)
            {
                return null;
            }
            else
            {
                var item = await folder.TryGetItemAsync(filename);
                return item as StorageFile;
            }
        }

        public async Task RenameStorageFile(string filepath, string newname)
        {
            var storagefile = await GetStorageFile(filepath);
            if (storagefile != null)
            {
                await storagefile.RenameAsync(newname);
            }
        }

        public async Task DeleteStorageFile(string path, StorageDeleteOption storageDeleteOption)
        {
            var storagefile = await GetStorageFile(path);
            if (storagefile != null)
            {
                await storagefile.DeleteAsync(storageDeleteOption);
            }
        }

    }
}
