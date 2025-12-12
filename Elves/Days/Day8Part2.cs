using System.Diagnostics;

namespace Elves.Days;

public static class Day8Part2
{
    public static void Go(string input)
    {
        var sw = Stopwatch.StartNew();
        
        var lines = input.Split('\n');
        var points = lines
            .Select(t => t.Split(','))
            .Select((xyz, i) => new Point
            {
                Index = i,
                X = double.Parse(xyz[0]),
                Y = double.Parse(xyz[1]),
                Z = double.Parse(xyz[2])
            }).ToList();
        foreach (var p1 in points)
            foreach (var p2 in points.Where(p => p.Index > p1.Index))
                p1.Distances[p2] = GetDistance(p1, p2);

        var globalCircuit = new HashSet<Point>();
        while (globalCircuit.Count != points.Count)
        {
            var (p1, p2) = FindMinDistanceAndConnect(points);
            globalCircuit.Add(p1);
            globalCircuit.Add(p2);
            Console.WriteLine($"{globalCircuit.Count} of {points.Count}");
        }

        var last = globalCircuit.Last();
        var result = last.X * last.Neighbours.First().X;
        
        sw.Stop();
        Console.WriteLine(result);
        Console.WriteLine(sw.Elapsed);
    }
    
    private static (Point P1, Point p2) FindMinDistanceAndConnect(List<Point> points)
    {
        var index1 = 0;
        var index2 = 0;
        var minDistance = double.MaxValue;
        foreach (var p in points)
            foreach (var distance in p.Distances)
            {
                if (p.Neighbours.Contains(distance.Key)) continue;
                if (!(distance.Value < minDistance)) continue;
                minDistance = distance.Value;
                index1 = p.Index;
                index2 = distance.Key.Index;
            }

        var p1 = points[index1];
        var p2 = points[index2];
        p1.Neighbours.Add(p2);
        p2.Neighbours.Add(p1);
        return (p1, p2);
    }
    
    private static double GetDistance(Point p1, Point p2)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dz = p2.Z - p1.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}