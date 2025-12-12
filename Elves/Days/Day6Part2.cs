using System.Diagnostics;

namespace Elves.Days;

public static class Day6Part2
{
    public static void Go(string input)
    {
        var sw = Stopwatch.StartNew();
        
        var lines = input.Split('\n');
        var numbers = lines
            .SkipLast(1)
            .ToArray();
        var operatorsRaw = lines
            .Last()
            .Split(' ');
        var operators = new List<(char Ch, int Ln)>();
        var ch = '-';
        var len = 0;
        foreach (var op in operatorsRaw)
        {
            if (!string.IsNullOrWhiteSpace(op))
            {
                if (len > 0) operators.Add((ch, len + 1));
                ch = op[0];
                len = 0;
            }
            else
            {
                len++;
            }
        }
        //todo: the last len is wrong!!! (it drops the last whitespaces)
        operators.Add((ch, len + 1));
        
        var result = 0L;
        var offset = 0;
        foreach (var op in operators)
        {
            var localResult = 0L;
            var log = string.Empty;
            for (var x = op.Ln - 1; x >= 0; x--)
            {
                var value = numbers.Select(t => t[x + offset]).ToList();
                var num = int.Parse(string.Join("", value));
                switch (op.Ch)
                {
                    case '+':
                        localResult += num;
                        log += string.IsNullOrEmpty(log) ? $"{num} " : $"+ {num} ";
                        break;
                    case '*':
                        if (localResult == 0) localResult = 1;
                        localResult *= num;
                        log += string.IsNullOrEmpty(log) ? $"{num} " : $"* {num} ";
                        break;
                }
            }
            offset += op.Ln + 1;
            result += localResult;
            Console.WriteLine($"{log}= {localResult}");
        }
        sw.Stop();
        Console.WriteLine(result);
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
    }
}