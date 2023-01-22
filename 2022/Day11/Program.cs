using System.Diagnostics;
using System.Reflection;
using Day11;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day11.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

// read input file, create monkeys
var monkeys = new List<Monkey>();
var monkeys2 = new List<Monkey>();
while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (!string.IsNullOrEmpty(line))
    {
        var id = line
            .Split(" ")
            .Last()
            .TrimEnd(':')
            .Select(x => int.Parse(x.ToString()))
            .First();
        
        var monkey = Monkey.Read(reader, id);
        
        monkeys.Add(monkey);
        monkeys2.Add(monkey.DeepCopy());
    }
}

// part 1
Monkey.Play(monkeys, 20);
var topMonkeys = monkeys
    .OrderByDescending(x => x.InspectCount)
    .ToList();
var ans = topMonkeys[0].InspectCount * topMonkeys[1].InspectCount;
Console.WriteLine(ans);

// part 2
Monkey.Play2(monkeys2, 10000, Monkey.CommonDenominator(monkeys2));
var topMonkeys2 = monkeys2
    .OrderByDescending(x => x.InspectCount)
    .ToList();
var ans2 = (long)topMonkeys2[0].InspectCount * topMonkeys2[1].InspectCount;
Console.WriteLine(ans2);