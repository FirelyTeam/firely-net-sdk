using Hl7.Fhir.Specification.Source.BlobDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests.Source
{
    [TestClass]
    public class BlobDbTests
    {
        [TestMethod]
        public void TestWriteAndReadBlob()
        {
            var data = writeData((Stream s) =>
            {
                Blob b = new Blob(new byte[] { 1, 2, 3, 4 }, "application/data");
                s.WriteBlob(b);
            });
        }

        private byte[] writeData(Action<Stream> writer)
        {
            using (var m = new MemoryStream())
            {
                writer(m);
                m.Flush();
                return m.ToArray();
            }
        }

        private void assert(byte[] input, Action<Stream> asserter)
        {
            using (var s = new MemoryStream(input))
            {
                asserter(s);
                Assert.IsTrue(s.ReadByte() == -1);
            }
        }
    }
}
