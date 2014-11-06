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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Atom Xml feed for a given entry. The feed is
    /// not read in its entirety, but it yields an enumerable set of matching XElements and has therefore a
    /// smaller memory footprint.
    /// </summary>
    internal class XmlFeedScanner
    {
        public static readonly XName ENTRY_ID = XmlNs.XATOM + "id";        

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
        public XElement FindEntryById(Uri entryUri)
        {
            if(entryUri == null) throw Error.ArgumentNull("entryUri");

            var reader = xmlReaderFromStream(_input);
            var entries = streamFeedEntries(reader);

            // We're a bit lenient here, but find 0..1 entry which has ANY entry.id that
            // matches the artifactId we're looking for
            var entryId = entryUri.ToString();
            var matchingEntry = entries
                    .SingleOrDefault(entry => entry.Elements(ENTRY_ID).Any(idElem => idElem.Value == entryId));

            return matchingEntry;
        }


        public IEnumerable<string> ListEntryIds()
        {
            var reader = xmlReaderFromStream(_input);
            var entries = streamFeedEntries(reader);

            return entries.Elements(ENTRY_ID).Select(idElem => idElem.Value);
        }


        private static XmlReader xmlReaderFromStream(Stream input)
        {
            if (input.Position != 0)
            {
                if (input.CanSeek)
                    input.Seek(0, SeekOrigin.Begin);
                else
                    throw Error.InvalidOperation("Stream is not at beginning, and seeking is not supported by this stream");
            }

            var reader = XmlReader.Create(input);
            return reader;
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
                        && reader.NamespaceURI == XmlNs.ATOM)
                {
                    var entryNode = (XElement)XElement.ReadFrom(reader);
                    yield return entryNode;
                }
            }
        }
    }
}
