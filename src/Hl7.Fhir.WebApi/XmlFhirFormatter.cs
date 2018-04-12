/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Text;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.WebApi
{
    public class XmlFhirFormatter : FhirMediaTypeFormatter
    {
        public XmlFhirFormatter() : base()
        {
            foreach (var mediaType in ContentType.XML_CONTENT_HEADERS)
                SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = FhirMediaType.GetMediaTypeHeaderValue(type, ResourceFormat.Xml);
            //  headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "fhir.resource.xml" };
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            try
            {
                var body = base.ReadBodyFromStream(readStream, content);

                if (typeof(Resource).IsAssignableFrom(type))
                {
                    if (!string.IsNullOrEmpty(body))
                    {
                        Resource resource = new FhirXmlParser().Parse<Resource>(body);
                        return System.Threading.Tasks.Task.FromResult<object>(resource);
                    }
                    return System.Threading.Tasks.Task.FromResult<object>(null);
                }
                else
                    throw new NotSupportedException(String.Format("Cannot read unsupported type {0} from body", type.Name));
            }
            catch (FormatException exc)
            {
                throw new FhirServerException(HttpStatusCode.BadRequest, "Body parsing failed: " + exc.Message);
            }
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            XmlWriter writer = new XmlTextWriter(writeStream, Encoding.UTF8);
            if (type == typeof(OperationOutcome))
            {
                Resource resource = (Resource)value;
                FhirSerializer.SerializeResource(resource, writer);
            }
            else if (typeof(Resource).IsAssignableFrom(type))
            {
                if (value != null)
                {
                    Resource r = value as Resource;
                    SummaryType st = SummaryType.False;
                    if (r.HasAnnotation<SummaryType>())
                        st = r.Annotation<SummaryType>();
                    FhirSerializer.SerializeResource(r, writer, st);
                }
            }

            writer.Flush();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
