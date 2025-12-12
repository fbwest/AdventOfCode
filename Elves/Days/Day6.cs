using System.Diagnostics;
using Elves.Helpers;

namespace Elves.Days;

public static class Day6
{
    public static void Go(string input)
    {
        var sw = Stopwatch.StartNew();
        
        var lines = input.Split('\n');
        var numbers = lines
            .SkipLast(1)
            .Select(x => x.Split(' ', options: StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Select(long.Parse).ToArray())
            .ToArray()
            .Transpose();
        var operators = lines
            .Last()
            .Split(' ', options: StringSplitOptions.RemoveEmptyEntries);
        var result = 0L;
        for (long i = 0; i < operators.Length; i++)
        {
            var value = 0L;
            for (var n = 0; n < numbers[i].Length; n++)
            {
                switch (operators[i])
                {
                    case "+":
                        value += numbers[i][n];
                        break;
                    case "*":
                        if (value == 0) value = 1;
                        value *= numbers[i][n];
                        break;
                }
            }
            result += value;
        }

        sw.Stop();
        Console.WriteLine(result);
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
    }
}