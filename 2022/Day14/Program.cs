using System.Diagnostics;
using System.Reflection;
using Day14;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day14.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file
var input = new List<List<(int, int)>>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (!string.IsNullOrEmpty(line))
    {
        var tuples = line
            .Split(" -> ")
            .Select(pr => pr.Split(","))
            .Select(pr => (int.Parse(pr[0]), int.Parse(pr[1])));

        input.Add(tuples.ToList());
    }
}

var rockPath = new Dictionary<(int, int), char>();
Utils.BuildRockPath(input, rockPath);
Utils.PrintRockPath(rockPath);

// part 1
var sandGrains = 0;
var depth = 0;
var mxDepth = rockPath.Keys.Max(k => k.Item2);
while (depth <= mxDepth)
{
    depth = Utils.PourSand(Utils.SandOrigin, 0, rockPath);
    sandGrains++;

    //AnimatePart1(rockPath, sandGrains, depth, mxDepth);
}

Console.WriteLine(sandGrains - 1);

// part 2
// add floor line
var floorDepth = mxDepth + 2;
var mxX = rockPath.Keys.Max(k => k.Item1);
var minX = rockPath.Keys.Min(k => k.Item1);
const int extend = 250; // make sure we have enough floor space to pour sand
Utils.DrawLine((minX - extend, floorDepth), (mxX + extend, floorDepth), rockPath);

var sw = new Stopwatch();
sw.Start();

// continue pouring until sand blocks the source
while (!rockPath.ContainsKey(Utils.SandOrigin))
{
    depth = Utils.PourSand(Utils.SandOrigin, 0, rockPath);
    if (depth + 1 == floorDepth)
    {
        Console.WriteLine($"sand hit the floor! grains: {sandGrains}, time: {sw.Elapsed}");
    }

    Debug.Assert(depth < floorDepth); // otherwise extend more
    sandGrains++;

    //AnimatePart2(rockPath, sandGrains);
}

sw.Stop();
Console.WriteLine($"{sandGrains - 1} time: {sw.Elapsed}"); // 27.936K grains ~7m:51s (with some print, no print for faster)

//
// rough animation utils
//
void AnimatePart1(Dictionary<(int, int), char> chars, int sandGrains1, int depth1, int mxDepth1)
{
    Utils.PrintRockPath(chars);
    Console.WriteLine($"part 1 sand grains: {sandGrains1}");
    Thread.Sleep(25);
    if (depth1 < mxDepth1) Console.Clear();
}

void AnimatePart2(Dictionary<(int, int), char> dictionary, int i)
{
    Utils.PrintRockPath(dictionary);
    Console.WriteLine($"part 2 sand grains: {i}");
    Thread.Sleep(25);
    if (!dictionary.ContainsKey(Utils.SandOrigin)) Console.Clear();
}