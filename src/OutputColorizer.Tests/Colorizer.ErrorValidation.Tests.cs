using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorValidationTest1()
        {
            Colorizer.WriteLine("[foo!Foo]");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorValidationTest2()
        {
            Colorizer.WriteLine("{[foo!Foo]}");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest3()
        {
            Colorizer.WriteLine("[Green!Test");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest4()
        {
            Colorizer.WriteLine("[Green!Test]]");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest5()
        {
            Colorizer.WriteLine("}");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest6()
        {
            Colorizer.WriteLine("}}}");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorValidationTest7()
        {
            Colorizer.WriteLine("{");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest8()
        {
            Colorizer.WriteLine("[Yellow!{1}]", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest9()
        {
            Colorizer.WriteLine("[Yellow!{0}]");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest10()
        {
            Colorizer.WriteLine("[Yellow!{0}] {1}", "a");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest11()
        {
            Colorizer.WriteLine("[Yellow!{1}] {0}", "a");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ErrorValidationTest12()
        {
            Colorizer.WriteLine("[Yellow!{-1}]");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ErrorValidationTest13()
        {
            Colorizer.WriteLine("[Yellow!{a}]");
        }
    }
}
