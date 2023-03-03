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
    internal static class HttpContentFactory
    {
        public static HttpContent CreateContentFromBinary(Binary b)
        {
            var content = new ByteArrayContent(b.Data ?? b.Content);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(b.ContentType);
            return content;
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

        public static HttpContent CreateContentFromJson(string json, DateTimeOffset? lastUpdated, string? fhirVersion)
        { 
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(json));
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Json,fhirVersion);
            content.Headers.LastModified = lastUpdated;
            return content;
        }

        public static HttpContent CreateContentFromXml(string xml, DateTimeOffset? lastUpdated, string? fhirVersion)
        {
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(xml));
            content.Headers.ContentType = ContentType.BuildMediaType(ResourceFormat.Xml, fhirVersion);
            content.Headers.LastModified = lastUpdated;
            return content;
        }

        public static HttpContent CreateFromResource(Resource resource, ResourceFormat serialization, IFhirSerializationEngine ser, string? fhirVersion)
        {
            var lastUpdated = resource.Meta.LastUpdated;
            
            switch(serialization)
            {
                case ResourceFormat.Json:
                    var json = ser.SerializeToJson(resource); 
                    return CreateContentFromJson(json, lastUpdated, fhirVersion);
                case ResourceFormat.Xml:
                    var xml = ser.SerializeToXml(resource);
                    return CreateContentFromXml(xml, lastUpdated, fhirVersion);
                default:
                    throw new ArgumentException($"Unsupported resource serialization {serialization}.", nameof(serialization));
            }
        }    
    }
}

#nullable restore