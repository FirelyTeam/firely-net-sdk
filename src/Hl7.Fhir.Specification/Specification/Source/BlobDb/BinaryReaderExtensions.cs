using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if NET_COMPRESSION
#endif

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    internal static class BinaryReaderExtensions
    {
        public static Header ReadHeader(this Stream s)
        {
            var pos = s.Position;

            try
            {
                using (var reader = new BinaryReader(s, Encoding.UTF8, leaveOpen: true))
                {
                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.HEADER_MAGIC)
                        throw new DatabaseFileCorruptException($"No header found at {pos}, found magic 0x{magic:X} instead", pos);

                    short version = reader.ReadInt16();
                    long indexBlockPosition = reader.ReadInt64();
                    long dataBlockPosition = reader.ReadInt64();

                    return new Header(version, indexBlockPosition, dataBlockPosition);
                }
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading an index block", pos, eos);
            }
        }

        public static Blob ReadBlob(this Stream s)
        {
            var pos = s.Position;

            try
            {
                using (var reader = new BinaryReader(s, Encoding.UTF8, leaveOpen: true))
                {

                    ushort magic = reader.ReadUInt16();
                    if (magic != BinaryWriterExtensions.BLOB_MAGIC)
                        throw new DatabaseFileCorruptException($"No blob found at {pos}, found magic 0x{magic:X} instead", pos);
                    byte deflate = reader.ReadByte();

                    if (deflate != 0)
                        throw new NotImplementedException("Deflating a blob has not been implemented yet");

                    string mimeType = reader.ReadString();
                    int length = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(length);
                    return new Blob(data, mimeType);
                }
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading a blob", pos, eos);
            }
        }


        public static Index[] ReadIndexBlock(this Stream s)
        {
            var pos = s.Position;

            try
            {
                int numIndices = -1;

                using (var reader = new BinaryReader(s, Encoding.UTF8, leaveOpen: true))
                {

                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.INDEXBLOCK_MAGIC)
                        throw new DatabaseFileCorruptException($"No index block found at {pos}, found magic 0x{magic:X} instead", pos);

                    numIndices = reader.ReadInt32();
                }

                var result = new List<Index>();

                for (var i = 0; i < numIndices; i++)
                    result.Add(s.ReadIndex());

                return result.ToArray();
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading an index block", pos, eos);
            }
        }

        public static Index ReadIndex(this Stream s)
        {
            var pos = s.Position;

            try
            {
                using (var reader = new BinaryReader(s, Encoding.UTF8, leaveOpen: true))
                {

                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.INDEX_MAGIC)
                        throw new DatabaseFileCorruptException($"No blob found at {pos}, found magic 0x{magic:X} instead", pos);

                    string name = reader.ReadString();
                    int length = reader.ReadInt32();
                    byte[] data = reader.ReadBytes(length);
                    Index result = new Index(name);

                    using (var mem = new MemoryStream(data))
                    {
                        using (var entriesReader = new BinaryReader(mem))
                        {
                            while (entriesReader.PeekChar() != -1)
                                result.Add(readIndexEntry(entriesReader));

                            IndexEntry readIndexEntry(BinaryReader r)
                            {
                                string key = r.ReadString();
                                long position = r.ReadInt64();
                                return new IndexEntry(key, position);
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