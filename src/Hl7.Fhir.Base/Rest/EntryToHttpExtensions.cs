/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Net.Http;

namespace Hl7.Fhir.Rest
{
    public static class EntryToHttpExtensions
    {     
        public static HttpRequestMessage ToHttpRequestMessage(
            this Bundle.RequestComponent entry,
            Uri baseUrl,
            Resource resource,
            InteractionType? interaction,
            ResourceFormat serialization,
            IFhirSerializationEngine ser,
            string? fhirVersion,
            bool useFormatParameter,
            ReturnPreference? returnPreference,
            bool useAsync,
            SearchParameterHandling? searchParameterHandling,
            bool requestCompressedResponse)
        {
            var method = entry.Method?.toHttpMethod(interaction)
                        ?? throw new ArgumentException("EntryComponent should specify a Request.Method.", nameof(entry));

            var uri = getRequestUrl(entry.Url, baseUrl);
            var request = new HttpRequestMessage(method, uri);

            request = setBody(request)
                .WithDefaultAgent()
                .WithPreconditions(entry.IfMatch, entry.IfNoneMatch, entry.IfModifiedSince, entry.IfNoneExist);

            if (!useFormatParameter)
                request = request.WithAccept(serialization, fhirVersion, requestCompressedResponse);
            else
                request = request.WithFormatParameter(serialization);

            bool canHaveReturnPreference = interaction is InteractionType.Create or InteractionType.Update or InteractionType.Patch or InteractionType.Transaction;

            if (canHaveReturnPreference)
                request = request.WithReturnPreference(returnPreference);

            if (interaction == InteractionType.Search)
                request = request.WithSearchParamHandling(searchParameterHandling);

            if (useAsync)
                request = request.WithPreferAsync();

            return request;

            HttpRequestMessage setBody(HttpRequestMessage message)
            {
                bool isSearchUsingPost = method == HttpMethod.Post && interaction == InteractionType.Search;

                return resource switch
                {
                    Binary bin => message.WithBinaryContent(bin),
                    Parameters pars when isSearchUsingPost => message.WithFormUrlEncodedParameters(pars),
                    Resource resource => message.WithResourceContent(resource, serialization, ser, fhirVersion),
                    null => message
                };
            }
        }

        private static Uri getRequestUrl(string requestUrl, Uri baseUrl)
        {
            // Create an absolute uri when the interaction.Url is relative.
            var uri = new Uri(
                requestUrl ?? throw new ArgumentException("EntryComponent should specify a Request.Url.", nameof(requestUrl)),
                UriKind.RelativeOrAbsolute);

            if (!uri.IsAbsoluteUri)
            {
                uri = HttpUtil.MakeAbsoluteToBase(uri, baseUrl);
            }

            return uri;
        }


#if NETSTANDARD
        private readonly static HttpMethod HTTP_PATCH = new("PATCH");
#else
        private readonly static HttpMethod HTTP_PATCH = HttpMethod.Patch;
#endif

        /// <summary>
        /// Converts the <see cref="Bundle.HTTPVerb" /> (e.g. from a <see cref="Bundle.RequestComponent.Method"/>) to a <see cref="HttpMethod"/>. />
        /// </summary>
        /// <param name="bundleVerb">The FHIR HTTPVerb.</param>
        /// <param name="interaction">The kind of FHIR interaction, if known.</param>
        /// <exception cref="ArgumentException">The given HTTPVerb cannot be translated to a .NET HttpMethod.</exception>
        private static HttpMethod toHttpMethod(this Bundle.HTTPVerb bundleVerb, InteractionType? interaction = default)
        {
            return bundleVerb switch
            {
                Bundle.HTTPVerb.POST => HttpMethod.Post,
                Bundle.HTTPVerb.GET => HttpMethod.Get,
                Bundle.HTTPVerb.DELETE => HttpMethod.Delete,

                //No PATCH in Bundle.HttpVerb in STU3, so this is corrected here. 
                Bundle.HTTPVerb.PUT when interaction == InteractionType.Patch => HTTP_PATCH,
                Bundle.HTTPVerb.PUT => HttpMethod.Put,
                Bundle.HTTPVerb.PATCH => HTTP_PATCH,
                Bundle.HTTPVerb.HEAD => HttpMethod.Head,

                _ => throw new ArgumentException($"There is no known mapping from HTTPVerb {bundleVerb} to a HttpMethod.", nameof(bundleVerb))
            };
        }

    }
}

#nullable restore