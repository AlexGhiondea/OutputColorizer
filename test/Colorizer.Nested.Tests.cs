using System;
using Xunit;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Fact]
        public void NestedTest1()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar]]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"));
        }

        [Fact]
        public void NestedTest2()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [Fact]
        public void NestedTest3()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar] [Yellow!Foo] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"),
                new TextAndColor(ConsoleColor.Green, " "),
                new TextAndColor(ConsoleColor.Yellow, "Foo"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [Fact]
        public void NestedTest4()
        {
            Colorizer.WriteLine("[Green!Foo[Red!Bar [Yellow!Foo]] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Red, "Bar "),
                new TextAndColor(ConsoleColor.Yellow, "Foo"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [Fact]
        public void NestedTest5()
        {
            Colorizer.WriteLine("[Green![Yellow!Foo]]");

            Validate(new TextAndColor(ConsoleColor.Yellow, "Foo"));
        }

        [Fact]
        public void NestedTest6()
        {
            Colorizer.WriteLine("[Green![Yellow![Red!Foo]]]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"));
        }
    }
}
