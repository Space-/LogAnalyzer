using Castle.Core.Logging;
using LogAn.UnitTest.ExtensionManager;
using LogAn.UnitTest.Interface;
using System.IO;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager _manager;
        private readonly IWebService _webService;
        private readonly ILogger _logger;
        public int MinNameLength = 8;
        public int MaxFileSize = 50;

        public LogAnalyzer()
        {
            _manager = ExtensionManagerFactory.Create();
        }

        public LogAnalyzer(IExtensionManager mgr)
        {
            _manager = mgr;
        }

        public LogAnalyzer(IWebService webService)
        {
            _webService = webService;
        }

        public LogAnalyzer(ILogger logger)
        {
            _logger = logger;
        }

        public virtual bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
        }

        protected virtual IExtensionManager GetManager()
        {
            return _manager;
        }

        public void AnalyzeFileName(string fileName)
        {
            if (fileName.Length < MinNameLength)
            {
                _webService.LogError($"Filename too short: {fileName}");
            }
        }

        public void AnalyzeFileSize(int fileSize)
        {
            if (fileSize > MaxFileSize)
            {
                _logger.Error($"File size {fileSize} too big, it should be less than max size {MaxFileSize}");
            }
        }
    }
}