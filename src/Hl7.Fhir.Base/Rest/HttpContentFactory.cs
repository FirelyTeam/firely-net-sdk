/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using Hl7.Fhir.Serialization;
using System;
using System.Text.Unicode;
using System.Text;

namespace Hl7.Fhir.Rest
{
    internal static class HttpContentBuilders
    {
        public static HttpRequestMessage WithBinaryContent(this HttpRequestMessage message, Binary b)
        {
            message.Content = CreateContentFromBinary(b);
            return message;
        }

        internal static HttpContent CreateContentFromBinary(Binary b)
        {
            var content = new ByteArrayContent(b.Data ?? b.Content);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(b.ContentType);
            return content;
        }

        public static HttpRequestMessage WithFormUrlEncodedParameters(this HttpRequestMessage message, Parameters parameters)
        {
            message.Content = CreateContentFromParams(parameters);
            return message;
        }

        internal static HttpContent CreateContentFromParams(Parameters pars)
        {
            var bodyParameters = pars.Parameter
                .Where(p => p.Name is not null && p.Value is not null)
                .Select(p => new KeyValuePair<string, string>(p.Name, p.Value.ToString()!))
                .ToList();

            var content = new FormUrlEncodedContent(bodyParameters);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType.FORM_URL_ENCODED);

            return content;
        }

        internal static HttpContent CreateContentFromJson(string json, DateTimeOffset? lastUpdated, string? mimeTypeFhirVersion)
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Json, mimeTypeFhirVersion);
            content.Headers.LastModified = lastUpdated;
            return content;
        }

        internal static HttpContent CreateContentFromXml(string xml, DateTimeOffset? lastUpdated, string? mimeTypeFhirVersion)
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(xml));
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Xml, mimeTypeFhirVersion);
            content.Headers.LastModified = lastUpdated;
            return content;
        }

        public static HttpRequestMessage WithResourceContent(this HttpRequestMessage message, Resource resource, ResourceFormat serialization, IFhirSerializationEngine ser, string? mimeTypeFhirVersion)
        {
            message.Content = CreateContentFromResource(resource, serialization, ser, mimeTypeFhirVersion);
            return message;
        }


        internal static HttpContent CreateContentFromResource(Resource resource, ResourceFormat serialization, IFhirSerializationEngine ser, string? mimeTypeFhirVersion)
        {
            var lastUpdated = resource.Meta.LastUpdated;

            switch (serialization)
            {
                case ResourceFormat.Json:
                    var json = ser.SerializeToJson(resource);
                    return CreateContentFromJson(json, lastUpdated, mimeTypeFhirVersion);
                case ResourceFormat.Xml:
                    var xml = ser.SerializeToXml(resource);
                    return CreateContentFromXml(xml, lastUpdated, mimeTypeFhirVersion);
                default:
                    throw new ArgumentException($"Unsupported resource serialization {serialization}.", nameof(serialization));
            }
        }

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

        public static HttpRequestMessage WithAgent(this HttpRequestMessage message, string product, string version)
        {
            message.Headers.UserAgent.Add(new ProductInfoHeaderValue(product, version));
            return message;
        }

        internal const string FIRELY_SDK_CLIENT_AGENT = "firely-sdk-client";

        public static HttpRequestMessage WithDefaultAgent(this HttpRequestMessage message)
        {
            // Never knew there was an official format for the UserAgent, which we have always ignored.
            // Ever since we have used the newer .NET HttpRequestMessage, we've been sending incorrect headers instead...
            // Corrected since 2023-03-03 (5.1?)
            var productVersion = ReflectionHelper.GetProductVersion(typeof(BaseFhirClient).Assembly);
            return message.WithAgent(FIRELY_SDK_CLIENT_AGENT, productVersion);
        }


        public static HttpRequestMessage WithAccept(this HttpRequestMessage message,
            ResourceFormat serialization,
            string? contentTypeFhirVersion)
        {
            message.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(
                    ContentType.BuildContentType(serialization, contentTypeFhirVersion)));
            return message;
        }


        public static HttpRequestMessage WithFormatParameter(this HttpRequestMessage message,
            ResourceFormat serialization,
            string? contentTypeFhirVersion)
        {
            if (message.RequestUri is null)
                throw new ArgumentException("The request message should have its RequestUri set to add the format parameter.");

            var queryToAppend = $"{HttpUtil.RESTPARAM_FORMAT}={ContentType.BuildFormatParam(serialization)}";
            var baseUri = new UriBuilder(message.RequestUri);

            if (baseUri.Query.Length > 1)
                // Note: In .NET Core and .NET 5+, you can simplify by removing
                // the call to Substring(), which removes the leading "?" character.
                baseUri.Query = baseUri.Query.Substring(1) + "&" + queryToAppend;
            else
                baseUri.Query = queryToAppend;

            return message;
        }

        public static HttpRequestMessage WithPreconditions(
            this HttpRequestMessage message,
            string? ifMatch,
            string? ifNoneMatch,
            DateTimeOffset? ifModifiedSince,
            string? ifNoneExist)
        {
            if (ifMatch is not null) message.Headers.IfMatch.Add(EntityTagHeaderValue.Parse(ifMatch));
            if (ifNoneMatch is not null) message.Headers.IfNoneMatch.Add(EntityTagHeaderValue.Parse(ifNoneMatch));
            message.Headers.IfModifiedSince = ifModifiedSince?.UtcDateTime;

            // Add the HL7 defined extension header If-None-Exist
            if (ifNoneExist is not null) message.Headers.Add("If-None-Exist", ifNoneExist);

            return message;
        }

        public static HttpRequestMessage WithReturnPreference(this HttpRequestMessage message, Prefer preference)
        {
            if (preference == Prefer.RespondAsync)
                throw new ArgumentException($"Async is not a return preference, call {nameof(WithPreferAsync)} instead.");

            message.Headers.Add("Prefer", $"return={preference.GetLiteral()}");
            return message;
        }

        public static HttpRequestMessage WithSearchParamHandling(this HttpRequestMessage message, SearchParameterHandling handling)
        {
            message.Headers.Add("Prefer", $"handling={handling.GetLiteral()}");
            return message;
        }

        public static HttpRequestMessage WithPreferAsync(this HttpRequestMessage message)
        {
            message.Headers.Add("Prefer", "respond-async");
            return message;
        }
    }
}

#nullable restore