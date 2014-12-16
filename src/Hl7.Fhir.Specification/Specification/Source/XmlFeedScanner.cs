/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Atom Xml feed for a given entry. The feed is
    /// not read in its entirety, but it yields an enumerable set of matching XElements and has therefore a
    /// smaller memory footprint.
    /// </summary>
    internal class XmlFeedScanner
    {
        private Stream _input;

        public XmlFeedScanner(Stream stream)
        {
            _input = stream;
        }

        /// <summary>
        /// Scan a supplied bundle (atom) file with the core artifacts for an entry with an id equal to the given uri 
        /// </summary>
        /// <param name="entryUri"></param>
        /// <returns></returns>
        public XElement FindConformanceResourceById(string identifier)
        {
            if(identifier == null) throw Error.ArgumentNull("identifier");

            var reader = FhirParser.XmlReaderFromStream(_input);
            var entries = streamFeedEntries(reader);

            // We're a bit lenient here, but find 0..1 entry which has ANY entry.id that
            // matches the artifactId we're looking for
            var artifactResources = entries.Select(entry=>entry.Elements().First().Elements().First());

            return artifactResources.Where(res =>
                res.Element(XmlNs.XFHIR + ConformanceArtifactScanner.GetIdentifyingElementNameForConformanceResource(res.Name.LocalName))
                            .Attribute("value").Value == identifier)
                            .SingleOrDefault();
        }


        public IEnumerable<string> ListEntryIds()
        {
            var reader = FhirParser.XmlReaderFromStream(_input);
            var entries = streamFeedEntries(reader);

            var artifactResources = entries.Select(entry => entry.Elements().First().Elements().First());
            return artifactResources.Select(res =>
                res.Element(XmlNs.XFHIR + ConformanceArtifactScanner.GetIdentifyingElementNameForConformanceResource(res.Name.LocalName))
                        .Attribute("value").Value);

        }
    
        // Use a forward-only XmlReader to scan through a possibly huge bundled file,
        // and yield the feed entries, so only one entry is in memory at a time
        private static IEnumerable<XElement> streamFeedEntries(XmlReader reader)
        {
            reader.MoveToContent();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                        && reader.LocalName == "entry"
                        && reader.NamespaceURI == XmlNs.FHIR)
                {
                    var entryNode = (XElement)XElement.ReadFrom(reader);
                    yield return entryNode;
                }
            }
        }
    }
}
