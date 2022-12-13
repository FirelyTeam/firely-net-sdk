/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class HttpToEntryExtensions
    {


        private const string USERDATA_BODY = "$body";
        private const string EXTENSION_RESPONSE_HEADER = "http://hl7.org/fhir/StructureDefinition/http-response-header";

        internal static EntryResponse ToEntryResponse(this HttpResponseMessage response, byte[] body)
        {
            var result = new EntryResponse
            {
                Status = ((int)response.StatusCode).ToString(),
                ResponseUri = response.RequestMessage.RequestUri,//this is actually the requestUri, can't find the responseUri
                Body = body,
                Location = response.Headers.Location?.OriginalString ?? response.Content.Headers.ContentLocation?.OriginalString,
                LastModified = response.Content.Headers.LastModified,
                Etag = response.Headers.ETag?.Tag.Trim('\"'),
                ContentType = response.Content.Headers.ContentType?.MediaType
            };
            result.SetHeaders(response.Headers);


            return result;
        }

        internal static void SetHeaders(this EntryResponse interaction, HttpResponseHeaders headers)
        {
            foreach (var header in headers)
            {
                //TODO: check multiple values for a header??
                interaction.Headers.Add(header.Key, header.Value.ToList().FirstOrDefault());
            }
        }

        public static bool IsSuccessful(this EntryResponse response)
        {
            int.TryParse(response.Status, out int code);
            return code >= 200 && code < 300;
        }

        public static string GetBodyAsText(this EntryResponse interaction)
        {
            var body = interaction.Body;

            if (body != null)
                return HttpUtil.DecodeBody(body, Encoding.UTF8);
            else
                return null;
        }

        internal static EntryResponse ToEntryResponse(this HttpWebResponse response, byte[] body)
        {
            var result = new EntryResponse
            {
                Status = ((int)response.StatusCode).ToString()
            };

            foreach (var key in response.Headers.AllKeys)
            {
                result.Headers.Add(key, response.Headers[key]);
            }
            result.ResponseUri = response.ResponseUri;
            result.Location = response.Headers[HttpUtil.LOCATION] ?? response.Headers[HttpUtil.CONTENTLOCATION];
            result.LastModified = response.LastModified;
            result.Etag = getETag(response);
            result.ContentType = getContentType(response);
            result.Body = body;

            return result;
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


    }
}
