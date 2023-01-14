namespace day15;

public static class Utils
{
    // ex) "Sensor at x=2, y=18:"
    public static (int, int) GetSensorCoords(string s)
    {
        var parts = s.Split(',');
        var x = int.Parse(parts[0].Split('=')[1]);
        var y = int.Parse(parts[1].Split('=')[1].Split(':')[0]);

        return (x, y);
    }

    // ex) "closest beacon is at x=-2, y=15"
    public static (int, int) GetBeaconCoords(string s)
    {
        var parts = s.Split(',');
        var x = int.Parse(parts[0].Split('=')[1]);
        var y = int.Parse(parts[1].Split('=')[1]);

        return (x, y);
    }

    public static void AddToGrid(Dictionary<(int, int), char> grid, (int, int) sensorTuple, (int, int) beaconTuple)
    {
        grid[sensorTuple] = 'S';
        grid[beaconTuple] = 'B';

        var dist = ManhattanDist(sensorTuple, beaconTuple);
        AddCoverage(grid, sensorTuple, dist);
    }

    private static void AddCoverage(IDictionary<(int, int), char> grid, (int, int) sensorTuple, int dist)
    {
        // let's do this from top to middle and bottom to middle
        var (x1, y1) = sensorTuple;
        var top = y1 - dist;
        var mid = y1;

        // top to middle
        var i = 0;
        for (var y = top; y <= mid; y++)
        {
            for (var x = x1 - i; x <= x1 + i; x++)
            {
                if (!grid.ContainsKey((x, y)))
                {
                    grid[(x, y)] = '#';
                }
            }

            i++;
        }

        // bottom to middle
        var bottom = y1 + dist;
        i = 0;
        for (var y = bottom; y > mid; y--)
        {
            for (var x = x1 - i; x <= x1 + i; x++)
            {
                if (!grid.ContainsKey((x, y)))
                {
                    grid[(x, y)] = '#';
                }
            }

            i++;
        }
    }

    private static int ManhattanDist((int, int) valueTuple, (int, int) valueTuple1)
    {
        var (x1, y1) = valueTuple;
        var (x2, y2) = valueTuple1;

        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    public static void PrintGrid(Dictionary<(int, int), char> grid)
    {
        var mnX = grid.Keys.Min(k => k.Item1) - 1;
        var mxX = grid.Keys.Max(k => k.Item1) + 1;
        var mnY = grid.Keys.Min(k => k.Item2) - 1;
        var mxY = grid.Keys.Max(k => k.Item2) + 1;

        for (var y = mnY; y <= mxY; y++)
        {
            for (var x = mnX; x <= mxX; x++)
            {
                if (grid.ContainsKey((x, y)))
                {
                    Console.Write(grid[(x, y)]);
                }
                else
                {
                    Console.Write(".");
                }
            }

            Console.WriteLine();
        }
    }
}