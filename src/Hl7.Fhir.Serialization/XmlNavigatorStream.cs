/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

#if !NETSTANDARD1_1

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Provides efficient extraction of summary information from a raw FHIR XML resource file,
    /// without actually deserializing the full resource. Also supports resource bundles.
    /// </summary>
    /// <remarks>Replacement for XmlArtifactScanner (now obsolete).</remarks>
    public class XmlNavigatorStream : INavigatorStream
    {
        private readonly Stream _stream;
        private readonly bool _disposeStream;
        private XmlReader _reader;
        private (XElement element, string fullUrl)? _current = null;

        /// <summary>Create a new <see cref="XmlNavigatorStream"/> instance for the specified serialized xml resource file.</summary>
        /// <param name="path">The filepath of a serialized xml resource.</param>
        [Obsolete("Use XmlNavigatorStream.FromPath()")]
        public XmlNavigatorStream(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //
        }

        /// <summary>Create a new <see cref="XmlNavigatorStream"/> instance for the specified serialized xml resource file.</summary>
        /// <param name="path">The filepath of a serialized xml resource.</param>
        /// <returns>A new <see cref="XmlNavigatorStream"/> instance.</returns>
        public static XmlNavigatorStream FromPath(string path)
            => new XmlNavigatorStream(new FileStream(path, FileMode.Open, FileAccess.Read));

        /// <summary>Create a new <see cref="XmlNavigatorStream"/> instance for the specified serialized xml resource file.</summary>
        /// <param name="path">The filepath of a serialized xml resource.</param>
        /// <param name="disposeStream">Determines if the <see cref="Dispose()"/> method should also dispose the internal <see cref="Stream"/> instance.</param>
        /// <param name="settings">Parser settings that control de-serialization behavior.</param>
        /// <returns>A new <see cref="XmlNavigatorStream"/> instance.</returns>
        public static XmlNavigatorStream FromPath(string path, bool disposeStream, FhirXmlParsingSettings settings)
            => new XmlNavigatorStream(new FileStream(path, FileMode.Open, FileAccess.Read), disposeStream, settings);

        /// <summary>Create a new <see cref="XmlNavigatorStream"/> instance for the specified xml resource stream.</summary>
        /// <param name="stream">A stream that returns a serialized xml resource.</param>
        /// <remarks>The <see cref="Dispose()"/> method also disposes the specified <paramref name="stream"/> instance.</remarks>
        public XmlNavigatorStream(Stream stream) : this(stream, true) { }

        /// <summary>Create a new <see cref="XmlNavigatorStream"/> instance for the specified xml resource stream.</summary>
        /// <param name="stream">A stream that returns a serialized xml resource.</param>
        /// <param name="disposeStream">Determines if the <see cref="Dispose()"/> method should also dispose the specified <paramref name="stream"/> instance.</param>
        public XmlNavigatorStream(Stream stream, bool disposeStream)
            : this(stream, disposeStream, null) { }

        /// <summary>Create a new <see cref="XmlNavigatorStream"/> instance for the specified xml resource stream.</summary>
        /// <param name="stream">A stream that returns a serialized xml resource.</param>
        /// <param name="disposeStream">Determines if the <see cref="Dispose()"/> method should also dispose the specified <paramref name="stream"/> instance.</param>
        /// <param name="settings">Parser settings that control de-serialization behavior.</param>
        public XmlNavigatorStream(Stream stream, bool disposeStream, FhirXmlParsingSettings settings)
        {
            _stream = stream ?? throw Error.ArgumentNull(nameof(stream));
            _disposeStream = disposeStream;
            ParserSettings = settings?.Clone() ?? FhirXmlParsingSettings.CreateDefault();

            // Don't reset stream by default!
            // Relies on Stream.Seek() method, not supported by forward-only readers (e.g. zip archive)
            // Reset();
            initializeReader();
        }


        #region IDisposable

        bool _disposed;

        public void Dispose() => Dispose(true);

        private void disposeReader()
        {
            if (_reader != null)
            {
                ((IDisposable)_reader).Dispose();
                _reader = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    disposeReader();

                    if (_stream != null && _disposeStream)
                    {
                        _stream.Dispose();
                        //_stream = null;
                    }
                }

                // release any unmanaged objects
                // set the object references to null

                _disposed = true;
            }
        }

        #endregion

        /// <summary>The typename of the underlying resource (container).</summary>
        /// <remarks>Call Current.Type to determine the type of the currently enumerated resource.</remarks>
        public string ResourceType { get; private set; }

        // [WMR 20171023] Obsolete, to make INavigatorStream more generic (e.g. allow PoCo input)
        // <summary>The full path of the current resource file, or of the containing resource bundle file.</summary>
        // public string Path => _fileStream?.Name;

        /// <summary>Returns <c>true</c> if the underlying file represents a Bundle resource, or <c>false</c> otherwise.</summary>
        public bool IsBundle => ResourceType == "Bundle";

        /// <summary>
        /// Reset the stream to the start position.
        /// Requires the internal stream, as specified in the ctor, to support seeking.
        /// </summary>
        /// <exception cref="NotSupportedException">The internal stream does not support seeking.</exception>
        public void Reset()
        {
            throwIfDisposed();

            // This will fail if the internal stream does not support seeking
            var stream = _stream;
            if (!stream.CanSeek)
            {
                throw Error.NotSupported($"Unable to reset the {nameof(XmlNavigatorStream)}. The internal '{stream.GetType().Name}' instance does not support seeking.");
            }
            stream.Seek(0, SeekOrigin.Begin);

            initializeReader();
        }

        void initializeReader()
        {
            disposeReader();
            _reader = SerializationUtil.XmlReaderFromStream(_stream);

            ResourceType = getRootName(_reader);

            // In a Bundle, try to move to first entry - which really is our first resource.
            // We ignore the result, MoveNext() will correctly return false if searching here fails.
            if (IsBundle) _reader.ReadToDescendant("entry", XmlNs.FHIR);
        }

        public bool MoveNext() => MoveNext(null);

        // [WMR 20181026] For MoveNext
        static readonly XName urlName = XmlNs.XFHIR + "url";
        static readonly XName idName = XmlNs.XFHIR + "id";

        public bool MoveNext(string fullUrl)
        {
            throwIfDisposed();

            if (_reader.EOF) return false;
            if (ResourceType == null) return false;

            // Move to first real content node
            if (_reader.NodeType != XmlNodeType.Element)
            {
                while (_reader.Read() && _reader.NodeType != XmlNodeType.Element) ;
                if (_reader.NodeType != XmlNodeType.Element) return false;
            }

            if (IsBundle)
            {
                do
                {
                    using (var entryReader = _reader.ReadSubtree())
                    {
#if true
                        // [WMR 20190304] #890 Also return entries with missing fullUrl property

                        bool IsFhirElement(string elemName)
                            => entryReader.NodeType == XmlNodeType.Element
                            && entryReader.NamespaceURI == XmlNs.FHIR
                            && entryReader.LocalName == elemName;

                        // Consume <entry>
                        if (entryReader.Read() && IsFhirElement("entry"))
                        {
                            // Consume <fullUrl> or <resource>
                            if (entryReader.Read())
                            {
                                string entryUrl = null;
                                bool foundResource;
                                if (IsFhirElement("fullUrl"))
                                {
                                    if (entryReader.MoveToAttribute("value"))
                                    {
                                        entryUrl = entryReader.Value;
                                        entryReader.MoveToElement();
                                    }
                                    foundResource = entryReader.ReadToNextSibling("resource", XmlNs.FHIR);
                                }
                                else
                                {
                                    foundResource = IsFhirElement("resource");
                                }
                                if (foundResource)
                                {
                                    // To be on the safe side, only include anonymous
                                    // resources w/o id if PermissiveParsing equals true
                                    if (entryUrl is null && !ParserSettings.PermissiveParsing)
                                    {
                                        return false;
                                    }
                                    if (fullUrl is null || fullUrl == entryUrl)
                                    {
                                        var resourceNode = (XElement)XElement.ReadFrom(entryReader);
                                        if (!(resourceNode is null))
                                        {
                                            var resource = resourceNode.Elements().FirstOrDefault();
                                            if (!(resource is null))
                                            {
                                                _current = (resource, entryUrl);
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
#else
                        // [WMR 20190304] Original logic skips entries w/o fullUrl

                        var entryUrl = readFullUrl(entryReader);
                        emptyUrl = entryUrl is null;

                        // [WMR 20190304] return bundle entries w/o fullUrl
                        //if (entryUrl != null && (fullUrl == null || entryUrl == fullUrl))
                        if (fullUrl is null || entryUrl == fullUrl)
                        {
                            if (entryReader.ReadToNextSibling("resource", XmlNs.FHIR))
                            {
                                var resourceNode = (XElement)XElement.ReadFrom(entryReader);

                                if (resourceNode != null)
                                {
                                    var resource = resourceNode.Elements().FirstOrDefault();
                                    if (resource != null)
                                    {
                                        _current = (resource, entryUrl);
                                        return true;
                                    }
                                }
                            }
                        }
#endif
                    }
                }
                while (_reader.ReadToNextSibling("entry", XmlNs.FHIR));

                return false;
            }

            // [WMR 20160908] Fixed, parse stand-alone (conformance) resources
            else
            {
                // A bit wasteful to parse the whole resource if we're just scanning for the url,
                // but by the time we have the url, we're too late to use ReadFrom() to parse the resource
                // unless we do Reset(). This is left as an exercise to the reader.
                var resourceNode = (XElement)XElement.ReadFrom(_reader);

                // First try to initialize from canonical url (conformance resources)
                var canonicalUrl = resourceNode.Elements(urlName).Attributes("value").SingleOrDefault()?.Value;

                // Otherwise try to initialize from resource id
                if (canonicalUrl == null)
                {
                    var resourceId = resourceNode.Elements(idName).Attributes("value").SingleOrDefault();
                    if (resourceId != null)
                    {
                        // [WMR 20171023] This is not a Bundle, so ResourceType
                        // property returns the actual type of the current entry
                        canonicalUrl = NavigatorStreamHelper.FormatCanonicalUrlForBundleEntry(ResourceType, resourceId.Value);
                    }
                }

                // [WMR 20190304] return instances w/o Resource Id
                //if (canonicalUrl != null && (fullUrl == null || canonicalUrl == fullUrl))
                if (fullUrl is null || canonicalUrl == fullUrl)
                {
                    _current = (resourceNode, canonicalUrl);
                    return true;
                }

                return false;
            }
        }


        // Klopt niet -> kan static zijn. Maar de aanroepers creeren toch een instance met het pad -> komt dus van twee plekken

        /// <summary>Navigate to the artifact with the specified uri.</summary>
        /// <param name="position">The uri of the target artifact.</param>
        /// <returns><c>true</c> if found, or <c>false</c> otherwise.</returns>
        /// <exception cref="NotSupportedException">The internal stream does not support seeking.</exception>
        public bool Seek(string position)
        {
            throwIfDisposed();
            if (position == null) throw Error.ArgumentNull(nameof(position));

            // start looking from the beginning
            Reset();

            return MoveNext(position);
        }

        /// <summary>Returns the full resource uri of the current artifact.</summary>
        public string Position => _current?.fullUrl;

        /// <summary>Returns a new <see cref="ISourceNode"/> instance for the current entry.</summary>
        public ISourceNode Current
        {
            get
            {
                throwIfDisposed();
                var xelem = _current?.element;
                return xelem != null ? FhirXmlNode.Create(xelem, ParserSettings) : null;
            }
        }

        object IEnumerator.Current => this.Current;

        // [WMR 20181026] NEW

        /// <summary>Gets the configuration settings that control parsing behavior.</summary>
        /// <value>Returns a (non-<c>null</c>) <see cref="FhirXmlParsingSettings"/> instance.</value>
        public FhirXmlParsingSettings ParserSettings { get; }

        #region Private helpers

        static string getRootName(XmlReader reader)
        {
            // Throws on invalid input
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.NamespaceURI == XmlNs.FHIR)
                    return reader.LocalName;
            }
            return null;
        }

        static string readFullUrl(XmlReader reader)
        {
            if (reader.ReadToDescendant("fullUrl", XmlNs.FHIR))
            {
                if (reader.MoveToAttribute("value"))
                {
                    var result = reader.Value;
                    reader.MoveToElement();
                    return result;
                }
            }
            return null;
        }

        void throwIfDisposed()
        {
            if (_disposed) { throw new ObjectDisposedException(GetType().FullName); }
        }

        #endregion

    }
}

#endif