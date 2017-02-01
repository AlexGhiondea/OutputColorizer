using System;
using Xunit;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Fact]
        public void BasicTest()
        {
            Colorizer.WriteLine("[Red!Foo]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"));
        }

        [Fact]
        public void BasicTest2()
        {
            Colorizer.WriteLine("[Red!Foo] [Blue!Bar]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"),
                new TextAndColor(ConsoleColor.Black, " "),
                new TextAndColor(ConsoleColor.Blue, "Bar"));
        }

        [Fact]
        public void BasicTest3()
        {
            Colorizer.WriteLine("Test");

            Validate(new TextAndColor(ConsoleColor.Black, "Test"));
        }

        [Fact]
        public void BasicTest4()
        {
            Colorizer.WriteLine("Test [Green!Foo]");

            Validate(new TextAndColor(ConsoleColor.Black, "Test "),
                new TextAndColor(ConsoleColor.Green, "Foo"));
        }

        [Fact]
        public void BasicTest5()
        {
            Colorizer.WriteLine("[Green!Foo] Test");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, " Test"));
        }

        [Fact]
        public void BasicTest6()
        {
            Colorizer.Write("[Green!Foo] Test");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, " Test"));
        }
    }
}
