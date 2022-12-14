using System.Diagnostics;
using System.Reflection;
using day10;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day10.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file
var input = new List<(string op, int cnt)>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        var op = line.Split(" ").First();
        // convert negative number string to int
        var ok = int.TryParse(line.Split(" ").Last(), out var cnt);
        cnt = ok ? cnt : 0;
        input.Add((op, cnt));
    }
}

// part 1
var signalStrength = Utils.Solve1(input);
var ans1 = signalStrength.Sum();
Console.WriteLine(ans1);

// part 2
var crtDisplay = Utils.Solve2(input);
foreach (var ans2AsciiArtLine in crtDisplay.Select(line => new string(line.ToArray())))
{
    Console.WriteLine(ans2AsciiArtLine);
}