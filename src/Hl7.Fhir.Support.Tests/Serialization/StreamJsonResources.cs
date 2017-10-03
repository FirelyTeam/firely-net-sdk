using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    public class StreamJsonResources
    {
        [Fact]
        public void ScanThroughBundle()
        {
            var jsonBundle = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\profiles-types.json");
            using (var stream = new JsonNavigatorStream(jsonBundle))
            {
                Assert.True(stream.IsBundle);
                Assert.Null(stream.Current);

                Assert.True(stream.MoveNext());
                Assert.True(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/integer", stream.Position);

                var nav = stream.Current;
                Assert.True(nav.MoveToFirstChild("name"));
                Assert.Equal("integer", nav.Value);

                var current = stream.Position;

                Assert.True(stream.MoveNext());
                Assert.True(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/unsignedInt", stream.Position);

                stream.Seek(current);
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/integer", stream.Position);

                stream.Reset();
                Assert.True(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/markdown", stream.Position);

                while (stream.MoveNext()) ;     // read to end

                Assert.Equal("http://hl7.org/fhir/StructureDefinition/SimpleQuantity", stream.Position);

                // Reading past end does not change position
                Assert.False(stream.MoveNext());
                Assert.Equal("http://hl7.org/fhir/StructureDefinition/SimpleQuantity", stream.Position);
            }
        }

        [Fact]
        public void ScanThroughSingle()
        {
            var xmlPatient = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\fp-test-patient.json");
            using (var stream = new JsonNavigatorStream(xmlPatient))
            {
                Assert.False(stream.IsBundle);
                Assert.Equal("Patient", stream.ResourceType);
                Assert.Null(stream.Current);

                Assert.True(stream.MoveNext());
                Assert.Equal("http://example.org/Patient/pat1", stream.Position);
                var current = stream.Position;

                var nav = stream.Current;
                Assert.True(nav.MoveToFirstChild("gender"));
                Assert.Equal("male", nav.Value);

                stream.Reset();
                stream.Seek(current);
                Assert.Equal("http://example.org/Patient/pat1", stream.Position);

                Assert.False(stream.MoveNext());
                Assert.Equal("http://example.org/Patient/pat1", stream.Position);
            }
        }

        [Fact]
        public void ReadCrap()
        {
            // Try a random other xml file
            var jsonfile = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\source-test\project.assets.json");

            using (var stream = new JsonNavigatorStream(jsonfile))
            {
                Assert.Null(stream.ResourceType);
                Assert.False(stream.MoveNext());
            }
        }
    }
}
