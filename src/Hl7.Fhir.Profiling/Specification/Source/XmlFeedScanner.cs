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

namespace Hl7.Fhir.Support
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Atom Xml feed for a given entry. The feed is
    /// not read in its entirety, but it yields an enumerable set of matching XElements and has therefore a
    /// smaller memory footprint.
    /// </summary>
    internal class XmlFeedScanner
    {
        public static readonly XName ENTRY_ID = BundleXmlParser.XATOMNS + BundleXmlParser.XATOM_ID;        

        private Stream _input;

        public XmlFeedScanner(Stream stream)
        {
            _input = stream;
        }

        // Scan a supplied bundle (atom) file with the core artifacts for an entry with the given uri
        public XElement FindEntryById(Uri entryUri)
        {
            if(entryUri == null) throw Error.ArgumentNull("entryUri");

            var entryId = entryUri.ToString();

            if(_input.Position != 0)
            {
                if(_input.CanSeek)
                    _input.Seek(0,SeekOrigin.Begin);
                else
                    throw Error.InvalidOperation("Stream is not at beginning, and seeking is not supported by this stream");
            }
                
            var reader = XmlReader.Create(_input);

            // We're a bit lenient here, but find 0..1 entry which has ANY entry.id that
            // matches the artifactId we're looking for
            var entries = streamFeedEntries(reader);
            var matchingEntry = entries
                    .SingleOrDefault(entry => entry.Elements(ENTRY_ID)
                        .Any(idElem => idElem.Value == entryId));

            return matchingEntry;
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
                        && reader.NamespaceURI == BundleXmlParser.ATOMPUB_NS)
                {
                    var entryNode = (XElement)XElement.ReadFrom(reader);
                    yield return entryNode;
                }
            }
        }
    }
}
