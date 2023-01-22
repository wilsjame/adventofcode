using System.Diagnostics;
using System.Reflection;
using Day03;

// Set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day03.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// Read input file
var l = new List<string>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        l.Add(line);
    }
}

// Part 1
var d = new Dictionary<char, int>();
foreach (var s in l)
{
    // split string in half
    var s1 = s[..(s.Length / 2)];
    var s2 = s[(s.Length / 2)..];
    
    // determine element shared by both strings
    var c = s1.Intersect(s2).ToArray();
    Debug.Assert(c.Length == 1);
    // add character to dictionary
    d[c[0]] = d.GetValueOrDefault(c[0], 0) + 1;
}

var ans1 = Utils.GetValue(d);
Console.WriteLine(ans1);

// Part 2
d.Clear();
for (var i = 0; i < l.Count; i += 3)
{
    var s1 = l[i];
    var s2 = l[i + 1];
    var s3 = l[i + 2];
    
    // determine element shared by all strings
    var c = s1.Intersect(s2).Intersect(s3).ToArray();
    Debug.Assert(c.Length == 1);
    // add character to dictionary
    d[c[0]] = d.GetValueOrDefault(c[0], 0) + 1;
}

var ans2 = Utils.GetValue(d);
Console.WriteLine(ans2);