using System;
using System.IO;

#if NET_FILESYSTEM
namespace Hl7.Fhir.Specification.Source.BlobDb
{
    public class BlobDatabase
    {
        public static BlobDatabase Open(string path) => throw new NotImplementedException();

        public static BlobDatabase Open(Stream data) => throw new NotImplementedException();

        public Blob Get(string indexName, string key) => throw new NotImplementedException();

        public string Dump() => throw new NotImplementedException();
    }


}
#endif