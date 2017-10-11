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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// For efficiently extracting identifying metadata from a raw FHIR resource file in XML format.
    /// Also allows to actually deserialize and resolve the resource based on the previously extracted metadata.
    /// </summary>
    internal sealed class XmlArtifactScanner : ArtifactScanner
    {
        /// <summary>ctor</summary>
        /// <param name="path">Full path specification of a FHIR resource file.</param>
        /// <param name="harvester">An <see cref="ArtifactSummaryHarvester"/> instance to extract a concrete set of summary data from the resource.</param>
        public XmlArtifactScanner(string path, ArtifactSummaryHarvester harvester) : base(path, harvester)
        {
        }

        protected override INavigatorStream CreateStream(string path) => new XmlNavigatorStream(path);

        /// <summary>Retrieve the artifact that is identified by the specified summary information.</summary>
        /// <param name="entry">Artifact summary.</param>
        /// <returns>A <see cref="Resource"/> instance.</returns>
        public override Resource Retrieve(ArtifactSummary entry)
        {
            if (entry == null) throw Error.ArgumentNull(nameof(entry));

            XElement found = null;

            using (var input = File.OpenRead(entry.Origin))
            {
                var resources = StreamResources(input);

                found = resources.Where(res => res.fullUrl == entry.ResourceUri).SingleOrDefault().element;
            }

            if (found == null) return null;

            var resultResource = new FhirXmlParser().Parse<Resource>(new XmlDomFhirReader(found));
            resultResource.SetOrigin(entry.Origin);

            return resultResource;
        }

        // Use a forward-only XmlReader to scan through a possibly huge bundled file,
        // and yield the feed entries, so only one entry is in memory at a time
        internal static IEnumerable<(XElement element, string fullUrl)> StreamResources(Stream input)
        {
            using (var reader = SerializationUtil.XmlReaderFromStream(input))
            {
                var root = getRootName(reader);

                if (root == "Bundle")
                {
                    if (!reader.ReadToDescendant("entry", XmlNs.FHIR)) yield break;

                    while (!reader.EOF)
                    {
                        if (reader.NodeType == XmlNodeType.Element
                            && reader.NamespaceURI == XmlNs.FHIR && reader.LocalName == "entry")
                        {
                            var entryNode = (XElement)XElement.ReadFrom(reader);
                            var fullUrl = entryNode.Elements(XmlNs.XFHIR + "fullUrl").Attributes("value").SingleOrDefault();
                            if (fullUrl != null)
                            {
                                var resourceNode = entryNode.Element(XName.Get("resource", XmlNs.FHIR));
                                if (resourceNode != null)
                                {
                                    var resource = resourceNode.Elements().First();
                                    yield return (resource, fullUrl.Value);
                                }
                            }
                        }
                        else
                            reader.Read();
                    }
                }

                // [WMR 20160908] Fixed, parse stand-alone (conformance) resources
                else if (root != null)
                {
                    var resourceNode = (XElement)XElement.ReadFrom(reader);
                    // First try to initialize from canonical url (conformance resources)
                    var canonicalUrl = resourceNode.Elements(XmlNs.XFHIR + "url").Attributes("value").SingleOrDefault();
                    if (canonicalUrl != null)
                        yield return (resourceNode, canonicalUrl.Value);
                    else
                    {
                        // Otherwise try to initialize from resource id
                        var resourceId = resourceNode.Elements(XmlNs.XFHIR + "id").Attributes("value").SingleOrDefault();
                        if (resourceId != null)
                        {
                            // var fullUrl = DefaultBaseUrl + resourceNode.Name.LocalName + "/" + resourceId.Value;
                            var fullUrl = CreateResourceUri(resourceNode.Name.LocalName, resourceId.Value);
                            yield return (resourceNode, fullUrl);
                        }
                    }
                }
            }
            yield break;
        }

        private static string getRootName(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == XmlNs.FHIR)
                    return reader.LocalName;
            }

            return null;
        }

    }

}