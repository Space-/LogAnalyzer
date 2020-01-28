using LogAn.UnitTest.ExtensionManager;
using System.IO;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager _manager;
        private readonly IWebService _webService;

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

        public virtual bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
        }

        protected virtual IExtensionManager GetManager()
        {
            return _manager;
        }

        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                _webService.LogError($"Filename too short: {fileName}");
            }
        }
    }

    public interface IWebService
    {
        void LogError(string fileName);
    }
}