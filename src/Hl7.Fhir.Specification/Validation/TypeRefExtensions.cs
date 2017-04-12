/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System.Collections.Generic;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Validation
{
    internal static class TypeRefExtensions
    {
        public static FHIRAllTypes BaseType(this StructureDefinition sd)
        {
            var result = ModelInfo.FhirTypeNameToFhirType(sd.Type) ?? ModelInfo.FhirTypeNameToFhirType(sd.Id);

            if (result == null)
                throw Error.NotSupported($"Encountered profile '{sd.Url}', for which the declaring core type cannot be determined");

            return result.Value;
        }

        public static string ReadableName(this StructureDefinition sd) => sd.Derivation == StructureDefinition.TypeDerivationRule.Constraint ? sd.Url : sd.Id;

        public static string GetDeclaredProfiles(this ElementDefinition.TypeRefComponent typeRef)
        {
            if (typeRef.Code == FHIRAllTypes.Reference.GetLiteral())
            {
                if (!System.String.IsNullOrEmpty(typeRef.TargetProfile))
                {
                    return typeRef.TargetProfile;
                }
                // It needs to be a reference to a resource.
                return ModelInfo.CanonicalUriForFhirCoreType(FHIRAllTypes.Resource.GetLiteral());
            }
            if (!System.String.IsNullOrEmpty(typeRef.Profile))
            {
                return typeRef.Profile;
            }
            else if (!string.IsNullOrEmpty(typeRef.Code))
                return ModelInfo.CanonicalUriForFhirCoreType(typeRef.Code);
            else
                return null;
        }


        public static bool IsChoice(this ElementDefinition definition)
        {
            return definition.Type.Where(tr => tr.Code != null).Distinct().Count() > 1;
        }

        public static List<FHIRAllTypes> ChoiceTypes(this ElementDefinition definition)
        {
            return definition.Type.Where(tr => tr.Code != null).Select(tr => EnumUtility.ParseLiteral<FHIRAllTypes>(tr.Code).Value).Distinct().ToList();
        }


        public static string GetPrimitiveValueRegEx(this ElementDefinition.TypeRefComponent typeRef)
        {
            var regex = typeRef.GetStringExtension("http://hl7.org/fhir/StructureDefinition/structuredefinition-regex");

            if (regex == null) return null;

            if (regex.StartsWith("urn:oid:")) regex = regex.Substring(8);
            return regex;
        }
    }

}