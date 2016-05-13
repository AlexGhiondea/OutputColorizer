using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [TestMethod]
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

        [TestMethod]
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
