using LogAn.UnitTest.ExtensionManager;
using System.IO;

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

        public virtual bool IsValidLogFileName(string fileName)
        {
            return GetManager().IsValid(fileName) && Path.GetFileNameWithoutExtension(fileName).Length > 5;
        }

        protected virtual IExtensionManager GetManager()
        {
            return _manager;
        }
    }
}