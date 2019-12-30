using System;
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

        [TestCase("fileWithBadExtension.foo", false)]
        [TestCase("fileWithGoodExtension.slf", true)]
        [TestCase("fileWithGoodExtension.SLF", true)]
        [Test]
        public void IsValidLogFileName_VariousExtensions_CheckThem(string fileName, bool expected)
        {
            Assert.AreEqual(expected, _logAnalyzer.IsValidLogFileName(fileName));
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_Throws_Exception()
        {
            var logAnalyzer = MakeAnalyzer();

            var ex = Assert.Catch<ArgumentException>(() => logAnalyzer.IsValidLogFileName(string.Empty));
            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }
    }
}