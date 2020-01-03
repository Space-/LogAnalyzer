using System.IO;
using LogAn.UnitTest.ExtensionManager;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        private readonly IExtensionManager _manager;

        public LogAnalyzer()
        {
            _manager = ExtensionManagerFactory.Create();
        }

        public LogAnalyzer(IExtensionManager mgr)
        {
            _manager = mgr;
        }

        public bool IsValidLogFileName(string fileName)
        {
            return _manager.IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
        }
    }
}