using System.Diagnostics;
using System.Reflection;
using day12;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day12.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file
var graph = new List<List<char>>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (!string.IsNullOrEmpty(line))
    {
        var chars = line.ToList();
        graph.Add(chars);
    }
}

var end = Utils.GetEnd(graph);

// part 1
var visited = new Dictionary<(int, int), bool>();
var distance = new Dictionary<(int, int), int>();
var start = Utils.GetStart(graph);
Utils.Bfs(graph, visited, distance, start);
var ans = distance[end];
Console.WriteLine(ans);

// part 2
var starts = Utils.GetStarts(graph);
var ans2 = int.MaxValue;
foreach (var s in starts)
{
    visited = new Dictionary<(int, int), bool>();
    distance = new Dictionary<(int, int), int>();
    Utils.Bfs(graph, visited, distance, s);
    if (visited.ContainsKey(end))
    {
        ans2 = Math.Min(ans2, distance[end]);
    }
}
Console.WriteLine(ans2);