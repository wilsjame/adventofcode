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

// visual debug for sample data (real data too large)
/*
var grid = new Dictionary<(int, int), char>();
for (var i = 0; i < sensor.Count; i++)
{
    Utils.AddToGrid(grid, sensor[i], beacon[i]);
}
Utils.PrintGrid(grid);
*/

// part 1
var n = sensor.Count;
const int row = 2000000;
var coverages = new List<(int, int)>();
for (var i = 0; i < n; i++)
{
    if (Utils.HasRowCoverage(row, sensor[i], beacon[i]))
    {
        coverages.Add(Utils.GetRowCoverage(row, sensor[i], beacon[i]));
    }
}

var leftBound = coverages.Min(x1 => x1.Item1);
var rightBound = coverages.Max(x2 => x2.Item2);

// assuming the entire row has coverage (i.e. does not contain the missing beacon)
var ans1 = rightBound - leftBound;
Console.WriteLine(ans1);

// part 2. find the missing beacon (i.e. single point without coverage)
const int mxRow = 4000000 + 1;
long ans2 = 0;
for (var r = 0; r < mxRow; r++)
{
    coverages.Clear();
    for (var i = 0; i < n; i++)
    {
        if (Utils.HasRowCoverage(r, sensor[i], beacon[i]))
        {
            coverages.Add(Utils.GetRowCoverage(r, sensor[i], beacon[i]));
        }
    }

    var (hasTotalCoverage, thisRowsCoverage) = Utils.HasTotalCoverage(coverages);
    if (!hasTotalCoverage)
    {
        foreach (var c in thisRowsCoverage)
        {
            Debug.Assert(thisRowsCoverage.Count == 2);
            Debug.Assert(thisRowsCoverage[1].Item1 - thisRowsCoverage[0].Item2 - 1 == 1);

            const long tuneFreq = 4000000;
            var col = thisRowsCoverage[0].Item2 + 1; // the missing beacon's location in the row
            ans2 = col * tuneFreq + r;
            //Console.WriteLine($"Found partial coverage for row {r} at column {col} ans = {ans2}");
        }
    }
}

Console.WriteLine(ans2);