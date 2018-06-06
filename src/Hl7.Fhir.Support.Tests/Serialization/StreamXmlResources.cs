using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    [TestClass]
    public class StreamXmlResources
    {
        [TestMethod]
        public void ScanThroughBundle()
        {
            var xmlBundle = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\profiles-types.xml");
            using (var stream = XmlNavigatorStream.FromPath(xmlBundle))
            {
                Assert.IsTrue(stream.IsBundle);
                Assert.IsNull(stream.Current);

                Assert.IsTrue(stream.MoveNext());
                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/dateTime", stream.Position);

                var nav = stream.Current;
                Assert.IsTrue(nav.MoveToFirstChild("name"));
                Assert.AreEqual("dateTime", nav.Value);

                var current = stream.Position;

                Assert.IsTrue(stream.MoveNext());
                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/string", stream.Position);

                stream.Seek(current);
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/dateTime", stream.Position);

                stream.Reset();
                Assert.IsTrue(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/date", stream.Position);

                while (stream.MoveNext()) ;     // read to end

                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/Age", stream.Position);

                // Reading past end does not change position
                Assert.IsFalse(stream.MoveNext());
                Assert.AreEqual("http://hl7.org/fhir/StructureDefinition/Age", stream.Position);
            }
        }

        [TestMethod]
        public void ScanThroughSingle()
        {
            var xmlPatient = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\fp-test-patient.xml");
            using (var stream = XmlNavigatorStream.FromPath(xmlPatient))
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
            var xmlfile = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\source-test\books.xml");

            using (var stream = XmlNavigatorStream.FromPath(xmlfile))
            {
                Assert.IsNull(stream.ResourceType);
                Assert.IsFalse(stream.MoveNext());
            }
        }

        [TestMethod]
        public void ScanPerformance()
        {
            var xmlBundle = Path.Combine(Directory.GetCurrentDirectory(), @"TestData\profiles-types.xml");

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 250; i++)
            {
                using (var stream = XmlNavigatorStream.FromPath(xmlBundle))
                {
                    Assert.IsTrue(stream.MoveNext("http://hl7.org/fhir/StructureDefinition/Age"));
                }
            }

            sw.Stop();
            Debug.WriteLine($"Scanning took {sw.ElapsedMilliseconds / 250} ms");
        }

#if NET_COMPRESSION

        [TestMethod]
        public void NavigateZipStream()
        {
            // Use XmlNavigatorStream to navigate resources stored inside a zip file
            // ZipDeflateStream does not support seeking (forward-only stream)
            // Therefore this only works for the XmlNavigatorStream, as the ctor does NOT (need to) call Reset()
            // JsonNavigatorStream cannot support zip streams; ctor needs to call Reset after scanning resourceType
            using (var archive = ZipFile.Open(ZipSource.SpecificationZipFileName, ZipArchiveMode.Read))
            {
                var entry = archive.Entries.FirstOrDefault(e => e.Name == "profiles-resources.xml");
                Assert.IsNotNull(entry);

                using (var entryStream = entry.Open())
                {
                    using (var navStream = new XmlNavigatorStream(entryStream, false))
                    {
                        while (navStream.MoveNext())
                        {
                            //Debug.WriteLine($"{navStream.Position} : {navStream.ResourceType} {(navStream.IsBundle ? "(Bundle)" : "")}");
                            Assert.IsTrue(navStream.IsBundle);
                            Assert.AreEqual(ResourceType.Bundle.GetLiteral(), navStream.ResourceType);
                            Assert.IsInstanceOfType(navStream.Current, typeof(XmlDomFhirNavigator));
                        };
                    }
                }

            }
        }

#endif

    }
}
