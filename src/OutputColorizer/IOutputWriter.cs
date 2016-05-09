using System;

namespace OutputColorizer
{
    public interface IOutputWriter
    {
        void Write(string text);
        void WriteLine(string text);

        ConsoleColor ForegroundColor { get; set; }
    }
}
