using System.Diagnostics;
using System.Reflection;
using day04;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day04.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file into tuples
var l = new List<List<(int, int)>>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        var parts = line.Split(',');
        var x1 = int.Parse(parts[0].Split('-')[0]);
        var y1 = int.Parse(parts[0].Split('-')[1]);
        var x2 = int.Parse(parts[1].Split('-')[0]);
        var y2 = int.Parse(parts[1].Split('-')[1]);

        l.Add(
            new List<(int, int)>()
            {
                (x1, y1),
                (x2, y2)
            }
        );
    }
}

// part 1
var ans1 = l.Sum(prs => Utils.HasContained(prs[0], prs[1]) ? 1 : 0);
Console.WriteLine(ans1);

// part 2
var ans2 = l.Sum(prs => Utils.HasOverlap(prs[0], prs[1]) ? 1 : 0);
Console.WriteLine(ans2);
