using System;
using Xunit;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Fact]
        public void ErrorValidationTest1()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("[foo!Foo]"));
        }

        [Fact]
        public void ErrorValidationTest2()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("{[foo!Foo]}"));
        }

        [Fact]
        public void ErrorValidationTest3()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Green!Test"));
        }

        [Fact]
        public void ErrorValidationTest4()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Green!Test]]"));
        }

        [Fact]
        public void ErrorValidationTest5()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("}"));
        }

        [Fact]
        public void ErrorValidationTest6()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("}}}"));
        }

        [Fact]
        public void ErrorValidationTest7()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("{"));
        }

        [Fact]
        public void ErrorValidationTest8()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{1}]", "test"));
        }

        [Fact]
        public void ErrorValidationTest9()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{0}]"));
        }

        [Fact]
        public void ErrorValidationTest10()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{0}] {1}", "a"));
        }

        [Fact]
        public void ErrorValidationTest11()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{1}] {0}", "a"));
        }

        [Fact]
        public void ErrorValidationTest12()
        {
            Assert.Throws<FormatException>(() => Colorizer.WriteLine("[Yellow!{-1}]"));
        }

        [Fact]
        public void ErrorValidationTest13()
        {
            Assert.Throws<ArgumentException>(() => Colorizer.WriteLine("[Yellow!{a}]"));
        }
    }
}
