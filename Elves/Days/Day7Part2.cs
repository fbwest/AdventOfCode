using System.Diagnostics;
using Elves.Helpers;

namespace Elves.Days;

public static class Day7Part2
{
    public static async Task Go(string input)
    {
        //Console.Clear();

        var lines = input.Split('\n');
        var matrix = lines
            .Select(line => line.ToArray())
            .ToArray();
        
        /*matrix.PrintEx(0, true);
        Console.CursorVisible = false;
        await Task.Delay(70);*/

        var s = Array.IndexOf(matrix[0], 'S');
        matrix[1][s] = '|';
        
        /*matrix.PrintEx(0, true);
        await Task.Delay(70);*/
        
        const char splitter = '^';
        const char tachyon = '|';
        const char empty = '.';
        var splits = 0L;
        var paths = new HashSet<string>();

        Console.Clear();
        while (true)
        {
            var mx = matrix.CloneMatrix();
            var path = string.Empty;
            for (var y = 1; y < mx.Length; y++)
            {
                for (var x = 0; x < mx[y].Length; x++)
                {
                    var cell = mx[y][x];
                    var upperCell = mx[y - 1][x];
                    switch (cell)
                    {
                        case empty when upperCell == tachyon:
                            mx[y][x] = tachyon;
                            path += $"{y}{x}";
                            continue;
                        case splitter when upperCell == tachyon:
                        {
                            var random = new Random().Next(0, 2);
                            if (random == 0)
                            {
                                if (x - 1 >= 0)
                                {
                                    mx[y][x - 1] = tachyon;
                                    path += $"{y}{x - 1}";
                                }
                            }
                            else
                            {
                                if (x + 1 < mx[y].Length)
                                {
                                    mx[y][x + 1] = tachyon;
                                    path += $"{y}{x + 1}";
                                }
                            }
                            splits++;
                            break;
                        }
                    }
                }
                /*mx.PrintEx(y, true);
                await Task.Delay(1);*/
            }

            paths.Add(path);
            Console.WriteLine($"paths: {paths.Count}                                                   ");
            Console.SetCursorPosition(0, 0);
            //await Task.Delay(1);
        }
    }
    
    public static void Go2(string input)
    {
        var sw = Stopwatch.StartNew();

        var lines = input.Split('\n');
        var matrix = lines
            .Select(line => line.ToArray())
            .ToArray();

        var h = matrix.Length;
        var w = matrix[0].Length;

        var dp = new long[h][];
        for (var i = 0; i < h; i++)
            dp[i] = new long[w];
        
        var start = Array.IndexOf(matrix[0], 'S');
        dp[0][start] = 1;
        
        const char splitter = '^';
        const char tachyon = '|';
        const char empty = '.';

        for (var y = 0; y < h-1; y++)
        {
            for (var x = 0; x < w; x++)
            {
                var count = dp[y][x];
                if (count == 0) continue;
                var downCell = matrix[y+1][x];
                switch (downCell)
                {
                    case empty or tachyon:
                        dp[y+1][x] += count;
                        break;
                    case splitter:
                        if (x > 0) dp[y+1][x-1] += count;
                        if (x+1 < w) dp[y+1][x+1] += count;
                        break;
                }
            }
        }
        
        dp.Print();
        
        sw.Stop();
        Console.WriteLine($"Timelines: {dp[h-1].Sum()}");
        Console.WriteLine($"Elapsed: {sw.Elapsed}");
    }
}