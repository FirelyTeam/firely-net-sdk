using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    [TestClass]
    public class StreamJsonResources
    {
        [TestMethod]
        public void ScanThroughBundle()
        {
            var jsonBundle = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\profiles-types.json");
            using (var stream = JsonNavigatorStream.FromPath(jsonBundle))
            {
                Assert.IsTrue(stream.IsBundle);
                Assert.IsNull(stream.Current);

                Assert.IsTrue(stream.MoveNext());
                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/integer", stream.Position);

                var nav = stream.Current;
                Assert.IsTrue(nav.MoveToFirstChild("name"));
                Assert.AreEqual("integer", nav.Value);

                var current = stream.Position;

                Assert.IsTrue(stream.MoveNext());
                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/unsignedInt", stream.Position);

                stream.Seek(current);
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/integer", stream.Position);

                stream.Reset();
                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/markdown", stream.Position);

                while (stream.MoveNext()) ;     // read to end

                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/SimpleQuantity", stream.Position);

                // Reading past end does not change position
                Assert.IsFalse(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/SimpleQuantity", stream.Position);
            }
        }

        [TestMethod]
        public void ScanThroughSingle()
        {
            var xmlPatient = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\fp-test-patient.json");
            using (var stream = JsonNavigatorStream.FromPath(xmlPatient))
            {
                Assert.IsFalse(stream.IsBundle);
                Assert.AreEqual("Patient", stream.ResourceType);
                Assert.IsNull(stream.Current);

                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://example.org/Patient/pat1", stream.Position);
                var current = stream.Position;

                var nav = stream.Current;
                Assert.IsTrue(nav.MoveToFirstChild("gender"));
                Assert.AreEqual("male", nav.Value);

                stream.Reset();
                stream.Seek(current);
                Assert.AreEqual("http://example.org/Patient/pat1", stream.Position);

                Assert.IsFalse(stream.MoveNext());
                Assert.AreEqual("http://example.org/Patient/pat1", stream.Position);
            }
        }

        [TestMethod]
        public void ReadCrap()
        {
            // Try a random other xml file
            var jsonfile = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\source-test\project.assets.json");

            using (var stream = JsonNavigatorStream.FromPath(jsonfile))
            {
                Assert.IsNull(stream.ResourceType);
                Assert.IsFalse(stream.MoveNext());
            }
        }

        [TestMethod]
        public void ScanPerformance()
        {
            var xmlBundle = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\profiles-types.json");

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 250; i++)
            {
                using (var stream = JsonNavigatorStream.FromPath(xmlBundle))
                {
                    Assert.IsTrue(stream.MoveNext("http://hl7.org/fhir/StructureDefinition/SimpleQuantity"));
                }
            }

            sw.Stop();
            Debug.WriteLine($"Scanning took {sw.ElapsedMilliseconds / 250} ms");
        }
    }
}
