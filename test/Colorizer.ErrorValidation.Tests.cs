using System;
using NUnit.Framework;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Test]
        public void ErrorValidationTest1()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("[foo!Foo]"));
        }

        [Test]
        public void ErrorValidationTest2()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("{[foo!Foo]}"));
        }

        [Test]
        public void ErrorValidationTest3()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Green!Test"));
        }

        [Test]
        public void ErrorValidationTest4()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Green!Test]]"));
        }

        [Test]
        public void ErrorValidationTest5()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("}"));
        }

        [Test]
        public void ErrorValidationTest6()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("}}}"));
        }

        [Test]
        public void ErrorValidationTest7()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("{"));
        }

        [Test]
        public void ErrorValidationTest8()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{1}]", "test"));
        }

        [Test]
        public void ErrorValidationTest9()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{0}]"));
        }

        [Test]
        public void ErrorValidationTest10()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{0}] {1}", "a"));
        }

        [Test]
        public void ErrorValidationTest11()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{1}] {0}", "a"));
        }

        [Test]
        public void ErrorValidationTest12()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{-1}]"));
        }

        [Test]
        public void ErrorValidationTest13()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("[Yellow!{a}]"));
        }

        [Test]
        public void ErrorValidationTest14()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[NoColor"));
        }


        [Test]
        public void ErrorValidationTest15()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[[]]"));
        }
        
    }
}
