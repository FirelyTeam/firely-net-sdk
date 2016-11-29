﻿/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;


namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// The supported formats for Fhir Resources
    /// </summary>
    public enum ResourceFormat
    {
        Xml = 1,
        Json = 2,
        Unknown = 3
    }

    public static class ContentType
    {
        public const string JSON_CONTENT_HEADER = "application/json+fhir";  // The formal FHIR mime type (still to be registered).
        public static readonly string[] JSON_CONTENT_HEADERS = new string[]
            { JSON_CONTENT_HEADER,
                "application/fhir+json", "application/json", "text/json"};

        public const string XML_CONTENT_HEADER = "application/xml+fhir";   // The formal FHIR mime type (still to be registered).
        public static readonly string[] XML_CONTENT_HEADERS = new string[]
            { XML_CONTENT_HEADER, "text/xml", "application/xml",
                "application/fhir+xml", "text/xml+fhir" };

        public const string FORMAT_PARAM_XML = "xml";
        public const string FORMAT_PARAM_JSON = "json";


        /// <summary>
        /// Converts a format string to a ResourceFormat
        /// </summary>
        /// <param name="format">A format string, as used by the _format Url parameter</param>
        /// <returns>The Resource format or the special value Unknow if the format was unrecognized</returns>
        public static ResourceFormat GetResourceFormatFromFormatParam(string format)
        {
            if (String.IsNullOrEmpty(format)) return ResourceFormat.Unknown;

            var f = format.ToLowerInvariant().Replace(" ", "+"); // spaces on the are decoded from the +, so convert them back

            if (f == FORMAT_PARAM_JSON || JSON_CONTENT_HEADERS.Contains(f))
                return ResourceFormat.Json;
            else if (f == FORMAT_PARAM_XML || XML_CONTENT_HEADERS.Contains(f))
                return ResourceFormat.Xml;
            else
                return ResourceFormat.Unknown;
        }


        /// <summary>
        /// Converts a content type to a ResourceFormat
        /// </summary>
        /// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
        /// <returns></returns>
        public static ResourceFormat GetResourceFormatFromContentType(string contentType)
        {
            if (String.IsNullOrEmpty(contentType)) return ResourceFormat.Unknown;
#if NETCore
            System.Net.Http.Headers.MediaTypeHeaderValue headerValue;
            System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out headerValue);
            var f = headerValue.MediaType.ToLowerInvariant();

#else
            var f = new System.Net.Mime.ContentType(contentType).MediaType.ToLowerInvariant();
#endif

            if (JSON_CONTENT_HEADERS.Contains(f))
                return ResourceFormat.Json;
            else if (XML_CONTENT_HEADERS.Contains(f))
                return ResourceFormat.Xml;
            else
                return ResourceFormat.Unknown;
        }


        public static string BuildContentType(ResourceFormat format, bool forBundle)
        {
            string contentType;

            if (format == ResourceFormat.Json)
                contentType = JSON_CONTENT_HEADER;
            else if (format == ResourceFormat.Xml)
                contentType = XML_CONTENT_HEADER;
            else
                throw new ArgumentException("Cannot determine content type for data format " + format);

            return contentType + ";charset=" + Encoding.UTF8.WebName;
        }


        public static string BuildFormatParam(ResourceFormat format)
        {
            if (format == ResourceFormat.Json)
                return FORMAT_PARAM_JSON;
            else if (format == ResourceFormat.Xml)
                return FORMAT_PARAM_XML;
            else
                throw new ArgumentException("Cannot determine content type for data format " + format);
        }

        /// <summary>
        /// Checks whether a given content type is valid as a content type for resource data
        /// </summary>
        /// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
        /// <returns></returns>
        public static bool IsValidResourceContentType(string contentType)
        {

#if NETCore
            System.Net.Http.Headers.MediaTypeHeaderValue headerValue;
            System.Net.Http.Headers.MediaTypeHeaderValue.TryParse(contentType, out headerValue);
            var f = headerValue.MediaType.ToLowerInvariant();
#else
               var f = new System.Net.Mime.ContentType(contentType).MediaType.ToLowerInvariant();
#endif


            return JSON_CONTENT_HEADERS.Contains(f) || XML_CONTENT_HEADERS.Contains(f);
        }


        /// <summary>
        /// Checks whether a given content type is valid as a content type for bundles
        /// </summary>
        /// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
        /// <returns></returns>
        public static bool IsValidBundleContentType(string contentType)
        {
            var f = contentType.ToLowerInvariant();

            return (JSON_CONTENT_HEADERS.Contains(f));
        }


        /// <summary>
        /// Checks whether a given format parameter is a valid as a content type for resource data
        /// </summary>
        /// <param name="paramValue">The content type, as it appears on the URL parameter</param>
        /// <returns></returns>
        public static bool IsValidFormatParam(string paramValue)
        {
            return GetResourceFormatFromFormatParam(paramValue) != ResourceFormat.Unknown;
        }

    }


}
