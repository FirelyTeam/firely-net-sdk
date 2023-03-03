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
#if NETSTANDARD
        public readonly static HttpMethod HTTP_PATCH = new("PATCH");
#else
        public readonly static HttpMethod HTTP_PATCH = HttpMethod.Patch;
#endif

        /// <summary>
        /// Converts the <see cref="Bundle.HTTPVerb" /> (e.g. from a <see cref="Bundle.RequestComponent.Method"/>) to a <see cref="HttpMethod"/>. />
        /// </summary>
        /// <param name="bundleVerb">The FHIR HTTPVerb.</param>
        /// <param name="interaction">The kind of FHIR interaction, if known.</param>
        /// <exception cref="ArgumentException">The given HTTPVerb cannot be translated to a .NET HttpMethod.</exception>
        public static HttpMethod ToHttpMethod(this Bundle.HTTPVerb bundleVerb, InteractionType? interaction = default)
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
            SearchParameterHandling? searchParameterHandling
            )
        {
            var interaction = entry.Annotation<InteractionType>();
            var method = entry.Request.Method?.ToHttpMethod(interaction) 
                        ?? throw new ArgumentException("EntryComponent should specify a Request.Method.", nameof(entry));

            var uri = entry.GetRequestUrl(baseUrl);
            var location = new RestUrl(uri);

            if (useFormatParameter)
                location.AddParam(HttpUtil.RESTPARAM_FORMAT, ContentType.BuildFormatParam(serialization));

            var request = new HttpRequestMessage(method, location.Uri);

            bool isSearchUsingPost = method == HttpMethod.Post && interaction == InteractionType.Search;

            var body = entry.Resource switch
            {
                Binary bin => HttpContentFactory.CreateContentFromBinary(bin),
                Parameters pars when isSearchUsingPost => HttpContentFactory.CreateContentFromParams(pars),
                Resource resource => HttpContentFactory.CreateFromResource(resource, serialization, ser, fhirVersion),
                null => null
            };

            if (body is not null)
                request.Content = body;

            // Never knew there was an official format for the UserAgent, which we have always ignored.
            // Ever since we have used the newer .NET HttpRequestMessage, we've been sending incorrect headers instead...
            // Corrected since 2023-03-03 (5.1?)
            var productVersion = ReflectionHelper.GetProductVersion(typeof(BaseFhirClient).Assembly);
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("firely-sdk-client", productVersion));

            if (!useFormatParameter)
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(ContentType.BuildContentType(serialization, fhirVersion)));

            if (entry.Request.IfMatch is not null) request.Headers.IfMatch.Add(EntityTagHeaderValue.Parse(entry.Request.IfMatch));
            if (entry.Request.IfNoneMatch is not null) request.Headers.IfNoneMatch.Add(EntityTagHeaderValue.Parse(entry.Request.IfNoneMatch));
            request.Headers.IfModifiedSince = entry.Request.IfModifiedSince?.UtcDateTime;
            
            // Add the HL7 defined extension header If-None-Exist
            if (entry.Request.IfNoneExist != null) request.Headers.Add("If-None-Exist", entry.Request.IfNoneExist);

            bool canHaveReturnPreference = interaction is InteractionType.Create or InteractionType.Update or InteractionType.Patch or InteractionType.Transaction;

            if (canHaveReturnPreference && preferredReturn is not null && preferredReturn != Prefer.RespondAsync)
                request.Headers.Add("Prefer", $"return={preferredReturn.GetLiteral()}");
           
            if (interaction == InteractionType.Search && searchParameterHandling is not null)
                request.Headers.Add("Prefer", $"handling={searchParameterHandling.GetLiteral()}");

            if(preferredReturn == Prefer.RespondAsync)
                    request.Headers.Add("Prefer", preferredReturn.GetLiteral());

            return request;
        }
    }  
}

#nullable restore