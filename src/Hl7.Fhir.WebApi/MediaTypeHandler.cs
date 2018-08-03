/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Rest;

namespace Hl7.Fhir.WebApi
{
    public class MediaTypeHandler : DelegatingHandler
    {
        private bool isBinaryRequest(HttpRequestMessage request)
        {
            var format = FhirMediaType.Interpret(request.Content != null 
                && request.Content.Headers != null 
                && request.Content.Headers.ContentType != null
                ? request.Content.Headers.ContentType.MediaType : "Unknown");
            if (format != FhirMediaType.JsonResource && format != FhirMediaType.XmlResource)
            {
                if (format == FhirMediaType.BinaryResource)
                    return true;
                var ub = new UriBuilder(request.RequestUri);
                return ub.Path.Contains("/Binary"); // todo: replace quick hack by solid solution.
            }
            return false;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // ballot: binary upload should be determined by the Content-Type header, instead of the Rest url?
            if (isBinaryRequest(request) && (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put))
            {
                if (request.Content.Headers.ContentType != null)
                {
                    var format = request.Content.Headers.ContentType.MediaType;
                    request.Content.Headers.Replace("X-Content-Type", format);
                }

                request.Content.Headers.ContentType = new MediaTypeHeaderValue(FhirMediaType.BinaryResource);
                // request.Headers.Replace("Accept", FhirMediaType.BinaryResource); // HACK
                // TODO: HACK. passes to BinaryFhirFormatter
            }
            else
            {
                // The requested response format can be overridden by the url parameter 'format'
                // Can only be json/xml (or equivalent MIME types) otherwise, ignore.
                string formatParam = request.GetParameter("_format");
                if (!string.IsNullOrEmpty(formatParam))
                {
                    var accepted = ContentType.GetResourceFormatFromFormatParam(formatParam);
                    if (accepted != ResourceFormat.Unknown)
                    {
                        request.Headers.Accept.Clear();

                        if (accepted == ResourceFormat.Json)
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON_CONTENT_HEADER));
                        else
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.XML_CONTENT_HEADER));
                    }
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }

        
    }

    
    // Instead of using the general purpose DelegatingHandler, could we use IContentNegotiator?
    public class FhirContentNegotiator : IContentNegotiator
    {
        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            throw new NotImplementedException();
        }
    }

}
