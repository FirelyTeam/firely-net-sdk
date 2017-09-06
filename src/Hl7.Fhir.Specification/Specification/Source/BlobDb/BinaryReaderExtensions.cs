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
    internal static class BinaryReaderExtensions
    {
        public static Header ReadManifest(this Stream s, out Index[] indices)
        {
            var readHeader = s.ReadHeader();
            indices = s.ReadIndexBlock();

            return readHeader;
        }

        public static Header ReadHeader(this Stream s)
        {
            var pos = s.Position;

            try
            {
                using (var reader = getReader(s))
                {
                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.HEADER_MAGIC)
                        throw new DatabaseFileCorruptException($"No header found at {pos}, found magic 0x{magic:X} instead", pos);

                    short version = reader.ReadInt16();
                    int blobCount = reader.ReadInt32();
                    long indexBlockPosition = reader.ReadInt64();
                    long dataBlockPosition = reader.ReadInt64();

                    return new Header(version)
                    {
                        BlobCount = blobCount,
                        IndexBlockPosition = indexBlockPosition,
                        DataBlockPosition = dataBlockPosition
                    };
                }
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading the header", pos, eos);
            }
        }

        private static BinaryReader getReader(Stream s) => new BinaryReader(s, Encoding.UTF8, leaveOpen: true);

        public static Blob ReadBlob(this Stream s)
        {
            var pos = s.Position;

            try
            {
                bool compressed = false;

                using (var reader = getReader(s))
                {
                    ushort magic = reader.ReadUInt16();
                    if (magic != BinaryWriterExtensions.BLOB_MAGIC)
                        throw new DatabaseFileCorruptException($"No blob found at {pos}, found magic 0x{magic:X} instead", pos);
                    compressed = reader.ReadByte() != 0;
                }

                Blob result = null;
                readCompressed(s, compressed, ds =>
                {
                    using (var reader = getReader(ds))
                    {
                        string mimeType = reader.ReadString();
                        int length = reader.ReadInt32();
                        byte[] data = reader.ReadBytes(length);
                        result = new Blob(data, mimeType);
                    }
                });

                return result;

            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading a blob", pos, eos);
            }
        }


        private static void readCompressed(Stream s, bool compressed, Action<Stream> action)
        {
            if (compressed)
            {
#if !NET_COMPRESSION
                throw Error.NotImplemented("Data is compressed, but this platform does not support compression");
#endif
                using (var decompressedStream = new DeflateStream(s, CompressionMode.Decompress, leaveOpen: true))
                {
                    action(decompressedStream);
                }
            }
            else
            {
                action(s);
            }
        }

        public static Index[] ReadIndexBlock(this Stream s)
        {
            var pos = s.Position;

            try
            {
                ushort numIndices = 0;
                bool compressed = false;

                using (var reader = getReader(s))
                {
                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.INDEXBLOCK_MAGIC)
                        throw new DatabaseFileCorruptException($"No index block found at {pos}, found magic 0x{magic:X} instead", pos);

                    numIndices = reader.ReadUInt16();
                    compressed = reader.ReadByte() != 0;
                }

                var result = new List<Index>();

                readCompressed(s, compressed, ds =>
                {
                    for (var i = 0; i < numIndices; i++)
                        result.Add(ds.ReadIndex(s.Position));
                });

                return result.ToArray();
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading an index block", pos, eos);
            }
        }

        public static Index ReadIndex(this Stream s, long pos)
        {
            try
            {
                using (var reader = new BinaryReader(s, Encoding.UTF8, leaveOpen: true))
                {

                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.INDEX_MAGIC)
                        throw new DatabaseFileCorruptException($"No index found at {pos}, found magic 0x{magic:X} instead", pos);

                    string name = reader.ReadString();
                    int length = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(length);
                    Index result = new Index(name);

                    using (var mem = new MemoryStream(data))
                    {
                        using (var entriesReader = new BinaryReader(mem))
                        {
                            while (entriesReader.PeekChar() != -1)
                            {
                                string key = entriesReader.ReadString();
                                long position = entriesReader.ReadInt64();
                                result.Add(new IndexEntry(key, position));
                            }
                        }
                    }

                    return result;
                }
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading an index", pos, eos);
            }
        }

    }
}
#endif