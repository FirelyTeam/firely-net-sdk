/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest.Http
{
   internal class Requester : IRequester, IDisposable
    {
        public Uri BaseUrl { get; private set; }
        public HttpClient Client { get; private set; }

        public bool UseFormatParameter { get; set; }
        public ResourceFormat PreferredFormat { get; set; }
        public int Timeout { get; set; }           // In milliseconds

        public Prefer? PreferredReturn { get; set; }
        public SearchParameterHandling? PreferredParameterHandling { get; set; }

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

        public Requester(Uri baseUrl, HttpMessageHandler messageHandler)
        {
            BaseUrl = baseUrl;
            Client = new HttpClient(messageHandler);

            Client.DefaultRequestHeaders.Add("User-Agent", ".NET FhirClient for FHIR " + Model.ModelInfo.Version);
            UseFormatParameter = false;
            PreferredFormat = ResourceFormat.Xml;
            Client.Timeout = new TimeSpan(0, 0, 100);       // Default timeout is 100 seconds            
            PreferredReturn = Rest.Prefer.ReturnRepresentation;
            PreferredParameterHandling = null;
            ParserSettings = Hl7.Fhir.Serialization.ParserSettings.Default;
        }


        public Bundle.EntryComponent LastResult { get; private set; }
        public HttpStatusCode? LastStatusCode => LastResponse?.StatusCode;
        public HttpResponseMessage LastResponse { get; private set; }
        public HttpRequestMessage LastRequest { get; private set; }

        public Bundle.EntryComponent Execute(Bundle.EntryComponent interaction)
        {
            return ExecuteAsync(interaction).WaitResult();
        }

        public async Task<Bundle.EntryComponent> ExecuteAsync(Bundle.EntryComponent interaction)
        {
            if (interaction == null) throw Error.ArgumentNull(nameof(interaction));
            bool compressRequestBody = false;

            compressRequestBody = CompressRequestBody; // PCL doesn't support compression at the moment

            using (var requestMessage = interaction.ToHttpRequestMessage(this.PreferredParameterHandling, this.PreferredReturn, PreferredFormat, UseFormatParameter, compressRequestBody))
            {
                if (PreferCompressedResponses)
                {
                    requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                }

                LastRequest = requestMessage;

                byte[] outgoingBody = null;
                if (requestMessage.Method == HttpMethod.Post || requestMessage.Method == HttpMethod.Put)
                {
                    outgoingBody = await requestMessage.Content.ReadAsByteArrayAsync();
                }

                using (var response = await Client.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    try
                    {
                        var body = await response.Content.ReadAsByteArrayAsync();

                        LastResponse = response;

                        // Do this call after AfterResponse, so AfterResponse will be called, even if exceptions are thrown by ToBundleEntry()
                        try
                        {
                            LastResult = null;

                            if (response.IsSuccessStatusCode)
                            {
                                LastResult = response.ToBundleEntry(body, ParserSettings, throwOnFormatException: true);
                                return LastResult;
                            }
                            else
                            {
                                LastResult = response.ToBundleEntry(body, ParserSettings, throwOnFormatException: false);
                                throw buildFhirOperationException(response.StatusCode, LastResult.Resource);
                            }
                        }
                        catch (UnsupportedBodyTypeException bte)
                        {
                            // The server responded with HTML code. Still build a FhirOperationException and set a LastResult.
                            // Build a very minimal LastResult
                            var errorResult = new Bundle.EntryComponent();
                            errorResult.Response = new Bundle.ResponseComponent();
                            errorResult.Response.Status = ((int)response.StatusCode).ToString();

                            OperationOutcome operationOutcome = OperationOutcome.ForException(bte, OperationOutcome.IssueType.Invalid);

                            errorResult.Resource = operationOutcome;
                            LastResult = errorResult;

                            throw buildFhirOperationException(response.StatusCode, operationOutcome);
                        }
                    }
                    catch (AggregateException ae)
                    {
                        //EK: This code looks weird. Is this correct?
                        if (ae.GetBaseException() is WebException)
                        {
                        }
                        throw ae.GetBaseException();
                    }
                }
            }
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

      #region IDisposable Support
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
      #endregion
   }
}
