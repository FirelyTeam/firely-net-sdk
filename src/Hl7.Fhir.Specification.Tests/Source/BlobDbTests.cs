using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Source.BlobDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests.Source
{
    [TestClass]
    public class BlobDbTests
    {      
        private Blob b1 = new Blob(new byte[] { 20, 21, 22, 23 }, "application/binary");
        private Blob b2 = new Blob(Encoding.UTF8.GetBytes("Hello"), "text/plain");

        [TestMethod]
        public void TestWriteAndReadDatabase()
        {
            var filename = Path.GetTempFileName();
            Debug.WriteLine("writing blobdb to " + filename);
            Debug.WriteLine("");

            using (var w = new BlobDatabaseWriter(filename))
            {
                w.Add(b1, new[] { ("name", "Ewout"), ("works_for", "Furore") });
                w.Add(b2, new[] { ("name", "Martijn"), ("has_child", "true"), ("works_for", "Furore") });

                w.Build();
            }

            using (var r = new BlobDatabase(filename))
            {
                Debug.WriteLine(r.Dump());

                Blob[] b = r.Get("name", "Ewout");
                Assert.AreEqual(1, b.Length);
                Assert.AreEqual(b1, b[0]);

                b = r.Get("name", "Martijn");
                Assert.AreEqual(1, b.Length);
                Assert.AreEqual(b2, b[0]);

                b = r.Get("name", "martijn");
                Assert.AreEqual(0, b.Length);

                b = r.Get("has_child", "true");
                Assert.AreEqual(1, b.Length);
                Assert.AreEqual(b2, b[0]);

                b = r.Get("has_child", "false");
                Assert.AreEqual(0, b.Length);

                b = r.Get("works_for", "Furore");
                Assert.AreEqual(2, b.Length);
                Assert.IsTrue((new[] { b1, b2 }).SequenceEqual(b));
            }
        }

        [TestMethod]
        public void CreateDbFromSpec()
        {
            var filename = @"C:\git\fhir-net-api\src\Hl7.Fhir.Specification\data\profiles-resources.xml";

            using (var w = new BlobDatabaseWriter(@"c:\temp\specification.bin", compress:false))
            {
                using (var s = new FileStream(filename, FileMode.Open))
                {
                    var stream = XmlFileConformanceScanner.StreamResources(s);

                    foreach (var item in stream)
                    {                        
                        var b = new Blob(Encoding.UTF8.GetBytes(item.element.ToString()), "application/fhir+xml");
                        w.Add(b, new[] { ("resourceUri", item.fullUrl), ("resourceType", item.element.Name.LocalName) });
                    }
                }

                w.Build();
            }
        }

        [TestMethod]
        public void DumpDb()
        {
            using (var r = new BlobDatabase(@"c:\temp\specification.bin"))
            {
                //Debug.WriteLine(r.Dump());
                Blob[] sds = null;

                var sw = new Stopwatch();
                sw.Start();
                for (var repeat = 0; repeat < 1000; repeat++)
                {
                    sds = r.Get("resourceUri", ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient));
                    //var p = parser.Parse<StructureDefinition>(SerializationUtil.JsonReaderFromStream(new MemoryStream(sds[0].Data)));
                }
                sw.Stop();
                Debug.WriteLine(sw.ElapsedMilliseconds);

                Assert.AreEqual(1,sds.Length);
            }
        }
    }
}
