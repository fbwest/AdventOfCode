namespace Elves.Days;

public class Day3
{
    public static void Go(string input)
    {
        var batteries = input.Split('\n');
        var joltages = new List<int>();
        foreach (var battery in batteries)
        {
            var max1 = battery.Max();
            char max2;
            string sub;
            var i = battery.IndexOf(max1);
            if (i == battery.Length - 1)
            {
                sub = battery[..i];
                max2 = sub.Max();
                joltages.Add(byte.Parse([max2,max1]));
                Console.WriteLine($"battery:{battery} = {max2}{max1}");
                continue;
            }
            sub = battery[(i+1)..];
            max2 = sub.Max();
            joltages.Add(byte.Parse([max1,max2]));
            Console.WriteLine($"battery:{battery} = {max1}{max2}");
        }
        Console.WriteLine(joltages.Sum());
    }
    
    public static void Go2(string input)
    {
        var batteries = input.Split('\n');
        var joltages = new List<long>();
        foreach (var battery in batteries)
        {
            var buf = new List<char>();
            var cur = battery;
            while (true)
            {
                if (cur.Length + buf.Count == 12)
                {
                    buf.AddRange(cur);
                    break;
                }
                var sub = cur[..^(11 - buf.Count)];
                var max = sub.Max();
                buf.Add(max);
                if (buf.Count == 12) break;
                var index = sub.IndexOf(max);
                cur = cur[(index + 1)..];
            }

            var joltage = string.Join("", buf);
            joltages.Add(long.Parse(joltage));
            Console.WriteLine($"battery:{battery} = {joltage}");
        }
        Console.WriteLine(joltages.Sum());
    }
}