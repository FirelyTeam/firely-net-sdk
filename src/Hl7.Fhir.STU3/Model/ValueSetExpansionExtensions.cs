using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public static class ValueSetExpansionExtensions
    {
     
        public static ValueSet.ContainsComponent FindCode(this IEnumerable<ValueSet.ContainsComponent> cnt, string code, string system=null)
        {
            foreach (var contains in cnt)
            {
                var result = contains.FindCode(code, system);
                if (result != null) return result;
            }

            return null;
        }


        public static ValueSet.ContainsComponent FindCode(this ValueSet.ContainsComponent contains, string code, string system=null)
        {
            // Direct hit
            if (code == contains.Code && (system == null || system == contains.System))
                return contains;

            // Not in this node, but this node may have child nodes to check
            if (contains.Contains != null && contains.Contains.Any())
                return contains.Contains.FindCode(code, system);
            else
                return null;
        }
    }
    
    //public static class ValueSetExtensionExtensions
    //{
    //    public const string EXT_DEPRECATED = "http://hl7.org/fhir/StructureDefinition/valueset-deprecated";

    //    public static bool? GetDeprecated(this ValueSet.ConceptDefinitionComponent def)
    //    {
    //        return def.GetBoolExtension(EXT_DEPRECATED);
    //    }

    //    public static void SetDeprecated(this ValueSet.ConceptDefinitionComponent def, bool value)
    //    {
    //        def.SetBoolExtension(EXT_DEPRECATED, value);
    //    }
    //}
}
