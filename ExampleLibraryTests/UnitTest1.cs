using System;
using Xunit;
using ExampleLibrary;

namespace ExampleLibraryTests
{
    public class TestSth
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(0.01f,DoSth.TestFunction());
        }
    }
}
