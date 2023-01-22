using System.Diagnostics;

namespace day09;

public static class Utils
{
    private static void MoveH(
        int dir,
        IDictionary<(int, int), int> visitH,
        ref (int, int) posH)
    {
        switch (dir)
        {
            case 'L':
                posH.Item2 -= 1;
                break;
            case 'R':
                posH.Item2 += 1;
                break;
            case 'U':
                posH.Item1 -= 1;
                break;
            case 'D':
                posH.Item1 += 1;
                break;
        }
        
        if (visitH.ContainsKey(posH))
        {
            visitH[posH] += 1;
        }
        else
        {
            visitH[posH] = 1;
        }
    }

    private static void MoveT(
        (int, int) posH,
        IDictionary<(int, int), int> visitT,
        ref (int, int) posT)
    {
        if (IsNear(posH, posT))
            return;
        
        if (IsDiagonal(posH, posT))
        {
            // top right corner
            if (posH.Item1 < posT.Item1 && posH.Item2 > posT.Item2)
            {
                posT.Item1 -= 1;
                posT.Item2 += 1;
            }
            // bottom right corner
            else if (posH.Item1 > posT.Item1 && posH.Item2 > posT.Item2)
            {
                posT.Item1 += 1;
                posT.Item2 += 1;
            }
            // bottom left corner
            else if (posH.Item1 > posT.Item1 && posH.Item2 < posT.Item2)
            {
                posT.Item1 += 1;
                posT.Item2 -= 1;
            }
            // top left corner
            else if (posH.Item1 < posT.Item1 && posH.Item2 < posT.Item2)
            {
                posT.Item1 -= 1;
                posT.Item2 -= 1;
            }
        }
        else
        {
            // same row
            if (posH.Item1 == posT.Item1)
            {
                if (posH.Item2 > posT.Item2)
                {
                    posT.Item2 += 1;
                }
                else
                {
                    posT.Item2 -= 1;
                }
            }
            // same col
            else
            {
                if (posH.Item1 > posT.Item1)
                {
                    posT.Item1 += 1;
                }
                else
                {
                    posT.Item1 -= 1;
                }
            }
        }
        
        if (visitT.ContainsKey(posT))
        {
            visitT[posT] += 1;
        }
        else
        {
            visitT[posT] = 1;
        }
    }

    private static bool IsDiagonal((int, int) posH, (int, int) postT)
    {
        var (x1, y1) = posH;
        var (x2, y2) = postT;
        
        return x1 != x2 && y1 != y2;
    }
    
    private static bool IsNear((int, int) posH, (int, int) posT)
    {
        return Math.Abs(posH.Item1 - posT.Item1) < 2 && Math.Abs(posH.Item2 - posT.Item2) < 2;
    } 
    
    public static void Play1(
        List<(char, int)> move,
        Dictionary<(int, int), int> visitH, 
        Dictionary<(int, int), int> visitT,
        (int, int) posH, 
        (int, int) posT)
    {
        foreach (var (dir, cnt) in move)
        {
            for (var k = 0; k < cnt; k++)
            {
                MoveH(dir, visitH, ref posH);
                MoveT(posH, visitT, ref posT);
                Debug.Assert(IsNear(posH, posT));
            }
        }
    }

    // oh my god
    public static void Play2(
        List<(char, int)> move,
        Dictionary<(int, int), int> visitH,
        Dictionary<(int, int), int> visitT1,
        Dictionary<(int, int), int> visitT2,
        Dictionary<(int, int), int> visitT3,
        Dictionary<(int, int), int> visitT4,
        Dictionary<(int, int), int> visitT5,
        Dictionary<(int, int), int> visitT6,
        Dictionary<(int, int), int> visitT7,
        Dictionary<(int, int), int> visitT8,
        Dictionary<(int, int), int> visitT9,
        (int, int) posH,
        (int, int) posT1,
        (int, int) posT2,
        (int, int) posT3,
        (int, int) posT4,
        (int, int) posT5,
        (int, int) posT6,
        (int, int) posT7,
        (int, int) posT8,
        (int, int) posT9)
    {
        foreach(var (dir, cnt) in move)
        {
            for (var k = 0; k < cnt; k++)
            {
                MoveH(dir, visitH, ref posH);
                MoveT(posH, visitT1, ref posT1);
                MoveT(posT1, visitT2, ref posT2);
                MoveT(posT2, visitT3, ref posT3);
                MoveT(posT3, visitT4, ref posT4);
                MoveT(posT4, visitT5, ref posT5);
                MoveT(posT5, visitT6, ref posT6);
                MoveT(posT6, visitT7, ref posT7);
                MoveT(posT7, visitT8, ref posT8);
                MoveT(posT8, visitT9, ref posT9);
            }
        }
    }
}