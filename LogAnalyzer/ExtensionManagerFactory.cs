using LogAn.UnitTest.ExtensionManager;

namespace LogAn.UnitTest
{
    public class ExtensionManagerFactory
    {
        private static IExtensionManager _customerManager;

        public static void SetManager(IExtensionManager mgr)
        {
            _customerManager = mgr;
        }

        public static IExtensionManager Create()
        {
            if (_customerManager != null)
            {
                return _customerManager;
            }

            return new FileExtensionManager();
        }
    }
}