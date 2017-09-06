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
    public class BlobDbExtensionsTests
    {
        [TestMethod]
        public void TestWriteAndReadBlob()
        {
            Blob originalBlob = new Blob(new byte[] { 1, 2, 3, 4 }, "application/data");

            var data = writeData((Stream s) =>
            {
                s.WriteBlob(originalBlob);
            });

            assert(data, (Stream s) =>
            {
                var readData = s.ReadBlob();
                Assert.AreEqual(originalBlob, readData);
            });

        }

        private Index testIndex1 = new Index("index1")
            {
                new IndexEntry("keyA", 1),
                new IndexEntry("keyB", 2),
                new IndexEntry("keyC", 3)
            };

        private Index testIndex2 = new Index("index2")
            {
                new IndexEntry("keyB", 4),
                new IndexEntry("keyC", 5),
                new IndexEntry("keyD", 6)
            };


        [TestMethod]
        public void TestWriteAndReadIndex()
        {
            var data = writeData((Stream s) =>
            {
                s.WriteIndex(testIndex1);
            });

            assert(data, (Stream s) =>
            {
                var readData = s.ReadIndex(s.Position);
                Assert.AreEqual(testIndex1, readData);
            });
        }

        [TestMethod]
        public void TestWriteAndReadIndexBlock()
        {
            var block = new[] { testIndex1, testIndex2 };
            var data = writeData((Stream s) =>
            {
                s.WriteIndexBlock(block);
            });

            assert(data, (Stream s) =>
            {
                var readData = s.ReadIndexBlock();
                Assert.IsTrue(block.SequenceEqual(readData));
            });
        }

        [TestMethod]
        public void TestWriteAndReadManifest()
        {
            var block = new[] { testIndex1, testIndex2 };

            var data = writeData((Stream s) =>
            {
                s.WriteManifest(10, 100, block);
            });

            assert(data, (Stream s) =>
            {
                var readHeader = s.ReadManifest(out var readIxBlock);

                Assert.AreEqual(10, readHeader.Version);
                Assert.AreEqual(100, readHeader.BlobCount);
                Assert.AreEqual(24, readHeader.IndexBlockPosition);
                Assert.AreEqual(132, readHeader.DataBlockPosition);
                Assert.IsTrue(block.SequenceEqual(readIxBlock));
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
