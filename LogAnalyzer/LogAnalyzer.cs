using System;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (fileName.Equals(string.Empty))
            {
                throw new ArgumentException("filename has to be provided");
            }
            return fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}