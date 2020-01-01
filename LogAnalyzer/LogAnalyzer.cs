using LogAn.UnitTest.ExtensionManager;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            IExtensionManager mgr = new FileExtensionManager();

            return mgr.IsValid(fileName);
        }
    }
}