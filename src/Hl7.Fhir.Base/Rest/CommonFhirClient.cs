/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
* available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
*/

using Hl7.Fhir.Introspection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Hl7.Fhir.Rest
{
    public class CommonFhirClient : BaseFhirClient
    {
        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <remarks>
        /// If the messageHandler is provided then it must be disposed by the caller
        /// </remarks>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="inspector"></param>
        /// <param name="fhirVersion"></param>
        /// <param name="settings"></param>
        /// <param name="messageHandler"></param>
        public CommonFhirClient(Uri endpoint, ModelInspector inspector, string fhirVersion, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null)
            : base(endpoint, inspector, fhirVersion, settings)
        {
            // If user does not supply message handler, create our own and add decompression strategy in default handler.
            var handler = messageHandler ?? new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            HttpClientRequester requester = new(Endpoint, Settings, handler, messageHandler == null);
            Requester = requester;

            // Expose default request headers to user.
            RequestHeaders = requester.Client.DefaultRequestHeaders;
        }

        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <remarks>
        /// The httpClient must be disposed by the caller
        /// </remarks>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="settings"></param>
        /// <param name="httpClient"></param>
        /// <param name="inspector"></param>
        /// <param name="fhirVersion"></param>
        public CommonFhirClient(Uri endpoint, HttpClient httpClient, ModelInspector inspector, string fhirVersion, FhirClientSettings settings = null)
            : base(endpoint, inspector, fhirVersion, settings)
        {
            HttpClientRequester requester = new(Endpoint, Settings, httpClient);
            Requester = requester;

            // Expose default request headers to user.
            RequestHeaders = requester.Client.DefaultRequestHeaders;
        }

        /// <summary>
        /// Default request headers that can be modified to persist default headers to internal client.
        /// </summary>
        public HttpRequestHeaders RequestHeaders { get; protected set; }

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

