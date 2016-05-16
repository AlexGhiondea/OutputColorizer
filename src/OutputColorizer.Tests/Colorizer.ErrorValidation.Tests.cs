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
    }
}
