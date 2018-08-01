/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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
        public static FHIRDefinedType BaseType(this StructureDefinition sd)
        {
            var result = sd.ConstrainedType ?? EnumUtility.ParseLiteral<FHIRDefinedType>(sd.Id);

            if (result == null)
                throw Error.NotSupported($"Encountered profile '{sd.Url}', for which the declaring core type cannot be determined");

            return result.Value;
        }

        public static string ReadableName(this StructureDefinition sd) => sd.ConstrainedType != null ? sd.Url : sd.Id;

        public static string GetDeclaredProfiles(this ElementDefinition.TypeRefComponent typeRef)
        {
            if (typeRef.Profile.Any())
            {
                return typeRef.Profile.First();     // Take the first, this will disappear in STU3 anyway
            }
            else if (typeRef.Code.HasValue)
                return ModelInfo.CanonicalUriForFhirCoreType(typeRef.Code.Value);
            else
                return null;
        }


        public static bool IsChoice(this ElementDefinition definition)
        {
            return definition.Type.Where(tr => tr.Code != null).Distinct().Count() > 1;
        }

        public static List<FHIRDefinedType> ChoiceTypes(this ElementDefinition definition)
        {
            return definition.Type.Where(tr => tr.Code != null).Select(tr => tr.Code.Value).Distinct().ToList();
        }
    }
}