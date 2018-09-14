/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Rest
{
	/*
	 * Brian 16 Dec 2014:
	 *		Removed the Category in the HTML Header as we don't do this anymore for DSTU2
	 *		Implement everything using the native .net async patterns
	 */
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

            byte[] byteBuffer = new byte[bufferSize];
            MemoryStream buffer = new MemoryStream();

            int readLen = s.Read(byteBuffer, 0, byteBuffer.Length);

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

        public static Uri MakeRelativeFromBase(Uri location, Uri baseUrl)
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

            var endp = location.ToString();
            var bUrl = baseUrl.ToString();
            if (endp.StartsWith(bUrl))
            {
                return new Uri(endp.Substring(bUrl.Length).TrimStart('/'), UriKind.Relative);
            }

            return location;
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
            var meLength = meSegments.Length;

            if (meSegments.Length < otherSegments.Length) 
                return false;
            for (int index = 0; index < otherLength; index++)
            {
                if (otherSegments[index].TrimEnd('/') != meSegments[index].TrimEnd('/')) 
                    return false;
            }

            return true;
        }


        static readonly string RESTURI_PATTERN;

        // Static constructor is called at most one time, before any 
        // instance constructor is invoked or member is accessed. 
        static HttpUtil()
        {
            RESTURI_PATTERN = @"((http | https)://([A-Za-z0-9\\\/\.\:\%\$])*)?(";
            RESTURI_PATTERN += String.Join("|", ModelInfo.SupportedResources);
            RESTURI_PATTERN += @")\/[A-Za-z0-9\-\.]{1,64}(\/_history\/[A-Za-z0-9\-\.]{1,64})?";
        }


        public static bool IsRestResourceIdentity(this Uri uri)
        {
            return IsRestResourceIdentity(uri.OriginalString);
        }

        public static bool IsRestResourceIdentity(string uri)
        {
            if (uri == null) return false;

            if (uri.Contains("$"))
                return false; // This is an operation, so not a resource identity

            return Regex.IsMatch(uri, RESTURI_PATTERN);
        }

        public static bool IsInformational(this HttpStatusCode code)
        {
            return (int)code >= 100 && (int)code < 200;
        }
        public static bool IsSuccessful(this HttpStatusCode code)
        {
            return (int)code >= 200 && (int)code < 300;
        }

        public static bool IsRedirection(this HttpStatusCode code)
        {
            return (int)code >= 300 && (int)code < 400;
        }

        public static bool IsClientError(this HttpStatusCode code)
        {
            return (int)code >= 400 && (int)code < 500;
        }

        public static bool IsServerError(this HttpStatusCode code)
        {
            return (int)code >= 500 && (int)code < 600;
        }
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


    public enum Prefer
    {
        /// <summary>
        /// Prefer to receive the full resource in the body after completion of the interaction
        /// </summary>
        [EnumLiteral("representation")]
        ReturnRepresentation,

        /// <summary>
        /// Prefer to not a receive a body after completion of the interaction
        /// </summary>
        [EnumLiteral("minimal")]
        ReturnMinimal,

        /// <summary>
        /// Prefer to receive an OperationOutcome resource containing hints and warnings about the 
        /// operation rather than the full resource
        /// </summary>
        [EnumLiteral("OperationOutcome")]
        OperationOutcome
    }
}
