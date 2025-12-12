namespace Elves.Days;

public class Day2
{
    public static void Go(string input)
    {
        var ranges = input.Split(',');
        var invalids = new List<long>();
        foreach (var range in ranges)
        {
            var ids = range.Split('-');
            Console.WriteLine($"\n###range:{ids[0]}-{ids[1]}");
            var cur = ids[0];
            while (true)
            {
                var len = cur.Length;
                for (var i = 2; i <= len; i++)
                {
                    if (len % i != 0) continue;
                    var isInvalid = true;
                    var sublen = len / i;
                    var subcur = cur[..sublen];
                    for (var j = 1; j < i; j++)
                    {
                        var fr = sublen * j;
                        var to = sublen * j + sublen;
                        var sub = cur[fr..to];
                        if (subcur == sub) continue;
                        isInvalid = false;
                        break;
                    }

                    if (!isInvalid) continue;
                    Console.WriteLine($"cur:{cur} is invalid;");
                    invalids.Add(long.Parse(cur));
                    break;
                }
                if (cur == ids[1]) break;
                var curInt = long.Parse(cur);
                cur = (++curInt).ToString();
            }
        }
        Console.WriteLine($"\nInvalids: {invalids.Sum()}");
    }
}