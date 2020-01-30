using LogAn.UnitTest.Interface;
using System;

namespace LogAn.UnitTest
{
    public class LogAnalyzerTwo
    {
        private readonly IWebService _service;
        private readonly IEmailService _email;

        public LogAnalyzerTwo(IWebService service, IEmailService email)
        {
            _service = service;
            _email = email;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
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
    }
}