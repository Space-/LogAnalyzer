using LogAn.UnitTest.ExtensionManager;
using NUnit.Framework;
using System;

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

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            var fakeExtensionManager = new FileExtensionManager();
            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            var logAnalyzer = new LogAnalyzer();
            var result = logAnalyzer.IsValidLogFileName("LongLongLong.slf");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            var fakeExtensionManager = new FakeExtensionManager { WillThrow = new Exception("this is fake") };
            var logAnalyzer = new LogAnalyzer(fakeExtensionManager);
            var result = logAnalyzer.IsValidLogFileName("anything.anyExtension");
            Assert.False(result);
        }

        [Test]
        public void OverrideTest()
        {
            var stub = new FakeExtensionManager { WillBeValid = true };
            var logan = new TestableLogAnalyzer(stub);
            const string fileName = "file.ext";

            var result = logan.IsValidLogFileName(fileName);

            Assert.True(result, $"file name should be {fileName}");
        }

        private void ValidateResultShouldBe(bool expected, string fileName)
        {
            Assert.AreEqual(expected, _logAnalyzer.IsValidLogFileName(fileName));
        }

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer(new FileExtensionManager());
        }
    }

    public class ExtensionManagerFactory
    {
        private static IExtensionManager _customerManager;

        public static void SetManager(IExtensionManager mgr)
        {
            _customerManager = mgr;
        }

        public static IExtensionManager Create()
        {
            if (_customerManager != null)
            {
                return _customerManager;
            }

            return new FileExtensionManager();
        }
    }

    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid { get; set; }
        public Exception WillThrow = null;

        public bool IsValid(string fileName)
        {
            try
            {
                if (WillThrow != null)
                {
                    throw WillThrow;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return WillBeValid;
        }
    }
}