using System;
using NUnit.Framework;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Test]
        public void NestedTest1()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar]]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"));
        }

        [Test]
        public void NestedTest2()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [Test]
        public void NestedTest3()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar] [Yellow!Foo] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"),
                new TextAndColor(ConsoleColor.Green, " "),
                new TextAndColor(ConsoleColor.Yellow, "Foo"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [Test]
        public void NestedTest4()
        {
            Colorizer.WriteLine("[Green!Foo[Red!Bar [Yellow!Foo]] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Red, "Bar "),
                new TextAndColor(ConsoleColor.Yellow, "Foo"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [Test]
        public void NestedTest5()
        {
            Colorizer.WriteLine("[Green![Yellow!Foo]]");

            Validate(new TextAndColor(ConsoleColor.Yellow, "Foo"));
        }

        [Test]
        public void NestedTest6()
        {
            Colorizer.WriteLine("[Green![Yellow![Red!Foo]]]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"));
        }
    }
}
