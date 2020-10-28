/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
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
        public static string ReadableName(this StructureDefinition sd) => sd.Derivation == StructureDefinition.TypeDerivationRule.Constraint ? sd.Url : sd.Id;

        public static string GetDeclaredProfiles(this ElementDefinition.TypeRefComponent typeRef)
        {
            if (!System.String.IsNullOrEmpty(typeRef.Profile))
                return typeRef.Profile;
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
    }
}