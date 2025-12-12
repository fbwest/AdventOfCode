using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Elves.Days;

public static class Day5
{
    public static void Go(string input)
    {
        var watch = new Stopwatch();
        watch.Start();
        
        var temp = input.Split("\n\n");
        var ranges = temp[0]
            .Split('\n')
            .Select(x => x.Split('-'))
            .Select(x => (long.Parse(x[0]), long.Parse(x[1])))
            .ToArray();
        
        var ids = temp[1].Split('\n').Select(long.Parse);
        var fresh = new List<long>();

        foreach (var id in ids)
        {
            if (id <= 0) continue;
            foreach (var range in ranges)
            {
                if (id.InRange(range.Item1, range.Item2))
                {
                    fresh.Add(id);
                    break;
                }
            }
        }
        
        watch.Stop();
        Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds}ms");
        Console.WriteLine(fresh.Count);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool InRange(this long x, long min, long max) =>
        (ulong)(x - min) <= (ulong)(max - min);
}