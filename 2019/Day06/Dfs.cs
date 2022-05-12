namespace day06;

public class Dfs
{
    private Dictionary<string, List<string>> adj;

    public Dfs(Dictionary<string, List<string>> graph)
    {
        adj = graph;
    }
    
    // Returns the path from s to "COM" in the adj graph.
    public List<string> Search(string s, List<string> path)
    {
        
        if (s == "COM")
        {
            return path;
        }

        foreach (string u in adj[s])
        {
            path.Add(u);
            Search(u, path);
        }

        return path;
    }
    
}

