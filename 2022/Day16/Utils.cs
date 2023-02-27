using System.Diagnostics;

namespace Day16;

public static class Utils
{
    // input line: "Valve AA has flow rate=0; tunnels lead to valves DD, II, BB"

    public static string GetNodeFromInput(string line)
    {
        return line.Split(' ')[1];
    }

    public static int GetNodeFlowRateFromInput(string line)
    {
        // get number between '=' and ';'
        var arr = line.Split('=');
        var arr2 = arr[1].Split(';');

        return int.Parse(arr2[0]);
    }

    public static IEnumerable<string> GetAdjacentNodesFromInput(string line)
    {
        var adjNodes = new List<string>();
        if (line.Contains("valve "))
        {
            var arr = line.Split("valve ");
            adjNodes.Add(arr[1]);
        }
        else
        {
            Debug.Assert(line.Contains("valves"));
            var arr = line.Split("valves ");
            var arr2 = arr[1].Split(',');
            adjNodes.AddRange(arr2.Select(node => node.Trim()));
        }

        return adjNodes;
    }

    public static IDictionary<string, int> BfsGetShortestDistancesFrom(
        string startNode,
        Dictionary<string, IEnumerable<string>> graph)
    {
        Dictionary<string, bool> visited = new();
        Dictionary<string, int> distance = new()
        {
            [startNode] = 0
        };

        Queue<string> queue = new();
        queue.Enqueue(startNode);
        while (queue.Any())
        {
            var currNode = queue.Dequeue();
            visited[currNode] = true;

            foreach (var adjNode in graph[currNode])
            {
                if (!visited.ContainsKey(adjNode))
                {
                    distance[adjNode] = distance[currNode] + 1;
                    queue.Enqueue(adjNode);
                }
            }
        }

        return distance;
    }
}