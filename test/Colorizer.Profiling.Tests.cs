using System;
using Xunit;
using OutputColorizer;

namespace UnitTests
{
    public partial class ColorizerTests
    {
        [Fact]
        public void ProfileTest1()
        {
            Colorizer.WriteLine("[Green!{0}]", "one");
            Colorizer.WriteLine("[Red!Foo] [Blue!Bar]");
            Colorizer.WriteLine("[Green!Foo [Red!Bar] Baz]");
            Colorizer.WriteLine("{{[Green!\\[Foo\\]]}}");

            for (int i = 0; i < 10000; i++)
            {
                Colorizer.WriteLine("[Green!{0}]", "one");
                Colorizer.WriteLine("[Red!Foo] [Blue!Bar]");
                Colorizer.WriteLine("[Green!Foo [Red!Bar] Baz]");
                Colorizer.WriteLine("{{[Green!\\[Foo\\]]}}");
            }
        }
    }
}
