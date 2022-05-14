using day07;

List<int> arr = File.ReadAllLines("day07.in")[0].Split(",").Select(x => int.Parse(x)).ToList();

// Part 1.

int mx = 0;

// Variables used by search().
List<bool> chosen = new List<bool>();

int n = 5;
for (int i = 0; i < n; i++)
{
    chosen.Add(false);
}

List<int> permutation = new List<int>();

// search() generates all permutations of a number of length n
// consisting of digits [0, n). 
void search()
{
    
    if (permutation.Count == n)
    {
        // process permutation
        Stack<int> st = new Stack<int>(permutation);
        int last_out = 0;
        
        while (st.Count > 0)
        {
            int aa = st.Pop();
            int bb = last_out;
            
            IntCode bot = new IntCode(new List<int>(arr), aa, bb);
            last_out = bot.Run();
        }
        
        if (last_out > mx)
        {
            mx = last_out;
        }
                            
    }
    else
    {
        
        for (int i = 0; i < n; i++)
        {
            
            if (chosen[i] == true)
            {
                continue;
            }
            
            chosen[i] = true;
            permutation.Add(i);
            search();
            chosen[i] = false;
            permutation.RemoveAt(permutation.Count - 1);
        }
        
    }
    
}

search();
Console.WriteLine($"mx {mx}");
