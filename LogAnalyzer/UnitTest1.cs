using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.Fail("un implement");
        }
    }

    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
        }
    }
}