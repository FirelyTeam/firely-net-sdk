using System.Collections.Generic;
using System.IO;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteBlob(this BinaryWriter writer, Blob b)
        {
            writer.Write(b.Data);
            writer.Write(b.MimeType);
            writer.Flush();
        }

        public static void WriteIndices(this BinaryWriter writer, IEnumerable<Index> indices)
        {
            foreach (var index in indices)
                writer.WriteIndex(index);
        }

        public static void WriteIndex(this BinaryWriter writer, Index index)
        {
            writer.Write(index.Name);
            foreach (var entry in index)
                writeIndexEntry(entry);
            writer.Flush();

            void writeIndexEntry(IndexEntry e)
            {
                writer.Write(e.Key);
                writer.Write(e.Position);
            }
        }

    }


}
#endif