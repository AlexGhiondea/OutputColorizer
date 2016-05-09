using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [TestMethod]
        public void NestedTest1()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar]]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"));
        }

        [TestMethod]
        public void NestedTest2()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [TestMethod]
        public void NestedTest3()
        {
            Colorizer.WriteLine("[Green!Foo [Red!Bar] [Yellow!Foo] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo "),
                new TextAndColor(ConsoleColor.Red, "Bar"),
                new TextAndColor(ConsoleColor.Green, " "),
                new TextAndColor(ConsoleColor.Yellow, "Foo"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [TestMethod]
        public void NestedTest4()
        {
            Colorizer.WriteLine("[Green!Foo[Red!Bar [Yellow!Foo]] Baz]");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Red, "Bar "),
                new TextAndColor(ConsoleColor.Yellow, "Foo"),
                new TextAndColor(ConsoleColor.Green, " Baz"));
        }

        [TestMethod]
        public void NestedTest5()
        {
            Colorizer.WriteLine("[Green![Yellow!Foo]]");

            Validate(new TextAndColor(ConsoleColor.Yellow, "Foo"));
        }

        [TestMethod]
        public void NestedTest6()
        {
            Colorizer.WriteLine("[Green![Yellow![Red!Foo]]]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"));
        }
    }
}
