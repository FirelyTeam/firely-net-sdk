/*
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
*
* This file is licensed under the BSD 3-Clause license
* available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
*/

using Hl7.Fhir.Model;
using System;
using System.Net.Http;


namespace Hl7.Fhir.Rest
{
    public class FhirClient : BaseFhirClient
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
        /// <param name="settings"></param>
        /// <param name="messageHandler"></param>
        public FhirClient(Uri endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null) :
            base(endpoint, messageHandler, ModelInfo.ModelInspector, settings)
        {
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
        public FhirClient(Uri endpoint, HttpClient httpClient, FhirClientSettings settings = null)
            : base(endpoint, httpClient, ModelInfo.ModelInspector, settings)
        {
        }

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
        /// <param name="settings"></param>
        /// <param name="messageHandler"></param>
        public FhirClient(string endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null)
            : this(new Uri(endpoint), settings, messageHandler)
        {
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
        public FhirClient(string endpoint, HttpClient httpClient, FhirClientSettings settings = null)
            : this(new Uri(endpoint), httpClient, settings)
        {
        }
    }
}
