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
    internal class HttpClientRequester : IRequester, IDisposable
    {
        public FhirClientSettings Settings { get; set; }
        public Uri BaseUrl { get; private set; }
        public HttpClient Client { get; private set; }

        public HttpClientRequester(Uri baseUrl, FhirClientSettings settings, HttpMessageHandler messageHandler)
        {
            Settings = settings;
            BaseUrl = baseUrl;

            Client = new HttpClient(messageHandler);
            Client.DefaultRequestHeaders.Add("User-Agent", $".NET FhirClient for FHIR {Model.ModelInfo.Version}");
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

            compressRequestBody = Settings.CompressRequestBody; // PCL doesn't support compression at the moment

            using (var requestMessage = interaction.ToHttpRequestMessage(Settings.PreferredParameterHandling,
                            Settings.PreferredReturn, Settings.PreferredFormat, Settings.UseFormatParameter, compressRequestBody))
            {
                Client.Timeout = new TimeSpan(0, 0, 0, Settings.Timeout);

                if (Settings.PreferCompressedResponses)
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
                                LastResult = response.ToBundleEntry(body, Settings.ParserSettings, throwOnFormatException: true);
                                return LastResult;
                            }
                            else
                            {
                                LastResult = response.ToBundleEntry(body, Settings.ParserSettings, throwOnFormatException: false);
                                throw HttpUtil.BuildFhirOperationException(response.StatusCode, LastResult.Resource);
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

                            throw HttpUtil.BuildFhirOperationException(response.StatusCode, operationOutcome);
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
