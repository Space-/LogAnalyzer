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

        [TestCase("fileWithGoodExtension.slf")]
        [TestCase("fileWithGoodExtension.SLF")]
        [Test]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string fileName)
        {
            Assert.True(_logAnalyzer.IsValidLogFileName(fileName));
        }
    }
}