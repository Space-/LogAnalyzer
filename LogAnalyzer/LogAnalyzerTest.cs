using Castle.Core.Logging;
using LogAn.UnitTest.ExtensionManager;
using LogAn.UnitTest.Interface;
using NSubstitute;
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

            logAnalyzer.AnalyzeFileName(tooShortFileName);
            StringAssert.Contains($"Filename too short: {tooShortFileName}", mockService.LastError);
        }

        [Test]
        public void Analyze_TooBigFileSize_CallLogger()
        {
            var logger = Substitute.For<ILogger>();
            var maxFileSize = 100;
            var logAnalyzer = new LogAnalyzer(logger)
            {
                MaxFileSize = maxFileSize
            };

            var tooBigFileSize = 101;

            logAnalyzer.AnalyzeFileSize(tooBigFileSize);

            logger.Received().Error($"File size {tooBigFileSize} too big, it should be less than max size {maxFileSize}");
        }

        [Test]
        public void Analyze_WebServiceThrows_SendEmail()
        {
            var stubService = new FakeWebService { ToThrow = new Exception("fake exception") };
            var mockEmail = new FakeEmailService();
            var log = new LogAnalyzerTwo(stubService, mockEmail);
            var tooShortFileName = "abc.ext";

            log.Analyze(tooShortFileName);

            var expectedEmail = new EmailInfo()
            {
                Body = "fake exception",
                To = "someone@somewhere.com",
                Subject = "can't log"
            };

            Assert.True(expectedEmail.Equals(mockEmail.Email));
        }

        [Test]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            var fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.IsValidFileName(Arg.Any<string>()).Returns(true);
            Assert.True(fakeRules.IsValidFileName("anything.txt"));
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            var fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.When(x => x.IsValidFileName(Arg.Any<string>()))
                .Do(context => throw new Exception("fake exception"));

            Assert.Throws<Exception>(() => fakeRules.IsValidFileName("anything.txt"));
        }

        // p.123
        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            var mockService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger.When(logger => logger.Error(Arg.Any<string>()))
                .Do(info => throw new Exception("fake exception"));

            var logAnalyzer = new LogAnalyzerTwo(mockService, stubLogger)
            {
                MaxFileSize = 100
            };

            var tooBigFileSize = 101;

            logAnalyzer.AnalyzeFileSize(tooBigFileSize);
            mockService.Received().Write(Arg.Is<string>(s => s.Contains("fake exception")));
        }

        // p.124
        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObject()
        {
            var mockService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            stubLogger.When(logger => logger.Error(Arg.Any<string>()))
                .Do(info => throw new Exception("fake exception"));

            var analyzer = new LogAnalyzerTwo(mockService, stubLogger)
            {
                MinNameLength = 10
            };

            analyzer.Analyze("Short.txt");

            mockService.Received().Write(Arg.Is<ErrorInfo>(info => info.Severity == 1000 && info.Message.Contains("fake exception")));
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

    public class ErrorInfo
    {
        public int Severity { get; set; }
        public string Message { get; set; }
    }

    public class FakeEmailService : IEmailService
    {
        public EmailInfo Email = null;

        public void SendEmail(EmailInfo emailInfo)
        {
            Email = emailInfo;
        }
    }

    public class FakeWebService : IWebService
    {
        public string LastError { get; private set; }
        public Exception ToThrow { get; set; }
        public string MessageToWebService { get; set; }

        public void LogError(string errorMessage)
        {
            LastError = errorMessage;

            if (ToThrow != null)
            {
                throw ToThrow;
            }
        }

        public void Write(string message)
        {
            MessageToWebService = message;
        }

        public void Write(ErrorInfo message)
        {
            throw new NotImplementedException();
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