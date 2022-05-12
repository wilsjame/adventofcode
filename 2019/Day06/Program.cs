using day06;
    
List<string> lines = File.ReadAllLines("day06.in").ToList();

Dictionary<string, List<string>> adj = new();
HashSet<string> myset = new();

foreach (string line in lines)
{
    string u = line.Split(')')[0];
    string v = line.Split(')')[1];
    
    if (adj.ContainsKey(v))
    {
        adj[v].Add(u);
    }
    else
    {
        adj[v] = new List<string> { u };
    }

    myset.Add(u);
    myset.Add(v);
}

Dfs dfs = new Dfs(adj);

// Part 1.

int ans1 = 0;

foreach (string u in myset)
{
    List<string> path = dfs.Search(u, new List<string>());
    ans1 += path.Count;
}

Console.WriteLine(ans1);

// Part 2.

List<string> path1 = dfs.Search("YOU", new List<string>());
List<string> path2 = dfs.Search("SAN", new List<string>());
string meet = null;

foreach (string u in path1)
{
    
    if (path2.Contains(u))
    {
        // Where path1 and path2 meet.
        meet = u;
        break;
    }
    
}

int d1 = path1.IndexOf(meet);
int d2 = path2.IndexOf(meet);
int ans2 = d1 + d2;
Console.WriteLine(ans2);
