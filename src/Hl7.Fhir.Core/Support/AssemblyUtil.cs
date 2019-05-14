using Hl7.Fhir.Utility;
using System.Reflection;

namespace Hl7.Fhir.Support
{
    public static class AssemblyUtil
    {
        public static EnumFhirVersion AssemblyVersion = GetFhirVersionForCurrentAssembly();

        private static EnumFhirVersion GetFhirVersionForCurrentAssembly()
        {
            var assemblyName = typeof(AssemblyUtil).GetTypeInfo().Assembly.FullName;
            if (assemblyName.Contains("Hl7.Fhir.STU3.Core"))
                return EnumFhirVersion.STU3;
            else if (assemblyName.Contains("Hl7.Fhir.R4.Core"))
                return EnumFhirVersion.R4;

            return EnumFhirVersion.Default;
        }
    }
}
