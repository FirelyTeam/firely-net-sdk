/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest
{
    internal class Requester : IDisposable
    {
        public Uri BaseUrl { get; private set; }
        public HttpClient Client { get; private set; }

        public bool UseFormatParameter { get; set; }
        public ResourceFormat PreferredFormat { get; set; }
        public int Timeout { get; set; }           // In milliseconds
        public Prefer Prefer { get; set; }

        /// <summary>
        /// This will do 2 things:
        /// 1. Add the header Accept-Encoding: gzip, deflate
        /// 2. decompress any responses that have Content-Encoding: gzip (or deflate)
        /// </summary>
        public bool PreferCompressedResponses { get; set; }

        /// <summary>
        /// Compress any Request bodies 
        /// (warning, if a server does not handle compressed requests you will get a 415 response)
        /// </summary>
        public bool CompressRequestBody { get; set; }

        public ParserSettings ParserSettings { get; set; }

        public Action<HttpRequestMessage> BeforeRequest { get; set; }
        public Action<HttpResponseMessage> AfterResponse { get; set; }

        public Requester(Uri baseUrl, HttpMessageHandler messageHandler) : this(baseUrl, new HttpClient(messageHandler))
        { }

        public Requester(Uri baseUrl, HttpClient httpClient)
        {
            BaseUrl = baseUrl;
            Client = httpClient;

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET FhirClient for FHIR " + Version);
            UseFormatParameter = false;
            PreferredFormat = ResourceFormat.Xml;
            Client.Timeout = new TimeSpan(0, 0, 100);       // Default timeout is 100 seconds            
            Prefer = Rest.Prefer.ReturnRepresentation;
            ParserSettings = ParserSettings.CreateDefault();
        }

        public Bundle.EntryComponent Execute(Bundle.EntryComponent interaction)
        {
            return ExecuteAsync(interaction).WaitResult();
        }

        public async Task<Bundle.EntryComponent> ExecuteAsync(Bundle.EntryComponent interaction)
        {
            if (interaction == null) throw Error.ArgumentNull(nameof(interaction));

            using (var requestMessage = interaction.ToHttpRequestMessage(BaseUrl, Prefer, PreferredFormat, UseFormatParameter, CompressRequestBody))
            {
                if (PreferCompressedResponses)
                {
                    requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                }

                BeforeRequest(requestMessage);

                using (var response = await Client.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    AfterResponse(response);
                    var body = await response.Content.ReadAsByteArrayAsync();

                    try
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return response.ToBundleEntry(body, ParserSettings, throwOnFormatException: true);
                        }
                        else
                        {
                            var bundle = response.ToBundleEntry(body, ParserSettings, throwOnFormatException: false);
                            throw buildFhirOperationException(response.StatusCode, bundle.Resource);
                        }
                    }
                    catch (UnsupportedBodyTypeException bte)
                    {
                        var operationOutcome = OperationOutcome.ForException(bte, OperationOutcome.IssueType.Invalid);
                        throw buildFhirOperationException(response.StatusCode, operationOutcome);
                    }
                }
            }
        }

        protected string Version => ModelInfo.Version;

        protected Bundle.EntryComponent CreateBundleEntry(HttpResponseMessage response, byte[] body, bool throwOnFormatException)
        {
            return response.ToBundleEntry( body, ParserSettings, throwOnFormatException);
        }

        private static Exception buildFhirOperationException(HttpStatusCode status, Resource body)
        {
            string message;

            if (status.IsInformational())
                message = $"Operation resulted in an informational response ({status})";
            else if (status.IsRedirection())
                message = $"Operation resulted in a redirection response ({status})";
            else if (status.IsClientError())
                message = $"Operation was unsuccessful because of a client error ({status})";
            else
                message = $"Operation was unsuccessful, and returned status {status}";

            if (body is OperationOutcome outcome)
                return new FhirOperationException($"{message}. OperationOutcome: {outcome.ToString()}.", status, outcome);
            else if (body != null)
                return new FhirOperationException($"{message}. Body contains a {body.TypeName}.", status);
            else
                return new FhirOperationException($"{message}. Body has no content.", status);
        }

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Client.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
