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
                    _email.SendEmail("someone@somewhere.com", "can't log", e.Message);
                }
            }
        }
    }
}