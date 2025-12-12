using System.Diagnostics;

namespace Elves.Days;

public static class Day4
{
    public static void Go(string input)
    {
        var watch = new Stopwatch();
        watch.Start();
        
        var lines = input.Split('\n');
        var matrix = lines
            .Select(line => line.Select(c => c == '@').ToArray())
            .ToArray();
        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var transposed = new bool[cols][];

        for (var i = 0; i < cols; i++)
        {
            transposed[i] = new bool[rows];
            for (var j = 0; j < rows; j++)
            {
                transposed[i][j] = matrix[j][i];
            }
        }

        matrix = transposed;
        
        for (var xt = 0; xt < matrix.Length; xt++)
        {
            for (var yt = 0; yt < matrix[xt].Length; yt++)
            {
                var cellP = matrix[xt][yt] ? "@" : ".";
                Console.Write($"[{xt}][{yt}] = {cellP} | ");
            }

            Console.WriteLine();
        }
        
        var rolls = 0;
        for (var x = 0; x < matrix.Length; x++)
        {
            for (var y = 0; y < matrix[x].Length; y++)
            {
                if (!matrix[x][y]) continue;
                var result = 0;
                var ok = true;
                for (var r = 1; r <= 8; r++)
                {
                    var cell = r switch
                    {
                        1 => (-1, -1),
                        2 => ( 0, -1),
                        3 => ( 1, -1),
                        4 => ( 1,  0),
                        5 => ( 1,  1),
                        6 => ( 0,  1),
                        7 => (-1,  1),
                        8 => (-1,  0),
                        _ => throw new Exception("Invalid roll")
                    };
                    var x1 = x + cell.Item1;
                    var y1 = y + cell.Item2;
                    if (x1 < 0 || y1 < 0 || x1 > matrix.Length - 1 || y1 > matrix[x].Length - 1) continue;
                    var value = matrix[x1][y1];
                    if (value) result++;
                    if (result == 4)
                    {
                        ok = false;
                        break;
                    }
                }
                
                if (ok)
                {
                    rolls++;
                    Console.WriteLine($"matrix[{x}][{y}] rolls:{rolls}");
                }
            }
        }
        
        watch.Stop();
        Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds}ms");
        Console.WriteLine($"rolls: {rolls}");
    }
    
    public static void Go2(string input)
    {
        var watch = new Stopwatch();
        watch.Start();
        
        var lines = input.Split('\n');
        var matrix = lines
            .Select(line => line.Select(c => c == '@').ToArray())
            .ToArray();
        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var transposed = new bool[cols][];

        for (var i = 0; i < cols; i++)
        {
            transposed[i] = new bool[rows];
            for (var j = 0; j < rows; j++)
            {
                transposed[i][j] = matrix[j][i];
            }
        }

        matrix = transposed;
        
        var rolls = 0;
        while (true)
        {
            var newMatrix = matrix;
            /*for (var xt = 0; xt < matrix.Length; xt++)
            {
                for (var yt = 0; yt < matrix[xt].Length; yt++)
                {
                    var cellP = matrix[xt][yt] ? "@" : ".";
                    Console.Write($"[{xt}][{yt}] = {cellP} | ");
                }

                Console.WriteLine();
            }*/
            var localRolls = 0;
            for (var x = 0; x < matrix.Length; x++)
            {
                for (var y = 0; y < matrix[x].Length; y++)
                {
                    if (!matrix[x][y]) continue;
                    var result = 0;
                    var ok = true;
                    for (var r = 1; r <= 8; r++)
                    {
                        var cell = r switch
                        {
                            1 => (-1, -1),
                            2 => ( 0, -1),
                            3 => ( 1, -1),
                            4 => ( 1,  0),
                            5 => ( 1,  1),
                            6 => ( 0,  1),
                            7 => (-1,  1),
                            8 => (-1,  0),
                            _ => throw new Exception("Invalid roll")
                        };
                        var x1 = x + cell.Item1;
                        var y1 = y + cell.Item2;
                        if (x1 < 0 || y1 < 0 || x1 > matrix.Length - 1 || y1 > matrix[x].Length - 1) continue;
                        var value = matrix[x1][y1];
                        if (value) result++;
                        if (result == 4)
                        {
                            ok = false;
                            break;
                        }
                    }

                    if (ok)
                    {
                        localRolls++;
                        newMatrix[x][y] = false;
                        Console.WriteLine($"matrix[{x}][{y}] localRolls:{localRolls}");
                    }
                }
            }
            
            if (localRolls == 0) break;
            
            rolls += localRolls;
            Console.WriteLine($"rolls: {rolls}");
            matrix = newMatrix;
        }
        watch.Stop();
        Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds}ms");
        Console.WriteLine($"total rolls: {rolls}");
    }
}