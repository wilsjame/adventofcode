using System.Diagnostics;
using System.Reflection;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day06.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

var l = reader.ReadLine();
Debug.Assert(l != null);
Console.WriteLine(l);

// part 1
int? ans1 = null;
for (var i = 0; i + 4 < l.Length; i++)
{
    var set = new HashSet<char>
    {
        l[i],
        l[i + 1],
        l[i + 2],
        l[i + 3]
    };
    
    if (set.Count == 4)
    {
        ans1 = i + 4;
        break;
    }
}
Console.WriteLine(ans1);

// part 2
int? ans2 = null;
for (var i = 0; i + 14 < l.Length; i++)
{
    var set = new HashSet<char>();
    for (var j = 0; j < 14; j++)
    {
        set.Add(l[i + j]);
    };
    
    if (set.Count == 14)
    {
        ans2 = i + 14;
        break;
    }
}
Console.WriteLine(ans2);