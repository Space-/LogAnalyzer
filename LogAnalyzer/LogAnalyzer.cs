using System;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        public bool WasLastFileNameValid { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            var mgr = new FileExtensionManager();
            return mgr.IsValid(fileName);
        }
    }

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

    public interface IExtensionManager
    {
        bool IsValid(string fileName);
    }
}