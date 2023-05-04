/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
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
using System.Net;

namespace Hl7.Fhir.Rest
{
    internal static class HttpContentBuilders
    {
        public static HttpRequestMessage WithBinaryContent(this HttpRequestMessage message, Binary b)
        {
            message.Content = CreateContentFromBinary(b);
            return message;
        }

        public static HttpContent CreateContentFromBinary(Binary b)
        {
            var content = new ByteArrayContent(b.Data ?? b.Content);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(b.ContentType);

            if (b.SecurityContext?.Reference is { } secRef)
                content.Headers.Add(HttpUtil.SECURITYCONTEXT, secRef);

            return content;
        }

        public static HttpRequestMessage WithFormUrlEncodedParameters(this HttpRequestMessage message, Parameters parameters)
        {
            message.Content = CreateContentFromParams(parameters);
            return message;
        }

        public static HttpContent CreateContentFromParams(Parameters pars)
        {
            var bodyParameters = pars.Parameter
                .Where(p => p.Name is not null && p.Value is not null)
                .Select(p => new KeyValuePair<string, string>(p.Name, p.Value.ToString()!))
                .ToList();

            var content = new FormUrlEncodedContent(bodyParameters);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType.FORM_URL_ENCODED);

            return content;
        }

        public static HttpContent CreateContentFromJson(string json, DateTimeOffset? lastUpdated, string? mimeTypeFhirVersion)
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Json, mimeTypeFhirVersion);
            content.Headers.LastModified = lastUpdated;
            return content;
        }

        public static HttpContent CreateContentFromXml(string xml, DateTimeOffset? lastUpdated, string? mimeTypeFhirVersion)
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(xml));
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Xml, mimeTypeFhirVersion);
            content.Headers.LastModified = lastUpdated;
            return content;
        }

        public static HttpRequestMessage WithNoBody(this HttpRequestMessage message)
        {
            message.Content = null;
            return message;
        }
        public static HttpRequestMessage WithResourceContent(this HttpRequestMessage message, Resource resource, ResourceFormat serialization, IFhirSerializationEngine ser, string? mimeTypeFhirVersion)
        {
            message.Content = CreateContentFromResource(resource, serialization, ser, mimeTypeFhirVersion);
            return message;
        }

        public static HttpRequestMessage WithRequestCompression(this HttpRequestMessage message, DecompressionMethods method)
        {
            if (method is not DecompressionMethods.None && message.Content is not null)
                message.Content = new CompressedContent(message.Content, method);

            return message;
        }

        public static HttpContent CreateContentFromResource(Resource resource, ResourceFormat serialization, IFhirSerializationEngine ser, string? mimeTypeFhirVersion)
        {
            var lastUpdated = resource.Meta?.LastUpdated;

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

        public static HttpRequestMessage WithAgent(this HttpRequestMessage message, string product, string version)
        {
            message.Headers.UserAgent.Add(new ProductInfoHeaderValue(product, version));
            return message;
        }

        private const string FIRELY_SDK_CLIENT_AGENT = "firely-sdk-client";
        private static readonly string PRODUCT_VERSION = ReflectionHelper.GetProductVersion(typeof(BaseFhirClient).Assembly);

        public static HttpRequestMessage WithDefaultAgent(this HttpRequestMessage message)
        {
            // Never knew there was an official format for the UserAgent, which we have always ignored.
            // Ever since we have used the newer .NET HttpRequestMessage, we've been sending incorrect headers instead...
            // Corrected since 2023-03-03 (5.1?)
            return message.WithAgent(FIRELY_SDK_CLIENT_AGENT, PRODUCT_VERSION);
        }

        public static HttpRequestMessage WithAccept(this HttpRequestMessage message,
            ResourceFormat serialization,
            string? contentTypeFhirVersion,
            bool requestCompressedResponse)
        {
            message.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(
                    ContentType.BuildContentType(serialization, contentTypeFhirVersion)));

            if (requestCompressedResponse)
            {
                message.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(ContentType.DecompressionMethodHeaderValue(DecompressionMethods.GZip)));
                message.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(ContentType.DecompressionMethodHeaderValue(DecompressionMethods.Deflate)));
            }

            return message;
        }


        public static HttpRequestMessage WithFormatParameter(this HttpRequestMessage message,
            ResourceFormat serialization)
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

            message.RequestUri = baseUri.Uri;
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

        public static HttpRequestMessage WithReturnPreference(this HttpRequestMessage message, ReturnPreference? preference)
        {
            if (preference is not null) message.Headers.Add("Prefer", $"return={preference.GetLiteral()}");
            return message;
        }

        public static HttpRequestMessage WithSearchParamHandling(this HttpRequestMessage message, SearchParameterHandling? handling)
        {
            if (handling is not null) message.Headers.Add("Prefer", $"handling={handling.GetLiteral()}");
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