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


        internal class TypeCaseBuilder
        {
            public readonly ISchemaResolver Resolver;

            public TypeCaseBuilder(ISchemaResolver resolver)
            {
                Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            }

            public SliceAssertion BuildSliceAssertionForTypeCases(IEnumerable<(FHIRDefinedType? code, IEnumerable<string> profiles)> typeCases)
            {
                // typeCases have a unique key, so there's only one default case, even though
                // we use SelectMany(). It's either empty or has a list of profiles for the only
                // default case (where Code = null in the typeref)
                var defaultCases = typeCases.Where(tc => tc.code == null)
                    .SelectMany(tc => tc.profiles);
                var sliceCases = typeCases.Where(tc => tc.code != null)                                
                    .Select(tc => buildSliceForTypeCase(tc.code.Value, tc.profiles));

                var defaultSlice =
                    defaultCases.Any() ?
                        (IAssertion)BuildSliceForProfiles(defaultCases) : buildSliceFailure();

                return new SliceAssertion(ordered: false, @default: defaultSlice, sliceCases);

                IAssertion buildSliceFailure()
                {
                    var allowedCodes = String.Join(",", typeCases.Select(t => $"'{t.code?.GetLiteral() ?? "(any)"}'"));
                    return
                        new ResultAssertion(ValidationResult.Failure,
                        new Trace($"Element is a choice, but the instance does not use of the allowed choice types ({allowedCodes})",
                             Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE));
                }

                SliceAssertion.Slice buildSliceForTypeCase(FHIRDefinedType code, IEnumerable<string> profiles) 
                    => new SliceAssertion.Slice(code.GetLiteral(),
                        new FhirTypeLabel(code), BuildSliceForProfiles(profiles));
            }


            public IAssertion BuildSliceForProfiles(IEnumerable<string> profiles)
            {
                // "special" case, only one possible profile, no need to build a nested
                // discriminatorless slicer to validate possible options
                if (profiles.Count() == 1) return BuildProfileRef(profiles.Single());

                var sliceCases = profiles.Select(p => buildSliceForProfile(p));

                return new SliceAssertion(ordered: false, @default: buildSliceFailure(), sliceCases);

                IAssertion buildSliceFailure()
                {
                    var allowedProfiles = String.Join(",", profiles.Select(p => $"'{p}'")); 
                    return
                        new ResultAssertion(ValidationResult.Failure,
                        new Trace($"Element does not validate against any of the expected profiles ({allowedProfiles})",
                             Issue.CONTENT_ELEMENT_HAS_INCORRECT_TYPE));
                }

                SliceAssertion.Slice buildSliceForProfile(string profile)
                    => new SliceAssertion.Slice(makeSliceName(profile),
                        BuildProfileRef(profile), ResultAssertion.Success);

                string makeSliceName(string profile) =>
                    new String(profile.Where(c => Char.IsLetterOrDigit(c)).ToArray());
            }

            public ReferenceAssertion BuildProfileRef(string profile)
            {
                var uri = new Uri(profile, UriKind.Absolute);
                return new ReferenceAssertion(() => Resolver.GetSchema(uri), uri);
            }
        }
    }
}

