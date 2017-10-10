/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    internal sealed class JsonArtifactScanner : IArtifactScanner
    {
        /// <summary>Default base url used for generating virtual resource urls.</summary>
        public const string DefaultBaseUrl = "http://example.org/";

        ArtifactSummaryHarvester _harvester;
        string _path;

        public JsonArtifactScanner(string path, ArtifactSummaryHarvester harvester)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
            _harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
        }

        /// <summary>Scan the source and extract summary information from all the available artifacts.</summary>
        /// <returns>A list of <see cref="ArtifactSummary"/> instances.</returns>
        public List<ArtifactSummary> List()
        {
            IEnumerable<ArtifactSummary> summaries = Enumerable.Empty<ArtifactSummary>();
            var input = createStream(_path);
            if (input != null)
            {
                using (input)
                {
                    summaries = _harvester.Harvest(input);
                }
            }
            return new List<ArtifactSummary>(summaries);
        }

        #region Json specific logic

        static INavigatorStream createStream(string path) => new JsonNavigatorStream(path);

        /// <summary>Retrieve the artifact that is identified by the specified summary information.</summary>
        /// <param name="entry">Artifact summary.</param>
        /// <returns>A <see cref="Resource"/> instance.</returns>
        public Resource Retrieve(ArtifactSummary entry)
        {
            if (entry == null) throw Error.ArgumentNull(nameof(entry));

            JObject found = null;

            var resourceType = pollResourceType(entry.Origin);
            if (resourceType != null)
            {
                using (var input = File.OpenRead(entry.Origin))
                {
                    var resources = StreamResources(input, isBundle: resourceType == "Bundle");
                    found = resources.Where(res => res.fullUrl == entry.ResourceUri).SingleOrDefault().element;
                }
            }

            if (found == null) return null;

            var resultResource = new FhirJsonParser().Parse<Resource>(new JsonDomFhirReader(found));
            resultResource.SetOrigin(entry.Origin);

            return resultResource;
        }

        // Use a forward-only XmlReader to scan through a possibly huge bundled file,
        // and yield the feed entries, so only one entry is in memory at a time
        internal static IEnumerable<(JObject element, string fullUrl)> StreamResources(Stream input, bool isBundle)
        {
            using (var reader = SerializationUtil.JsonReaderFromStream(input))
            {
                if (isBundle)
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
                        var resourceType = resource.Value<string>("resourceType");

                        if (canonicalUrl != null)
                            yield return (resource, canonicalUrl);

                        // Otherwise try to initialize from resource id
                        else
                        {
                            var resourceId = resource.Value<string>("id");
                            if (resourceId != null)
                            {
                                var fullUrl = DefaultBaseUrl + resourceType + "/" + resourceId;
                                yield return (resource, fullUrl);
                            }
                        }
                    }
                }
            }
        }

        private string pollResourceType(string path)
        {
            using (var input = File.OpenRead(path))
            using (var stream = new StreamReader(input))
            using (var reader = new JsonTextReader(stream))
            {
                if (skipTo(reader, "resourceType"))
                {
                    return reader.ReadAsString();
                }
            }
            return null;
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

        #endregion

    }

}