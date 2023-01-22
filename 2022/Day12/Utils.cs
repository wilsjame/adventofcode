namespace Day12;

public static class Utils
{
    public static (int, int) GetStart(List<List<char>> graph)
    {
        var ans = (0, 0);
        for (var i = 0; i < graph.Count; i++)
        {
            for (var j = 0; j < graph[i].Count; j++)
            {
                if (graph[i][j] == 'S')
                {
                    ans = (i, j);
                }
            }
        }

        return ans;
    }
    
    public static List<(int, int)> GetStarts(List<List<char>> graph)
    {
        var ans = new List<(int, int)>();
        for (var i = 0; i < graph.Count; i++)
        {
            for (var j = 0; j < graph[i].Count; j++)
            {
                if (graph[i][j] == 'S' || graph[i][j] == 'a')
                {
                    ans.Add((i, j));
                }
            }
        }

        return ans;
    }

    public static (int, int) GetEnd(List<List<char>> graph)
    {
        var ans = (0, 0);
        for (var i = 0; i < graph.Count; i++)
        {
            for (var j = 0; j < graph[i].Count; j++)
            {
                if (graph[i][j] == 'E')
                {
                    ans = (i, j);
                }
            }
        }

        return ans;
    }
    
    private static bool Ok(IReadOnlyList<List<char>> graph, (int, int) curr, (int x, int y) next)
    {
        var a = graph[curr.Item1][curr.Item2];
        a = a == 'S' ? 'a' : a;
        
        var b = graph[next.Item1][next.Item2];
        b = b == 'E' ? 'z' : b;
        
        return b - a <= 1;
    }
    
    private static IEnumerable<(int, int)> GetAdj(List<List<char>> graph, (int, int) curr)
    {
        // up right down left
        var dx = new[] { -1, 0, 1, 0 };
        var dy = new[] { 0, 1, 0, -1 };
        
        for (var i = 0; i < 4; i++)
        {
            var x = curr.Item1 + dx[i];
            var y = curr.Item2 + dy[i];
            if (x >= 0 && x < graph.Count && y >= 0 && y < graph[x].Count && Ok(graph, curr, (x, y)))
            {
                yield return (x, y);
            }
        }
    }

    public static void Bfs(
        List<List<char>> graph, 
        Dictionary<(int, int), bool> visited, 
        Dictionary<(int, int), int> distance,
        (int, int) start)
    {
        var queue = new Queue<(int, int)>();
        
        queue.Enqueue(start);
        visited[start] = true;
        distance[start] = 0;
        
        while (queue.Count > 0)
        {
            var curr = queue.Dequeue();
            foreach (var adj in GetAdj(graph, curr))
            {
                var visitedAdj = visited.ContainsKey(adj) && visited[adj];
                if (!visitedAdj)
                {
                    visited[adj] = true;
                    distance[adj] = distance[curr] + 1;
                    queue.Enqueue(adj);
                }
            }
        }
    }
}