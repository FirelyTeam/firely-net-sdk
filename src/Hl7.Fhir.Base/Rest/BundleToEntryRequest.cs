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
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Rest
{
    internal static class BundleToEntryRequest
    {
        public static async Task<EntryRequest> ToEntryRequestAsync(this Bundle.EntryComponent entry, FhirClientSettings settings, IFhirSerializationEngine ser, string fhirRelease)
        {
            var result = new EntryRequest
            {
                FhirRelease = fhirRelease,
                Method = bundleHttpVerbToRestHttpVerb(entry.Request.Method, entry.Annotation<InteractionType>()),
                Type = entry.Annotation<InteractionType>(),
                Url = entry.Request.Url,
                Headers = new EntryRequestHeaders
                {
                    IfMatch = entry.Request.IfMatch,
                    IfModifiedSince = entry.Request.IfModifiedSince,
                    IfNoneExist = entry.Request.IfNoneExist,
                    IfNoneMatch = entry.Request.IfNoneMatch
                }
            };

            if (!settings.UseFormatParameter)
            {
                result.Headers.Accept = ContentType.BuildContentType(settings, fhirRelease);
            }

            if (entry.Resource != null)
            {
                bool searchUsingPost =
                    result.Method == HTTPVerb.POST
                    && entry.Annotation<InteractionType>() == InteractionType.Search
                    && entry.Resource is Parameters;
                await setBodyAndContentTypeAsync(result, entry.Resource, settings, searchUsingPost, ser, fhirRelease).ConfigureAwait(false);
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

        private static async Task setBodyAndContentTypeAsync(EntryRequest request, Resource data, FhirClientSettings settings, 
            bool searchUsingPost, IFhirSerializationEngine ser, string fhirVersion)
        {
            if (data == null) throw Error.ArgumentNull(nameof(data));

            if (data is Binary bin)
            {
                //Binary.Content is available for STU3. This has changed for R4 as it is Binary.Data
                request.RequestBodyContent = bin.Data ?? bin.Content;
                request.ContentType = bin.ContentType;
            }
            else if (searchUsingPost)
            {
                var bodyParameters = new List<KeyValuePair<string, string>>();
                foreach (Parameters.ParameterComponent parameter in ((Parameters)data).Parameter)
                {
                    bodyParameters.Add(new KeyValuePair<string, string>(parameter.Name, parameter.Value.ToString()));
                }
                if (bodyParameters.Count > 0)
                {
                    var content = new FormUrlEncodedContent(bodyParameters);
                    request.RequestBodyContent = await content.ReadAsByteArrayAsync().ConfigureAwait(false);
                }
                else
                {
                    request.RequestBodyContent = null;
                }

                request.ContentType = "application/x-www-form-urlencoded";
            }
            else
            {
                var serialized = settings.PreferredFormat == ResourceFormat.Xml
                    ? ser.SerializeToXml(data)
                    : ser.SerializeToJson(data);

                request.RequestBodyContent = Encoding.UTF8.GetBytes(serialized);

                // This is done by the caller after the OnBeforeRequest is called so that other properties
                // can be set before the content is committed
                // request.WriteBody(CompressRequestBody, body);
                request.ContentType = ContentType.BuildContentType(settings, fhirVersion);
            }
        }
    }
}
