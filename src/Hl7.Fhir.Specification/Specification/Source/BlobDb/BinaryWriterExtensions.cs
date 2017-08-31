using System;
using System.IO;

#if NET_COMPRESSION
using System.Text;
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

        public static long WriteManifest(this Stream s, short version, Index[] indices)
        {
            // create a header which we will update later when we have the necessary file positions
            var header = new Header(1, -1, -1);

            s.WriteHeader(header);
            s.Flush();
            var indexPosition = s.Position;
            s.WriteIndexBlock(indices);
            s.Flush();
            var dataPosition = s.Position;

            // And finally, update the header with the final positions
            header = new Header(header.Version, indexPosition, dataPosition);
            s.Seek(0, SeekOrigin.Begin);
            s.WriteHeader(header);
            s.Flush();

            return dataPosition;
        }

        public static void WriteHeader(this Stream s, Header header)
        {
            using (var writer = new BinaryWriter(s, Encoding.UTF8, leaveOpen:true))
            {
                writer.Write(HEADER_MAGIC);
                writer.Write(header.Version);
                writer.Write(header.IndexBlockPosition);
                writer.Write(header.DataBlockPosition);
                writer.Flush();
            }
        }

        public static void WriteBlob(this Stream s, Blob b, bool deflate = false)
        {
            using (var writer = new BinaryWriter(s, Encoding.UTF8, leaveOpen: true))
            {
                writer.Write(BLOB_MAGIC);
                writer.Write(deflate ? (byte)1 : (byte)0);
                writer.Write(b.MimeType);

                byte[] data = b.Data;

#if NET_COMPRESSION
                if (deflate)
                    throw new NotImplementedException("Deflating a blob has not been implemented yet");
#endif

                writer.Write(data.Length);
                writer.Write(data);
                writer.Flush();
            }
        }

        public static void WriteIndexBlock(this Stream s, Index[] indices)
        {
            using (var writer = new BinaryWriter(s, Encoding.UTF8, leaveOpen: true))
            {
                writer.Write(INDEXBLOCK_MAGIC);
                int numIndices = indices.Length;
                writer.Write(numIndices);
                writer.Flush();
            }

            foreach (var index in indices)
                s.WriteIndex(index);
        }

        public static void WriteIndex(this Stream s, Index index)
        {
            using (var writer = new BinaryWriter(s, Encoding.UTF8, leaveOpen: true))
            {
                writer.Write(INDEX_MAGIC);
                writer.Write(index.Name);
                byte[] data;

                using (var mem = new MemoryStream())
                {
                    using (var entriesWriter = new BinaryWriter(mem))
                    {
                        foreach (var entry in index)
                            writeIndexEntry(entriesWriter, entry);
                        entriesWriter.Flush();

                        void writeIndexEntry(BinaryWriter w, IndexEntry e)
                        {
                            w.Write(e.Key);
                            w.Write(e.Position);
                        }
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