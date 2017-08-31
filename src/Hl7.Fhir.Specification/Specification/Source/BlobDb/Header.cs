#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class Header
    {
        public readonly short Version;
        public long IndexBlockPosition;
        public long DataBlockPosition;

        public Header(short version, long indexPosition, long dataPosition)
        {
            Version = version;
            IndexBlockPosition = indexPosition;
            DataBlockPosition = dataPosition;
        }
    }


}
#endif