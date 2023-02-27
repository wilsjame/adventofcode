using System.Diagnostics;
using System.Reflection;
using Day16;

// set up stream reader to read input file
var assembly = Assembly.GetExecutingAssembly();
using var stream = assembly.GetManifestResourceStream("Day16.in.txt");
Debug.Assert(stream != null, nameof(stream) + " != null");
using var reader = new StreamReader(stream);

var graph = new Dictionary<string, IEnumerable<string>>();
var flowRates = new Dictionary<string, int>();

while (!reader.EndOfStream)
{
    var line = reader.ReadLine();
    if (!string.IsNullOrEmpty(line))
    {
        var node = Utils.GetNodeFromInput(line);
        graph[node] = Utils.GetAdjacentNodesFromInput(line);
        flowRates[node] = Utils.GetNodeFlowRateFromInput(line);
    }
}

const string startNode = "AA";
var nodePath = new Stack<string>();
nodePath.Push(startNode);

var mxNode = new List<string> { startNode }; // idk c# strings are immutable cries in c++
var totalFlow = 0;

Dictionary<string, bool> isOpen = new();

var minutesRemaining = 30;
while (minutesRemaining > 0)
{
    var currNode = nodePath.Peek();
    var shortestDistances = Utils.BfsGetShortestDistancesFrom(currNode, graph);

    var mxFlow = 0;
    foreach (var (node, _) in graph)
    {
        if (isOpen.ContainsKey(node))
            continue;

        var dist = shortestDistances[node];

        // dirty but if timeOn is negative it's too far away
        var timeOn = minutesRemaining - dist - 1; // -1 to turn on
        var flow = 0;
        if (dist > 0)
        {
            flow = (flowRates[node] * timeOn) / dist; // idk try flow distance ratio
        }

        if (flow > mxFlow)
        {
            mxNode.Clear();
            mxNode.Add(node);
            mxFlow = flow;
        }
    }

    if (mxFlow > 0 && !isOpen.ContainsKey(mxNode.First()))
    {
        isOpen[mxNode.First()] = true;
        nodePath.Push(mxNode.First());
        totalFlow += mxFlow * shortestDistances[mxNode.First()]; // don't use ratio


        minutesRemaining -= shortestDistances[mxNode.First()] + 1; // +1 to turn on
        mxNode.Clear();
    }
    else
    {
        // mxFlow is 0 we can't go anywhere
        break;
    }
}

Console.WriteLine(totalFlow);

// greedy on flow distance ratio?
// bfs O((nodes+edges) * nodes) => O(nodes^2)
// edge case >1 nodes have the same mx flow, which one do we choose?

// correct nodes visited just need to get the order right

// could prob brute force after looking at the input..
