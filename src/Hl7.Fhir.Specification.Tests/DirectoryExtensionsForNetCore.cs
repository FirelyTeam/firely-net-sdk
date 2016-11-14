using System.IO;
using System;

namespace Hl7.Fhir.Specification.Tests
{
    public static class DirectoryExtensions
    {
        public static string GetCurrentBinDirectory()
        {
#if NETCore
            return  AppContext.BaseDirectory;
#else
            return Directory.GetCurrentDirectory();
#endif
        }
    }
}