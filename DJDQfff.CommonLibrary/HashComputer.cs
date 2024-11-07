using System.IO.Compression;
using System.Security.Cryptography;

namespace DJDQfff.CommonLibrary;

/// <summary> 文件流计算帮助类 </summary>
public static class HashComputer
{
    /// <summary>
    /// 计算Stream流的sha256值。注意：此操作会改变原Stream流
    /// </summary>
    /// <param name="stream"> 文件流 </param>
    /// <returns> </returns>
    public static string ComputeHash(this Stream stream)
    {
        SHA256 sHA256 = SHA256.Create();
        byte[] vs = sHA256.ComputeHash(stream);
        string hash = BitConverter.ToString(vs).Replace("-", "");
        sHA256.Dispose();
        return hash;
    }

    /// <summary>
    /// 计算ZipArchiveEntry打开流的sha256值。注意：此操作会改变原Stream流
    /// </summary>
    /// <param name="entry"> </param>
    /// <returns> </returns>
    public static string ComputeHash(this ZipArchiveEntry entry)
    {
        string hash;
        using (Stream stream = entry?.Open())
        {
            hash = stream.ComputeHash();
        }
        return hash;
    }

    /// <summary>
    /// 计算量类库SharpCompress的entry
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static string ComputeHash(this SharpCompress.Archives.IArchiveEntry entry)
    {
        string hash;
        using (Stream stream = entry?.OpenEntryStream())
        {
            hash = stream.ComputeHash();
        }
        return hash;
    }
}
