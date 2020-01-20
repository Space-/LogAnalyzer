using LogAn.UnitTest.ExtensionManager;

namespace LogAn.UnitTest
{
    public class TestableLogAnalyzer : LogAnalyzer
    {
        private readonly IExtensionManager _manager;

        public TestableLogAnalyzer(IExtensionManager manager)
        {
            _manager = manager;
        }

        //        protected override IExtensionManager GetManager()
        //        {
        //            return _manager;
        //        }

        public override bool IsValidLogFileName(string fileName)
        {
            return fileName.Equals("file.ext");
        }
    }
}