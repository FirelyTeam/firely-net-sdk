﻿/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;


namespace Hl7.Fhir.Client
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
        public const string JSON_CONTENT_HEADER3 = "application/fhir json";  // The formal FHIR mime type (still to be registered).
        public const string JSON_CONTENT_HEADER = "application/fhir+json";  // The formal FHIR mime type (still to be registered).
        public const string JSON_CONTENT_HEADER2 = "application/json";      // Also accepted, but never sent
        
        public const string XML_CONTENT_HEADER = "application/fhir+xml";           // The formal FHIR mime type (still to be registered).
        public const string XML_CONTENT_HEADER4 = "application/fhir xml";           // The formal FHIR mime type (still to be registered).
        public const string XML_CONTENT_HEADER2 = "text/xml";               // Also accepted, but never sent
        public const string XML_CONTENT_HEADER3 = "application/xml";        // Also accepted, but never sent
        
        public const string ATOM_CONTENT_HEADER = "application/atom+xml";

        public const string FORMAT_PARAM_XML = "xml";
        public const string FORMAT_PARAM_JSON = "json";

        

        /// <summary>
        /// Converts a format string to a ResourceFormat
        /// </summary>
        /// <param name="format">A format string, as used by the _format Url parameter</param>
        /// <returns>The Resource format or the special value Unknow if the format was unrecognized</returns>
        public static ResourceFormat GetResourceFormatFromFormatParam(string format)
        {
            switch (format.ToLowerInvariant())
            {
                case FORMAT_PARAM_JSON:
                case JSON_CONTENT_HEADER:
                case JSON_CONTENT_HEADER2:
                case JSON_CONTENT_HEADER3:
                    return ResourceFormat.Json;
                case FORMAT_PARAM_XML:
                case XML_CONTENT_HEADER:
                case XML_CONTENT_HEADER2:
                case XML_CONTENT_HEADER3:
                case XML_CONTENT_HEADER4:
                    return ResourceFormat.Xml;
                default:
                    return ResourceFormat.Unknown;
            }
        }



        /// <summary>
        /// Converts a content type to a ResourceFormat
        /// </summary>
        /// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
        /// <returns></returns>
        public static ResourceFormat GetResourceFormatFromContentType(string contentType)
        {
            if (String.IsNullOrEmpty(contentType))
                return ResourceFormat.Unknown;

            switch (contentType.ToLowerInvariant())
            {
                case JSON_CONTENT_HEADER:
                case JSON_CONTENT_HEADER2:
                    return ResourceFormat.Json;
                case XML_CONTENT_HEADER:
                case XML_CONTENT_HEADER2:
                case XML_CONTENT_HEADER3:                
                    return ResourceFormat.Xml;
                case ATOM_CONTENT_HEADER:
                    return ResourceFormat.Xml;
                default:
                    return ResourceFormat.Unknown;
            }
        }


        public static string BuildContentType(ResourceFormat format, bool forBundle)
        {
            string contentType;

            if (format == ResourceFormat.Json)
                contentType = JSON_CONTENT_HEADER;
            else if (format == ResourceFormat.Xml && forBundle)
                contentType = ATOM_CONTENT_HEADER;
            else if (format == ResourceFormat.Xml && !forBundle)
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
            return (contentType == JSON_CONTENT_HEADER ||
                contentType == JSON_CONTENT_HEADER2 ||
                contentType == XML_CONTENT_HEADER2 ||
                contentType == XML_CONTENT_HEADER3 ||
                contentType == XML_CONTENT_HEADER);
        }


        /// <summary>
        /// Checks whether a given content type is valid as a content type for bundles
        /// </summary>
        /// <param name="contentType">The content type, as it appears on e.g. a Http Content-Type header</param>
        /// <returns></returns>
        public static bool IsValidBundleContentType(string contentType)
        {
            return (contentType == JSON_CONTENT_HEADER || contentType == JSON_CONTENT_HEADER2 ||
                contentType == ATOM_CONTENT_HEADER);
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
