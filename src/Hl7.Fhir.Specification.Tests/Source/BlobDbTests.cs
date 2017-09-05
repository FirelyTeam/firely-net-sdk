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

    }
}
