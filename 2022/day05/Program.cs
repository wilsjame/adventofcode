using System.Diagnostics;
using System.Reflection;
using day05;
using static System.Int32;

// hard code the stacks of crates
var k1 = new Stack<char>(new[] { 'B', 'Q', 'C' });
var k2 = new Stack<char>(new[] { 'R', 'Q', 'W', 'Z' });
var k3 = new Stack<char>(new[] { 'B', 'M', 'R', 'L', 'V' });
var k4 = new Stack<char>(new[] { 'C', 'Z', 'H', 'V', 'T', 'W' });
var k5 = new Stack<char>(new[] { 'D', 'Z', 'H', 'B', 'N', 'V', 'G' });
var k6 = new Stack<char>(new[] { 'H', 'N', 'P', 'C', 'J', 'F', 'V', 'Q' });
var k7 = new Stack<char>(new[] { 'D', 'G', 'T', 'R', 'W', 'Z', 'S' });
var k8 = new Stack<char>(new[] { 'C', 'G', 'M', 'N', 'B', 'W', 'Z', 'P' });
var k9 = new Stack<char>(new[] { 'N', 'J', 'B', 'M', 'W', 'Q', 'F', 'P' });
var stacks = new List<Stack<char>> { k1, k2, k3, k4, k5, k6, k7, k8, k9 };

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day05.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file until empty line, we hard coded the first set of input so we can skip it
while (true)
{
    var line = reader.ReadLine();
    if (string.IsNullOrEmpty(line))
        break;
}

var ll = new List<List<int>>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        var moves = new List<int>();
        foreach (var s in line.Split(' ').ToList())
        {
            var ok = TryParse(s, out var n);
            if (ok)
                moves.Add(n);
        }
        
        ll.Add(moves);
    }
}

// part 1
foreach (var moves in ll)
{
    Utils.Move(stacks, moves);
}
var ans1 = Utils.GetTops(stacks);
Console.WriteLine(ans1);

// part 2
// hard code the stacks of crates (again) cause c# passes objects by reference and i don't want to deal wih that right now >_>
k1 = new Stack<char>(new[] { 'B', 'Q', 'C' });
k2 = new Stack<char>(new[] { 'R', 'Q', 'W', 'Z' });
k3 = new Stack<char>(new[] { 'B', 'M', 'R', 'L', 'V' });
k4 = new Stack<char>(new[] { 'C', 'Z', 'H', 'V', 'T', 'W' });
k5 = new Stack<char>(new[] { 'D', 'Z', 'H', 'B', 'N', 'V', 'G' });
k6 = new Stack<char>(new[] { 'H', 'N', 'P', 'C', 'J', 'F', 'V', 'Q' });
k7 = new Stack<char>(new[] { 'D', 'G', 'T', 'R', 'W', 'Z', 'S' });
k8 = new Stack<char>(new[] { 'C', 'G', 'M', 'N', 'B', 'W', 'Z', 'P' });
k9 = new Stack<char>(new[] { 'N', 'J', 'B', 'M', 'W', 'Q', 'F', 'P' });
stacks = new List<Stack<char>> { k1, k2, k3, k4, k5, k6, k7, k8, k9 };

foreach (var moves in ll)
{
    Utils.Move2(stacks, moves);
}
var ans2 = Utils.GetTops(stacks);
Console.WriteLine(ans2);