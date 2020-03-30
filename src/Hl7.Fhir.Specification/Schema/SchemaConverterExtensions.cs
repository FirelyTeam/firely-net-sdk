/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
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
                .MaybeAdd(BuildFixed(def))
                .MaybeAdd(BuildMinValue(def))
                .MaybeAdd(BuildMaxValue(def))
                .MaybeAdd(BuildFp(def))
                .MaybeAdd(BuildCardinality(def))
                .MaybeAdd(BuildRegex(def))
                //.MaybeAdd(BuildElementRegEx(def))
                //.MaybeAdd(BuildTypeRefRegEx(def))
                //.MaybeAdd(BuildMinItems(def))
                //.MaybeAdd(BuildMaxItems(def))
                .MaybeAdd(BuildTypeRefValidation(def, resolver))
               ;

            return new ElementSchema(id: new Uri("#" + def.Path, UriKind.Relative), elements);
        }



        private static IAssertion BuildMinValue(ElementDefinition def) =>
            def.MinValue != null ? new MinMaxValue("TODO", def.MinValue.ToTypedElement(), Fhir.Validation.Impl.MinMax.MinValue) : null;

        private static IAssertion BuildMaxValue(ElementDefinition def) =>
            def.MaxValue != null ? new MinMaxValue("TODO", def.MaxValue.ToTypedElement(), Fhir.Validation.Impl.MinMax.MaxValue) : null;

        private static IAssertion BuildFixed(ElementDefinition def) =>
            def.Fixed != null ? new Fixed("TODO", def.Fixed.ToTypedElement()) : null;

        private static IAssertion BuildMaxLength(ElementDefinition def) =>
            def.MaxLength.HasValue ? new MaxLength(def.ElementId ?? def.Path, def.MaxLength.Value) : null;

        private static IAssertion BuildFp(ElementDefinition def)
        {
            var list = new List<IAssertion>();
            foreach (var constraint in def.Constraint)
            {
                var bestPractice = constraint.GetBoolExtension("http://hl7.org/fhir/StructureDefinition/elementdefinition-bestpractice") ?? false;
                list.Add(new FhirPathAssertion("TODO", constraint.Key, constraint.Expression, Convert(constraint.Severity), bestPractice));
            }

            return list.Count > 0 ? new ElementSchema(id: new Uri("#" + def.Path, UriKind.Relative), list) : null;

            IssueSeverity? Convert(ElementDefinition.ConstraintSeverity? constraintSeverity)
            {
                switch (constraintSeverity)
                {
                    case ElementDefinition.ConstraintSeverity.Error:
                        return IssueSeverity.Error;
                    case ElementDefinition.ConstraintSeverity.Warning:
                        return IssueSeverity.Warning;
                    case null:
                    default:
                        return null;
                }
            }
        }

        private static IAssertion BuildCardinality(ElementDefinition def) =>
            def.Min != null || def.Max != null ? new CardinalityAssertion(def.Min, def.Max, def.Path) : null;

        private static IAssertion BuildRegex(ElementDefinition def)
        {
            var list = new List<IAssertion>();

            foreach (var type in def.Type)
            {
                var pattern = type?.GetStringExtension("http://hl7.org/fhir/StructureDefinition/regex");
                if (pattern != null)
                {
                    list.Add(new RegExAssertion("TODO", pattern));
                }

            }
            return list.Count > 0 ? new ElementSchema(id: new Uri("#" + def.Path, UriKind.Relative), list) : null;
        }

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

            if (def.IsPrimitiveConstraint())
            {
                return builder.BuildProfileRef("http://hl7.org/fhir/StructureDefinition/System.String"); // TODO MV: this was profile and not profile.Single()
            }

            if (typeRefs.Count() == 1)
                return builder.BuildProfileRef(typeRefs.Single().profile.Single()); // TODO MV: this was profile and not profile.Single()


            /*
             * Identifier:[][]
             * HumanName:[HumanNameDE,HumanNameBE]:[]
             * Reference:[WithReqDefinition,WithIdentifier]:[Practitioner,OrganizationBE]
             * 
             * Any
             * {
             *     {
             *          InstanceType: "Identifier"
             *          ref: "http://hl7.org/SD/Identifier"
             *     }
             *     {
             *          InstanceType: "HumanName"
             *          Any { ref: "HumanNameDE", ref: "HumanNameBE" }
             *     },
             *     {
             *          InstanceType: "Reference"
             *           Any { ref: "WithReqDefinition", ref: "WithIdentifier" }
             *          Any { validate: [http://example4] [http://hl7.oerg/fhir/SD/Practitioner],
             *              validate: [http://example] [http://..../OrganizationBE] } 
             *     }
             * }
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
