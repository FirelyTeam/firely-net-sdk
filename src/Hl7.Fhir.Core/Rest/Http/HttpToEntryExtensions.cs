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
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Hl7.Fhir.Rest.Http
{
    internal static class HttpToEntryExtensions
    {
        private const string USERDATA_BODY = "$body";
        private const string EXTENSION_RESPONSE_HEADER = "http://hl7.org/fhir/StructureDefinition/http-response-header";

        internal static Bundle.EntryComponent ToBundleEntry(this HttpResponseMessage response, byte[] body, ParserSettings parserSettings, bool throwOnFormatException)
        {
            var result = new Bundle.EntryComponent();

            result.Response = new Bundle.ResponseComponent();
            result.Response.Status = ((int)response.StatusCode).ToString();
            result.Response.SetHeaders(response.Headers);

            var contentType = response.Content.Headers.ContentType;

            Encoding charEncoding;

            try
            {
                charEncoding = Encoding.GetEncoding(response.Content.Headers.ContentType.CharSet);
            }
            catch (ArgumentException e)
            {
                charEncoding = Encoding.UTF8;
            }

            result.Response.Location = response.Headers.Location?.AbsoluteUri ?? response.Content.Headers.ContentLocation?.AbsoluteUri;

            result.Response.LastModified = response.Content.Headers.LastModified;
            result.Response.Etag = response.Headers.ETag?.Tag;

            if (body != null && body.Length != 0)
            {
                result.Response.SetBody(body);

                if (Rest.HttpToEntryExtensions.IsBinaryResponse(result.Response.Location, contentType.ToString()))
                {
                    result.Resource = Rest.HttpToEntryExtensions.MakeBinaryResource(body, contentType.ToString());
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
                    var bodyText = Rest.HttpToEntryExtensions.DecodeBody(body, charEncoding);
                    var resource = Rest.HttpToEntryExtensions.ParseResource(bodyText, contentType.ToString(), parserSettings, throwOnFormatException);
                    result.Resource = resource;

                    if (result.Response.Location != null)
                        result.Resource.ResourceBase = new ResourceIdentity(result.Response.Location).BaseUri;
                }
            }

            return result;
        }

        internal static void SetHeaders(this Bundle.ResponseComponent interaction, HttpResponseHeaders headers)
        {
            foreach (var header in headers)
            {
                interaction.AddExtension(EXTENSION_RESPONSE_HEADER, new FhirString(header.Key + ":" + header.Value));
            }
        }
    }
}
