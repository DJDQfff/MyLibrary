namespace CommonLibrary;

/// <summary> 对数组操作 </summary>
public static class ArrayOperation
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="rank"></param>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public static void SetLineValue<T>(this T[,] array, int rank, int index, T[] value)
    {
        int row = array.GetLength(0);
        int col = array.GetLength(1);

        int minRow = Math.Min(row, value.Length);
        int minCol = Math.Min(col, value.Length);

        switch (rank)
        {
            case 0:
                for (int i = 0; i < minCol; i++)
                {
                    array[index, i] = value[i];
                }
                break;

            case 1:
                for (int i = 0; i < minRow; i++)
                {
                    array[i, index] = value[i];
                }
                break;
        }
    }

    /// <summary> 未添加对null字符的验证，须自行保证无null字符 </summary>
    /// <param name="vs"> </param>
    /// <returns> </returns>
    public static char[,] StringListToArray(this List<string> vs)
    {
        int row = vs.Count;
        int col = vs.Max(n => n.Length);

        char[,] array = new char[row, col];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                array[i, j] = vs[i][j];
            }
        }

        return array;
    }
}