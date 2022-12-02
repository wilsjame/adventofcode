using System.Diagnostics;
using System.Reflection;
using Utils = day02.Utils;

// Set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day02.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// Consume input file
var l1 = new List<string>();
var l2 = new List<string>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        l1.Add(line.Split(' ')[0]);
        l2.Add(line.Split(' ')[1]);
    }
}

// Part 1
var score1 = 0;
for (var i = 0; i < l1.Count; i++)
{
    score1 += Utils.GetPlayerScore1(l1[i], l2[i]);
}
Console.WriteLine(score1);

// Part 2
var score2 = 0;
for (var i = 0; i < l1.Count; i++)
{
    score2 += Utils.GetPlayerScore2(l1[i], l2[i]);
}
Console.WriteLine(score2);
