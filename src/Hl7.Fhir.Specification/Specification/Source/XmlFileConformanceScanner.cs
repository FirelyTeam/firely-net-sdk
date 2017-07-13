/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;
using Hl7.Fhir.Introspection;
using System.Diagnostics;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Xml FHIR (conformance) resource from a given stream
    /// </summary>
    internal class XmlFileConformanceScanner : IConformanceScanner
    {
        string _path;

        public XmlFileConformanceScanner(string path)
        {
            _path = path;
        }

        public List<ConformanceScanInformation> List()
        {
            using (var input = File.OpenRead(_path))
            {
                return streamResources(input)

                    // [WMR 20170420] Issue: if the resource type is unknown (i.e. DSTU Conformance), 
                    // then we cannot parse res.Name to a ResourceType enum value 
                    // (ParseLiteral returns null and .Value throws an exception) 
                    // => First skip unknown resources 
                    .Where(res => ModelInfo.IsKnownResource(res.element.Name.LocalName))

                    .Select(res =>
                            new ConformanceScanInformation()
                            {
                                ResourceType = EnumUtility.ParseLiteral<ResourceType>(res.element.Name.LocalName).Value,
                                ResourceUri = res.fullUrl,
                                Canonical = getPrimitiveValueElement(res.element, "url"),
                                ValueSetSystem = getValueSetSystem(res.element),
                                UniqueIds = getUniqueIds(res.element),
                                ConceptMapSource = getCmSource(res.element),
                                ConceptMapTarget = getCmTarget(res.element),
                                Origin = _path,
                            })

                     .ToList();
            }
        }


        public Resource Retrieve(ConformanceScanInformation entry)
        {
            if (entry == null) throw Error.ArgumentNull(nameof(entry));

            XElement found = null;

            using (var input = File.OpenRead(entry.Origin))
            {
                var resources = streamResources(input);

                found = resources.Where(res => res.fullUrl == entry.ResourceUri).SingleOrDefault().element;
            }

            if (found == null) return null;

            var resultResource = new FhirXmlParser().Parse<Resource>(new XmlDomFhirReader(found));
            resultResource.AddAnnotation(new OriginAnnotation { Origin = entry.Origin });

            return resultResource;
        }


        private string getValueSetSystem(XElement vs)
        {
            return vs.Elements(XmlNs.XFHIR + "codeSystem")
                     .Elements(XmlNs.XFHIR + "system")
                     .Attributes("value")
                     .Select(a => a.Value).SingleOrDefault();
        }

        private string[] getUniqueIds(XElement ns)
        {
            return ns.Elements(XmlNs.XFHIR + "uniqueId")
                     .Elements(XmlNs.XFHIR + "value")
                     .Attributes("value")
                     .Select(a => a.Value).ToArray<string>();
        }

        private string getCmSource(XElement cm)
        {
            return cm
                 .Elements(XmlNs.XFHIR + "sourceUri")
                 .Concat(cm
                    .Elements(XmlNs.XFHIR + "sourceReference")
                    .Elements(XmlNs.XFHIR + "reference"))
                 .Attributes("value").Select(a => a.Value).SingleOrDefault();
        }

        private string getCmTarget(XElement cm)
        {
            return cm
                 .Elements(XmlNs.XFHIR + "targetUri")
                 .Concat(cm
                    .Elements(XmlNs.XFHIR + "targetReference")
                    .Elements(XmlNs.XFHIR + "reference"))
                 .Attributes("value").Select(a => a.Value).SingleOrDefault();
        }

        private string getPrimitiveValueElement(XElement element, string name)
        {
            return element.Elements(XmlNs.XFHIR + name)
                    .Attributes("value")
                    .Select(a => a.Value).SingleOrDefault();
        }

        private class FullUrlAnnotation
        {
            public string FullUrl { get; set; }
        }

        // Use a forward-only XmlReader to scan through a possibly huge bundled file,
        // and yield the feed entries, so only one entry is in memory at a time
        private IEnumerable<(XElement element, string fullUrl)> streamResources(Stream input)
        {
            var reader = SerializationUtil.XmlReaderFromStream(input);
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
                        var fullUrl = "http://example.org/" + resourceNode.Name.LocalName + "/" + resourceId.Value;
                        yield return (resourceNode, fullUrl);
                    }
                }
            }

            else
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
