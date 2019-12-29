using System;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            return fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}