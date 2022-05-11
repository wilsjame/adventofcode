namespace day05;

public class IntCode
{
    public List<int> Arr { get; set; }
    public int A { get; set; }

    public IntCode(List<int> arr, int a)
    {
        Arr = arr;
        A = a;
    }

    public int Run()
    {
        int ptr = 0;
        
        while (true)
        {
            
            // ABCDE where DE - opcode, C - mode of 1st parameter, B - mode of 2nd, A - 3rd
            //  1002                    0 is position mode and 1 is immediate mode.
            string op = Arr[ptr].ToString().PadLeft(5, '0');
            int code = int.Parse(op.Substring(3));
            int m1 = int.Parse(op.Substring(2, 1));
            int m2 = int.Parse(op.Substring(1, 1));
            int m3 = int.Parse(op.Substring(0, 1));

            // code 99 - halts,  1 - *, 2 - +, 3 - input, 4 - outputs
            // 5 - jump-if-true, 6 - jump-if-false, 7 - less than, 8 - equals
            // TODO refactor with a switch statement if we add more opcodes.
            if (code == 99)
            {
                break;
            }
            else if (code == 1 || code == 2)
            {
                
                // Set values based on parameter modes.
                int v1 = (m1 == 0) ? Arr[Arr[ptr + 1]] : Arr[ptr + 1];
                int v2 = (m2 == 0) ? Arr[Arr[ptr + 2]] : Arr[ptr + 2];
                int pos = Arr[ptr + 3];

                if (code == 1)
                {
                    Arr[pos] = v1 + v2;
                }
                else if (code == 2)
                {
                    Arr[pos] = v1 * v2;
                }

                ptr += 4;
            }
            else if (code == 3 || code == 4)
            {
                int pos = Arr[ptr + 1];

                if (code == 3)
                {
                    Arr[pos] = A; 
                }
                else if (code == 4)
                {
                    Console.WriteLine($"output at addr {pos} {Arr[pos]}");
                }

                ptr += 2;
            }
            else if (code > 4)
            {
                
                // Set values based on parameter modes.
                int v1 = (m1 == 0) ? Arr[Arr[ptr + 1]] : Arr[ptr + 1];
                int v2 = (m2 == 0) ? Arr[Arr[ptr + 2]] : Arr[ptr + 2];
                int pos = Arr[ptr + 3];

                if (code == 5)
                {
                    
                    if (v1 > 0)
                    {
                        ptr = v2;
                    }
                    else
                    {
                        ptr += 3;
                    }
                    
                }
                else if (code == 6)
                {
                    
                    if (v1 == 0)
                    {
                        ptr = v2;
                    }
                    else
                    {
                        ptr += 3;
                    }
                    
                }
                else if (code == 7)
                {

                    if (v1 < v2)
                    {
                        Arr[pos] = 1;
                    }
                    else
                    {
                        Arr[pos] = 0;
                    }

                    ptr += 4;
                }
                else if (code == 8)
                {

                    if (v1 == v2)
                    {
                        Arr[pos] = 1;
                    }
                    else
                    {
                        Arr[pos] = 0;
                    }

                    ptr += 4;
                }
                
            }
            
        }

        return 0;
    }
    
}
