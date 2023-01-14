using System.Diagnostics;
using System.Reflection;
using day15;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day15.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file
var sensor = new List<(int, int)>();
var beacon = new List<(int, int)>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (!string.IsNullOrEmpty(line))
    {
        sensor.Add(Utils.GetSensorCoords(line.Split(":")[0]));
        beacon.Add(Utils.GetBeaconCoords(line.Split(":")[1]));
    }
}

// populate grid
var grid = new Dictionary<(int, int), char>();
for (var i = 0; i < sensor.Count; i++)
{
    // print sensor and beacon coordinates
    //Console.WriteLine($"{sensor[i]} - {beacon[i]} - {Utils.ManhattanDist(sensor[i], beacon[i])}");
    Utils.AddToGrid(grid, sensor[i], beacon[i]);
}

// debug
Utils.PrintGrid(grid);

// part 1
// todo index compression?
var ans1 = 0;
const int row = 10;
var mnX = grid.Keys.Min(k => k.Item1) - 1;
var mxX = grid.Keys.Max(k => k.Item1) + 1;

for (var x = mnX; x <= mxX; x++)
{
    if (grid.ContainsKey((x, row)))
    {
        if (grid[(x, row)] == '#')
        {
            ans1++;
        }
    }
}

Console.WriteLine($"{ans1}");