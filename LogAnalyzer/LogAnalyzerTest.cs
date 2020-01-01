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
            _logAnalyzer = MakeAnalyzer();
        }

        [TestCase("fileWithBadExtension.foo", false)]
        [TestCase("fileWithGoodExtension.slf", true)]
        [TestCase("fileWithGoodExtension.SLF", true)]
        [Test]
        public void IsValidLogFileName_VariousExtensions_CheckThem(string fileName, bool expected)
        {
            ValidateResultShouldBe(expected, fileName);
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_Throws_Exception()
        {
            var ex = Assert.Catch<ArgumentException>(() => _logAnalyzer.IsValidLogFileName(string.Empty));
            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [TestCase("badName.foo", false)]
        [TestCase("goodName.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string fileName, bool expected)
        {
            ValidateResultShouldBe(expected, fileName);
        }

        private void ValidateResultShouldBe(bool expected, string fileName)
        {
            Assert.AreEqual(expected, _logAnalyzer.IsValidLogFileName(fileName));
        }

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }
    }
}