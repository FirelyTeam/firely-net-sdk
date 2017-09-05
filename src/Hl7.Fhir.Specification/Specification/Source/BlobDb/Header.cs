#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class Header
    {
        public readonly short Version;
        public long IndexBlockPosition = -1;
        public long DataBlockPosition = -1;
        public int BlobCount;

        public Header(short version)
        {
            Version = version;
        }

        public override string ToString()
        {
            return $"BlobDb file using binary format version {Version}. " 
                + $"First index at position {IndexBlockPosition}, "
                + $"first of {BlobCount} blobs at position {DataBlockPosition}";
        }
    }


}
#endif