List<int> arr = File.ReadAllLines("day02.in")[0].Split(',').Select(x => int.Parse(x)).ToList();

// intcode's first two parameters change the
// start values in arr, and these will be brute
// forced in part 2. intcode expects new copies
// of arr every time. 
int intcode(int a, int b, List<int> arr)
{
    arr[1] = a;
    arr[2] = b;
    int pos = 0;

    while (true)
    {
        int op = arr[pos];

        if (op == 99)
        {
            break;
        }
        else
        {
            int v1 = arr[arr[pos+1]];
            int v2 = arr[arr[pos+2]];
            int ptr = arr[pos+3];

            if (op == 1)
            {
                arr[ptr] = v1 + v2;
            }
            else if (op == 2)
            {
                arr[ptr] = v1 * v2;
            }
        }

        pos += 4;
    }

    return arr[0];
}

// Part 1. 
int ans1 = intcode(12, 2, new List<int>(arr));
Console.WriteLine(ans1);

// Part 2.
for (int a = 0; a <= 99; a++)
{
    for (int b = 0; b <= 99; b++)
    {
        if (intcode(a, b, new List<int>(arr)) == 19690720)
        {
            int ans2 = 100 * a + b;
            Console.Write(ans2);
            return;
        }
    }
}

