# Output Colorizer
Colorize your output with this simple to use and extensible library

## Syntax
```csharp
Colorizer.WriteLine("[<color>!<text>]");
```
- The segment that should be colored with a given color is included in `[` and `]`. 
- The color is specified immediately after the `[` up to the `!` character.
- After the `!` character is the text to rended in that color
- `{0}` syntax for parameters is supported

## Examples
```csharp
// Simple pattern
Colorizer.WriteLine("[Green!Hello world!]"); 
// You can nest colors
Colorizer.WriteLine("[Green!Green. And [Yellow!Yellow] then green."); 
// You can use parameters
Colorizer.WriteLine("[Green!Hello {0}]", "world"); 
// And can use them without restriction
Colorizer.WriteLine("[Green!Hello {1}, this is [Yellow!{0}]]", "me", "world"); 
// And you can use string interpolation
Colorizer.WriteLine("[{ConsoleColor.Green}!Hello {1}, this is [Yellow!{0}]]", "me", "world"); 
```
