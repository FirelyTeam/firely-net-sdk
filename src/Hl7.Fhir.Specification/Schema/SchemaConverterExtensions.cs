/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Validation.Impl;
using Hl7.Fhir.Validation.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal static class SchemaConverterExtensions
    {
        public static ElementSchema Convert(this ElementDefinition def, ISchemaResolver resolver)
        {

            var elements = new List<IAssertion>()
                .MaybeAdd(BuildMaxLength(def))
                //.MaybeAdd(BuildElementRegEx(def))
                //.MaybeAdd(BuildTypeRefRegEx(def))
                //.MaybeAdd(BuildMinItems(def))
                //.MaybeAdd(BuildMaxItems(def))
                .MaybeAdd(BuildTypeRefValidation(def, resolver))
               ;

            return new ElementSchema(id: new Uri("#" + def.Path, UriKind.Relative), elements);
        }



        private static MaxLength BuildMaxLength(ElementDefinition def) =>
            def.IsPrimitiveValueConstraint() && def.Type.Count == 1 && def.MaxLength.HasValue
            ? new MaxLength(def.MaxLength.Value) : null;

        public static IAssertion BuildTypeRefValidation(this ElementDefinition def, ISchemaResolver resolver)
        {
            var builder = new TypeCaseBuilder(resolver);

            var typeRefs = from tr in def.Type
                           let profile = tr.GetDeclaredProfiles()
                           where profile != null
                           select (code: tr.Code, profile);

            //Distinguish between:
            // * elem with a single TypeRef - does not need any slicing
            // * genuine choice elements (suffix [x]) - needs to be sliced on FhirTypeLabel 
            // * elem with multiple TypeRefs - without explicit suffix [x], this is a slice 
            // without discriminator

            if (typeRefs.Count() == 1)
                return builder.BuildProfileRef(typeRefs.Single().profile.Single()); // TODO MV: this was profile and not profile.Single()

            /*
            if (isChoice(def))
            {
                var typeCases = typeRefs
                    .GroupBy(tr => tr.code)
                    .Select(tc => (code: tc.Key, profiles: tc.Select(dp => dp.profile)));

                return builder.BuildSliceAssertionForTypeCases(typeCases);
            }
            else if (typeRefs.Count() == 1)
                return builder.BuildProfileRef(typeRefs.Single().profile);
            else
                return builder.BuildSliceForProfiles(typeRefs.Select(tr => tr.profile));

            bool isChoice(ElementDefinition d) => d.Base?.Path?.EndsWith("[x]") == true ||
                            d.Path.EndsWith("[x]");

            */
            return null;
        }

        private static List<IAssertion> MaybeAdd(this List<IAssertion> assertions, IAssertion element)
        {
            if (element != null)
                assertions.Add(element);

            return assertions;
        }
    }
}
