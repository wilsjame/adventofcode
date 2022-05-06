// Read input into two lists of tuples; one for each player.
// Each tuple contains two items: direction and distance.
// Since the input size isn't too large we can brute force.
// For each player we store the spaces they walked on and the number
// of steps it took for them to get there.

List<Tuple<char, int>> p1 = new();

foreach (string s in File.ReadAllLines("day03.in")[0].Split(',').ToList())
{
    char dir = s[0];
    int n = int.Parse(s.Substring(1));

    p1.Add(Tuple.Create(dir, n));
}

Dictionary<Tuple<int, int>, int> mp1 = new();
int x = 0;
int y = 0;
int steps = 0;

foreach (Tuple<char, int> pr in p1)
{
    char dir = pr.Item1;
    int n = pr.Item2;

    for (int k = 0; k < n; k++)
    {
        steps += 1;

        if (dir == 'U')
        {
            y += 1;
        }
        else if (dir == 'R')
        {
            x += 1;
        }
        else if (dir == 'D')
        {
            y -= 1;
        }
        else if (dir == 'L')
        {
            x -= 1;
        }

        mp1.TryAdd(Tuple.Create(x, y), steps);
    }
}

List<Tuple<char, int>> p2 = new();

foreach (string s in File.ReadAllLines("day03.in")[1].Split(',').ToList())
{
    char dir = s[0];
    int n = int.Parse(s.Substring(1));
    p2.Add(Tuple.Create(dir, n));
}

Dictionary<Tuple<int, int>, int> mp2 = new();
x = 0;
y = 0;
steps = 0;

foreach (Tuple<char, int> pr in p2)
{
    char dir = pr.Item1;
    int n = pr.Item2;

    for (int k = 0; k < n; k++)
    {
        steps += 1;

        if (dir == 'U')
        {
            y += 1;
        }
        else if (dir == 'R')
        {
            x += 1;
        }
        else if (dir == 'D')
        {
            y -= 1;
        }
        else if (dir == 'L')
        {
            x -= 1;
        }

        mp2.TryAdd(Tuple.Create(x, y), steps);
    }
}

// Part 1. Minimum manhattan distance from intersection to origin.
// Part 2. Intersection with the least steps to reach by both players.

int ans1 = int.MaxValue;
int ans2 = int.MaxValue;

foreach (KeyValuePair<Tuple<int, int>, int> kvp in mp1)
{

    if (mp2.ContainsKey(kvp.Key))
    {
        ans1 = Math.Min(ans1, Math.Abs(kvp.Key.Item1) + Math.Abs(kvp.Key.Item2));

        int d1 = kvp.Value;
        int d2 = mp2[kvp.Key];

        ans2 = Math.Min(ans2, d1+d2);
    }

}

Console.WriteLine(ans1);
Console.WriteLine(ans2);

