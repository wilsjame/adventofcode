using System.Diagnostics;
using System.Reflection;
using day09;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day09.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file
var moves = new List<(char, int)>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        var dir = Convert.ToChar(line.Split(" ").First());
        var cnt = Convert.ToInt32(line.Split(" ").Last());
        moves.Add((dir, cnt));
    }
}

// part 1
var visitH = new Dictionary<(int, int), int>();
var visitT = new Dictionary<(int, int), int>();
var posH = (0, 0);
var posT = (0, 0);

visitH[posH] = 1;
visitT[posT] = 1;

Utils.Play1(moves, visitH, visitT, posH, posT);
Console.WriteLine(visitT.Count);

// part 2
posH = (0, 0);
visitH.Clear();

// forgive me
var posT1 = (0, 0); 
var posT2 = (0, 0);
var posT3 = (0, 0);
var posT4 = (0, 0);
var posT5 = (0, 0);
var posT6 = (0, 0);
var posT7 = (0, 0);
var posT8 = (0, 0);
var posT9 = (0, 0);
var visitT1 = new Dictionary<(int, int), int>();
var visitT2 = new Dictionary<(int, int), int>();
var visitT3 = new Dictionary<(int, int), int>();
var visitT4 = new Dictionary<(int, int), int>();
var visitT5 = new Dictionary<(int, int), int>();
var visitT6 = new Dictionary<(int, int), int>();
var visitT7 = new Dictionary<(int, int), int>();
var visitT8 = new Dictionary<(int, int), int>();
var visitT9 = new Dictionary<(int, int), int>();
visitT1[posT1] = 1;
visitT2[posT2] = 1;
visitT3[posT3] = 1;
visitT4[posT4] = 1;
visitT5[posT5] = 1;
visitT6[posT6] = 1;
visitT7[posT7] = 1;
visitT8[posT8] = 1;
visitT9[posT9] = 1;

Utils.Play2(
    moves,
    visitH,
    visitT1,
    visitT2,
    visitT3,
    visitT4,
    visitT5,
    visitT6,
    visitT7,
    visitT8,
    visitT9,
    posH,
    posT1,
    posT2,
    posT3,
    posT4,
    posT5,
    posT6,
    posT7,
    posT8,
    posT9);

Console.WriteLine(visitT9.Count);