using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Schema;
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
                .MaybeAdd(BuildMaxItems(def));

            return new ElementSchema(id: new Uri("#" + def.Path, UriKind.Relative), elements);
        }

        private static List<IAssertion> MaybeAdd(this List<IAssertion> assertions, IAssertion element)
        {
            if (element != null)
                assertions.Add(element);

            return assertions;
        }

        public static Pattern BuildElementRegEx(this ElementDefinition def)
        {
            return buildPattern(def, "http://hl7.org/fhir/StructureDefinition/regex");
        }

        public static Pattern BuildTypeRefRegEx(this ElementDefinition def)
        {
            if (def.IsPrimitiveValueConstraint() && def.Type.Count == 1)
                return buildPattern(def.Type.Single(), "http://hl7.org/fhir/StructureDefinition/structuredefinition-regex");
            else
                return null;
        }

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
    }
}

