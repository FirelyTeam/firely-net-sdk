using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Source.BlobDb;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public void ReadFromZippedBundle()
        {
            using (var zs = new FileStream(@"c:\temp\specification-bundle.zip", FileMode.Open, FileAccess.Read))
            {
                using (var za = new ZipArchive(zs, ZipArchiveMode.Read))
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    for (var i = 0; i < 100000; i++)
                    {
                        var e = za.GetEntry("url-index.json");
                        var json = JObject.Parse(readFromStream(e));
                        var entryName = json["http://hl7.org/fhir/StructureDefinition/Patient"].First.ToString();


                        var file = za.GetEntry(entryName);
                        //     Console.WriteLine(readFromStream(file));

                    }
                    sw.Stop();
                    Console.WriteLine(sw.ElapsedMilliseconds);
                }

            }

            string readFromStream(ZipArchiveEntry e)
            {
                using (var es = e.Open())
                {
                    using (var sr = new StreamReader(es))
                        return sr.ReadToEnd();
                }
            }
        }


        [TestMethod]
        public void CreateZipFromBundle()
        {
            var filename = @"C:\git\fhir-net-api\src\Hl7.Fhir.Specification\data\profiles-resources.xml";

            using (var zs = new FileStream(@"c:\temp\specification-bundle.zip", FileMode.Create, FileAccess.ReadWrite))
            {
                using (var za = new ZipArchive(zs, ZipArchiveMode.Create))
                {
                    var urlIndex = new JObject();
                    var typeIndex = new JObject();

                    using (var s = new FileStream(filename, FileMode.Open))
                    {
                        var stream = XmlFileConformanceScanner.StreamResources(s);

                        foreach (var item in stream)
                        {
                            var uri = new Uri(item.fullUrl);
                            var type = item.element.Name.LocalName;
                            var entryName = uri.Host + uri.AbsolutePath + ".xml";
                            var entry = za.CreateEntry(entryName);
                            urlIndex.Add(item.fullUrl, new JArray(new JValue(entryName)));

                            var typeKey = (JArray)typeIndex[type];
                            if (typeKey == null)
                            {
                                typeKey = new JArray();
                                typeIndex.Add(type, typeKey);
                            }

                            typeKey.Add(entryName);

                            using (StreamWriter writer = new StreamWriter(entry.Open()))
                            {
                                writer.Write(item.element.ToString());
                            }
                            //var b = new Blob(Encoding.UTF8.GetBytes(item.element.ToString()), "application/fhir+xml");
                            //w.Add(b, new[] { ("resourceUri", item.fullUrl), ("resourceType", item.element.Name.LocalName) });

                        }

                        var ie = za.CreateEntry("url-index.json");

                        using (StreamWriter writer = new StreamWriter(ie.Open()))
                        {
                            writer.Write(urlIndex.ToString());
                        }

                        ie = za.CreateEntry("type-index.json");

                        using (StreamWriter writer = new StreamWriter(ie.Open()))
                        {
                            writer.Write(typeIndex.ToString());
                        }

                    }
                }
            }
        }

        [TestMethod]
        public void CreateDbFromSpec()
        {
            var filename = @"C:\git\fhir-net-api\src\Hl7.Fhir.Specification\data\profiles-resources.xml";

            using (var w = new BlobDatabaseWriter(@"c:\temp\specification.bin", compress: false))
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
            using (var r = new BlobDatabase(@"c:\temp\specification-compressed.bin"))
            {
                //Debug.WriteLine(r.Dump());
                Blob[] sds = null;
                var parser = new FhirXmlParser();

                var sw = new Stopwatch();
                sw.Start();
                for (var repeat = 0; repeat < 1000; repeat++)
                {
                    sds = r.Get("resourceUri", ModelInfo.CanonicalUriForFhirCoreType(FHIRDefinedType.Patient));
                    var p = parser.Parse<StructureDefinition>(SerializationUtil.XmlReaderFromStream(new MemoryStream(sds[0].Data)));
                }
                sw.Stop();
                Debug.WriteLine(sw.ElapsedMilliseconds);

                Assert.AreEqual(1, sds.Length);
            }
        }
    }
}
