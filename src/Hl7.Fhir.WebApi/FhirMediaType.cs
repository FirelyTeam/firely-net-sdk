/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Hl7.Fhir.WebApi
{ 
    public static class FhirMediaType
    {
        // TODO: This class can be merged into HL7.Fhir.ContentType

        public const string XmlResource = "application/fhir+xml";
        public const string JsonResource = "application/fhir+json";
        public const string BinaryResource = "application/fhir+binary";

        public static ICollection<string> StrictFormats 
        {
            get 
            {
                return new List<string>() { XmlResource, JsonResource };
            }
        }
        public static string[] LooseXmlFormats = { "xml", "text/xml", "application/xml" };
        public static readonly string[] LooseJsonFormats = { "json", "application/json" };

        public static string Interpret(string format)
        {
            if (format == null) return XmlResource;
            if (StrictFormats.Contains(format)) return format;
            else if (LooseXmlFormats.Contains(format)) return XmlResource;
            else if (LooseJsonFormats.Contains(format)) return JsonResource;
            //else throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            else return format;
        }
        public static ResourceFormat GetResourceFormat(string format)
        {
            string strict = Interpret(format);
            if (strict == XmlResource) 
                return ResourceFormat.Xml;
            else if (strict == JsonResource)
                return ResourceFormat.Json;

            // Default to XML format
            else return ResourceFormat.Xml;

        }

        public static string GetContentType(Type type, ResourceFormat format) 
        {
            if (typeof(Resource).IsAssignableFrom(type))
            {
                switch (format)
                {
                    case ResourceFormat.Json: return JsonResource;
                    case ResourceFormat.Xml: return XmlResource;
                    default: return XmlResource;
                }
            }
            else 
                return "application/octet-stream";
        }

        public static MediaTypeHeaderValue GetMediaTypeHeaderValue(Type type, ResourceFormat format)
        {
            string mediatype = FhirMediaType.GetContentType(type, format);
            MediaTypeHeaderValue header = new MediaTypeHeaderValue(mediatype);
            header.CharSet = Encoding.UTF8.WebName;
            return header;
        }
    }
}