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
using Hl7.Fhir.Validation.Support;
using Hl7.FhirPath.Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal static class SchemaConverterExtensions
    {
        public static IElementSchema Convert(this ElementDefinition def, ISchemaResolver resolver, IElementDefinitionAssertionFactory assertionFactory)
        {

            var elements = new List<IAssertion>()
                .MaybeAdd(BuildMaxLength(def, assertionFactory))
                .MaybeAdd(BuildFixed(def, assertionFactory))
                .MaybeAdd(BuildPattern(def, assertionFactory))
                .MaybeAdd(BuildBinding(def, assertionFactory))
                .MaybeAdd(BuildMinValue(def, assertionFactory))
                .MaybeAdd(BuildMaxValue(def, assertionFactory))
                .MaybeAdd(BuildFp(def, assertionFactory))
                .MaybeAdd(BuildCardinality(def, assertionFactory))
                .MaybeAdd(BuildElementRegEx(def, assertionFactory))
                .MaybeAdd(BuildTypeRefRegEx(def, assertionFactory))

                //.MaybeAdd(BuildMinItems(def))
                //.MaybeAdd(BuildMaxItems(def))
                .MaybeAdd(BuildTypeRefValidation(def, resolver, assertionFactory))
               ;

            return assertionFactory.CreateElementSchemaAssertion(id: new Uri("#" + def.Path, UriKind.Relative), elements);
        }

        public static IAssertion ValueSlicingConditions(this ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory)
        {
            var elements = new List<IAssertion>()
                   .MaybeAdd(BuildFixed(def, assertionFactory))
                   .MaybeAdd(BuildPattern(def, assertionFactory))
                   .MaybeAdd(BuildBinding(def, assertionFactory));

            return new AllAssertion(elements);
        }

        private static IAssertion BuildBinding(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory)
            => def.Binding != null ? assertionFactory.CreateBindingAssertion(def.Binding.ValueSet, ConvertStrength(def.Binding.Strength), false, def.Binding.Description) : null;

        private static BindingAssertion.BindingStrength ConvertStrength(BindingStrength? strength)
        {
            // TODO Dirty cast
            return (BindingAssertion.BindingStrength)strength;

        }

        private static IAssertion BuildTypeRefRegEx(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory)
        {
            var list = new List<IAssertion>();

            foreach (var type in def.Type)
            {
                var assertion = BuildRegex(type, assertionFactory);
                if (assertion != null)
                {
                    list.Add(assertion);
                }

            }
            return list.Count > 0 ? assertionFactory.CreateElementSchemaAssertion(id: new Uri("#" + def.Path, UriKind.Relative), list) : null;
        }

        private static IAssertion BuildElementRegEx(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) => BuildRegex(def, assertionFactory);


        private static IAssertion BuildMinValue(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) =>
            def.MinValue != null ? assertionFactory.CreateMinMaxValueAssertion(def.MinValue.ToTypedElement(), Fhir.Validation.Impl.MinMax.MinValue) : null;

        private static IAssertion BuildMaxValue(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) =>
            def.MaxValue != null ? assertionFactory.CreateMinMaxValueAssertion(def.MaxValue.ToTypedElement(), Fhir.Validation.Impl.MinMax.MaxValue) : null;

        private static IAssertion BuildFixed(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) =>
            def.Fixed != null ? assertionFactory.CreateFixedValueAssertion(def.Fixed.ToTypedElement()) : null;

        private static IAssertion BuildPattern(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) =>
           def.Pattern != null ? assertionFactory.CreatePatternAssertion(def.Pattern.ToTypedElement()) : null;

        private static IAssertion BuildMaxLength(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) =>
            def.MaxLength.HasValue ? assertionFactory.CreateMaxLengthAssertion(def.MaxLength.Value) : null;

        private static IAssertion BuildFp(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory)
        {
            var list = new List<IAssertion>();
            foreach (var constraint in def.Constraint)
            {
                var bestPractice = constraint.GetBoolExtension("http://hl7.org/fhir/StructureDefinition/elementdefinition-bestpractice") ?? false;
                list.Add(assertionFactory.CreateFhirPathAssertion(constraint.Key, constraint.Expression, constraint.Human, Convert(constraint.Severity), bestPractice));
            }

            return list.Any() ? assertionFactory.CreateElementSchemaAssertion(id: new Uri("#constraints", UriKind.Relative), list) : null;

            IssueSeverity? Convert(ElementDefinition.ConstraintSeverity? constraintSeverity) => constraintSeverity switch
            {
                ElementDefinition.ConstraintSeverity.Error => IssueSeverity.Error,
                ElementDefinition.ConstraintSeverity.Warning => IssueSeverity.Warning,
                _ => null,
            };
        }

        private static IAssertion BuildCardinality(ElementDefinition def, IElementDefinitionAssertionFactory assertionFactory) =>
            def.Min != null || def.Max != null ? assertionFactory.CreateCardinalityAssertion(def.Min, def.Max, def.Path) : null;

        private static IAssertion BuildRegex(IExtendable elementDef, IElementDefinitionAssertionFactory assertionFactory)
        {
            var pattern = elementDef?.GetStringExtension("http://hl7.org/fhir/StructureDefinition/regex");
            return pattern != null ? assertionFactory.CreateRegexAssertion(pattern) : null;
        }

        // TODO this should be somewhere else
        private static AggregationMode? convertAggregationMode(ElementDefinition.AggregationMode? aggregationMode) => aggregationMode switch
        {
            ElementDefinition.AggregationMode.Bundled => AggregationMode.Bundled,
            ElementDefinition.AggregationMode.Contained => AggregationMode.Contained,
            ElementDefinition.AggregationMode.Referenced => AggregationMode.Referenced,
            _ => null
        };

        public static IAssertion BuildTypeRefValidation(this ElementDefinition def, ISchemaResolver resolver, IElementDefinitionAssertionFactory assertionFactory)
        {
            var builder = new TypeCaseBuilder(resolver, assertionFactory);

            var typeRefs = from tr in def.Type
                           let profile = tr.GetDeclaredProfiles()
                           where profile != null
                           select (code: tr.Code, profile, tr.Aggregation.Select(a => convertAggregationMode(a)));

            //Distinguish between:
            // * elem with a single TypeRef - does not need any slicing
            // * genuine choice elements (suffix [x]) - needs to be sliced on FhirTypeLabel 
            // * elem with multiple TypeRefs - without explicit suffix [x], this is a slice 
            // without discriminator

            //if (def.IsPrimitiveConstraint())
            // {
            //    return builder.BuildProfileRef("System.String", "http://hl7.org/fhir/StructureDefinition/System.String"); // TODO MV: this was profile and not profile.Single()
            //}

            /*
            var result = Assertions.Empty;
            foreach (var (code, profile) in typeRefs)
            {
                result += new AnyAssertion(profile.Select(p => builder.BuildProfileRef(code, p)));
            }
            return result.Count > 0 ? new AnyAssertion(result) : null;
            */

            /*
            if (def.Slicing != null)
            {
                return BuildSlicing(def);
            }
            */


            if (isChoice(def))
            {
                var typeCases = typeRefs
                    .GroupBy(tr => tr.code)
                    .Select(tc => (code: tc.Key, profiles: tc.SelectMany(dp => dp.profile)));

                return builder.BuildSliceAssertionForTypeCases(typeCases);
            }
            else
            {
                var result = Assertions.Empty;
                foreach (var (code, profile, aggregations) in typeRefs)
                {
                    result += new AnyAssertion(profile.Select(p => builder.BuildProfileRef(code, p, aggregations)));
                }
                return result.Count > 0 ? new AnyAssertion(result) : null;
            }
            /*else if (typeRefs.Count() == 1)
            {
                var (code, profile) = typeRefs.Single();
                var assertion = new FhirTypeLabel(code, "TODO");

                var profileAssertions = new AnyAssertion(profile.Select(p => builder.BuildProfileRef(code, p)));
                return new AllAssertion(assertion, profileAssertions);
            }
            else
                return new TraceText("TODO");*/
            //return builder.BuildSliceForProfiles(typeRefs.Select(tr => tr.profile));
            // return new AnyAssertion(typeRefs.SelectMany(t => t.profile.Select(p => builder.BuildProfileRef(t.code, p))));

            //if (typeRefs.Count() == 1)
            //    return builder.BuildProfileRef(typeRefs.Single().code, typeRefs.Single().profile.Single()); // TODO MV: this was profile and not profile.Single()


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



            */
            //return null;
            bool isChoice(ElementDefinition d) => d.Base?.Path?.EndsWith("[x]") == true ||
                            d.Path.EndsWith("[x]");
        }

        private static List<IAssertion> MaybeAdd(this List<IAssertion> assertions, IAssertion element)
        {
            if (element != null)
                assertions.Add(element);

            return assertions;
        }
    }
}
