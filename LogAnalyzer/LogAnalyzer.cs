namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        public bool IsValidLogFileName(string fileName)
        {
            if (!fileName.EndsWith(".slf"))
            {
                return false;
            }

            return true;
        }
    }
}