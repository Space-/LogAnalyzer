using LogAn.UnitTest.ExtensionManager;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        private IExtensionManager _manager;

        public LogAnalyzer()
        {
        }

        public LogAnalyzer(IExtensionManager mgr)
        {
            _manager = mgr;
        }

        public bool IsValidLogFileName(string fileName)
        {
            return _manager.IsValid(fileName);
        }
    }
}