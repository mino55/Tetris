using Xunit;

namespace Tetris
{
    public class PrintHelperTests
    {
        private readonly PrintHelper _printHelper;

        public PrintHelperTests()
        {
            _printHelper = new PrintHelper();
        }

        [Theory]
        [InlineData("\u001b[0msup\n", "sup\n")]
        [InlineData("\u001b[12mHello World!", "Hello World!")]
        public void CleanString_ReturnsCleanString(string str, string expected)
        {
            string cleanStr = _printHelper.CleanString(str);

            Assert.Equal(expected, cleanStr);
        }
    }
}
