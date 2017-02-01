using System;
using Xunit;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Fact]
        public void EscapingTest1()
        {
            Colorizer.WriteLine("\\[[Green!Foo]\\]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "]"));
        }

        [Fact]
        public void EscapingTest2()
        {
            Colorizer.WriteLine("[Green!\\[Foo\\]]");

            Validate(new TextAndColor(ConsoleColor.Green, "[Foo]"));
        }

        [Fact]
        public void EscapingTest3()
        {
            Colorizer.WriteLine("\\[[Green!Foo\\]]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "Foo]"));
        }

        [Fact]
        public void EscapingTest4()
        {
            Colorizer.WriteLine("\\[[Green!\\[Foo\\]]\\]");

            Validate(new TextAndColor(ConsoleColor.Black, "["),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "]"));
        }

        [Fact]
        public void EscapingTest5()
        {
            Colorizer.WriteLine("\\ [Green!\\[Foo\\]]\\");

            Validate(new TextAndColor(ConsoleColor.Black, "\\ "),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "\\"));
        }

        [Fact]
        public void EscapingTest6()
        {
            Colorizer.WriteLine("\\\\[Green!\\[Foo\\]]\\");

            Validate(new TextAndColor(ConsoleColor.Black, "\\\\"),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "\\"));
        }

        [Fact]
        public void EscapingTest7()
        {
            Colorizer.WriteLine("{{[Green!\\[Foo\\]]}}");

            Validate(new TextAndColor(ConsoleColor.Black, "{"),
                new TextAndColor(ConsoleColor.Green, "[Foo]"),
                new TextAndColor(ConsoleColor.Black, "}"));
        }


        [Fact]
        public void EscapingTest8()
        {
            Colorizer.WriteLine("\"[Green!Foo]\"");

            Validate(new TextAndColor(ConsoleColor.Black, "\""),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "\""));
        }

        [Fact]
        public void EscapingTest9()
        {
            Colorizer.WriteLine("}}[Green!Foo]{{");

            Validate(new TextAndColor(ConsoleColor.Black, "}"),
                new TextAndColor(ConsoleColor.Green, "Foo"),
                new TextAndColor(ConsoleColor.Black, "{"));
        }

        [Fact]
        public void EscapingTest10()
        {
            Colorizer.WriteLine("{{}}");

            Validate(new TextAndColor(ConsoleColor.Black, "{}"));
        }

        [Fact]
        public void EscapingTest11()
        {
            Colorizer.WriteLine("[Yellow!{0}]", "[Serializable]");
            Validate(new TextAndColor(ConsoleColor.Yellow, "[Serializable]"));
        }
    }
}
