namespace CommonLibrary;

/// <summary>
/// 对 Stream 操作
/// </summary>
public static class StreamOperation
{
    /// <summary>
    /// 传入流，读取流的所有行
    /// </summary>
    /// <param name="stream">stream，必须是由文本打开的流</param>
    /// <returns>字符串集合</returns>
    public static List<string> ReadAllLines(this Stream stream)
    {
        List<string> lines = [];

        StreamReader streamReader = new(stream);

        string templine;
        while ((templine = streamReader.ReadLine()) != null)
        {
            lines.Add(templine);
        }

        return lines;
    }
}