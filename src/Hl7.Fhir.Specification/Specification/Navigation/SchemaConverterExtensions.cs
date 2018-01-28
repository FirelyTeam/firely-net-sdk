using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    internal static class SchemaConverterExtensions
    {
        public static ElementSchema Convert(this ElementDefinition def, ISchemaResolver resolver)
        {
            var elements = new List<IAssertion>()
                .MaybeAdd(BuildElementRegEx(def))
                .MaybeAdd(BuildTypeRefRegEx(def))
                .MaybeAdd(BuildMinItems(def))
                .MaybeAdd(BuildMaxItems(def))
                .MaybeAdd(BuildTypeRefValidation(def, resolver));

            return new ElementSchema(id: new Uri("#" + def.Path, UriKind.Relative), elements);
        }

        private static List<IAssertion> MaybeAdd(this List<IAssertion> assertions, IAssertion element)
        {
            if (element != null)
                assertions.Add(element);

            return assertions;
        }

        public static Pattern BuildElementRegEx(this ElementDefinition def) =>
            def.IsPrimitiveValueConstraint() ?
                buildPattern(def, "http://hl7.org/fhir/StructureDefinition/regex") : null;

        public static Pattern BuildTypeRefRegEx(this ElementDefinition def) =>
            def.IsPrimitiveValueConstraint() && def.Type.Count == 1 ?
                buildPattern(def.Type.Single(), "http://hl7.org/fhir/StructureDefinition/structuredefinition-regex") : null;

        private static Pattern buildPattern(IExtendable element, string uri)
        {
            var pattern = element.GetStringExtension(uri);
            if (pattern == null) return null;

            return new Pattern(pattern);
        }

        public static MinItems BuildMinItems(this ElementDefinition def) =>
            def.Min != null && def.Min != 0 ? new MinItems(def.Min.Value) : null;
        // Note - required in the original validator:
        //    validator.Trace(outcome, $"Element definition does not specify a 'min' value, which is required. Cardinality has not been validated",



        public static MaxItems BuildMaxItems(this ElementDefinition def) =>
            def.Max != null && def.Max != "*" ? new MaxItems(Int32.Parse(def.Max)) : null;
        // Note - required in the original validator:
        //      validator.Trace(outcome, $"Element definition does not specify a 'max' value, which is required. Cardinality has not been validated",
        //            Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING, parent);

        //TODO: Won't resolve contained SDs, convert to define schema constructs?
        //TODO: Must resolve locally defined schemas
        //TODO: Treat Reference types differently (DSTU2 only)

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
        }
    }
}

