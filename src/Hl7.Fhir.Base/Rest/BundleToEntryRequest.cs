#nullable enable

/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Rest
{
    internal static class BundleToEntryRequest
    {
        public static async Task<EntryRequest> ToEntryRequestAsync(this Bundle.EntryComponent entry, FhirClientSettings settings, 
            IFhirSerializationEngine ser, string fhirVersion)
        {
            var method = bundleHttpVerbToRestHttpVerb(entry.Request.Method, entry.Annotation<InteractionType>());

            byte[]? body = null;
            string? contentType = null;

            if (entry.Resource != null)
            {
                bool searchUsingPost =
                    method == HTTPVerb.POST
                    && entry.Annotation<InteractionType>() == InteractionType.Search
                    && entry.Resource is Parameters;
                (body, contentType) = await getBodyAndContentTypeAsync(entry.Resource, settings, searchUsingPost, ser, fhirVersion).ConfigureAwait(false);
            }

            var result = new EntryRequest(
                Method: method,
                Url: entry.Request.Url,
                Type: entry.Annotation<InteractionType>())
            {
                FhirVersion = fhirVersion,                
                Headers = new EntryRequestHeaders
                {
                    IfMatch = entry.Request.IfMatch,
                    IfModifiedSince = entry.Request.IfModifiedSince,
                    IfNoneExist = entry.Request.IfNoneExist,
                    IfNoneMatch = entry.Request.IfNoneMatch
                },
                RequestBodyContent = body,
                ContentType = contentType
            };

            if (!settings.UseFormatParameter)
            {
                result.Headers.Accept = ContentType.BuildContentType(settings, fhirVersion);
            }

            return result;
        }

        private static HTTPVerb? bundleHttpVerbToRestHttpVerb(Bundle.HTTPVerb? bundleHttp, InteractionType type)
        {
            switch (bundleHttp)
            {
                case Bundle.HTTPVerb.POST:
                    {
                        return HTTPVerb.POST;
                    }
                case Bundle.HTTPVerb.GET:
                    {
                        return HTTPVerb.GET;
                    }
                case Bundle.HTTPVerb.DELETE:
                    {
                        return HTTPVerb.DELETE;
                    }
                case Bundle.HTTPVerb.PUT:
                    {
                        //No PATCH in Bundle.HttpVerb in STU3, so this is corrected here. 
                        return type == InteractionType.Patch ? HTTPVerb.PATCH : HTTPVerb.PUT;
                    }
                case Bundle.HTTPVerb.PATCH:
                    {
                        return HTTPVerb.PATCH;
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        private static async Task<(byte[]? body, string contentType)> getBodyAndContentTypeAsync(Resource data, FhirClientSettings settings, 
            bool searchUsingPost, IFhirSerializationEngine ser, string fhirVersion)
        {
            if (data == null) throw Error.ArgumentNull(nameof(data));

            if (data is Binary bin)
            {
                //Binary.Content is available for STU3. This has changed for R4 as it is Binary.Data
                return (bin.Data ?? bin.Content, bin.ContentType);
            }
            else if (data is Parameters pars && searchUsingPost)
            {                              
                if (pars.Parameter.Any())
                {
                    var bodyParameters = pars.Parameter
                        .Where(p => p.Name is not null && p.Value is not null)
                        .Select(p => new KeyValuePair<string, string>(p.Name, p.Value.ToString()!))
                        .ToList();

                    var content = new FormUrlEncodedContent(bodyParameters);
                    return (await content.ReadAsByteArrayAsync().ConfigureAwait(false), ContentType.FORM_URL_ENCODED);
                }
                else
                {
                    return (null, ContentType.FORM_URL_ENCODED);
                }
            }
            else
            {
                var serialized = settings.PreferredFormat == ResourceFormat.Xml
                    ? ser.SerializeToXml(data)
                    : ser.SerializeToJson(data);

                return (Encoding.UTF8.GetBytes(serialized), ContentType.BuildContentType(settings, fhirVersion));
            }
        }
    }
}

#nullable restore