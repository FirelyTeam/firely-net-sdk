/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Hl7.Fhir.Rest.Http
{
    public partial class FhirHttpClient : BaseFhirClient, IFhirClient
    {
        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="settings">
        /// <param name="messageHandler"></param>
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirHttpClient(Uri endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null) : base(endpoint, settings)
        {
            // If user does not supply message handler, add decompression strategy in default handler.
            var handler = messageHandler ?? new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var requester = new HttpClientRequester(Endpoint, Settings, handler);
            Requester = requester;

            // Expose default request headers to user.
            RequestHeaders = requester.Client.DefaultRequestHeaders;
        }


        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="settings">
        /// <param name="messageHandler"></param>
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirHttpClient(string endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null)
            : this(new Uri(endpoint), settings, messageHandler)
        {
        }

        /// <summary>
        /// Default request headers that can be modified to persist default headers to internal client.
        /// </summary>
        public HttpRequestHeaders RequestHeaders { get; protected set; }

        /// <summary>
        /// Returns the HttpRequestMessage as it was last constructed to execute a call on the FhirClient
        /// </summary>
        public HttpRequestMessage LastRequestMessage { get { return (Requester as HttpClientRequester)?.LastRequest; } }

        /// <summary>
        /// Returns the HttpResponseMessage as it was last received during a call on the FhirClient
        /// </summary>
        /// <remarks>Note that the FhirClient will have read the body data from the HttpResponseMessage, so this is
        /// no longer available. Use LastBody, LastBodyAsText and LastBodyAsResource to get access to the received body (if any)</remarks>
        public HttpResponseMessage LastResponseMessage { get { return (Requester as HttpClientRequester)?.LastResponse; } }


        /// <summary>
        /// Override dispose in order to clean up request headers tied to disposed requester.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.RequestHeaders = null;
                    base.Dispose(disposing);
                }

                disposedValue = true;
            }
        }
    }
}
