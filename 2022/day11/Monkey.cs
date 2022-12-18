using System.Diagnostics;

namespace day11;

public class Monkey
{
    private readonly int _id;
    private List<long> _items; // worry level for each item the monkey is holding
    private List<string> _operations;
    private readonly int _divisibleBy;
    private readonly int _ifTrueMonkey;
    private readonly int _ifFalseMonkey;
    
    public int InspectCount;

    private Monkey(
        int id,
        List<long> items,
        List<string> operations,
        int divisibleBy,
        int ifTrueMonkey,
        int ifFalseMonkey)
    {
        _id = id;
        _items = items;
        _operations = operations;
        _divisibleBy = divisibleBy;
        _ifTrueMonkey = ifTrueMonkey;
        _ifFalseMonkey = ifFalseMonkey;
    }

    public Monkey DeepCopy()
    {
        var other = (Monkey)MemberwiseClone();
        other._items = new List<long>(_items);
        other._operations = new List<string>(_operations);
        return other;
    }

    public static Monkey Read(StreamReader reader, int id)
    {
        // first line are starting items
        var line = reader.ReadLine();
        Debug.Assert(line != null, nameof(line) + " != null");
        var items = line
            .Split(":")
            .Last()
            .Split(",")
            .Select(x => x.Trim())
            .Select(x => long.Parse(x.ToString()))
            .ToList();
        
        // second line are operations
        line = reader.ReadLine();
        Debug.Assert(line != null, nameof(line) + " != null");
        var operations = line
            .Split("=")
            .Last()
            .TrimStart()
            .Split(" ")
            .ToList();

        // third line - fifth line is test
        line = reader.ReadLine();
        Debug.Assert(line != null, nameof(line) + " != null");
        var divisibleBy = line
            .Split(" ")
            .Last().Split()
            .Select(x => int.Parse(x.ToString()))
            .First();


        line = reader.ReadLine();
        Debug.Assert(line != null, nameof(line) + " != null");
        var ifTrueMonkey = line
            .Split(" ")
            .Last().Split()
            .Select(x => int.Parse(x.ToString()))
            .First();

        line = reader.ReadLine();
        Debug.Assert(line != null, nameof(line) + " != null");
        var ifFalseMonkey = line
            .Split(" ")
            .Last().Split()
            .Select(x => int.Parse(x.ToString()))
            .First();
        
        // sixth line is empty
        
        return new Monkey(
            id,
            items,
            operations,
            divisibleBy,
            ifTrueMonkey,
            ifFalseMonkey);
    }
    
    // how your worry level changes as the monkey inspects each item
    private long Operation(long old)
    {
        var n1 = long.TryParse(_operations.First(), out var result1) ? result1 : old;
        var n2 = long.TryParse(_operations.Last(), out var result2) ? result2 : old;

        var ans = _operations[1] switch
        {
            "+" => n1 + n2,
            "*" => n1 * n2,
            _ => throw new Exception("unknown operation")
        };
        return ans;
    }

    // how the monkey uses your worry level to decide which monkey to throw the item to next
    private int Test(long worryLevel)
    {
        return worryLevel % _divisibleBy == 0 ? _ifTrueMonkey : _ifFalseMonkey;
    }

    private static void ThrowItem(long item, int tgtId, IEnumerable<Monkey> monkeys)
    {
        foreach (var monkey in monkeys.Where(monkey => monkey._id == tgtId))
        {
            monkey._items.Add(item);
            break;
        }
    }
    
    public static void Play(List<Monkey> monkeys, int numRounds)
    {
        for (var i = 0; i < numRounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                // ToList() makes a copy so removing items from the list doesn't affect the loop
                foreach (var item in monkey._items.ToList())
                {
                    monkey.InspectCount++;
                    var worryLevel = monkey.Operation(item) / 3;
                    var destMonkey = monkey.Test(worryLevel);
                    ThrowItem(worryLevel, destMonkey, monkeys);
                    monkey._items.Remove(item);
                }
            }
        }
    }

    public static int CommonDenominator(IEnumerable<Monkey> monkeys)
    {
        return monkeys.Aggregate(1, (current, monkey) => current * monkey._divisibleBy);
    }
    
    public static void Play2(List<Monkey> monkeys, int numRounds, int commonDenominator)
    {
        for (var i = 0; i < numRounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey._items.ToList())
                {
                    monkey.InspectCount++;
                    var worryLevel = monkey.Operation(item % commonDenominator);
                    var destMonkey = monkey.Test(worryLevel);
                    ThrowItem(worryLevel, destMonkey, monkeys);
                    monkey._items.Remove(item);
                }
            }
        }
    }
}