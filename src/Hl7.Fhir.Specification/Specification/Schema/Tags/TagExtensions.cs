using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Specification.Schema.Tags
{
    public static class TagExtensions
    {
        public static SchemaTags Validate(this IGroupAssertion assertion, IElementNavigator input, ValidationContext vc)
            => assertion.Validate(new[] { input }, vc);

        public static SchemaTags SucceedWith(this TaggedAssertion ta, string message)
            => ta.Success + new TraceTag(message);

        public static SchemaTags FailWith(this TaggedAssertion ta, string message)
            => ta.Failure + new TraceTag(message);
    }
}

