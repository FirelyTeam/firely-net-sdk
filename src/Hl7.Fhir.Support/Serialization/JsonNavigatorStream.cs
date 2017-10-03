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
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Hl7.Fhir.ElementModel;
using System.Collections;

#if NET_FILESYSTEM

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Internal class which is able to scan a (possibly) large Xml FHIR (conformance) resource from a given stream
    /// </summary>
    public class JsonNavigatorStream : IElementNavigatorStream, IDisposable
    {
        private JsonReader _reader = null;
        private Stream _fileStream = null;

        public string ResourceType { get; private set; }
        public bool IsBundle => ResourceType == "Bundle";

        public JsonNavigatorStream(string path)
        {
            _fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            _reader = SerializationUtil.JsonReaderFromStream(_fileStream);

            Reset();
        }

        public void Reset()
        {
            _fileStream.Seek(0, SeekOrigin.Begin);
            _reader = SerializationUtil.JsonReaderFromStream(_fileStream);

            ResourceType = scanForResourceType(_reader);

            // Reset - again, since getrootName may have found the resource type at the end of the file
            _fileStream.Seek(0, SeekOrigin.Begin);
            _reader = SerializationUtil.JsonReaderFromStream(_fileStream);

            // In a Bundle, try to move to first entry - which really is our first resource.
            // We ignore the result, MoveNext() will correctly return false if searching here fails.
            if (IsBundle) skipTo(_reader, "entry");
        }

        private string scanForResourceType(JsonReader reader) => skipTo(reader, "resourceType") ? reader.ReadAsString() : null;

        private static bool skipTo(JsonReader reader, string path)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Path == path)
                    return true;
            }

            return false;
        }

        private (JObject element, string fullUrl)? _current = null;

        public bool MoveNext()
        {
            if (ResourceType == null) return false;

            if (IsBundle)
            {
                while (_reader.Read())
                {
                    // Does this even work? entry[] is an array, I don't see that accounted for here
                    if (_reader.TokenType == JsonToken.StartObject && _reader.Path.StartsWith("entry["))
                    {
                        var entry = (JObject)JObject.ReadFrom(_reader);
                        var fullUrl = entry.Value<string>("fullUrl");
                        if (fullUrl != null)
                        {
                            if (entry["resource"] is JObject resourceNode)
                            {
                                _current = (resourceNode, fullUrl);
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            else
            {
                var resource = (JObject)JObject.ReadFrom(_reader);

                if (resource != null)
                {
                    // First try to initialize from canonical url (conformance resources)
                    var canonicalUrl = resource.Value<string>("url");

                    // Otherwise try to initialize from resource id
                    if (canonicalUrl == null)
                    {
                        var resourceId = resource.Value<string>("id");
                        if (resourceId != null)
                            canonicalUrl = "http://example.org/" + ResourceType + "/" + resourceId;
                    }

                    if(canonicalUrl != null)
                    {
                        _current = (resource, canonicalUrl);
                        return true;
                    }
                }

                return false;
            }
        }

        public bool Seek(string position)
        {
            if (position == null) throw Error.ArgumentNull(nameof(position));

            // start looking from the beginning
            Reset();

            while (MoveNext())
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

                    if (_fileStream != null)
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
                var jelem = _current?.element;
                if (jelem != null)
                    return JsonDomFhirNavigator.Create(jelem);
                else
                    return null;
            }
        }

        object IEnumerator.Current => this.Current;
    }
}

#endif