using System.Diagnostics;
using System.Text;

namespace day13;

public static class Utils
{
    public static List<object> GetLists(string input)
    {
        var stack = new Stack<object>();
        var sbForNumber = new StringBuilder();
        
        for (var i = 0; i < input.Length; i++)
        {
            var k = input[i];
            if (k == '[') // start of a list
            {
                stack.Push(k);
            }
            else if (k == ']') // end of a list
            {
                // add the last number
                if (sbForNumber.Length > 0)
                {
                    stack.Push(int.Parse(sbForNumber.ToString()));
                    sbForNumber.Clear();
                }
                
                var list = new List<object>();
                while (true)
                {
                    var top = stack.Pop();
                    if (top is List<object>)
                    {
                        list.Add(top); 
                    }
                    else if (top is int n)
                    {
                        list.Add(n);
                    }
                    else
                    {
                        Debug.Assert((char)top == '[');
                        break;
                    }
                }
                list.Reverse();
                stack.Push(list);
            }
            else // number or comma
            {
                if (k == ',')
                {
                    if (sbForNumber.Length == 0) continue; // comma between two lists
                    stack.Push(int.Parse(sbForNumber.ToString()));
                    sbForNumber.Clear();
                }
                else
                {
                    Debug.Assert(char.IsDigit(k));
                    sbForNumber.Append(k);
                }
            }
        }

        // convert stack to list
        List<object> ans = stack.Reverse().ToList();
        
        return ans;
    }
    
    
    // return values are: 1 good, 0 continue search, -1 bad
    public static int CompareList(List<object> l1, List<object> l2)
    {
        for (var i = 0; i < Math.Min(l1.Count, l2.Count); i++)
        {
            var o1 = l1[i];
            var o2 = l2[i];
            
            if (bothInts(o1, o2))
            {
                var n1 = (int)o1;
                var n2 = (int)o2;
                if (n1 == n2)
                {
                    continue;
                }

                return n1 < n2 ? 1 : -1;
            }

            if (bothLists(o1, o2))
            {
                var ret = CompareList((List<object>)o1, (List<object>)o2);
                
                // lists are equal, continue to compare next element
                if (ret == 0)
                {
                    continue;
                }

                return ret;
            }

            Debug.Assert(mixedTypes(o1, o2));
            // either o1 is a list and o2 is an int, or vice versa
            return o1 is List<object> list 
                ? CompareList(list, new List<object> {o2}) 
                : CompareList(new List<object> {o1}, (List<object>)o2);
        }

        if (l1.Count != l2.Count)
        {
            return l1.Count < l2.Count ? 1 : -1;
        }
        
        // lists are equal
        return 0;
    }

    private static bool mixedTypes(object o1, object o2)
    {
        return o1 is List<object> && o2 is int || o1 is int && o2 is List<object>;
    }

    private static bool bothLists(object o1, object o2)
    {
        return o1 is List<object> && o2 is List<object>;
    }

    private static bool bothInts(object o1, object o2)
    {
        return o1 is int && o2 is int;
    }
}