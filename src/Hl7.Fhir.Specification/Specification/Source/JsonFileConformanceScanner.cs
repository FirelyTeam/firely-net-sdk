/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Xml FHIR (conformance) resource from a given stream
    /// </summary>
    internal class JsonFileConformanceScanner : IConformanceScanner
    {
        string _path;

        public JsonFileConformanceScanner(string path)
        {
            _path = path;
        }

        public List<ConformanceScanInformation> List()
        {
            var rootResourceType = pollResourceType(_path);

            using (var input = File.OpenRead(_path))
            {
                var result =                     
                    from res in streamResources(input, rootResourceType)
                    let resourceType = res.element.Value<string>("resourceType")

                    // [WMR 20170420] Issue: if the resource type is unknown (i.e. DSTU Conformance), 
                    // then we cannot parse res.Name to a ResourceType enum value 
                    // (ParseLiteral returns null and .Value throws an exception) 
                    // => First skip unknown resources 
                    where ModelInfo.IsKnownResource(resourceType)

                    select  new ConformanceScanInformation()
                    {
                        ResourceType = EnumUtility.ParseLiteral<ResourceType>(resourceType).Value,
                        ResourceUri = res.fullUrl,
                        Canonical = res.element.Value<string>("url"),
                        ValueSetSystem = getValueSetSystem(res.element),
                        UniqueIds = getUniqueIds(res.element),
                        ConceptMapSource = getCmSource(res.element),
                        ConceptMapTarget = getCmTarget(res.element),
                        Origin = _path,
                    };

                return result.ToList();
            }
        }


        public Resource Retrieve(ConformanceScanInformation entry)
        {
            if (entry == null) throw Error.ArgumentNull(nameof(entry));

            JObject found = null;

            var resourceType = pollResourceType(entry.Origin);
            if (resourceType != null)
            {
                using (var input = File.OpenRead(entry.Origin))
                {
                    var resources = streamResources(input, resourceType);
                    found = resources.Where(res => res.fullUrl == entry.ResourceUri).SingleOrDefault().element;
                }
            }

            if (found == null) return null;

            var resultResource = new FhirJsonParser().Parse<Resource>(new JsonDomFhirReader(found));
            resultResource.AddAnnotation(new OriginAnnotation { Origin = entry.Origin });

            return resultResource;
        }


        private string getValueSetSystem(JObject vs) => vs["codeSystem"]?["system"]?.Value<string>();

        private string[] getUniqueIds(JObject ns) => ns["uniqueId"]?.Select(id => (string)id["value"]).ToArray() ?? new string[0];

        private string getCmSource(JObject cm) => cm["sourceUri"]?.Value<string>() + cm["sourceReference"]?["reference"]?.Value<string>();

        private string getCmTarget(JObject cm) => cm["targetUri"]?.Value<string>() + cm["targetReference"]?["reference"]?.Value<string>();

 
        // Use a forward-only XmlReader to scan through a possibly huge bundled file,
        // and yield the feed entries, so only one entry is in memory at a time
        private IEnumerable<(JObject element, string fullUrl)> streamResources(Stream input, string resourceType)
        {
            if (resourceType == null) throw Error.ArgumentNull(nameof(resourceType));

            JsonTextReader reader = new JsonTextReader(new StreamReader(input));

            if (resourceType == "Bundle")
            {
                if (!skipTo(reader, "entry")) yield break;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject && reader.Path.StartsWith("entry["))
                    {
                        var entry = JObject.ReadFrom(reader);

                        var fullUrl = entry.Value<string>("fullUrl");
                        if (fullUrl != null)
                        {
                            var resourceNode = entry["resource"];
                            if (resourceNode is JObject resource) yield return (resource, fullUrl);
                        }
                    }
                }
            }

            else
            {
                var resource = (JObject)JObject.ReadFrom(reader);

                if (resource != null)
                {
                    // First try to initialize from canonical url (conformance resources)
                    var canonicalUrl = resource.Value<string>("url");

                    if (canonicalUrl != null)
                        yield return (resource, canonicalUrl);

                    // Otherwise try to initialize from resource id
                    else
                    {
                        var resourceId = resource.Value<string>("id");
                        if (resourceId != null)
                        {
                            var fullUrl = "http://example.org/" + resourceType + "/" + resourceId;
                            yield return (resource, fullUrl);
                        }
                    }
                }
            }
        }

        private string pollResourceType(string path)
        {
            using (var input = File.OpenRead(path))
            {
                JsonTextReader reader = new JsonTextReader(new StreamReader(input));

                if (!skipTo(reader, "resourceType")) return null;
                return reader.ReadAsString();
            }
        }

        private static bool skipTo(JsonReader reader, string path)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Path == path)
                    return true;
            }

            return false;
        }
    }
}
