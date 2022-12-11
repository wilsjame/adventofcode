using System.Diagnostics;
using System.Reflection;
using day07;
using static System.Math;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("day07.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file into list of strings
var input = new List<string>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (line != null)
    {
        input.Add(line);
    }
}

var ptr = 0;
var dirStack = new Stack<string>();
var dirContents = new Dictionary<string, List<string>>();

while (ptr < input.Count)
{
    var command = input[ptr];
    Debug.Assert(Utils.IsCommand(command));
    
    if (Utils.IsCd(command))
    {
        if (command.Contains(".."))
        {
            dirStack.Pop();
        }
        else
        {
            dirStack.Push(Utils.GetNewDir(command));
        }
        ptr++;
    }
    else if (Utils.IsLs(command))
    {
        var dirList = Utils.GetDirList(ptr, input);
        var currDir = Utils.GetCurrDir(dirStack);
        
        if (!dirContents.ContainsKey(currDir))
        {
            dirContents[currDir] = dirList;
        }
        ptr += dirList.Count + 1;
    }
}


// part 1
var memo = new Dictionary<string, int>();

var ans1 = 0;
foreach (var (dir, _) in dirContents)
{
    memo.Clear();
    var sz = Utils.GetDirMemory(dir, dirContents, memo);
    if (sz <= 100000)
    {
        ans1 += sz;
    }
}
Console.WriteLine(ans1);

// part 2
const int total = 70000000;
memo.Clear();
var used = Utils.GetDirMemory("//", dirContents, memo);
var unused = total - used;
var need = 30000000 - unused;

var ans2 = int.MaxValue;
foreach (var (dir, _) in dirContents)
{
    memo.Clear();
    var sz = Utils.GetDirMemory(dir, dirContents, memo);
    if (sz >= need)
    {
        ans2 = Min(sz, ans2);
    }
}
Console.WriteLine(ans2);