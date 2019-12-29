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
            bool result = logAnalyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(result);
        }
    }
}