namespace day10;

public static class Utils
{
    public static IEnumerable<int> Solve1(List<(string, int)> input)
    {
        var sigStrength = new List<int>();
        var registerX = 1;
        var cycle = 0;
        
        foreach (var (op, cnt) in input)
        {
            if (op == "noop")
            {
                cycle++;
                if (cycle == 20 || (cycle - 20) % 40 == 0)
                {
                    sigStrength.Add(cycle * registerX);
                }
            }
            else
            {
                cycle++;
                if (cycle == 20 || (cycle - 20) % 40 == 0)
                {
                    sigStrength.Add(cycle * registerX);
                } 
                
                cycle++;
                if (cycle == 20 || (cycle - 20) % 40 == 0)
                {
                    sigStrength.Add(cycle * registerX);
                }
                
                registerX += cnt;
            }
            
        }

        return sigStrength;
    }
    
    public static IEnumerable<List<char>> Solve2(List<(string, int)> input)
    {
        var registerX = 1;
        var cycle = 1;
        
        var crtDisplay = new List<List<char>>();
        var crtLine = new List<char>();

        foreach (var (op, cnt) in input)
        {
            if (op == "noop")
            {
                // update crt line
                if (cycle - 1 == registerX - 1
                    || cycle - 1 == registerX
                    || cycle - 1 == registerX + 1)
                {
                    crtLine.Add('#');
                }
                else
                {
                    crtLine.Add('.');
                }
                
                cycle++;
                // update register
                if (cycle % 41 == 0)
                {
                    crtDisplay.Add(new List<char>(crtLine));
                    crtLine.Clear();
                    cycle = 1;
                }
            }
            else
            {
                // update crt line
                if (cycle - 1 == registerX - 1
                    || cycle - 1 == registerX
                    || cycle - 1 == registerX + 1)
                {
                    crtLine.Add('#');
                }
                else
                {
                    crtLine.Add('.');
                } 
                
                cycle++;
                // update register
                if (cycle % 41 == 0)
                {
                    crtDisplay.Add(new List<char>(crtLine));
                    crtLine.Clear();
                    cycle = 1;
                } 
                
                // update crt line
                if (cycle - 1 == registerX - 1
                    || cycle - 1 == registerX
                    || cycle - 1 == registerX + 1)
                {
                    crtLine.Add('#');
                }
                else
                {
                    crtLine.Add('.');
                } 
                
                cycle++;
                // update register
                if (cycle % 41 == 0)
                {
                    crtDisplay.Add(new List<char>(crtLine));
                    crtLine.Clear();
                    cycle = 1;
                }
                
                registerX += cnt;
            }
        }
        
        return crtDisplay;
    } 
}