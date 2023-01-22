using System.Diagnostics;
using System.Reflection;

// Set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day01.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// Consume input file
var l = new List<int>();
var sum = 0;
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        if (line.Length > 0)
        {
            var num = int.Parse(line);
            sum += num;
        }
        else
        {
            l.Add(sum);
            sum = 0;
        }
    }
}
// Add last sum
l.Add(sum);

// Part 1
var ans1 = l.Max();
Console.WriteLine(ans1);

// Part 2
var ans2 = l
    .OrderByDescending(x => x)
    .Take(3)
    .Sum();
Console.WriteLine(ans2);