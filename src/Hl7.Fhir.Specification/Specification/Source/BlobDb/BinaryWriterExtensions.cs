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

        public static void WriteBinary(this Stream destination, Action<BinaryWriter> action)
        {
            using (var writer = getWriter(destination))
            {
                action(writer);
                writer.Flush();
            }

            BinaryWriter getWriter(Stream s) => new BinaryWriter(s, Encoding.UTF8, leaveOpen: true);
        }


        public static void WriteCompressed(this Stream destination, Action<Stream> action)
        {
#if !NET_COMPRESSION
            throw new NotSupportedException("This platform does not support compression, which is required to write the data.");
#endif
            long lengthPosition = destination.Position;  // need to get back here to update compressed size
            WriteBinary(destination, writer => writer.Write(0));  // write a placeholder first, will get back to it later

            long startPosition = destination.Position;

            using (var compressedStream = new DeflateStream(destination, CompressionLevel.Fastest, leaveOpen: true))
            {
                action(compressedStream);
                compressedStream.Flush();
            }

            long endPosition = destination.Position;
            long compressedSize = endPosition - startPosition;

            destination.Seek(lengthPosition);
            WriteBinary(destination, writer => writer.Write((int)compressedSize));
            destination.Seek(endPosition);
        }

        //public static void WriteCompressed(this Stream destination, byte[] data) => destination.WriteCompressed(ws => ws.Write(data, 0, data.Length));

        //public static void CopyToCompressed(this Stream source, Stream destination) => destination.WriteCompressed(ws => source.CopyTo(ws));

        public static long Seek(this Stream s, long offset) => s.Seek(offset, SeekOrigin.Begin);


        public static long WriteManifest(this Stream destination, short version, int blobCount, Index[] indices, bool compress = false)
        {
            // create a header which we will update later when we have the necessary file positions
            var header = new Header(version);

            destination.WriteHeader(header);
            destination.Flush();
            var indexPosition = destination.Position;
            destination.WriteIndexBlock(indices, compress);
            destination.Flush();
            var dataPosition = destination.Position;

            // And finally, update the header with the final positions
            header.IndexBlockPosition = indexPosition;
            header.DataBlockPosition = dataPosition;
            header.BlobCount = blobCount;

            destination.Seek(0);
            destination.WriteHeader(header);
            destination.Flush();

            return dataPosition;
        }

        public static void WriteHeader(this Stream destination, Header header)
        {
            destination.WriteBinary(writer =>
            {
                writer.Write(HEADER_MAGIC);
                writer.Write(header.Version);
                writer.Write(header.BlobCount);
                writer.Write(header.IndexBlockPosition);
                writer.Write(header.DataBlockPosition);                
            });
        }



        public static void WriteBlob(this Stream destination, Blob blob, bool compressed = false)
        {
            destination.WriteBinary(writer =>
            {
                writer.Write(BLOB_MAGIC);
                writer.Write(compressed ? (byte)1 : (byte)0);
            });

            writeCompressedOrNot(destination, compressed, (cs) =>
            {
                // We need to know the compressed size of the data in order to write it 
                WriteBinary(cs, w =>
                {
                    w.Write(blob.MimeType);
                    byte[] data = blob.Data;
                    w.Write(data.Length);
                    w.Write(data);
                    w.Flush();
                });
            });
        }

        private static void writeCompressedOrNot(Stream destination, bool compressed, Action<Stream> action)
        {
            if (compressed)
                destination.WriteCompressed(action);
            else
                action(destination);
        }
    
        public static void WriteIndexBlock(this Stream destination, Index[] indices, bool compressed=false)
        {
            destination.WriteBinary(writer =>
            {
                writer.Write(INDEXBLOCK_MAGIC);
                ushort numIndices = (ushort)indices.Length;
                writer.Write(numIndices);
                writer.Write(compressed ? (byte)1 : (byte)0);
            });

            writeCompressedOrNot(destination, compressed, cs =>
            {
                foreach (var index in indices)
                    cs.WriteIndex(index);
            });
        }

        public static void WriteIndex(this Stream destination, Index index)
        {
            destination.WriteBinary(writer =>
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
            });
        }
    }
}
#endif