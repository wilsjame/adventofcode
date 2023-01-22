namespace Day03;

public static class Utils
{
    public static int GetValue(Dictionary<char, int> d)
    {
        var ans = 0;
        foreach (var (key, value) in d)
        {
            if (char.IsLower(key))
            {
                ans += (key - 'a' + 1) * value;
            }
            else if (char.IsUpper(key))
            {
                ans += (key - 'A' + 27) * value;
            }
        }

        return ans;
    }
}