using NUnit.Framework;
using OutputColorizer;
using System;
using System.IO;

namespace UnitTests
{
    public partial class ConsoleWriterTests
    {
        [Test]
        public void TestGetForegroundColor()
        {
            ConsoleWriter cw = new ConsoleWriter();
            Assert.AreEqual(Console.ForegroundColor, cw.ForegroundColor);
        }

        [Test]
        public void TestSetForegroundColor()
        {
            ConsoleWriter cw = new ConsoleWriter();
            ConsoleColor before = Console.ForegroundColor;

            cw.ForegroundColor = ConsoleColor.Black;

            Assert.AreEqual(Console.ForegroundColor, ConsoleColor.Black);

            cw.ForegroundColor = before;
            Assert.AreEqual(Console.ForegroundColor, before);
        }

        [Test]
        public void TestWrite()
        {
            ConsoleWriter cw = new ConsoleWriter();
            StringWriter tw = new StringWriter();
            Console.SetOut(tw);

            cw.Write("test");
            cw.Write("bar");

            Assert.AreEqual("testbar", tw.GetStringBuilder().ToString());
        }
        
        [Test]
        public void TestWriteLine()
        {
            ConsoleWriter cw = new ConsoleWriter();
            StringWriter tw = new StringWriter();
            Console.SetOut(tw);

            cw.WriteLine("test2");
            cw.WriteLine("bar");

            Assert.AreEqual($"test2{Environment.NewLine}bar{Environment.NewLine}", tw.GetStringBuilder().ToString());
        }
    }
}
