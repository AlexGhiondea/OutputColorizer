# Output Colorizer

[![NuGet version](https://img.shields.io/nuget/v/OutputColorizer.svg?style=flat)](https://www.nuget.org/packages/OutputColorizer)
[![Nuget downloads](https://img.shields.io/nuget/dt/OutputColorizer.svg?style=flat)](https://www.nuget.org/packages/OutputColorizer)
[![Build status](https://ci.appveyor.com/api/projects/status/github/AlexGhiondea/OutputColorizer?branch=master&svg=true)](https://ci.appveyor.com/project/AlexGhiondea/OutputColorizer)
[![codecov](https://codecov.io/gh/AlexGhiondea/OutputColorizer/branch/master/graph/badge.svg)](https://codecov.io/gh/AlexGhiondea/OutputColorizer)
[![MIT License](https://img.shields.io/github/license/AlexGhiondea/OutputColorizer.svg)](https://github.com/AlexGhiondea/OutputColorizer/blob/master/LICENSE)
========

Colorize your output with this simple to use and extensible library

## Download
This is available as a [Nuget package](https://www.nuget.org/packages/OutputColorizer/) and you can use it in your Desktop for .NET Core application.

## Syntax
```csharp
Colorizer.WriteLine("[<color>!<text>]");
```
- The segment that should be colored with a given color is included in `[` and `]`. 
- The color is specified immediately after the `[` and up to the `!` character.
- After the `!` character is the text to rended in that color
- `{0}` syntax for parameters is supported
- Segments can be nested together for greater flexibility

## Examples
```csharp
// Simple pattern
Colorizer.WriteLine("[Green!Hello world!]"); 
// You can nest colors
Colorizer.WriteLine("[Green!Green. And [Yellow!Yellow] then green.]"); 
// You can use parameters
Colorizer.WriteLine("[Green!Hello {0}]", "world"); 
// And can use them without restriction
Colorizer.WriteLine("[Green!Hello {1}, this is [Yellow!{0}]]", "me", "world"); 
 // And you can use string interpolation
string s1 = "me", s2 = "world";
Colorizer.WriteLine($"[{ConsoleColor.Red}!Hello {s2}, this is [Cyan!{s1}]]");
```
