namespace Elves.Days;

public class Day1
{
    public static void Go(string input)
    {
        var codes = input.Split("\n");
        var dial = 50;
        var result = 0;
        foreach (var code in codes)
        {
            dial = code.StartsWith('L')
                ? Wrap100(dial, int.Parse(code[1..]), false)
                : Wrap100(dial, int.Parse(code[1..]), true);
            if (dial == 0) result++;
        }
        Console.WriteLine($"Wrap100: {result}\n");

        dial = 50;
        var crossings = 0;
        foreach (var code in codes)
        {
            var shift = int.Parse(code[1..]) * (code.StartsWith('L') ? -1 : 1);
            var (newDial, segmentCross) = WrapWithCross(dial, shift, 100);

            Console.WriteLine($"from {dial} shift {shift} -> {newDial}, crosses {segmentCross}");

            dial = newDial;
            crossings += segmentCross;
        }
        Console.Write($"WrapWithCross: {crossings}");


        static int Wrap100(int value, int shift, bool forward) => forward
            ? (value + shift) % 100
            : (value - shift + 100) % 100;

        static (int result, int crossZero) WrapWithCross(int value, int shift, int mod)
        {
            var res = ((value + shift) % mod + mod) % mod;
            var crossZero = shift > 0
                ? (value + shift) / mod - value / mod
                : (int)Math.Floor((double)(value - 1) / mod) - (int)Math.Floor((double)(value + shift) / mod);
            if (shift < 0 && res == 0) crossZero++;
            return (res, crossZero);
        }
    }
}