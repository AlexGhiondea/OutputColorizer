using Xunit;
using System.Diagnostics;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        static TestWriter _printer;

        public ColorizerTests()
        {
            _printer = new TestWriter();
            Colorizer.SetupWriter(_printer);
        }
        
        private void Validate(params TextAndColor[] values)
        {
            Assert.Equal(values.Length, _printer.Segments.Count);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.True(values[i].Equals(_printer.Segments[i]),
                    string.Format("Expected Text={0}, Color={1}, Got {2}", values[i].Text, values[i].Color, _printer.Segments[i]));
            }
        }
    }
}
