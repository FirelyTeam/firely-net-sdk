using System.Collections.Generic;
using System.IO.Compression;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Utils
{
    public static class ZipArchiveEntryExtensions
    {
        public static IEnumerable<Resource> GetResources(this IEnumerable<ZipArchiveEntry> zipentries)
        {
            foreach (var zipentry in zipentries)
            {
                var stream = zipentry.Open();
                using (var reader = XmlReader.Create(stream))
                {
                    var resource = new FhirXmlParser().Parse<Resource>(reader);
                    if (resource is Bundle)
                    {
                        foreach (var entry in ((Bundle)resource).GetResources())
                        {
                            yield return entry;
                        }
                    }
                    else
                    {
                        yield return resource;
                    }
                }

            }
        }
    }
}
