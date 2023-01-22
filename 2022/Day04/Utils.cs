namespace day04;

public static class Utils
{
    public static bool HasContained((int, int) pr1, (int, int) pr2)
    {
        return (pr1.Item1 >= pr2.Item1 && pr1.Item2 <= pr2.Item2) 
               || (pr2.Item1 >= pr1.Item1 && pr2.Item2 <= pr1.Item2);
    }
    
    public static bool HasOverlap((int, int) pr1, (int, int) pr2)
    {
        var l1 = Enumerable.Range(pr1.Item1, pr1.Item2 - pr1.Item1 + 1).ToList();
        var l2 = Enumerable.Range(pr2.Item1, pr2.Item2 - pr2.Item1 + 1).ToList();
        
        // check if any element in l2 is in l1
        return l2.Any(l1.Contains);
    }
}