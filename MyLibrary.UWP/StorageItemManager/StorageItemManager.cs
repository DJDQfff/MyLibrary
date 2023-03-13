using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;

namespace MyUWPLibrary.StorageItemManager
{
    public class StorageItemManager
    {
        public ObservableCollection<StorageFolder> Folders = new ObservableCollection<StorageFolder>();
        public void InitialFolders(IEnumerable<StorageFolder> folders)
        {
            Folders.Clear();
            foreach(var folder in folders)
            {
                Folders.Add(folder);
            }
        }

        public async Task<StorageFile> GetStorageFile(string filepath)
        {
            string folderpath = Path.GetDirectoryName(filepath);
            string filename= Path.GetFileName(filepath);

            var folder=Folders.SingleOrDefault(f=>f.Path==folderpath);
            if(folder == null)
            {
                return null;
            }
            else
            {
                var item=await folder.TryGetItemAsync(filename);
                return item as StorageFile;
            }
        }

        
    }
}
