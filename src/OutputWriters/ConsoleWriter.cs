using System;

namespace OutputColorizer
{
    public class ConsoleWriter : IOutputWriter
    {
        public ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }
            set
            {
                Console.ForegroundColor = value;
            }
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void Write(ReadOnlySpan<char> text)
        {
#if NETCORE
            Console.Write(text);
#else
            // When the Span based APIs are not available, forward to the other APIs
            Write(text.ToString());
#endif
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteLine(ReadOnlySpan<char> text)
        {
#if NETCORE
            Console.WriteLine(text);
#else
            // When the Span based APIs are not available, forward to the other APIs
            WriteLine(text.ToString());
#endif
        }
    }
}
