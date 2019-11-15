using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTestAnalyzer
{

    /// <summary>
    /// This attribute needs to be added to the desired project, and decorate the members to be checked for version
    /// See TestClass as an example
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class FhirVersionAttribute : Attribute
    {
        public string StartingVersion { get; set; }
        public string EndingVersion { get; set; }
        public string ExcludedVersions { get; set; }
    }
}
