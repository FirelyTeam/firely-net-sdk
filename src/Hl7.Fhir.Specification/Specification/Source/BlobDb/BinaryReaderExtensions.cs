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
    internal class RestrictedStream : Stream
    {
        private readonly Stream _contained;
        private readonly long _maxLength;

        public RestrictedStream(Stream contained, long maxLength) : base()
        {
            _contained = contained;
            _maxLength = maxLength;
        }

        public override bool CanRead => _contained.CanRead;

        public override bool CanSeek => _contained.CanSeek;

        public override bool CanWrite => _contained.CanWrite;

        public override long Length => Math.Min(_maxLength, _contained.Length);

        public override long Position
        {
            get => _contained.Position;

            set
            {
                if (value > _maxLength)
                    _contained.Position = _maxLength;
                else
                    _contained.Position = value;
            }
        }

        public override void Flush() => _contained.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Position + count > _maxLength)
                count = (int)(_maxLength - Position);

            return _contained.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) => _contained.Seek(offset, origin);

        public override void SetLength(long value) => _contained.SetLength(Math.Min(_maxLength, value));

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (Position + count > _maxLength)
                count = (int) (_maxLength - Position);

            _contained.Write(buffer, offset, count);
        }
    }

    internal static class BinaryReaderExtensions
    {
        public static void ReadBinary(this Stream source, Action<BinaryReader> action)
        {
            using (var reader = getReader(source))
                action(reader);

            BinaryReader getReader(Stream s) => new BinaryReader(s, Encoding.UTF8, leaveOpen: true);
        }

        public static void ReadCompressed(this Stream source, Action<Stream> action)
        {
#if !NET_COMPRESSION
            throw new NotSupportedException("This platform does not support compression, which is required to read the data.");
#endif

            int compressedSize = -1;
            source.ReadBinary(br => compressedSize = br.ReadInt32());

            // The DeflateStream will decompress in blocks, so normally will read more bytes from the source
            // than are actually necessary to decode the data. To protect the decompressor from doing this,
            // make it read through a limited memory stream (or the custom RestrictedStream above) so we can
            // limit the readable bytes to the original compressedSize as calculated at the time of writing.
            byte[] limitedBuffer = new byte[compressedSize];
            source.Read(limitedBuffer, 0, compressedSize);

            using(var limitedStream = new MemoryStream(limitedBuffer, writable: false))
            //using(var limitedStream = new RestrictedStream(source,compressedSize))
            using (var decompressedStream = new DeflateStream(limitedStream, CompressionMode.Decompress, leaveOpen: true))
            {
                action(decompressedStream);
            }
        }

        public static Header ReadManifest(this Stream s, out Index[] indices)
        {
            var readHeader = s.ReadHeader();
            indices = s.ReadIndexBlock();

            return readHeader;
        }

        public static Header ReadHeader(this Stream source)
        {
            var pos = source.Position;

            try
            {
                Header result = null;

                source.ReadBinary(reader =>
                {
                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.HEADER_MAGIC)
                        throw new DatabaseFileCorruptException($"No header found at {pos}, found magic 0x{magic:X} instead", pos);

                    short version = reader.ReadInt16();
                    int blobCount = reader.ReadInt32();
                    long indexBlockPosition = reader.ReadInt64();
                    long dataBlockPosition = reader.ReadInt64();

                    result = new Header(version)
                    {
                        BlobCount = blobCount,
                        IndexBlockPosition = indexBlockPosition,
                        DataBlockPosition = dataBlockPosition
                    };
                });

                return result;
            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading the header", pos, eos);
            }
        }



        public static Blob ReadBlob(this Stream s)
        {
            var pos = s.Position;

            try
            {
                bool compressed = false;

                s.ReadBinary(reader =>
                {
                    ushort magic = reader.ReadUInt16();
                    if (magic != BinaryWriterExtensions.BLOB_MAGIC)
                        throw new DatabaseFileCorruptException($"No blob found at {pos}, found magic 0x{magic:X} instead", pos);
                    compressed = reader.ReadByte() != 0;
                });

                Blob result = null;
                readCompressedOrNot(s, compressed, ds =>
                {
                    ds.ReadBinary(reader =>
                    {
                        string mimeType = reader.ReadString();
                        int length = reader.ReadInt32();
                        byte[] data = reader.ReadBytes(length);
                        result = new Blob(data, mimeType);
                    });
                });

                return result;

            }
            catch (EndOfStreamException eos)
            {
                throw new DatabaseFileCorruptException("Unexpected EOF while reading a blob", pos, eos);
            }
        }


        private static void readCompressedOrNot(Stream source, bool compressed, Action<Stream> action)
        {
            if (compressed)
                source.ReadCompressed(action);
            else
                action(source);
        }

        public static Index[] ReadIndexBlock(this Stream s)
        {
            var pos = s.Position;

            try
            {
                ushort numIndices = 0;
                bool compressed = false;

                s.ReadBinary(reader =>
                {
                    ushort magic = reader.ReadUInt16();

                    if (magic != BinaryWriterExtensions.INDEXBLOCK_MAGIC)
                        throw new DatabaseFileCorruptException($"No index block found at {pos}, found magic 0x{magic:X} instead", pos);

                    numIndices = reader.ReadUInt16();
                    compressed = reader.ReadByte() != 0;
                });

                var result = new List<Index>();

                readCompressedOrNot(s, compressed, ds =>
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