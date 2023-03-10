using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Pickers;

namespace MyLibrary.UWP
{
    /// <summary> StorageItem选取器 </summary>
    public static class StorageItemPicker
    {
        /// <summary>
        /// 打开任意文件
        /// </summary>
        /// <returns></returns>
        public static async Task<IReadOnlyList<StorageFile>> OpenAnyFilesAsync ()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add("*");

            var storageFile = await openPicker.PickMultipleFilesAsync();
            return storageFile;
        }

        /// <summary> 打开单个文件 </summary>
        /// <param name="types"> 要打开的文件类型 </param>
        /// <returns> </returns>
        public static async Task<StorageFile> OpenSingleFileAsync (params string[] types)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            foreach (var t in types)
            {
                fileOpenPicker.FileTypeFilter.Add(t);
            }
            fileOpenPicker.FileTypeFilter.Add(".");
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFile storageFile = await fileOpenPicker.PickSingleFileAsync();
            return storageFile;
        }

        /// <summary> 打开多个文件 </summary>
        /// <returns> </returns>
        public static async Task<IReadOnlyList<StorageFile>> OpenMultiFilesAsync (params string[] types)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();

            foreach (var t in types)
            {
                fileOpenPicker.FileTypeFilter.Add(t);
            }
            fileOpenPicker.FileTypeFilter.Add(".");
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            var storageFile = await fileOpenPicker.PickMultipleFilesAsync();
            return storageFile;
        }

        /// <summary> 打开单个文件夹 </summary>
        /// <returns> </returns>
        public static async Task<StorageFolder> OpenSingleFolderAsync ()
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add(".");
            folderPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            return folder;
        }

        /// <summary> 保存图片文件（仅限jpg、png格式） </summary>
        /// <returns> </returns>
        public static async Task<StorageFile> SavePictureAsync ()
        {
            FileSavePicker file = new FileSavePicker();
            file.FileTypeChoices.Add("图片" , new List<string>() { ".jpg" , ".png" });
            file.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            StorageFile storageFile = await file.PickSaveFileAsync();
            return storageFile;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="types">文件类型</param>
        /// <returns></returns>
        public static async Task<StorageFile> SaveFileAsync (params string[] types)
        {
            FileSavePicker file = new FileSavePicker();
            file.FileTypeChoices.Add("类型" , types);
            file.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            StorageFile storageFile = await file.PickSaveFileAsync();
            return storageFile;
        }
    }
}