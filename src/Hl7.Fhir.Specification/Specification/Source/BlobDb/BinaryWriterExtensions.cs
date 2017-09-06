using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if NET_COMPRESSION
using System.IO.Compression;
#endif

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    internal static class BinaryWriterExtensions
    {
        public const ushort HEADER_MAGIC = 0xBBDB;
        public const ushort BLOB_MAGIC = 0xB10B;
        public const ushort INDEX_MAGIC = 0x1DEC;
        public const ushort INDEXBLOCK_MAGIC = 0x1DEB;

        public static long WriteManifest(this Stream s, short version, int blobCount, Index[] indices, bool compress=false)
        {
            // create a header which we will update later when we have the necessary file positions
            var header = new Header(version);

            s.WriteHeader(header);
            s.Flush();
            var indexPosition = s.Position;
            s.WriteIndexBlock(indices, compress);
            s.Flush();
            var dataPosition = s.Position;

            // And finally, update the header with the final positions
            header.IndexBlockPosition = indexPosition;
            header.DataBlockPosition = dataPosition;
            header.BlobCount = blobCount;

            s.Seek(0, SeekOrigin.Begin);
            s.WriteHeader(header);
            s.Flush();

            return dataPosition;
        }

        public static void WriteHeader(this Stream s, Header header)
        {
            using (var writer = getWriter(s))
            {
                writer.Write(HEADER_MAGIC);
                writer.Write(header.Version);
                writer.Write(header.BlobCount);
                writer.Write(header.IndexBlockPosition);
                writer.Write(header.DataBlockPosition);
                writer.Flush();
            }
        }

        private static BinaryWriter getWriter(Stream s) => new BinaryWriter(s, Encoding.UTF8, leaveOpen: true);

        public static void WriteBlob(this Stream s, Blob blob, bool compressed = false)
        {
            using (var writer = getWriter(s))
            {
                writer.Write(BLOB_MAGIC);
                writer.Write(compressed ? (byte)1 : (byte)0);
                writer.Flush();
            }

            writeCompressed(s, compressed, (cs) =>
            {
                using (var w = getWriter(cs))
                {
                    w.Write(blob.MimeType);
                    byte[] data = blob.Data;
                    w.Write(data.Length);
                    w.Write(data);
                    w.Flush();
                }
            });
        }

        private static void writeCompressed(Stream s, bool compressed, Action<Stream> action)
        {
            if (compressed)
            {
#if !NET_COMPRESSION
                throw Error.NotImplemented("Data is compressed, but this platform does not support compression");
#endif
                using (var compressedStream = new DeflateStream(s, CompressionLevel.Fastest, leaveOpen: true))
                {
                    action(compressedStream);
                    compressedStream.Flush();
                }
            }
            else
            {
                action(s);
            }
        }
    
        public static void WriteIndexBlock(this Stream s, Index[] indices, bool compressed=false)
        {
            using (var writer = getWriter(s))
            {
                writer.Write(INDEXBLOCK_MAGIC);
                ushort numIndices = (ushort)indices.Length;
                writer.Write(numIndices);
                writer.Write(compressed ? (byte)1 : (byte)0);
                writer.Flush();
            }

            writeCompressed(s, compressed, cs =>
            {
                foreach (var index in indices)
                    cs.WriteIndex(index);
            });
        }

        public static void WriteIndex(this Stream s, Index index)
        {
            using (var writer = getWriter(s))
            {
                writer.Write(INDEX_MAGIC);
                writer.Write(index.Name);
                byte[] data;

                using (var mem = new MemoryStream())
                {
                    using (var entriesWriter = new BinaryWriter(mem))
                    {
                        foreach (var entry in index)
                        {
                            entriesWriter.Write(entry.Key);
                            entriesWriter.Write(entry.Position);   // Position
                        }

                        entriesWriter.Flush();
                    }

                    data = mem.ToArray();
                }

                writer.Write(data.Length);
                writer.Write(data);
                writer.Flush();
            }
        }
    }
}
#endif