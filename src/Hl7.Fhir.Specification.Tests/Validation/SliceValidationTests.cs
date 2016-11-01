using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Validation
{
    public class SliceValidationTests : IClassFixture<ResolverFixture>
    {
        private IResourceResolver _resolver;

        public SliceValidationTests(ResolverFixture fixture)
        {
            _resolver = fixture.Resolver;
        }

        [Fact]
        public void TestBasicTelecomSlice()
        {
            var sd = _resolver.FindStructureDefinition("http://example.com/StructureDefinition/patient-telecom-slice-ek");
            Assert.NotNull(sd);

            var snapgen = new SnapshotGenerator(_resolver);
            snapgen.Update(sd);

            var xml = FhirSerializer.SerializeResourceToXml(sd);
            File.WriteAllText(@"c:\temp\sdout.xml", xml);
        }

    }
}
