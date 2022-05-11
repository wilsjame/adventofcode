using day05;

List<int> arr = File.ReadAllLines("day05.in")[0].Split(',').Select(x => int.Parse(x)).ToList();

// Part 1.
IntCode intCode = new IntCode(new List<int>(arr), 5);
intCode.Run();

// Part 2.
IntCode intCode2 = new IntCode(new List<int>(arr), 1);
intCode2.Run();
