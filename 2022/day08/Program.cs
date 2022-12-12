using System.Diagnostics;
using System.Reflection;
using day08;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day08.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file
var ll = new List<List<int>>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        ll.Add(line.
            Select(c => int.Parse(c.ToString()))
            .ToList());
    }
}

// part 1
var ans1 = Utils.Solve1(ll);
Console.WriteLine(ans1);

// part 2
var ans2 = Utils.Solve2(ll);
Console.WriteLine(ans2);