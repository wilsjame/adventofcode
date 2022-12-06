using System.Text;

namespace day05;

public class Utils
{
    public static void Move(List<Stack<char>> stacks, List<int> moves)
    {
        var cnt = moves[0];
        var src = moves[1] - 1;
        var tgt = moves[2] - 1;
        
        for (var i = 0; i < cnt; i++)
        {
            stacks[tgt].Push(stacks[src].Pop());
        }
    }

    public static void Move2(List<Stack<char>> stacks, List<int> moves)
    {
        var cnt = moves[0];
        var src = moves[1] - 1;
        var tgt = moves[2] - 1;

        var stack = new Stack<char>();
        // take them off
        for (var i = 0; i < cnt; i++)
        {
            stack.Push(stacks[src].Pop());
        }
        // put them back on
        for (var i = 0; i < cnt; i++)
        {
            stacks[tgt].Push(stack.Pop());
        }
    }
    
    public static string GetTops(List<Stack<char>> stacks)
    {
        var sb = new StringBuilder();
        foreach (var stack in stacks)
        {
            sb.Append(stack.Count == 0 ? ' ' : stack.Peek());
        }
        return sb.ToString();
    }
}