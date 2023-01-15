using System.Diagnostics;

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

    // ex) " closest beacon is at x=-2, y=15"
    public static (int, int) GetBeaconCoords(string s)
    {
        var parts = s.Split(',');
        var x = int.Parse(parts[0].Split('=')[1]);
        var y = int.Parse(parts[1].Split('=')[1]);

        return (x, y);
    }

    private static int ManhattanDist((int, int) valueTuple, (int, int) valueTuple1)
    {
        var (x1, y1) = valueTuple;
        var (x2, y2) = valueTuple1;

        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    // calculate a row coverage given a sensor beacon pair
    public static (int, int) GetRowCoverage(int row, (int, int) sensor, (int, int) beacon)
    {
        Debug.Assert(HasRowCoverage(row, sensor, beacon));
        var sensorRow = sensor.Item2;
        var radius = ManhattanDist(sensor, beacon);

        var fromSensorRow = Math.Abs(row - sensorRow);
        var rowCoverage = radius - fromSensorRow;

        var sensorCol = sensor.Item1;
        var leftBound = sensorCol - rowCoverage;
        var rightBound = sensorCol + rowCoverage;

        return (leftBound, rightBound);
    }

    public static bool HasRowCoverage(int row, (int, int) sensor, (int, int) beacon)
    {
        var sensorRow = sensor.Item2;
        var radius = ManhattanDist(sensor, beacon);

        return WithinRadius(row, sensorRow, radius);
    }

    private static bool WithinRadius(int row, int center, int radius)
    {
        var topRow = center - radius;
        var bottomRow = center + radius;

        return row <= bottomRow && row >= topRow;
    }

    // return value is not clean, but it works :}
    public static (bool, List<(int, int)>) HasTotalCoverage(List<(int, int)> coverages)
    {
        // merge overlapping coverages
        var needsMerge = true;
        while (needsMerge)
        {
            needsMerge = false;
            for (var i = 0; i < coverages.Count; i++)
            {
                for (var j = i + 1; j < coverages.Count; j++)
                {
                    if (HasOverlap(coverages[i], coverages[j]))
                    {
                        var merged = Merge(coverages[i], coverages[j]);
                        coverages[i] = merged;
                        coverages.RemoveAt(j);
                        needsMerge = true;
                        i = 0;
                        break;
                    }
                }
            }
        }

        // now coverages should be non-overlapping
        return coverages.Count == 1 ? (true, new List<(int, int)>()) : (false, coverages);
    }

    private static (int, int) Merge((int, int) pr1, (int, int) pr2)
    {
        var (left1, right1) = pr1;
        var (left2, right2) = pr2;

        return (Math.Min(left1, left2), Math.Max(right1, right2));
    }

    private static bool HasOverlap((int, int) pr1, (int, int) pr2)
    {
        var (left1, right1) = pr1;
        var (left2, right2) = pr2;

        /*
         * 1. pr1 is completely to the left of pr2
         * 2. pr1 is completely to the right of pr2
         * 3. they overlap
         */
        var pr1LeftOfPr2 = right1 < left2;
        var pr1RightOfPr2 = left1 > right2;
        var overlap = !pr1LeftOfPr2 && !pr1RightOfPr2;

        return overlap;
    }

    // following methods for printing sample data (real data too large) 
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