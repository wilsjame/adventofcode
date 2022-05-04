# 2019  

## C\# 
For now, using the .NET command-line interface to build and run solutions. The .NET CLI is included with the .NET SDK.

|Platform| Install the .NET SDK|
|---|---|
|Ubuntu| [6.0](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#2004-)| 
|macOS| [6.0](https://dotnet.microsoft.com/en-us/download)|

## New day 
```
Start a new day
$ dotnet new console -o day02  

In the day02/ project folder
$ dotnet run
```

## Program structure
Solutions use C#'s new _top-level statements_ feature. We don't have to
include a `Main` method for console applications. The compiler generates a 
class and `Main` method entry point for us. This approach makes it easier
to write simpler programs to solve small puzzles like these.

Good for learning C#, but code should definitely get refactored if reusability
and maintainability are important. 

[Overview](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements)
and
[Tutorial](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/top-level-statements).

### Note
I would like to use an IDE at some point (e.g. Rider or Visual Studio).
Sticking with the CLI at the beginning, so i can better understand how things work. 
