using System;
using NUnit.Framework;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Test]
        public void EscapingTest1()
        {
            Colorizer.WriteLine("\\[[Green!Foo]\\]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "]"));
        }

        [Test]
        public void EscapingTest2()
        {
            Colorizer.WriteLine("[Green!\\[Foo\\]]");

            Validate(new TextAndColor(ConsoleColor.Green, "[Foo]"));
        }

        [Test]
        public void EscapingTest3()
        {
            Colorizer.WriteLine("\\[[Green!Foo\\]]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "Foo]"));
        }

        [Test]
        public void EscapingTest4()
        {
            Colorizer.WriteLine("\\[[Green!\\[Foo\\]]\\]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "]"));
        }

        [Test]
        public void EscapingTest5()
        {
            Colorizer.WriteLine("\\ [Green!\\[Foo\\]]\\");

            Validate(new TextAndColor(ConsoleColor.Black, "\\ "),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "\\"));
        }

        [Test]
        public void EscapingTest6()
        {
            Colorizer.WriteLine("\\\\[Green!\\[Foo\\]]\\");

            Validate(new TextAndColor(ConsoleColor.Black, "\\\\"),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "\\"));
        }

        [Test]
        public void EscapingTest7()
        {
            Colorizer.WriteLine("{{[Green!\\[Foo\\]]}}");

            Validate(new TextAndColor(ConsoleColor.Black, "{"),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "}"));
        }


        [Test]
        public void EscapingTest8()
        {
            Colorizer.WriteLine("\"[Green!Foo]\"");

            Validate(new TextAndColor(ConsoleColor.Black, "\""),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "\""));
        }

        [Test]
        public void EscapingTest9()
        {
            Colorizer.WriteLine("}}[Green!Foo]{{");

            Validate(new TextAndColor(ConsoleColor.Black, "}"),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "{"));
        }

        [Test]
        public void EscapingTest10()
        {
            Colorizer.WriteLine("{{}}");

            Validate(new TextAndColor(ConsoleColor.Black, "{}"));
        }

        [Test]
        public void EscapingTest11()
        {
            Colorizer.WriteLine("[Yellow!{0}]", "[Serializable]");
            Validate(new TextAndColor(ConsoleColor.Yellow, "[Serializable]"));
        }

        [Test]
        public void EscapingTest12()
        {
            Colorizer.WriteLine("[NoColor]");
            Validate(new TextAndColor(ConsoleColor.Black, "[NoColor]"));
        }

        [Test]
        public void EscapingTest13()
        {
            Colorizer.WriteLine("[NoColor] [Red!Error]");
            Validate(new TextAndColor(ConsoleColor.Black, "[NoColor]"),
                new TextAndColor(ConsoleColor.Black, " "),
                new TextAndColor(ConsoleColor.Red, "Error"));
        }
    }
}
