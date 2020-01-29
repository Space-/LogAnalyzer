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

        [Test]
        public void Test_Internal_Class_MyInternalClassTest_Can_Be_Access()
        {
            var myClass = new MyInternalClass("Bruce");
            Assert.AreEqual("Bruce", myClass.Name);
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

        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            var mockService = new FakeWebService();
            var logAnalyzer = new LogAnalyzer(mockService);
            var tooShortFileName = "abc.ext";

            logAnalyzer.Analyze(tooShortFileName);
            StringAssert.Contains($"Filename too short: {tooShortFileName}", mockService.LastError);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendEmail()
        {
            var stubService = new FakeWebService { ToThrow = new Exception("fake exception") };
            var mockEmail = new FakeEmailService();
            var log = new LogAnalyzerTwo(stubService, mockEmail);
            var tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            StringAssert.Contains("someone@somewhere.com", mockEmail.To);
            StringAssert.Contains("fake exception", mockEmail.Body);
            StringAssert.Contains("can't log", mockEmail.Subject);
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

    public class FakeEmailService : IEmailService
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void SendEmail(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }

    public class FakeWebService : IWebService
    {
        public string LastError { get; private set; }
        public Exception ToThrow { get; set; }

        public void LogError(string errorMessage)
        {
            LastError = errorMessage;

            if (ToThrow != null)
            {
                throw ToThrow;
            }
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
            catch (Exception)
            {
                return false;
            }
            return WillBeValid;
        }
    }
}