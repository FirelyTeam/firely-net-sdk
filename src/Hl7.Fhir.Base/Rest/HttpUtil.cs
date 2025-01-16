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
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Rest
{
    public static class HttpUtil
    {
        #region << HTTP Headers >>

        /// <summary>
        /// "Content-Location" found in the HTTP Headers
        /// </summary>
        public const string CONTENTLOCATION = "Content-Location";

        /// <summary>
        /// "Location" found in the HTTP Headers
        /// </summary>
        public const string LOCATION = "Location";

        /// <summary>
        /// "Last-Modified" found in the HTTP Headers
        /// </summary>
        public const string LASTMODIFIED = "Last-Modified";

        /// <summary>
        /// "ETag" found in the HTTP Headers
        /// </summary>
        public const string ETAG = "ETag";

        /// <summary>
        /// The header used to communicate the Binary security context
        /// </summary>
        public const string SECURITYCONTEXT = "X-Security-Context";

        #endregion

        /// <summary>
        /// "_format" found as a parameter on the REST URL
        /// </summary>
        public const string RESTPARAM_FORMAT = "_format";

        /// <summary>
        /// "_since" found as a parameter on the REST History operation URL
        /// </summary>
        public const string HISTORY_PARAM_SINCE = "_since";

        /// <summary>
        /// "_count" found as a parameter on the REST History operation URL
        /// </summary>
        public const string HISTORY_PARAM_COUNT = SearchParams.SEARCH_PARAM_COUNT;

        public static byte[] ReadAllFromStream(Stream s)
        {
            int bufferSize = 4096;

            var byteBuffer = new byte[bufferSize];
            var buffer = new MemoryStream();

            var readLen = s.Read(byteBuffer, 0, byteBuffer.Length);

            while (readLen > 0)
            {
                buffer.Write(byteBuffer, 0, readLen);
                readLen = s.Read(byteBuffer, 0, byteBuffer.Length);
            }

            return buffer.ToArray();
        }

        public static Uri MakeAbsoluteToBase(Uri location, Uri baseUrl)
        {
            // If called without a location, just return the base endpoint
            if (location == null) return baseUrl;

            // If the location is absolute, verify whether it is within the endpoint
            if (location.IsAbsoluteUri)
            {
                if (!new RestUrl(baseUrl).IsEndpointFor(location))
                    throw Error.Argument(nameof(location), "Url is not located within the given base endpoint");
            }
            else
            {
                // Else, make location absolute within the endpoint
                //location = new Uri(Endpoint, location);
                var endp = baseUrl.OriginalString;
                if (!endp.EndsWith("/")) endp += "/";
                endp += location;
                location = new Uri(endp);
            }

            return location;
        }

        public static Uri? MakeRelativeFromBase(Uri? location, Uri baseUrl)
        {
            if (location == null) return null;

            if (!location.IsAbsoluteUri)
            {
                throw Error.Argument(nameof(location), "Url is not an absolute uri");
            }

            if (!baseUrl.IsAbsoluteUri)
            {
                throw Error.Argument(nameof(baseUrl), "Url is not an absolute uri");
            }

            var endp = location.AbsoluteUri;
            var bUrl = baseUrl.AbsoluteUri;

            return endp.StartsWith(bUrl) ? new Uri(endp.Substring(bUrl.Length).TrimStart('/'), UriKind.Relative) : location;
        }

        public static bool IsWithin(this Uri me, Uri other)
        {
            if (!other.IsAbsoluteUri)
                return false;     // can never be within a relative path

            if (me.IsAbsoluteUri)
            {
                if (other.Authority.ToLower() != me.Authority.ToLower())
                    return false;
            }

            var meSegments = me.OriginalString.TrimEnd('/').ToLower().Split('/');
            var otherSegments = other.OriginalString.TrimEnd('/').ToLower().Split('/');

            var otherLength = otherSegments.Length;

            if (meSegments.Length < otherSegments.Length)
                return false;
            for (int index = 0; index < otherLength; index++)
            {
                if (otherSegments[index].TrimEnd('/') != meSegments[index].TrimEnd('/'))
                    return false;
            }

            return true;
        }

        public static string? DecodeBody(byte[]? body, Encoding enc)
        {
            if (body == null) return null;
            enc ??= Encoding.UTF8;

            // [WMR 20160421] Explicit disposal
            // return (new StreamReader(new MemoryStream(body), enc, true)).ReadToEnd();
            using var stream = new MemoryStream(body);
            using var reader = new StreamReader(stream, enc, true);

            return reader.ReadToEnd();
        }

        private static readonly string RESTURI_PATTERN;

        // Static constructor is called at most one time, before any 
        // instance constructor is invoked or member is accessed. 
        static HttpUtil()
        {
            RESTURI_PATTERN = @"((http | https)://([A-Za-z0-9\\\/\.\:\%\$])*)?(";
            RESTURI_PATTERN += @"[A-Z][A-Za-z ]*";
            RESTURI_PATTERN += @")\/[A-Za-z0-9\-\.]{1,64}(\/_history\/[A-Za-z0-9\-\.]{1,64})?";
        }


        public static bool IsRestResourceIdentity(this Uri uri) => IsRestResourceIdentity(uri.OriginalString);

        public static bool IsRestResourceIdentity(string uri)
        {
            if (uri == null) return false;

            if (uri.Contains("$"))
                return false; // This is an operation, so not a resource identity

            return Regex.IsMatch(uri, RESTURI_PATTERN);
        }

        public static bool IsBinaryEndpoint(string uri)
        {
            if (ResourceIdentity.IsRestResourceIdentity(uri))
            {
                var id = new ResourceIdentity(uri);

                if (id.ResourceType != FhirTypeNames.BINARY_NAME) return false;

                if (id.Id != null && Id.IsValidValue(id.Id)) return true;
                if (id.VersionId != null && Id.IsValidValue(id.VersionId)) return true;
            }

            return false;
        }

        public static bool IsBinaryEndpoint(Uri uri) => IsBinaryEndpoint(uri.OriginalString);

        public static bool IsInformational(this HttpStatusCode code) => (int)code >= 100 && (int)code < 200;
        public static bool IsSuccessful(this HttpStatusCode code) => (int)code >= 200 && (int)code < 300;

        public static bool IsRedirection(this HttpStatusCode code) => (int)code >= 300 && (int)code < 400;

        public static bool IsClientError(this HttpStatusCode code) => (int)code >= 400 && (int)code < 500;

        public static bool IsServerError(this HttpStatusCode code) => (int)code >= 500 && (int)code < 600;
    }


    public enum SearchParameterHandling
    {
        /// <summary>
        /// Server should return an error for any unknown or unsupported parameter        
        /// </summary>
        [EnumLiteral("strict")]
        Strict,

        /// <summary>
        /// Server should ignore any unknown or unsupported parameter
        /// </summary>
        [EnumLiteral("lenient")]
        Lenient
    }

    public enum ReturnPreference
    {
        /// <summary>
        /// Prefer to receive the full resource in the body after completion of the interaction
        /// </summary>
        [EnumLiteral("representation")]
        Representation,

        /// <summary>
        /// Prefer to not a receive a body after completion of the interaction
        /// </summary>
        [EnumLiteral("minimal")]
        Minimal,

        /// <summary>
        /// Prefer to receive an OperationOutcome resource containing hints and warnings about the 
        /// operation rather than the full resource
        /// </summary>
        [EnumLiteral("OperationOutcome")]
        OperationOutcome
    }

    public enum HTTPVerb
    {
        [EnumLiteral("GET", "http://hl7.org/fhir/http-verb"), Description("GET")]
        GET,
        [EnumLiteral("HEAD", "http://hl7.org/fhir/http-verb"), Description("HEAD")]
        HEAD,
        [EnumLiteral("POST", "http://hl7.org/fhir/http-verb"), Description("POST")]
        POST,
        [EnumLiteral("PUT", "http://hl7.org/fhir/http-verb"), Description("PUT")]
        PUT,
        [EnumLiteral("DELETE", "http://hl7.org/fhir/http-verb"), Description("DELETE")]
        DELETE,
        [EnumLiteral("PATCH", "http://hl7.org/fhir/http-verb"), Description("PATCH")]
        PATCH,
    }
}

#nullable restore