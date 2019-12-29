using System.Dynamic;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    [TestFixture]
    public class Tests
    {
        private LogAnalyzer _logAnalyzer;

        [SetUp]
        public void Init()
        {
            _logAnalyzer = new LogAnalyzer();
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            Assert.False(_logAnalyzer.IsValidLogFileName("fileWithBadExtension.foo"));
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowerCase_ReturnsTrue()
        {
            Assert.True(_logAnalyzer.IsValidLogFileName("fileWithGoodExtension.slf"));
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUpperCase_ReturnsTrue()
        {
            Assert.True(_logAnalyzer.IsValidLogFileName("fileWithGoodExtension.SLF"));
        }
    }
}