using System;
using Xunit;
using OutputColorizer;
using System.IO;

namespace UnitTests
{
    public partial class ConsoleWriterTests
    {
        [Fact]
        public void TestGetForegroundColor()
        {
            ConsoleWriter cw = new ConsoleWriter();
            Assert.Equal(Console.ForegroundColor, cw.ForegroundColor);
        }

        [Fact]
        public void TestSetForegroundColor()
        {
            ConsoleWriter cw = new ConsoleWriter();
            ConsoleColor before = Console.ForegroundColor;

            cw.ForegroundColor = ConsoleColor.Black;

            Assert.Equal(Console.ForegroundColor, ConsoleColor.Black);

            cw.ForegroundColor = before;
            Assert.Equal(Console.ForegroundColor, before);
        }

        [Fact]
        public void TestWrite()
        {
            ConsoleWriter cw = new ConsoleWriter();
            StringWriter tw = new StringWriter();
            Console.SetOut(tw);

            cw.Write("test");
            cw.Write("bar");

            Assert.Equal("testbar", tw.GetStringBuilder().ToString());
        }
        
        [Fact]
        public void TestWriteLine()
        {
            ConsoleWriter cw = new ConsoleWriter();
            StringWriter tw = new StringWriter();
            Console.SetOut(tw);

            cw.WriteLine("test2");
            cw.WriteLine("bar");

            Assert.Equal("test2\nbar\n", tw.GetStringBuilder().ToString());
        }
    }
}
