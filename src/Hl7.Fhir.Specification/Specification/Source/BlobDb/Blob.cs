using System;
using System.IO;
using System.Linq;
using System.Text;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class Blob
    {
        public readonly byte[] Data;
        public readonly string MimeType;

        public Blob(byte[] data, string mimeType)
        {
            Data = data;
            MimeType = mimeType;
        }

        public static Blob ForText(string text, string mimeType = "text/plain") => new Blob(Encoding.UTF8.GetBytes(text), mimeType);

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append($"Blob of size {Data.Length} containing {MimeType}, starting with {dataHumanReadable(10)}");
            return builder.ToString();
        }

        private string dataHumanReadable(int maxChars)
        {
            try
            {
                var numchars = Math.Min(maxChars * 10, Data.Length);
                var s = Encoding.UTF8.GetString(Data, 0, numchars);
                s = new String(s.Where(c => !Char.IsControl(c)).ToArray());

                if(s.Length > numchars/2)
                    return "\"" + s.Substring(0, Math.Min(maxChars, s.Length)) + "\"";
            }
            catch
            {
            }

            // Not valid unicode, or not enough readable characters: display an array of bytes
            var size = Math.Min(maxChars, Data.Length);
            var bytes = Data.Take(size).Select(b => $"0x{b:X2}");
            return "{ " + String.Join(" ", bytes) + " }";

        }

        public override bool Equals(object obj) => (obj is Blob b) ? Equals(b) : false;

        public bool Equals(Blob b)
        {
            if (Object.ReferenceEquals(b, null)) return false;
            if (Object.ReferenceEquals(b, this)) return true;
            return (b.Data ?? new byte[0]).SequenceEqual(Data ?? new byte[0]) &&
                b.MimeType == MimeType;
        }

        public override int GetHashCode() => (Data, MimeType).GetHashCode();

    }


}
#endif