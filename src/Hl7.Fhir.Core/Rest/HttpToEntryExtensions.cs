/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
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

        internal static Bundle.EntryComponent ToBundleEntry(this HttpWebResponse response, byte[] body)
        {
            var result = new Bundle.EntryComponent();

            result.Response = new Bundle.ResponseComponent();
            result.Response.Status = ((int)response.StatusCode).ToString();
            result.Response.SetHeaders(response.Headers);

            var contentType = getContentType(response);
            var charEncoding = getCharacterEncoding(response);

            result.Response.Location = response.Headers[HttpUtil.LOCATION] ?? response.Headers[HttpUtil.CONTENTLOCATION];

#if PORTABLE45
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
                    var resource = parseResource(bodyText, contentType);
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
#if PORTABLE45
				return System.Net.Http.Headers.MediaTypeHeaderValue.Parse(response.ContentType).MediaType;
#else
                return new System.Net.Mime.ContentType(response.ContentType).MediaType;
#endif
            }
            else
                return null;
        }

        private static Encoding getCharacterEncoding(HttpWebResponse response)
        {
            Encoding result = null;

            if (!String.IsNullOrEmpty(response.ContentType))
            {
#if PORTABLE45
				var charset = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(response.ContentType).CharSet;
#else
                var charset = new System.Net.Mime.ContentType(response.ContentType).CharSet;
#endif

                if (!String.IsNullOrEmpty(charset))
                    result = Encoding.GetEncoding(charset);
            }
            return result;
        }      

        private static Resource parseResource(string bodyText, string contentType)
        {           
            Resource result= null;

            var fhirType = ContentType.GetResourceFormatFromContentType(contentType);

            if (fhirType == ResourceFormat.Unknown)
                throw new FormatException("Endpoint returned a body with contentType '{0}', while a valid FHIR xml/json body type was expected. Is this a FHIR endpoint? Body reads: {1}".FormatWith(contentType, bodyText));

            if (!FhirParser.ProbeIsJson(bodyText) && !FhirParser.ProbeIsXml(bodyText))
                throw new FormatException("Endpoint said it returned '{0}', but the body is not recognized as either xml or json: \"" + bodyText + "\"");

            if (fhirType == ResourceFormat.Json)
                result = (Resource)FhirParser.ParseFromJson(bodyText);
            else
                result = (Resource)FhirParser.ParseFromXml(bodyText);

            return result;
        }


        internal static bool IsBinaryResponse(string responseUri, string contentType)
        {
            if (!string.IsNullOrEmpty(contentType) && (contentType.ToLower() == "application/xml+fhir" || contentType.ToLower() == "application/json+fhir"))
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

            return (new StreamReader(new MemoryStream(body), enc, true)).ReadToEnd();
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

        public static byte[] GetBody(this Bundle.ResponseComponent interaction)
        {
            if (interaction.UserData.ContainsKey(USERDATA_BODY))
                return (byte[])interaction.UserData[USERDATA_BODY];
            else
                return null;
        }

        internal static void SetBody(this Bundle.ResponseComponent interaction, byte[] data)
        {
            interaction.UserData[USERDATA_BODY] = data;
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
}
