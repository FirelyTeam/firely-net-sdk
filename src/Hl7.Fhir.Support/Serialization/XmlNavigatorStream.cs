/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;
using System.Collections;

#if NET_FILESYSTEM

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Xml FHIR Bundle into separate entries, each represented by an
    /// IElementNavigator. Supports files with a single resource too.
    /// </summary>
    public class XmlNavigatorStream : ISeekableEnumerator<IElementNavigator>, IDisposable
    {
        private XmlReader _reader = null;
        private Stream _fileStream = null;

        public string RootName { get; private set; }
        public bool IsBundle => RootName == "Bundle";

        public XmlNavigatorStream(string path)
        {
            _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            Reset();
        }

        public void Reset()
        {
            _fileStream.Seek(0, SeekOrigin.Begin);
            _reader = SerializationUtil.XmlReaderFromStream(_fileStream);

            RootName = getRootName(_reader);

            // In a Bundle, try to move to first entry - which really is our first resource.
            // We ignore the result, MoveNext() will correctly return false if searching here fails.
            if (IsBundle) _reader.ReadToDescendant("entry", XmlNs.FHIR);
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
   
        private (XElement element, string fullUrl)? _current = null;

        public bool MoveNext()
        {
            if (RootName == null) return false;

            if (IsBundle)
            {
                // do....while(!_reader.ReadToSibling()) ??
                while (!_reader.EOF)
                {
                    if (_reader.NodeType == XmlNodeType.Element
                        && _reader.NamespaceURI == XmlNs.FHIR && _reader.LocalName == "entry")
                    {
                        var entryNode = (XElement)XElement.ReadFrom(_reader);
                        var fullUrl = entryNode.Elements(XmlNs.XFHIR + "fullUrl").Attributes("value").SingleOrDefault();
                        if (fullUrl != null)
                        {
                            var resourceNode = entryNode.Element(XName.Get("resource", XmlNs.FHIR));
                            if (resourceNode != null)
                            {
                                var resource = resourceNode.Elements().FirstOrDefault();
                                if (resource != null)
                                {
                                    _current = (resource, fullUrl.Value);
                                    return true;
                                }
                            }
                        }
                    }
                    else
                        _reader.Read();
                }

                return false;
            }

            // [WMR 20160908] Fixed, parse stand-alone (conformance) resources
            else
            {
                if (!_reader.EOF)
                {
                    var resourceNode = (XElement)XElement.ReadFrom(_reader);

                    // First try to initialize from canonical url (conformance resources)
                    var canonicalUrl = resourceNode.Elements(XmlNs.XFHIR + "url").Attributes("value").SingleOrDefault()?.Value;

                    // Otherwise try to initialize from resource id
                    if (canonicalUrl == null)
                    {
                        var resourceId = resourceNode.Elements(XmlNs.XFHIR + "id").Attributes("value").SingleOrDefault();
                        if (resourceId != null)
                            canonicalUrl = "http://example.org/" + resourceNode.Name.LocalName + "/" + resourceId.Value;
                    }

                    if (canonicalUrl != null)
                    {
                        _current = (resourceNode, canonicalUrl);
                        return true;
                    }
                }

                return false;
            }
        }


        // Klopt niet -> kan static zijn. Maar de aanroepers creeren toch een instance met het pad -> komt dus van twee plekken
        public bool Seek(string position)
        {
            if (position == null) throw Error.ArgumentNull(nameof(position));

            // start looking from the beginning
            Reset();

            // Slow - this parses all resources on the way (same in the old code). Better to just look
            // at the XmlReader
            while(MoveNext())
            {
                if (Position == position) return true;
            }

            return false;

            //This code needs to be moved to the DirectorySource - which now assumes the streamer
            //does this (but no longer - as we don't return Resources anymore)
            //var resultResource = new FhirXmlParser().Parse<Resource>(new XmlDomFhirReader(found));
            //resultResource.SetOrigin(entry.Origin);
            //return resultResource
        }

        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_reader != null)
                    {
                        ((IDisposable)_reader).Dispose();
                        _reader = null;
                    }

                    if(_fileStream != null)
                    {
                        _fileStream.Dispose();
                        _fileStream = null;
                    }
                }

                // release any unmanaged objects
                // set the object references to null

                _disposed = true;
            }
        }


        public string Position => _current?.fullUrl;

        public IElementNavigator Current
        {
            get
            {
                var xelem = _current?.element;
                if (xelem != null)
                    return XmlDomFhirNavigator.Create(xelem);
                else
                    return null;
            }
        }

        object IEnumerator.Current => this.Current;
    }
}

#endif