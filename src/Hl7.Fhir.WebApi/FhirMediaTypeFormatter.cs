/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.IO;
using System.Net;

namespace Hl7.Fhir.WebApi
{
    public abstract class FhirMediaTypeFormatter : MediaTypeFormatter
    {
        public FhirMediaTypeFormatter() : base()
        {
            this.SupportedEncodings.Clear();
            this.SupportedEncodings.Add(Encoding.UTF8);
        }

        protected Resource entry = null;

        private void setEntryHeaders(HttpContentHeaders headers)
        {
            if (entry != null)
            {
                if (entry.Meta != null && entry.Meta.LastUpdated.HasValue)
                    headers.LastModified = entry.Meta.LastUpdated.Value.UtcDateTime;
                else
                    headers.LastModified = DateTimeOffset.UtcNow;
                if (!string.IsNullOrEmpty(entry.Id))
                    headers.ContentLocation = new Uri(entry.ResourceIdentity(entry.ResourceBase).OriginalString);

                if (entry is Binary)
                {
                    headers.ContentType = new MediaTypeHeaderValue( ((Binary)entry).ContentType);
                }
            }
        }

        public override bool CanReadType(Type type)
        {
            if (typeof(Resource).IsAssignableFrom(type))
                return true;
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (typeof(Resource).IsAssignableFrom(type) || type == typeof(OperationOutcome))
                return true;
            return false;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            setEntryHeaders(headers);
        }

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            this.entry = request.GetEntry();
            return base.GetPerRequestFormatterInstance(type, request, mediaType);
        }

        protected string ReadBodyFromStream(Stream readStream, HttpContent content)
        {
            var charset = content.Headers.ContentType.CharSet ?? Encoding.UTF8.HeaderName;
            var encoding = Encoding.GetEncoding(charset);

            if (encoding != Encoding.UTF8)
                throw new FhirServerException(HttpStatusCode.BadRequest, "FHIR supports UTF-8 encoding exclusively, not " + encoding.WebName);

            StreamReader sr = new StreamReader(readStream, Encoding.UTF8, true);
            return sr.ReadToEnd();
        }
    }
}