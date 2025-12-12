using System.Diagnostics;
using Elves.Helpers;

namespace Elves.Days;

public static class Day7
{
    public static async Task Go(string input)
    {
        var sw = Stopwatch.StartNew();

        var lines = input.Split('\n');
        var matrix = lines
            .Select(line => line.ToArray())
            .ToArray();
        
        matrix.PrintEx(0, true);
        Console.CursorVisible = false;
        await Task.Delay(1000);

        var s = Array.IndexOf(matrix[0], 'S');
        matrix[1][s] = '|';
        
        matrix.PrintEx(0, true);
        await Task.Delay(70);
        
        const char splitter = '^';
        const char tachyon = '|';
        const char empty = '.';
        var splits = 0L;
        
        for (var y = 1; y < matrix.Length; y++)
        {
            for (var x = 0; x < matrix[y].Length; x++)
            {
                var cell = matrix[y][x];
                var upperCell = matrix[y - 1][x];
                switch (cell)
                {
                    case empty when upperCell == tachyon:
                        matrix[y][x] = tachyon;
                        continue;
                    case splitter when upperCell == tachyon:
                    {
                        if (x - 1 >= 0) matrix[y][x - 1] = tachyon;
                        if (x + 1 < matrix[y].Length) matrix[y][x + 1] = tachyon;
                        splits++;
                        break;
                    }
                }
            }
            matrix.PrintEx(y, true);
            await Task.Delay(70);
        }
        
        sw.Stop();
        Console.WriteLine($"Splits: {splits}");
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
    }
}