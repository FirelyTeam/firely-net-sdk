using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;

namespace Hl7.Fhir.Specification.Schema
{
    public static class ValidationExtensions
    {
        public static SchemaTags Validate(this Assertion assertion, IElementNavigator input, ValidationContext vc)
        {
            if (assertion is IGroupAssertion iga)
                return iga.Validate(input, vc);
            else if (assertion is IMemberAssertion ima)
                return ima.Validate(input, vc);
            else
                return SchemaTags.Success;
        }

        public static SchemaTags Validate(this IGroupAssertion assertion, IElementNavigator input, ValidationContext vc)
            => assertion.Validate(new[] { input }, vc);

        public static Assertion OnSuccess(this Assertion ass, SchemaTags tags)
            => new AssertionTagger(ass, ValidationResult.Success, tags);
        public static Assertion OnSuccess(this Assertion ass, SchemaTag tag)
            => OnSuccess(ass, new SchemaTags(tag));

        public static Assertion OnFailure(this Assertion ass, SchemaTags tags)
            => new AssertionTagger(ass, ValidationResult.Failure, tags);
        public static Assertion OnFailure(this Assertion ass, SchemaTag tag)
            => OnFailure(ass, new SchemaTags(tag));

        public static Assertion OnUndecided(this Assertion ass, SchemaTags tags)
            => new AssertionTagger(ass, ValidationResult.Undecided, tags);
        public static Assertion OnUndecided(this Assertion ass, SchemaTag tag)
            => OnUndecided(ass, new SchemaTags(tag));

    }
}

