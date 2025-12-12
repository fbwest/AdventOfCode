using System.Diagnostics;

namespace Elves.Days;

public static class Day8
{
    private const int Iterations = 1000;
    
    public static void Go(string input)
    {
        var sw = Stopwatch.StartNew();
        
        var lines = input.Split('\n');
        var points = lines.Select(t => t.Split(','))
            .Select((xyz, i) => new Point
            {
                Index = i,
                X = double.Parse(xyz[0]),
                Y = double.Parse(xyz[1]),
                Z = double.Parse(xyz[2])
            }).ToList();
        foreach (var p1 in points)
        {
            foreach (var p2 in points.Where(p => p.Index > p1.Index))
            {
                p1.Distances[p2] = GetDistance(p1, p2);
            }
        }

        for (var i = 0; i < Iterations; i++)
        {
            FindMinDistanceAndConnect(points);
        }
        
        var circuits = new List<HashSet<Point>>();
        var visited = new HashSet<Point>();

        foreach (var p in points)
        {
            if (visited.Contains(p)) continue;
            var circuit = GetCircuit(p);
            circuits.Add(circuit);
            foreach (var x in circuit)
                visited.Add(x);
        }

        var result = circuits
            .Select(c => c.Count)
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate((x, y) => x * y);
        
        sw.Stop();
        Console.WriteLine(string.Join(", ", result));
        Console.WriteLine(sw.Elapsed);
    }
    
    private static void FindMinDistanceAndConnect(List<Point> points)
    {
        var index1 = 0;
        var index2 = 0;
        var minDistance = double.MaxValue;
        foreach (var p in points)
        {
            foreach (var distance in p.Distances)
            {
                if (p.Neighbours.Contains(distance.Key)) continue;
                if (!(distance.Value < minDistance)) continue;
                minDistance = distance.Value;
                index1 = p.Index;
                index2 = distance.Key.Index;
            }
        }

        var p1 = points[index1];
        var p2 = points[index2];
        p1.Neighbours.Add(p2);
        p2.Neighbours.Add(p1);
    }
    
    private static double GetDistance(Point p1, Point p2)
    {
        var dx = p2.X - p1.X;
        var dy = p2.Y - p1.Y;
        var dz = p2.Z - p1.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    private static HashSet<Point> GetCircuit(Point start)
    {
        var visited = new HashSet<Point>();
        var stack = new Stack<Point>();
        stack.Push(start);
        while (stack.Count > 0)
        {
            var p = stack.Pop();
            if (!visited.Add(p)) continue;
            foreach (var n in p.Neighbours.Where(n => !visited.Contains(n)))
                stack.Push(n);
        }

        return visited;
    }
}

public class Point
{
    public int Index { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
    public double Z { get; init; }
    public List<Point> Neighbours { get; } = [];
    public Dictionary<Point, double> Distances { get; } = [];
}