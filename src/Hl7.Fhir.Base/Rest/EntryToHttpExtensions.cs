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
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Hl7.Fhir.Rest
{

    internal static class EntryToHttpExtensions
    {
        public static Uri GetRequestUrl(this Bundle.EntryComponent entry, Uri baseUrl)
        {
            // Create an absolute uri when the interaction.Url is relative.
            var uri = new Uri(
                entry.Request.Url ?? throw new ArgumentException("EntryComponent should specify a Request.Url.", nameof(entry)),
                UriKind.RelativeOrAbsolute);

            if (!uri.IsAbsoluteUri)
            {
                uri = HttpUtil.MakeAbsoluteToBase(uri, baseUrl);
            }

            return uri;
        }

        public static HttpRequestMessage ToHttpRequestMessage(
            this Bundle.EntryComponent entry, 
            Uri baseUrl, 
            ResourceFormat serialization, 
            IFhirSerializationEngine ser, 
            string? fhirVersion, 
            bool useFormatParameter,
            Prefer? preferredReturn,
            SearchParameterHandling? searchParameterHandling)
        {
            var interaction = entry.Annotation<InteractionType>();
            var method = entry.Request.Method?.ToHttpMethod(interaction) 
                        ?? throw new ArgumentException("EntryComponent should specify a Request.Method.", nameof(entry));

            var uri = entry.GetRequestUrl(baseUrl);
            var request = new HttpRequestMessage(method, uri);

            request = setBody(request)
                .WithDefaultAgent()
                .WithPreconditions(entry.Request.IfMatch, entry.Request.IfNoneMatch, entry.Request.IfModifiedSince, entry.Request.IfNoneExist);

            if (!useFormatParameter)
                request.WithAccept(serialization, fhirVersion);
            else
                request.WithFormatParameter(serialization, fhirVersion);

            bool canHaveReturnPreference = interaction is InteractionType.Create or InteractionType.Update or InteractionType.Patch or InteractionType.Transaction;

            if (canHaveReturnPreference && preferredReturn is not null && preferredReturn != Prefer.RespondAsync)
                request.WithReturnPreference(preferredReturn.Value);

            if (interaction == InteractionType.Search && searchParameterHandling is not null)
                request.WithSearchParamHandling(searchParameterHandling.Value);

            if (preferredReturn == Prefer.RespondAsync)
                request.WithPreferAsync();

            return request;

            HttpRequestMessage setBody(HttpRequestMessage message)
            {
                bool isSearchUsingPost = method == HttpMethod.Post && interaction == InteractionType.Search;

                return entry.Resource switch
                {
                    Binary bin => message.WithBinaryContent(bin),
                    Parameters pars when isSearchUsingPost => message.WithFormUrlEncodedParameters(pars),
                    Resource resource => message.WithResourceContent(resource, serialization, ser, fhirVersion),
                    null => message
                };
            }
        }
    }  
}

#nullable restore