namespace day08;

public static class Utils
{
    private static bool IsVisible(int i, int j, List<List<int>> ll)
    {
        var currHeight = ll[i][j];
        
        // scan up
        var isVisUp = true;
        for (var k = i - 1; k >= 0; k--)
        {
            if (ll[k][j] >= currHeight)
            {
                isVisUp = false;
                break;
            } 
        }
        // scan down
        var isVisDown = true;
        for (var k = i + 1; k < ll.Count; k++)
        {
            if (ll[k][j] >= currHeight)
            {
                isVisDown = false;
                break;
            } 
        }
        // scan left
        var isVisLeft = true;
        for (var k = j - 1; k >= 0; k--)
        {
            if (ll[i][k] >= currHeight)
            {
                isVisLeft = false;
                break;
            } 
        }
        // scan right
        var isVisRight = true;
        for (var k = j + 1; k < ll[i].Count; k++)
        {
            if (ll[i][k] >= currHeight)
            {
                isVisRight = false;
                break;
            } 
        }

        return isVisUp || isVisDown || isVisLeft || isVisRight;
    }
    
    public static int Solve1(List<List<int>> ll)
    {
        var ans = 0;
        for (var i = 0; i < ll.Count; i++)
        {
            for (var j = 0; j < ll.First().Count; j++)
            {
                if (IsVisible(i, j, ll))
                {
                    ans += 1;
                }
            }
        }

        return ans;
    }
    
    private static int ScenicScore(int i, int j, List<List<int>> ll)
    {
        var currHeight = ll[i][j];
        
        // scan up
        var scoreUp = 0;
        for (var k = i - 1; k >= 0; k--)
        {
            scoreUp++;
            if (ll[k][j] >= currHeight)
            {
                break;
            } 
        }
        // scan down
        var scoreDown = 0;
        for (var k = i + 1; k < ll.Count; k++)
        {
            scoreDown++;
            if (ll[k][j] >= currHeight)
            {
                break;
            } 
        }
        // scan left
        var scoreLeft = 0;
        for (var k = j - 1; k >= 0; k--)
        {
            scoreLeft++;
            if (ll[i][k] >= currHeight)
            {
                break;
            } 
        }
        // scan right
        var scoreRight = 0;
        for (var k = j + 1; k < ll[i].Count; k++)
        {
            scoreRight++;
            if (ll[i][k] >= currHeight)
            {
                break;
            } 
        }

        return scoreUp * scoreDown * scoreLeft * scoreRight;
    }
    
    public static int Solve2(List<List<int>> ll)
    {
        var ans = 0;
        for (var i = 0; i < ll.Count; i++)
        {
            for (var j = 0; j < ll.First().Count; j++)
            {
                var localScore = ScenicScore(i, j, ll);
                ans = Math.Max(ans, localScore);
            }
        }

        return ans;
    }
}