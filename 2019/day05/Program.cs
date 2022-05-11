using day05;

Console.WriteLine("Hello, World!");
List<int> arr = File.ReadAllLines("day05.in")[0].Split(',').Select(x => int.Parse(x)).ToList();

// Part 1.
IntCode intCode = new IntCode(arr, 1);
intCode.Run();
