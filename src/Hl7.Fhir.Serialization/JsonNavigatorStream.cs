/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;

#if !NETSTANDARD1_1

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Provides efficient extraction of summary information from a raw FHIR JSON resource file,
    /// without actually deserializing the full resource. Also supports resource bundles.
    /// </summary>
    /// <remarks>Replacement for JsonArtifactScanner (now obsolete).</remarks>
    public class JsonNavigatorStream : INavigatorStream
    {
        private readonly Stream _stream;
        private readonly bool _disposeStream;
        private JsonReader _reader;
        private (JObject element, string fullUrl)? _current = null;

        /// <summary>Create a new <see cref="JsonNavigatorStream"/> instance for the specified serialized json resource file.</summary>
        /// <param name="path">The filepath of a serialized json resource.</param>
        [Obsolete("Use JsonNavigatorStream.FromPath()")]
        public JsonNavigatorStream(string path) : this(new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //
        }

        /// <summary>Create a new <see cref="JsonNavigatorStream"/> instance for the specified serialized json resource file.</summary>
        /// <param name="path">The filepath of a serialized json resource.</param>
        /// <returns>A new <see cref="JsonNavigatorStream"/> instance.</returns>
        public static JsonNavigatorStream FromPath(string path)
            => new JsonNavigatorStream(new FileStream(path, FileMode.Open, FileAccess.Read));

        /// <summary>Create a new <see cref="JsonNavigatorStream"/> instance for the specified serialized json resource file.</summary>
        /// <param name="path">The filepath of a serialized json resource.</param>
        /// <param name="disposeStream">Determines if the <see cref="Dispose()"/> method should also dispose the internal <see cref="Stream"/> instance.</param>
        /// <param name="settings">Parser settings that control de-serialization behavior.</param>
        /// <returns>A new <see cref="JsonNavigatorStream"/> instance.</returns>
        public static JsonNavigatorStream FromPath(string path, bool disposeStream, FhirJsonParsingSettings settings)
            => new JsonNavigatorStream(new FileStream(path, FileMode.Open, FileAccess.Read), disposeStream, settings);

        /// <summary>Create a new <see cref="JsonNavigatorStream"/> instance for the specified json resource stream.</summary>
        /// <param name="stream">A stream that returns a serialized json resource.</param>
        /// <remarks>The <see cref="Dispose()"/> method also disposes the specified <paramref name="stream"/> instance.</remarks>
        public JsonNavigatorStream(Stream stream) : this(stream, true) { }

        /// <summary>Create a new <see cref="JsonNavigatorStream"/> instance for the specified json resource stream.</summary>
        /// <param name="stream">A stream that returns a serialized json resource.</param>
        /// <param name="disposeStream">Determines if the <see cref="Dispose()"/> method should also dispose the specified <paramref name="stream"/> instance.</param>
        public JsonNavigatorStream(Stream stream, bool disposeStream)
            : this(stream, disposeStream, null) { }

        /// <summary>Create a new <see cref="JsonNavigatorStream"/> instance for the specified json resource stream.</summary>
        /// <param name="stream">A stream that returns a serialized json resource.</param>
        /// <param name="disposeStream">Determines if the <see cref="Dispose()"/> method should also dispose the specified <paramref name="stream"/> instance.</param>
        /// <param name="settings">Parser settings that control de-serialization behavior.</param>
        public JsonNavigatorStream(Stream stream, bool disposeStream, FhirJsonParsingSettings settings)
        {
            _stream = stream ?? throw Error.ArgumentNull(nameof(stream));
            _reader = SerializationUtil.JsonReaderFromStream(stream);
            _disposeStream = disposeStream;
            ParserSettings = settings?.Clone() ?? FhirJsonParsingSettings.CreateDefault();

            // [WMR 20181026] TODO: Don't reset stream by default!
            // cf. XmlNavigatorStream
            Reset();
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
                        // _stream = null;
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

            var stream = _stream;
            if (!stream.CanSeek)
            {
                throw Error.NotSupported($"Unable to reset the {nameof(JsonNavigatorStream)}. The internal '{stream.GetType().Name}' instance does not support seeking.");
            }
            _stream.Seek(0, SeekOrigin.Begin);

            initializeReader();
        }

        void initializeReader()
        {
            //[EK 2017-11-07]
            //We should probably do this, but somewhere in the stack from _stream to _reader,
            //something is disposing _stream as well when _reader gets disposed. Probably
            //has something to do with the StreamReader that gets created and which will close
            //its parent stream, whether you like it or not...
            disposeReader();
            _reader = SerializationUtil.JsonReaderFromStream(_stream);

            ResourceType = scanForResourceType(_reader);

            // Reset - again, since getrootName may have found the resource type at the end of the file
            _stream.Seek(0, SeekOrigin.Begin);
            _reader = SerializationUtil.JsonReaderFromStream(_stream);

            // In a Bundle, try to move to first entry - which really is our first resource.
            // We ignore the result, MoveNext() will correctly return false if searching here fails.
            if (IsBundle) skipTo(_reader, "entry");
        }

        public bool MoveNext() => MoveNext(null);

        public bool MoveNext(string fullUrl)
        {
            throwIfDisposed();
            if (ResourceType == null) return false;

            var reader = _reader;
            if (IsBundle)
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject && reader.Path.StartsWith("entry["))
                    {
#if true
                        // [WMR 20190304] #890 Also return entries with missing fullUrl property
                        string entryUrl = null;
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                if (reader.Value.Equals("fullUrl"))
                                {
                                    entryUrl = reader.ReadAsString();
                                }
                                else if (reader.Value.Equals("resource"))
                                {
                                    // To be on the safe side, only include anonymous
                                    // resources w/o id if PermissiveParsing equals true
                                    if (entryUrl is null && !ParserSettings.PermissiveParsing)
                                    {
                                        return false;
                                    }
                                    if (fullUrl is null || fullUrl == entryUrl)
                                    {
                                        if (reader.Read())
                                        {
                                            var resourceNode = (JObject)JObject.ReadFrom(reader);
                                            _current = (resourceNode, entryUrl);
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
#else
                        // [WMR 20190304] Original logic skips entries w/o fullUrl

                        if (skipTo(reader, "fullUrl"))
                        {
                            var entryUrl = reader.ReadAsString();
                            if (entryUrl != null && (fullUrl == null || entryUrl == fullUrl))
                            {
                                if (skipTo(reader, "resource") && reader.Read())
                                {
                                    var resourceNode = (JObject)JObject.ReadFrom(reader);
                                    _current = (resourceNode, entryUrl);
                                    return true;
                                }
                            }
                        }
#endif
                    }
                }

                return false;
            }

            else
            {
                if (reader.TokenType != JsonToken.EndObject)
                {
                    var resource = (JObject)JObject.ReadFrom(reader);

                    if (resource != null)
                    {
                        // First try to initialize from canonical url (conformance resources)
                        var canonicalUrl = resource.Value<string>("url");

                        // Otherwise try to initialize from resource id
                        if (canonicalUrl == null)
                        {
                            var resourceId = resource.Value<string>("id");
                            if (resourceId != null)
                            {
                                // [WMR 20171023] This is not a Bundle, so ResourceType
                                // property returns the actual type of the current entry
                                canonicalUrl = NavigatorStreamHelper.FormatCanonicalUrlForBundleEntry(ResourceType, resourceId);
                            }
                        }

                        // [WMR 20190304] return instances w/o Resource Id
                        //if (canonicalUrl != null && (fullUrl == null || canonicalUrl == fullUrl))
                        if (fullUrl is null || canonicalUrl == fullUrl)
                        {
                            _current = (resource, canonicalUrl);
                        }
                        return true;
                    }
                }
            }

            return false;
        }

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
                var jelem = _current?.element;
                return jelem != null ? FhirJsonNode.Create(jelem, settings:ParserSettings) : null;
            }
        }

        object IEnumerator.Current => this.Current;

        // [WMR 20181026] NEW

        /// <summary>Gets the configuration settings that control parsing behavior.</summary>
        /// <value>Returns a (non-<c>null</c>) <see cref="FhirJsonParsingSettings"/> instance.</value>
        public FhirJsonParsingSettings ParserSettings { get; }

#region private helpers

        string scanForResourceType(JsonReader reader) => skipTo(reader, "resourceType") ? reader.ReadAsString() : null;

        static bool skipTo(JsonReader reader, string path)
        {
            // Throws on invalid input
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value.Equals(path))
                    return true;
            }
            return false;
        }

        void throwIfDisposed()
        {
            if (_disposed) { throw new ObjectDisposedException(GetType().FullName); }
        }

#endregion

    }
}

#endif