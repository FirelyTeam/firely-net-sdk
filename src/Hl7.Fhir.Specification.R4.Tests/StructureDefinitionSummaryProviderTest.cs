using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Specification.Tests
{
    public partial class StructureDefinitionSummaryProviderTest
    {
        [TestMethod]
        public void PocoAndSdSummaryProvidersShouldBeEqual()
        {
            assertPocoAndSdSummaryProviders();
        }
    }
}
