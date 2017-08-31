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
    }


}
#endif