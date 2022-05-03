string[] lines = File.ReadAllLines("day01.in");

int ans1 = 0;
int ans2 = 0;

foreach (string line in lines)
{
    try
    {
        int n = Int32.Parse(line);

        // Part 1. 
        ans1 += (n / 3) - 2;

        // Part 2.
        while (n > 0) 
        {
            n = (n / 3) - 2;
            ans2 += Math.Max(0, n);
        }
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
    }
}

Console.WriteLine(ans1);
Console.WriteLine(ans2);

