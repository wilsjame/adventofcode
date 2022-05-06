List<int> arr = File.ReadAllLines("day04.in")[0].Split('-').Select(x => int.Parse(x)).ToList();

// Returns true if number n has (atleast) two adjacent
// digits that are the same. False otherwise.
bool two_adj(int n)
{
    string digits = n.ToString();

    for (int i = 0; i+1 < digits.Count(); i++)
    {

        if (digits[i] == digits[i+1])
        {
            return true;
        }

    }

    return false;
}

// Returns true if digits from left to right
// in number never decrease. False otherwise.
bool never_dec(int n)
{
    string digits = n.ToString();

    for (int i = 0; i+1 < digits.Count(); i++)
    {

        if (digits[i+1] < digits[i])
        {
            return false;
        }

    }

    return true;
}

// Part 1. 

int ans1 = 0;

for (int i = arr[0]; i <= arr[1]; i++)
{

    if (two_adj(i) && never_dec(i))
    {
        ans1 += 1;
    }

}

Console.WriteLine(ans1);

// Part 2.

// Returns true if number n has exactly two adjacent
// that are the same. False otherwise.
bool two_adj2(int n)
{
    string digits = n.ToString();

    for (int i = 0; i+1 < digits.Count(); i++)
    {

        if (digits[i] == digits[i+1])
        {
            // Make sure element before i-1 is different and
            // element after i+2 is different.
            
            if (i-1 < 0 && digits[i+2] != digits[i])
            {
                return true;
            }
            else if (i+2 > digits.Count()-1 && digits[i-1] != digits[i])
            {
                return true;
            }
            else if (i > 0 && digits[i-1] != digits[i] && digits[i+2] != digits[i])
            {
                return true;
            }

        }

    }

    return false;
}

int ans2 = 0;

for (int i = arr[0]; i <= arr[1]; i++)
{

    if (two_adj2(i) && never_dec(i))
    {
        ans2 += 1;
    }

}

Console.WriteLine(ans2);

