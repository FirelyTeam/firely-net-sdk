using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests
{
    internal class TestContextIssue2117 : TestContextBase
    {
        public static IEnumerable<object[]> TestCases
        {
            get
            {
                yield return new object[] { "https://example.org/fhir/StructureDefinition/MyZibAdvanceDirective", "Consent.extension:comment.value[x]" };
                yield return new object[] { "https://example.org/fhir/StructureDefinition/MyZibAdvanceDirective", "Consent.extension:disorder.value[x]" };
                yield return new object[] { "http://nictiz.nl/fhir/StructureDefinition/zib-AdvanceDirective", "Consent.extension:disorder.value[x]" };
            }
        }

        public TestContextIssue2117() : base("Issue-2117")
        {
        }
    }
}
