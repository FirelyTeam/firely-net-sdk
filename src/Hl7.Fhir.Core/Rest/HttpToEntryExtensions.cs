/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Rest
{
    public static class HttpToEntryExtensions
    {
        private const string USERDATA_BODY = "$body";
        private const string EXTENSION_RESPONSE_HEADER = "http://hl7.org/fhir/StructureDefinition/http-response-header";      

        internal static Bundle.EntryComponent ToBundleEntry(this HttpWebResponse response, byte[] body, ParserSettings parserSettings, bool throwOnFormatException)
        {
            var result = new Bundle.EntryComponent();

            result.Response = new Bundle.ResponseComponent();
            result.Response.Status = ((int)response.StatusCode).ToString();
            result.Response.SetHeaders(response.Headers);

            var contentType = getContentType(response);
            var charEncoding = getCharacterEncoding(response);

            result.Response.Location = response.Headers[HttpUtil.LOCATION] ?? response.Headers[HttpUtil.CONTENTLOCATION];

#if !DOTNETFW
            if (!String.IsNullOrEmpty(response.Headers[HttpUtil.LASTMODIFIED]))
                    result.Response.LastModified = DateTimeOffset.Parse(response.Headers[HttpUtil.LASTMODIFIED]);
#else
            result.Response.LastModified = response.LastModified;
#endif
            result.Response.Etag = getETag(response);                     

            if (body != null)
            {
                result.Response.SetBody(body);

                if (IsBinaryResponse(response.ResponseUri.OriginalString, contentType))
                {
                    result.Resource = makeBinaryResource(body, contentType);
                    if (result.Response.Location != null)
                    {
                        var ri = new ResourceIdentity(result.Response.Location);
                        result.Resource.Id = ri.Id;
                        result.Resource.Meta = new Meta();
                        result.Resource.Meta.VersionId = ri.VersionId;
                        result.Resource.ResourceBase = ri.BaseUri;
                    }
                }
                else
                {
                    var bodyText = DecodeBody(body, charEncoding);
                    var resource = parseResource(bodyText, contentType, parserSettings, throwOnFormatException);
                    result.Resource = resource;

                    if (result.Response.Location != null)
                        result.Resource.ResourceBase = new ResourceIdentity(result.Response.Location).BaseUri;
                }
            }

            return result;
        }


        private static string getETag(HttpWebResponse response)
        {
            var result = response.Headers[HttpUtil.ETAG];

            if(result != null)
            {
                if(result.StartsWith(@"W/")) result = result.Substring(2);
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

        private static Resource parseResource(string bodyText, string contentType, ParserSettings settings, bool throwOnFormatException)
        {           
            Resource result= null;

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
                if (fhirType == ResourceFormat.Json)
                    result = new FhirJsonParser(settings).Parse<Resource>(bodyText);
                else
                    result = new FhirXmlParser(settings).Parse<Resource>(bodyText);
            }
            catch(FormatException fe)
            {
                if (throwOnFormatException) throw fe;
                return null;
            }

            return result;
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

                if (id.ResourceType != ResourceType.Binary.ToString()) return false;

                if (id.Id != null && Id.IsValidValue(id.Id)) return true;
                if (id.VersionId != null && Id.IsValidValue(id.VersionId)) return true;
            }
            
            return false;
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

        private static Binary makeBinaryResource(byte[] data, string contentType)
        {
            var binary = new Binary();

            binary.Content = data;
            binary.ContentType = contentType;

            return binary;
        }


        public static string GetBodyAsText(this Bundle.ResponseComponent interaction)
        {
            var body = interaction.GetBody();

            if (body != null)
                return DecodeBody(body, Encoding.UTF8);
            else
                return null;
        }


        private class Body
        {
            public byte[] Data;
        }


        public static byte[] GetBody(this Bundle.ResponseComponent interaction)
        {
            var body = interaction.Annotation<Body>();
            return body != null ? body.Data : null;
        }

        internal static void SetBody(this Bundle.ResponseComponent interaction, byte[] data)
        {
            interaction.RemoveAnnotations<Body>();
            interaction.AddAnnotation(new Body { Data = data });
        }

        internal static void SetHeaders(this Bundle.ResponseComponent interaction, WebHeaderCollection headers)
        {
            foreach (var key in headers.AllKeys)
            {
                interaction.AddExtension(EXTENSION_RESPONSE_HEADER, new FhirString(key + ":" + headers[key]));
            }
        }

        public static IEnumerable<Tuple<string,string>> GetHeaders(this Bundle.ResponseComponent interaction)
        {
            foreach (var headerExt in interaction.GetExtensions(EXTENSION_RESPONSE_HEADER))
            {
                if(headerExt.Value != null && headerExt.Value is FhirString)
                {
                    var header = ((FhirString)headerExt.Value).Value;

                    if (header != null)
                    {
                        yield return header.SplitLeft(':');
                    }
                }
            }
        }


        public static IEnumerable<string> GetHeader(this Bundle.ResponseComponent interaction, string header)
        {
            return interaction.GetHeaders().Where(h => h.Item1 == header).Select(h => h.Item2);
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
