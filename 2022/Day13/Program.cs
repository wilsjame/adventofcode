using System.Diagnostics;
using System.Reflection;
using Day13;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day13.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

var input = new List<(string, string)>();
while (!reader.EndOfStream)
{
    var line1 = reader.ReadLine();
    var line2 = reader.ReadLine();
    
    if (!string.IsNullOrEmpty(line1) && !string.IsNullOrEmpty(line2))
    {
        input.Add((line1, line2));
    }
    
    // skip empty line
    reader.ReadLine();
}

// part 1
var ans1 = 0;
var i = 1;
foreach (var (line1, line2) in input)
{
    var l1 = Utils.GetLists(line1);
    var l2 = Utils.GetLists(line2);
    
    var inOrder = Utils.CompareList(l1, l2);
    if (inOrder == 1)
    {
        ans1 += i;
    }
    Console.WriteLine($"Pair {i++}: {inOrder}, ans1: {ans1}");
}

// part 2
var allLists = new List<List<object>>();

// dividers from problem statement
var divider1 = new List<object> { new List<object>{2} };
allLists.Add(divider1);
var divider2 = new List<object> { new List<object>{6} };
allLists.Add(divider2);

foreach (var (line1, line2) in input)
{
    var l1 = Utils.GetLists(line1);
    allLists.Add(l1);
    var l2 = Utils.GetLists(line2);
    allLists.Add(l2);
}

Utils.BubbleSort(allLists);

var dividerIdx1 = Utils.FindIndex(allLists, divider1);
var dividerIdx2 = Utils.FindIndex(allLists, divider2);

var ans2 = (dividerIdx1 + 1) * (dividerIdx2 + 1);  
Console.WriteLine($"{ans2}");