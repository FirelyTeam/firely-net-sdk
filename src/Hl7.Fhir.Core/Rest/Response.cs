/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Hl7.Fhir.Rest
{
    public class Response
    {
        public string Status { get; set; }
        public string Location { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string Etag { get; set; }
        public List<Tuple<string, string>> Headers { get; } = new List<Tuple<string, string>>();
        public byte[] Body { get; set; }
        public Model.Resource Resource { get; set; }

        internal static Response FromHttpResponse(HttpWebResponse response, byte[] body, Serialization.ParserSettings parserSettings, Func<byte[], string, Model.Resource> makeBinaryResource, bool throwOnFormatException)
        {
            var result = new Response();

            result.Status = ((int)response.StatusCode).ToString();
            result.SetHeaders(response.Headers);

            var contentType = getContentType(response);
            var charEncoding = getCharacterEncoding(response);

            result.Location = response.Headers[HttpUtil.LOCATION] ?? response.Headers[HttpUtil.CONTENTLOCATION];

#if NETSTANDARD1_1
            if (!String.IsNullOrEmpty(response.Headers[HttpUtil.LASTMODIFIED]))
                result.LastModified = DateTimeOffset.Parse(response.Headers[HttpUtil.LASTMODIFIED]);
#else
            result.LastModified = response.LastModified;
#endif
            result.Etag = getETag(response);

            if (body != null)
            {
                result.Body = body;

                if (IsBinaryResponse(response.ResponseUri.OriginalString, contentType))
                {
                    result.Resource = makeBinaryResource(body, contentType);
                    if (result.Location != null)
                    {
                        var ri = new ResourceIdentity(result.Location);
                        result.Resource.Id = ri.Id;
                        result.Resource.Meta = new Model.Meta();
                        result.Resource.Meta.VersionId = ri.VersionId;
                        result.Resource.ResourceBase = ri.BaseUri;
                    }
                }
                else
                {
                    var bodyText = DecodeBody(body, charEncoding);
                    var resource = parseResource(bodyText, contentType, parserSettings, throwOnFormatException);
                    result.Resource = resource;

                    if (result.Location != null)
                        result.Resource.ResourceBase = new ResourceIdentity(result.Location).BaseUri;
                }
            }

            return result;
        }

        internal static string DecodeBody(byte[] body, Encoding enc)
        {
            if (body == null) return null;
            if (enc == null) enc = Encoding.UTF8;

            // [WMR 20160421] Explicit disposal
            // return (new StreamReader(new MemoryStream(body), enc, true)).ReadToEnd();
            using (var stream = new MemoryStream(body))
            using (var reader = new StreamReader(stream, enc, true))
            {
                return reader.ReadToEnd();
            }
        }

        internal static bool IsBinaryResponse(string responseUri, string contentType)
        {
            if (!string.IsNullOrEmpty(contentType)
                && (ContentType.XML_CONTENT_HEADERS.Contains(contentType.ToLower())
                    || ContentType.JSON_CONTENT_HEADERS.Contains(contentType.ToLower())
                )
                )
                return false;

            if (ResourceIdentity.IsRestResourceIdentity(responseUri))
            {
                var id = new ResourceIdentity(responseUri);

                if (id.ResourceType != Model.ResourceType.Binary.ToString()) return false;

                if (id.Id != null && Model.Id.IsValidValue(id.Id)) return true;
                if (id.VersionId != null && Model.Id.IsValidValue(id.VersionId)) return true;
            }

            return false;
        }

        public string GetBodyAsText()
        {
            if (Body != null)
                return DecodeBody(Body, Encoding.UTF8);
            else
                return null;
        }

        public void SetHeaders(WebHeaderCollection headers)
        {
            Headers.Clear();
            foreach (var key in headers.AllKeys)
            {
                Headers.Add(Tuple.Create(key, headers[key]));
            }
        }

        public IEnumerable<string> GetHeader(string header)
        {
            return Headers
                .Where(h => h.Item1 == header)
                .Select(h => h.Item2);
        }

        private static string getETag(HttpWebResponse response)
        {
            var result = response.Headers[HttpUtil.ETAG];

            if (result != null)
            {
                if (result.StartsWith(@"W/")) result = result.Substring(2);
                result = result.Trim('\"');
            }

            return result;
        }

        private static string getContentType(HttpWebResponse response)
        {
            if (!String.IsNullOrEmpty(response.ContentType))
            {
                return ContentType.GetMediaTypeFromHeaderValue(response.ContentType);
            }
            else
                return null;
        }

        private static Encoding getCharacterEncoding(HttpWebResponse response)
        {
            Encoding result = null;

            if (!String.IsNullOrEmpty(response.ContentType))
            {
                var charset = ContentType.GetCharSetFromHeaderValue(response.ContentType);

                if (!String.IsNullOrEmpty(charset))
                    result = Encoding.GetEncoding(charset);
            }
            return result;
        }

        private static Model.Resource parseResource(string bodyText, string contentType, Serialization.ParserSettings settings, bool throwOnFormatException)
        {
            Model.Resource result = null;

            var fhirType = ContentType.GetResourceFormatFromContentType(contentType);

            if (fhirType == ResourceFormat.Unknown)
                throw new UnsupportedBodyTypeException(
                    "Endpoint returned a body with contentType '{0}', while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint?"
                        .FormatWith(contentType), contentType, bodyText);

            if (!SerializationUtil.ProbeIsJson(bodyText) && !SerializationUtil.ProbeIsXml(bodyText))
                throw new UnsupportedBodyTypeException(
                        "Endpoint said it returned '{0}', but the body is not recognized as either xml or json.".FormatWith(contentType), contentType, bodyText);

            try
            {
                if (fhirType == ResourceFormat.Xml)
                {
                    result = new FhirXmlParser(settings).Parse<Resource>(bodyText);
                }
                else
                {
                    // To use the old JSON parser:
                    //
                    //     result = new FhirJsonParser(settings).Parse<Model.Resource>(bodyText);
                    //
                    try
                    {
                        result = JsonSerializer.Deserialize<Resource>(
                            bodyText,
                            new JsonSerializerOptions().ForFhir(settings)
                        );
                    }
                    catch (JsonException jsonException)
                    {
                        throw jsonException.ToFormatException();
                    }
                }
            }
            catch (FormatException) when (!throwOnFormatException)
            {
                // if (throwOnFormatException) throw fe;

                // [WMR 20181029] TODO...
                // ExceptionHandler.NotifyOrThrow(...)_

                return null;
            }
            return result;
        }
    }

    public class UnsupportedBodyTypeException : Exception
    {
        public string BodyType { get; set; }

        public string Body { get; set; }
        public UnsupportedBodyTypeException(string message, string mimeType, string body) : base(message)
        {
            BodyType = mimeType;
            Body = body;
        }
    }
}
