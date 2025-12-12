using System.Net;
using Elves.Days;

const byte prod = 0;
const byte day = 8;

var session = File.ReadAllText(".env").Trim();
var httpClientHandler = new HttpClientHandler();
httpClientHandler.CookieContainer.Add(
    new Uri("https://adventofcode.com"),
    new Cookie("session", session));
using var http = new HttpClient(httpClientHandler);
var input = prod == 1
    ? http.GetStringAsync($"https://adventofcode.com/2025/day/{day}/input").Result.Trim()
    : "162,817,812\n57,618,57\n906,360,560\n592,479,940\n352,342,300\n466,668,158\n542,29,236\n431,825,988\n739,650,466\n52,470,668\n216,146,977\n819,987,18\n117,168,530\n805,96,715\n346,949,466\n970,615,88\n941,993,340\n862,61,35\n984,92,344\n425,690,689";

Day8Part2.Go(input);