﻿using System;
using NUnit.Framework;

namespace LogAn.UnitTest
{
    public class LogAnalyzer
    {
        public bool WasLastFileNameValid { get; set; }

        public bool IsValidLogFileName(string fileName)
        {
            WasLastFileNameValid = false;
            if (fileName.Equals(string.Empty))
            {
                throw new ArgumentException("filename has to be provided");
            }

            WasLastFileNameValid = true;

            return fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}