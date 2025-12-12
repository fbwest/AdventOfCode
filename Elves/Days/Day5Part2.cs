using System.Diagnostics;

namespace Elves.Days;

public static class Day5Part2
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
            .ToList();

        var fresh = CountIds(ranges);
        
        watch.Stop();
        Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds}ms");
        Console.WriteLine($"count: {fresh}");
    }

    static long CountIds(List<(long start, long end)> ranges)
    {
        long res = 0;
        foreach (var (s, e) in MergeRanges(ranges))
            res += e - s + 1;
        return res;
    }

    static List<(long start, long end)> MergeRanges(List<(long start, long end)> ranges)
    {
        if (ranges.Count == 0) return [];
        ranges.Sort((a, b) => a.start.CompareTo(b.start));
        var merged = new List<(long start, long end)>();
        var curStart = ranges[0].start;
        var curEnd = ranges[0].end;
        for (var i = 1; i < ranges.Count; i++)
        {
            var (s, e) = ranges[i];
            if (s <= curEnd + 1)
            {
                if (e > curEnd) curEnd = e;
            }
            else
            {
                merged.Add((curStart, curEnd));
                curStart = s;
                curEnd = e;
            }
        }
        merged.Add((curStart, curEnd));
        Console.WriteLine($"ranges: {ranges.Count}; merged: {merged.Count}");
        return merged;
    }
}