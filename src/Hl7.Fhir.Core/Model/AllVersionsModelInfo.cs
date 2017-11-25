using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public partial class AllVersionsModelInfo
    {
        /// <summary>Determines if the specified value represents the name of a known FHIR resource.</summary>
        public static bool IsKnownResource(string name)
        {
            return SupportedResources.Contains(name);
        }

        /// <summary>Returns the FHIR type name represented by the specified C# <see cref="Type"/>, or <c>null</c>.</summary>
        public static string GetFhirTypeNameForType(Type type)
        {
            string result;
            FhirCsTypeToString.TryGetValue(type, out result);
            return result;
        }
    }
}
