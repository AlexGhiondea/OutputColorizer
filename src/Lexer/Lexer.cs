using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OutputColorizer.Format
{
    public class Lexer
    {
        private Token[] _tokens;

        private readonly string Text;

        public Lexer(string text)
        {
            Text = text;
        }

        public Token[] Tokenize()
        {
            if (_tokens != null)
            {
                return _tokens;
            }

            List<Token> tokens = new List<Token>();
            Token previousToken = default;

            int currentIndex = 0, previousTokenEnd = 0;

            while (currentIndex < Text.Length)
            {
                if (Text[currentIndex] == '\\')
                {
                    // skip over the next character
                    currentIndex++;
                }
                else
                {
                    char token = Text[currentIndex];

                    if (token == ']' || token == '[' || token == '!')
                    {
                        // put whatever was before this token into a string token
                        if (previousTokenEnd != currentIndex)
                        {
                            tokens.Add(new Token(TokenKind.String, previousTokenEnd, currentIndex - 1));
                        }

                        // this will throw for invalid tokens.
                        previousToken = new Token(token, currentIndex, currentIndex);
                        tokens.Add(previousToken);
                        previousTokenEnd = currentIndex + 1;
                    }
                }

                // continue if we need to.
                currentIndex++;
            }

            // add a last segment if current index is different than previous token (i.e. some text after the last token)
            if (currentIndex != previousTokenEnd)
            {
                tokens.Add(new Token(TokenKind.String, previousTokenEnd, Text.Length - 1));
            }

            _tokens = tokens.ToArray();
            return _tokens;
        }

        public string GetValue(Token token)
        {
            return Text.Substring(token.Start, token.End - token.Start + 1);
        }

#if DEBUG
        public string WriteTokens()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _tokens)
            {
                sb.AppendLine($"{item.Kind} ({item.Start}-{item.End}): {GetValue(item)}");
            }
            return sb.ToString();
        }
#endif
    }
}
