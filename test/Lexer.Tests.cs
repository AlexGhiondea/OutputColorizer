using NUnit.Framework;
using OutputColorizer.Format;
using System;

namespace UnitTests
{
    public partial class LexerTests
    {
        [Test]
        public void TestLexer1()
        {
            Lexer lx = new Lexer("[Red!Foo]");

            Token[] tokens = lx.Tokenize();

            Assert.IsTrue(tokens[0].Kind == TokenKind.OpenBracket);
            Assert.IsTrue(tokens[2].Kind == TokenKind.ColorDelimiter);
            Assert.IsTrue(tokens[4].Kind == TokenKind.CloseBracket);

            SequenceEqual(lx, "[", "Red", "!", "Foo", "]");
        }

        [TestCase("[Red!Foo]", "[", "Red", "!", "Foo", "]")]
        [TestCase("[Red!Foo] [Green!Bar]", "[", "Red", "!", "Foo", "]", " ", "[", "Green", "!", "Bar", "]")]
        [TestCase("Test [Green!Foo]", "Test ", "[", "Green", "!", "Foo", "]")]
        [TestCase("[Green!Foo] Test", "[", "Green", "!", "Foo", "]", " Test")]
        [TestCase("[Green!Foo [Red!Bar]]", "[", "Green", "!", "Foo ", "[", "Red", "!", "Bar", "]", "]")]
        [TestCase("[Green!Foo [Red!Bar] Baz]", "[", "Green", "!", "Foo ", "[", "Red", "!", "Bar", "]", " Baz", "]")]
        [TestCase("[Green!Foo[Red!Bar[Yellow!Foo]] Baz]", "[", "Green", "!", "Foo", "[", "Red", "!", "Bar", "[", "Yellow", "!", "Foo", "]", "]", " Baz", "]")]
        [TestCase("[Test][Green!Foo]", "[", "Test", "]", "[", "Green", "!", "Foo", "]")]
        [TestCase("Test", "Test")]
        public void TestLexerBasic(string text, params string[] expectedSegments)
        {
            SequenceEqual(new Lexer(text), expectedSegments);
        }

        [Test]
        public void TestLexer2()
        {
            Assert.Throws<InvalidOperationException>(() => new Token('a', 0, 1));
        }

        [TestCase("\\[[Green!Foo]\\]", "\\[", "[", "Green", "!", "Foo", "]", "\\]")]
        [TestCase("[Green!\\[Foo\\]]", "[", "Green", "!", "\\[Foo\\]", "]")]
        [TestCase("\\[[Green!\\[Foo\\]]\\]", "\\[", "[", "Green", "!", "\\[Foo\\]", "]", "\\]")]
        [TestCase("\\ [Green!\\[Foo\\]]\\", "\\ ", "[", "Green", "!", "\\[Foo\\]", "]", "\\")]
        public void TestLexerEscaping(string text, params string[] expectedSegments)
        {
            SequenceEqual(new Lexer(text), expectedSegments);
        }

        private void SequenceEqual(Lexer lx, params string[] elements)
        {
            Token[] tokens = lx.Tokenize();

            for (int i = 0; i < tokens.Length; i++)
            {
                Assert.AreEqual(elements[i], lx.GetValue(tokens[i]));
            }
        }
    }
}
