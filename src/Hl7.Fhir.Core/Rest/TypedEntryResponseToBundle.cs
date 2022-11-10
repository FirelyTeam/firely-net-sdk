/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    public static class TypedEntryResponseToBundle
    {
        private const string EXTENSION_RESPONSE_HEADER = "http://hl7.org/fhir/StructureDefinition/http-response-header";

        public static Bundle.EntryComponent ToBundleEntry(this TypedEntryResponse entry, ModelInspector inspector, ParserSettings parserSettings)
        {
            var result = new Bundle.EntryComponent
            {
                Response = new Bundle.ResponseComponent
                {
                    Status = entry.Status,
                    LastModified = entry.LastModified,
                    Etag = entry.Etag,
                    Location = entry.Location
                }
            };

            result.Response.SetHeaders(entry.Headers);

            if (entry.Body != null)
            {
                result.Response.SetBody(entry.Body);

                if (IsBinaryResponse(entry.ResponseUri.OriginalString, entry.ContentType))
                {
                    result.Resource = MakeBinaryResource(entry.Body, entry.ContentType);
                    if (result.Response.Location != null)
                    {
                        var ri = new ResourceIdentity(result.Response.Location);
                        result.Resource.Id = ri.Id;
                        result.Resource.Meta = new Meta
                        {
                            VersionId = ri.VersionId
                        };
                        result.Resource.ResourceBase = ri.BaseUri;
                    }
                }
                else
                {
                    if (entry.TypedElement != null)
                    {
                        try
                        {
                            result.Resource = new BaseFhirParser(inspector, parserSettings).Parse<Resource>(entry.TypedElement);

                            //if the response is an operation outcome, add it to response.outcome. This is necessary for when a client uses return=OperationOutcome as a prefer header.
                            // see also issue #1681
                            if (result.Resource is OperationOutcome)
                            {
                                result.Response.Outcome = result.Resource;
                            }

                            if (result.Response.Location != null)
                                result.Resource.ResourceBase = new ResourceIdentity(result.Response.Location).BaseUri;
                        }
                        catch (AggregateException ae)
                        {
                            throw ae.GetBaseException();
                        }
                    }
                }
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

                if (id.ResourceType != FhirTypeNames.BINARY_NAME) return false;

                if (id.Id != null && Id.IsValidValue(id.Id)) return true;
                if (id.VersionId != null && Id.IsValidValue(id.VersionId)) return true;
            }

            return false;
        }

        internal static Binary MakeBinaryResource(byte[] data, string contentType) =>
            new()
            {
                //Content is for STU3, from R4 Content has changed into Data
                Data = data,
                ContentType = contentType
            };

        private class Body
        {
            public byte[] Data;
        }


        public static byte[] GetBody(this Bundle.ResponseComponent interaction) => interaction.Annotation<Body>()?.Data;

        public static string GetBodyAsText(this Bundle.ResponseComponent interaction)
        {
            var body = interaction.GetBody();
            return body != null ? HttpUtil.DecodeBody(body, Encoding.UTF8) : null;
        }

        internal static void SetBody(this Bundle.ResponseComponent interaction, byte[] data)
        {
            interaction.RemoveAnnotations<Body>();
            interaction.AddAnnotation(new Body { Data = data });
        }

        internal static void SetHeaders(this Bundle.ResponseComponent interaction, IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                interaction.AddExtension(EXTENSION_RESPONSE_HEADER, new FhirString(header.Key + ":" + header.Value));
            }
        }
    }
}
