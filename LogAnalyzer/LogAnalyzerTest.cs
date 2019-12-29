using NUnit.Framework;

namespace LogAn.UnitTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidLogFileName("fileWithBadExtension.foo");

            Assert.False(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowerCase_ReturnsTrue()
        {
            var logAnalyzer = new LogAnalyzer();
            var result = logAnalyzer.IsValidLogFileName("fileWithGoodExtension.SLF");
            Assert.True(result);
        }
    }
}