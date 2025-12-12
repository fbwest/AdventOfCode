namespace Elves.Helpers;

public static class MatrixHelper
{
    public static T[][] Transpose<T>(this T[][] matrix)
    {
        if (matrix.Length == 0) return [];

        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var transposed = new T[cols][];

        for (var i = 0; i < cols; i++)
        {
            transposed[i] = new T[rows];
            for (var j = 0; j < rows; j++)
            {
                transposed[i][j] = matrix[j][i];
            }
        }

        return transposed;
    }

    public static void Print<T>(this T[][] matrix, bool clear = false)
    {
        if (clear) Console.Clear();
        foreach (var y in matrix)
        {
            foreach (var x in y)
            {
                Console.Write(x);
            }

            Console.WriteLine();
        }
    }
    
    public static void PrintEx<T>(this T[][] matrix, long line, bool clear = false, bool beautify = false)
    {
        var window = 16;
        if (window > matrix.Length) window = matrix.Length;
        
        if (clear) Console.SetCursorPosition(0, 0);
        var fr = line - window / 2;
        var to = line + window / 2 + 1;
        if (fr < 0)
        {
            fr = 0;
            to = window;
        }
        if (to >= matrix.Length)
        {
            fr = matrix.Length - window;
            to = matrix.Length;
        }
        for (var y = fr; y < to; y++)
        {
            for (var x = 0; x < matrix[y].Length; x++)
            {
                var cell = matrix[y][x];
                if (beautify)
                {
                    Console.ForegroundColor = cell switch
                    {
                        '.' => ConsoleColor.Black,
                        'S' => ConsoleColor.Red,
                        '|' => ConsoleColor.DarkGreen,
                        '^' => ConsoleColor.Yellow,
                        _ => ConsoleColor.Black
                    };
                    Console.Write(cell);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(cell);
                }
            }
            Console.WriteLine();
        }
    }
    
    public static void PrintWithCoordinates<T>(this T[][] matrix, bool clear = false)
    {
        if (clear) Console.Clear();
        for (var x = 0; x < matrix.Length; x++)
        {
            for (var y = 0; y < matrix[x].Length; y++)
            {
                Console.Write($"({x:00},{y:00}) {matrix[x][y]} ");
            }
            Console.WriteLine();
        }
    }
    
    public static T[][] CloneMatrix<T>(this T[][] source) => source
        .Select(row => row.ToArray())
        .ToArray();
}