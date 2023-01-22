namespace Day14;

public static class Utils
{
    public static readonly (int, int) SandOrigin = (500, 0);

    public static void BuildRockPath(List<List<(int, int)>> input, Dictionary<(int, int), char> rockPath)
    {
        //rockPath.Add(SandOrigin, '+');
        
        // rocks
        foreach (var path in input)
        {
            var n = path.Count;
            for (var i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    // start point
                    var (x, y) = path[i];
                    rockPath[(x, y)] = '#';
                }
                else
                {
                    // draw line from prev point to curr point
                    var (x1, y1) = path[i - 1];
                    var (x2, y2) = path[i];
                    DrawLine((x1, y1), (x2, y2), rockPath);
                }
            }
        }
    }

    public static void DrawLine((int x1, int y1) valueTuple, (int x2, int y2) valueTuple2, Dictionary<(int, int), char> rockPath)
    {
        var (x1, y1) = valueTuple;
        var (x2, y2) = valueTuple2;
        if (x1 == x2)
        {
            // horizontal line
            for (var k = Math.Min(y1, y2); k <= Math.Max(y1, y2); k++)
            {
                rockPath[(x1, k)] = '#';
            }
        }
        else if (y1 == y2)
        {
            // vertical line
            for (var k = Math.Min(x1, x2); k <= Math.Max(x1, x2); k++)
            {
                rockPath[(k, y1)] = '#';
            }
        }
        else
        {
            // should not happen
            throw new Exception("Invalid line");
        }
    }
    
    public static void PrintRockPath(Dictionary<(int, int), char> dictionary)
    {
        var minX = dictionary.Keys.Min(k => k.Item1);
        var maxX = dictionary.Keys.Max(k => k.Item1);
        var minY = dictionary.Keys.Min(k => k.Item2);
        var maxY = dictionary.Keys.Max(k => k.Item2);
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                // if (x, y) not in rockPath, print empty space
                if (!dictionary.ContainsKey((x, y)))
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(dictionary[(x, y)]);
                }
            }

            Console.WriteLine();
        }
        
    }

    public static int PourSand((int, int) pos, int depth, Dictionary<(int, int), char> rockPath)
    {
        var mxDepth = rockPath.Keys.Max(k => k.Item2);
        if (depth > mxDepth)
        {
            // sand reached the abyss
            return depth;
        }
        
        if (CanPourDown(pos, rockPath))
        {
            pos.Item2++;
        }
        else if (CanPourDownLeft(pos, rockPath))
        {
            pos.Item1--;
            pos.Item2++;

        }
        else if (CanPourDownRight(pos, rockPath))
        {
            pos.Item1++;
            pos.Item2++;
        }
        else
        {
            // sand has settled
            rockPath[pos] = 'o';
            return depth;
        }

        depth++;
        return PourSand(pos, depth, rockPath);
    }

    private static bool CanPourDown((int, int) pos, Dictionary<(int, int), char> rockPath)
    {
        var testPos = (pos.Item1, pos.Item2 + 1);
        return !rockPath.ContainsKey(testPos);
    }

    private static bool CanPourDownLeft((int, int) pos, Dictionary<(int, int), char> rockPath)
    {
        var testPos = (pos.Item1 - 1, pos.Item2 + 1);
        return !rockPath.ContainsKey(testPos);
    }

    private static bool CanPourDownRight((int, int) pos, Dictionary<(int, int), char> rockPath)
    {
        var testPos = (pos.Item1 + 1, pos.Item2 + 1);
        return !rockPath.ContainsKey(testPos);
    }
}