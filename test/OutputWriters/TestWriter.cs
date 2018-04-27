using OutputColorizer;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    internal class TestWriter : IOutputWriter
    {
        private ConsoleColor _currentColor;

        public List<TextAndColor> Segments = new List<TextAndColor>();

        public ConsoleColor ForegroundColor
        {
            get
            {
                return _currentColor;
            }

            set
            {
                _currentColor = value;
            }
        }

        public void Write(string text)
        {
            if (text != string.Empty)
                Segments.Add(new TextAndColor(_currentColor, text));
        }

        public void WriteLine(string text)
        {
            if (text != string.Empty)
                Segments.Add(new TextAndColor(_currentColor, text));
        }

        public void Write(ReadOnlySpan<char> text)
        {
            if (text.Length!=0)
                Segments.Add(new TextAndColor(_currentColor, text.ToString()));
        }

        public void WriteLine(ReadOnlySpan<char> text)
        {
            if (text.Length!=0)
                Segments.Add(new TextAndColor(_currentColor, text.ToString()));
        }

        public void Reset()
        {
            Segments.Clear();
        }
    }

    class TextAndColor
    {
        public ConsoleColor Color { get; set; }
        public string Text { get; set; }

        public TextAndColor(ConsoleColor color, string text)
        {
            Color = color;
            Text = text;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode() ^ Text.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TextAndColor))
                return false;

            TextAndColor other = obj as TextAndColor;
            return other.Color == Color && other.Text == Text;
        }

        public override string ToString()
        {
            return $"Text={Text}, Color={Color}";
        }
    }
}
