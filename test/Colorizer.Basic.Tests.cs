using System;
using NUnit.Framework;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {

        [Test]
        public void BasicTest()
        {
            Colorizer.WriteLine("[Red!Foo]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"));
        }

        [Test]
        public void BasicTest2()
        {
            Colorizer.WriteLine("[Red!Foo] [Blue!Bar]");

            Validate(new TextAndColor(ConsoleColor.Red, "Foo"),
                new TextAndColor(ConsoleColor.Black, " "),
                new TextAndColor(ConsoleColor.Blue, "Bar"));
        }

        [Test]
        public void BasicTest3()
        {
            Colorizer.WriteLine("Test");

            Validate(new TextAndColor(ConsoleColor.Black, "Test"));
        }

        [Test]
        public void BasicTest4()
        {
            Colorizer.WriteLine("Test [Green!Foo]");

            Validate(new TextAndColor(ConsoleColor.Black, "Test "),
                new TextAndColor(ConsoleColor.Green, "Foo"));
        }

        [Test]
        public void BasicTest5()
        {
            Colorizer.WriteLine("[Green!Foo] Test");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, " Test"));
        }

        [Test]
        public void BasicTest6()
        {
            Colorizer.Write("[Green!Foo] Test");

            Validate(new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, " Test"));
        }

        [Test]
        public void BasicTest7()
        {
            Colorizer.WriteLine("[Red]");

            Validate(new TextAndColor(ConsoleColor.Black, "[Red]"));
        }

        [Test]
        public void BasicTest8()
        {
            Colorizer.WriteLine("[Red!]");

            Validate();
        }

        [Test]
        public void BasicTest9()
        {
            Colorizer.WriteLine("[]");

            Validate(new TextAndColor(ConsoleColor.Black, "[]"));
        }
    }
}
