using System;
using System.Collections.Generic;
using System.Text;

namespace OutputColorizer
{
    public class Token
    {
        public Token(TokenKind kind, int start, int end)
        {
            Kind = kind;
            Start = start;
            End = end;
        }
        public Token(char charToken, int start, int end)
        {
            TokenKind kind;
            switch(charToken)
            {
                case ']':
                    kind = TokenKind.CloseBracket;
                    break;
                case '[':
                    kind = TokenKind.OpenBracket;
                    break;
                case '!':
                    kind = TokenKind.ExclamationPoint;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            Kind = kind;
            Start = start;
            End = end;
        }

        public TokenKind Kind { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public override string ToString()
        {
            return $"{Kind}";
        }
    }

    public enum TokenKind
    {
        OpenBracket,
        CloseBracket,
        ExclamationPoint,
        String
    }
}
