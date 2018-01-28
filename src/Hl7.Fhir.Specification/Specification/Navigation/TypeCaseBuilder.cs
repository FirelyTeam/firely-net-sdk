using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Schema;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
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

