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

        [Test]
        public void BasicTest10()
        {
            Colorizer.WriteLine("[Red!This] is an Error!");

            Validate(new TextAndColor(ConsoleColor.Red, "This"),
                new TextAndColor(ConsoleColor.Black, " is an Error"),
                new TextAndColor(ConsoleColor.Black, "!"));
        }

        [Test]
        public void BasicTest11()
        {
            Colorizer.WriteLine("[Red!This] [Green!is an Error!]");

            Validate(new TextAndColor(ConsoleColor.Red, "This"),
                new TextAndColor(ConsoleColor.Black, " "),
                new TextAndColor(ConsoleColor.Green, "is an Error"),
                new TextAndColor(ConsoleColor.Green, "!"));
        }

        [Test]
        public void BasicTest12()
        {
            Colorizer.WriteLine("[Red!This!!!]");

            Validate(new TextAndColor(ConsoleColor.Red, "This"),
                new TextAndColor(ConsoleColor.Red, "!"),
                new TextAndColor(ConsoleColor.Red, "!"),
                new TextAndColor(ConsoleColor.Red, "!"));
        }

        [Test]
        public void BasicTest13()
        {
            Colorizer.WriteLine("!! [Green!This] !!");

            Validate(new TextAndColor(ConsoleColor.Black, "!"),
                new TextAndColor(ConsoleColor.Black, "!"),
                new TextAndColor(ConsoleColor.Black, " "),
                new TextAndColor(ConsoleColor.Green, "This"),
                new TextAndColor(ConsoleColor.Black, " "),
                new TextAndColor(ConsoleColor.Black, "!"),
                new TextAndColor(ConsoleColor.Black, "!"));
        }

        [Test]
        public void BasicTest14()
        {
            Colorizer.WriteLine("[Green!!!]");

            Validate(new TextAndColor(ConsoleColor.Green, "!"),
                new TextAndColor(ConsoleColor.Green, "!"));
        }
    }
}
