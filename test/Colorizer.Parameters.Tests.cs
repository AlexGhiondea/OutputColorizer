using System;
using NUnit.Framework;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Test]
        public void ParameterTest1()
        {
            Colorizer.WriteLine("[Green!{0}]", "one");

            Validate(new TextAndColor(ConsoleColor.Green, "one"));
        }

        [Test]
        public void ParameterTest2()
        {
            Colorizer.WriteLine("[Green!{0} {0} {0}]", "one");

            Validate(new TextAndColor(ConsoleColor.Green, "one one one"));
        }

        [Test]
        public void ParameterTest3()
        {
            Colorizer.WriteLine("[Green!{0} {1} {2}]", "one", 2, "iii");

            Validate(new TextAndColor(ConsoleColor.Green, "one 2 iii"));
        }

        [Test]
        public void ParameterTest4()
        {
            Colorizer.WriteLine("[Green!{0} [Yellow!{1}] {2}]", "one", 2, "iii");

            Validate(new TextAndColor(ConsoleColor.Green, "one "),
                new TextAndColor(ConsoleColor.Yellow, "2"),
                new TextAndColor(ConsoleColor.Green, " iii"));
        }

        [Test]
        public void ParameterTest5()
        {
            Colorizer.WriteLine("[Green!{0} {1} {0}]", "one", "two");

            Validate(new TextAndColor(ConsoleColor.Green, "one two one"));
        }

        [Test]
        public void ParameterTest6()
        {
            Colorizer.WriteLine("[Green!{0}][Yellow!{1} {0}]", "one", "two");

            Validate(new TextAndColor(ConsoleColor.Green, "one"),
                new TextAndColor(ConsoleColor.Yellow, "two one"));
        }

        [Test]
        public void ParameterTest7()
        {
            Colorizer.WriteLine("[Green!{1}][Yellow!{0} {0}]", "one", "two");

            Validate(new TextAndColor(ConsoleColor.Green, "two"),
                new TextAndColor(ConsoleColor.Yellow, "one one"));
        }
    }
}
