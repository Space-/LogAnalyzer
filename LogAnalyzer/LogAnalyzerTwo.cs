using LogAn.UnitTest.Interface;
using System;
using Castle.Core.Logging;

namespace LogAn.UnitTest
{
    public class LogAnalyzerTwo
    {
        private readonly IWebService _service;
        private readonly IEmailService _email;
        private readonly ILogger _logger;

        public int MinNameLength = 8;
        public int MaxFileSize = 50;

        public LogAnalyzerTwo(IWebService service, IEmailService email)
        {
            _service = service;
            _email = email;
        }

        public LogAnalyzerTwo(IWebService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < MinNameLength)
            {
                try
                {
                    _service.LogError($"Filename too short: {fileName}");
                }
                catch (Exception e)
                {
                    _email.SendEmail(new EmailInfo()
                    {
                        To = "someone@somewhere.com",
                        Body = e.Message,
                        Subject = "can't log"
                    });
                }
            }
        }

        public void AnalyzeFileSize(int fileSize)
        {
            if (fileSize > MaxFileSize)
            {
                try
                {
                    _logger.Error($"File size {fileSize} too big, it should be less than max size {MaxFileSize}");
                }
                catch (Exception e)
                {
                    _service.Write($"Error from logger: {e}");
                }
            }
        }
    }
}