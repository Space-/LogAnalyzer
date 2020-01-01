using System;

namespace LogAn.UnitTest.ExtensionManager
{
    public class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            //            WasLastFileNameValid = false;
            if (fileName.Equals(string.Empty))
            {
                throw new ArgumentException("filename has to be provided");
            }

            //            WasLastFileNameValid = true;

            return fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}