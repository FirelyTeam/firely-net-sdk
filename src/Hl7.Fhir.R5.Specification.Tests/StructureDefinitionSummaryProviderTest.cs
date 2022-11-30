using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests
{
    public partial class StructureDefinitionSummaryProviderTest
    {
        [TestMethod]
        [Ignore("This should be fixed with the next release R5:  type of Account.coverage.id differ! poco: string, sd: id")]
        public void PocoAndSdSummaryProvidersShouldBeEqual()
        {
            assertPocoAndSdSummaryProviders();
        }
    }
}
