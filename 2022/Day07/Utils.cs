using System.Text;

namespace day07;

public static class Utils
{
    public static bool IsCommand(string s)
    {
        return s[0] == '$' ? true : false;
    }
    
    public static bool IsCd(string s)
    {
        return s.Split(" ")[1] == "cd";
    }

    public static bool IsLs(string s)
    {
        return s.Split(" ")[1] == "ls";
    }

    public static string GetNewDir(string s)
    {
        return s.Split(" ").Last();
    }

    public static List<string> GetDirList(int ptr, List<string> input)
    {
        var dirList = new List<string>();
        
        while (!IsCommand(input[++ptr]))
        {
            dirList.Add(input[ptr]);
            
            if (ptr == input.Count - 1)
                break;
        }
        
        return dirList;
    }

    public static int GetDirMemory(
        string dir,
        Dictionary<string, List<string>> dirContents,
        Dictionary<string, int> memo)
    {
        var memory = 0;
        var dirList = dirContents[dir];
        foreach (var item in dirList)
        {
            if (item.Split(" ").First() == "dir")
            {
                var dirName = item.Split(" ").Last();
                var dirPath = dir + dirName + "/";
                
                if (memo.TryGetValue(dirPath, out var lookup))
                {
                    return lookup;
                }
                
                memory += GetDirMemory(dirPath, dirContents, memo);
            }
            else
            {
                var sz = int.Parse(item.Split(" ").First());
                memory += sz;
            }
        }
        memo.Add(dir, memory);
        
        return memory;
    }

    public static string GetCurrDir(IEnumerable<string> dirStack)
    {
        var sb = new StringBuilder();
        foreach (var dir in dirStack.Reverse())
        {
            sb.Append(dir);
            sb.Append('/');
        }

        return sb.ToString();
    }
}