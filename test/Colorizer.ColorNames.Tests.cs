using System;
using NUnit.Framework;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Test]
        public void ColorTest1()
        {
            // Make sure we can actually parse all the console colors
            foreach (ConsoleColor item in Enum.GetValues(typeof(ConsoleColor)))
            {
                Colorizer.WriteLine($"[{item}!{item}]");
                Validate(new TextAndColor(item, item.ToString()));
                _printer.Reset();
            }
        }

        [Test]
        public void ColorTest2()
        {
            // Make sure we can actually parse all the console colors
            // !! even when using lowercase !!
            foreach (ConsoleColor item in Enum.GetValues(typeof(ConsoleColor)))
            {
                Colorizer.WriteLine($"[{item.ToString().ToLower()}!{item}]");
                Validate(new TextAndColor(item, item.ToString()));
                _printer.Reset();
            }
        }
    }
}
