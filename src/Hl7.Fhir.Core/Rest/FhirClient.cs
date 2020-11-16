/* 
* Copyright (c) 2014, Firely (info@fire.ly) and contributors
* See the file CONTRIBUTORS for details.
* 
* This file is licensed under the BSD 3-Clause license
* available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
*/

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Hl7.Fhir.Rest
{
    public partial class FhirClient : BaseFhirClient
    {
//disables warning that OnBeforeRequest and OnAfterResponse are never used.
#pragma warning disable CS0067
        
        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="settings"></param>
        /// <param name="messageHandler"></param>
        /// <param name="provider"></param>
        public FhirClient(Uri endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null, IStructureDefinitionSummaryProvider provider = null) : base(endpoint, settings, provider)
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
        /// <param name="settings"></param>
        /// <param name="messageHandler"></param>
        /// <param name="provider"></param>
        public FhirClient(string endpoint, FhirClientSettings settings = null, HttpMessageHandler messageHandler = null, IStructureDefinitionSummaryProvider provider = null)
            : this(new Uri(endpoint), settings, messageHandler, provider)
        {
        }

        /// <summary>
        /// Default request headers that can be modified to persist default headers to internal client.
        /// </summary>
        public HttpRequestHeaders RequestHeaders { get; protected set; }


        #region << Client Communication Defaults (PreferredFormat, UseFormatParam, Timeout, ReturnFullResource) >>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public bool VerifyFhirVersion
        {
            get => Settings.VerifyFhirVersion;
            set => Settings.VerifyFhirVersion = value;
        }

        /// <summary>
        /// The preferred format of the content to be used when communicating with the FHIR server (XML or JSON)
        /// </summary>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public ResourceFormat PreferredFormat
        {
            get => Settings.PreferredFormat;
            set => Settings.PreferredFormat = value;
        }

        /// <summary>
        /// When passing the content preference, use the _format parameter instead of the request header
        /// </summary>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public bool UseFormatParam
        {
            get => Settings.UseFormatParameter;
            set => Settings.UseFormatParameter = value;
        }

        /// <summary>
        /// The timeout (in milliseconds) to be used when making calls to the FHIR server
        /// </summary>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public int Timeout
        {
            get => Settings.Timeout;
            set => Settings.Timeout = value;
        }


        /// <summary>
        /// Should calls to Create, Update and transaction operations return the whole updated content?
        /// </summary>
        /// <remarks>Refer to specification section 2.1.0.5 (Managing Return Content)</remarks>
        [Obsolete("In STU3 this is no longer a true/false option, use the PreferredReturn property instead")]
        public bool ReturnFullResource
        {
            get => Settings.PreferredReturn == Prefer.ReturnRepresentation;
            set => Settings.PreferredReturn = value ? Prefer.ReturnRepresentation : Prefer.ReturnMinimal;
        }

        /// <summary>
        /// Should calls to Create, Update and transaction operations return the whole updated content, 
        /// or an OperationOutcome?
        /// </summary>
        /// <remarks>Refer to specification section 2.1.0.5 (Managing Return Content)</remarks>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public Prefer? PreferredReturn
        {
            get => Settings.PreferredReturn;
            set => Settings.PreferredReturn = value;
        }

        /// <summary>
        /// Should server return which search parameters were supported after executing a search?
        /// If true, the server should return an error for any unknown or unsupported parameter, otherwise
        /// the server may ignore any unknown or unsupported parameter.
        /// </summary>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public SearchParameterHandling? PreferredParameterHandling
        {
            get => Settings.PreferredParameterHandling;
            set => Settings.PreferredParameterHandling = value;
        }

        /// <summary>
        /// This will do 2 things:
        /// 1. Add the header Accept-Encoding: gzip, deflate
        /// 2. decompress any responses that have Content-Encoding: gzip (or deflate)
        /// </summary>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public bool PreferCompressedResponses
        {
            get => Settings.PreferCompressedResponses;
            set => Settings.PreferCompressedResponses = value;
        }
        /// <summary>
        /// Compress any Request bodies 
        /// (warning, if a server does not handle compressed requests you will get a 415 response)
        /// </summary>
        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public bool CompressRequestBody
        {
            get => Settings.CompressRequestBody;
            set => Settings.CompressRequestBody = value;
        }

        [Obsolete("Use the FhirClient.Settings property or the settings argument in the constructor instead")]
        public ParserSettings ParserSettings
        {
            get => Settings.ParserSettings;
            set => Settings.ParserSettings = value;
        }
        #endregion

        [Obsolete ("OnBeforeRequest is deprecated, please add a HttpClientEventHandler or another HttpMessageHandler to the constructor to use this functionality", true)]
        public event EventHandler<BeforeHttpRequestEventArgs> OnBeforeRequest;

        [Obsolete("OnAfterResponse is deprecated, please add a HttpClientEventHandler or another HttpMessageHandler to the constructor to use this functionality", true)]
        public event EventHandler<BeforeHttpRequestEventArgs> OnAfterResponseRequest;


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

