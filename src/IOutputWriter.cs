using System;

namespace OutputColorizer
{
    public interface IOutputWriter
    {
        void Write(string text);
        void Write(ReadOnlySpan<char> text);
        void WriteLine(string text);
        void WriteLine(ReadOnlySpan<char> text);

        ConsoleColor ForegroundColor { get; set; }
    }
}
