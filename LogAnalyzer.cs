using System;

public class LogAnalyzer
{
    public bool IsValidLogFileName(string fileName)
    {
        if (!fileName.EndsWith(".sln"))
        {
            return false;
        }

        return true;
    }
}