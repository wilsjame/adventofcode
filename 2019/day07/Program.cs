using day07;

var arr = File.ReadAllLines("day07.in")[0].Split(",").Select(x => int.Parse(x)).ToList();

// search() generates all permutations of a number of length r - l + 1
// consisting of digits [l, r] and adds it to the phase list. 
var chosen = new bool[10];
var permutation = new List<int>();

void search(int l, int r, List<List<int>> phase)
{
    if (permutation.Count == r - l + 1)
    {
        // process permutation
        phase.Add(new List<int>(permutation));
    }
    else
    {
        for (var i = l; i < r + 1; i++)
        {
            if (chosen[i]) continue;
            permutation.Add(i);
            chosen[i] = true;
            search(l, r, phase);
            chosen[i] = false;
            permutation.RemoveAt(permutation.Count - 1);
        }
    }
}

// preprocess permutations
var phaseA = new List<List<int>>(); // holds permutations of digits [0, 4]
var phaseB = new List<List<int>>(); // holds permutations of digits [5, 9]

search(0, 4, phaseA);
search(5, 9, phaseB);

// Part 1.
var mx = 0;

foreach (var p in phaseA)
{
    var st = new Stack<int>(p);
    var last_out = 0;

    while (st.Count > 0)
    {
        var a = st.Pop();
        var b = last_out;

        IntCode bot = new IntCode(new List<int>(arr), a, b);
        last_out = bot.Run();
        
        mx = Math.Max(mx, last_out);
    }
}

Console.WriteLine($"mx {mx}");

// Part 2. 
// first loop [0, 4] second loop [5, 9]