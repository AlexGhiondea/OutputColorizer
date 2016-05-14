using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [TestMethod]
        public void EscapingTest1()
        {
            Colorizer.WriteLine("\\[[Green!Foo]\\]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "]"));
        }

        [TestMethod]
        public void EscapingTest2()
        {
            Colorizer.WriteLine("[Green!\\[Foo\\]]");

            Validate(new TextAndColor(ConsoleColor.Green, "[Foo]"));
        }

        [TestMethod]
        public void EscapingTest3()
        {
            Colorizer.WriteLine("\\[[Green!Foo\\]]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "Foo]"));
        }

        [TestMethod]
        public void EscapingTest4()
        {
            Colorizer.WriteLine("\\[[Green!\\[Foo\\]]\\]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "]"));
        }

        [TestMethod]
        public void EscapingTest5()
        {
            Colorizer.WriteLine("\\ [Green!\\[Foo\\]]\\");

            Validate(new TextAndColor(ConsoleColor.Black, "\\ "),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "\\"));
        }

        [TestMethod]
        public void EscapingTest6()
        {
            Colorizer.WriteLine("\\\\[Green!\\[Foo\\]]\\");

            Validate(new TextAndColor(ConsoleColor.Black, "\\\\"),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "\\"));
        }

        [TestMethod]
        public void EscapingTest7()
        {
            Colorizer.WriteLine("{{[Green!\\[Foo\\]]}}");

            Validate(new TextAndColor(ConsoleColor.Black, "{"),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "}"));
        }


        [TestMethod]
        public void EscapingTest8()
        {
            Colorizer.WriteLine("\"[Green!Foo]\"");

            Validate(new TextAndColor(ConsoleColor.Black, "\""),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "\""));
        }

        [TestMethod]
        public void EscapingTest9()
        {
            Colorizer.WriteLine("}}[Green!Foo]{{");

            Validate(new TextAndColor(ConsoleColor.Black, "}"),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "{"));
        }

        [TestMethod]
        public void EscapingTest10()
        {
            Colorizer.WriteLine("{{}}");

            Validate(new TextAndColor(ConsoleColor.Black, "{}"));
        }
    }
}
