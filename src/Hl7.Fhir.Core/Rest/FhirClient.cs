/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Hl7.Fhir.Rest
{
    // [Obsolete]
    public partial class FhirClient : BaseFhirClient
    {
        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="verifyFhirVersion">
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirClient(Uri endpoint, bool verifyFhirVersion = false)
        {
            Endpoint = GetValidatedEndpoint(endpoint);

            Requester = new Requester(Endpoint)
            {
                BeforeRequest = this.BeforeRequest,
                AfterResponse = this.AfterResponse
            };

            VerifyFhirVersion = verifyFhirVersion;
        }


        /// <summary>
        /// Creates a new client using a default endpoint
        /// If the endpoint does not end with a slash (/), it will be added.
        /// </summary>
        /// <param name="endpoint">
        /// The URL of the server to connect to.<br/>
        /// If the trailing '/' is not present, then it will be appended automatically
        /// </param>
        /// <param name="verifyFhirVersion">
        /// If parameter is set to true the first time a request is made to the server a 
        /// conformance check will be made to check that the FHIR versions are compatible.
        /// When they are not compatible, a FhirException will be thrown.
        /// </param>
        public FhirClient(string endpoint, bool verifyFhirVersion = false)
            : this(new Uri(endpoint), verifyFhirVersion)
        {
        }


        public override byte[] LastBody => LastResult?.GetBody();
        public override string LastBodyAsText => LastResult?.GetBodyAsText();
        public override Resource LastBodyAsResource => Requester.LastResult?.Resource;

        /// <summary>
        /// Returns the HttpWebRequest as it was last constructed to execute a call on the FhirClient
        /// </summary>
        [Obsolete]
        public override HttpWebRequest LastRequest { get { return (Requester as Requester)?.LastRequest; } }

        /// <summary>
        /// Returns the HttpWebResponse as it was last received during a call on the FhirClient
        /// </summary>
        /// <remarks>Note that the FhirClient will have read the body data from the HttpWebResponse, so this is
        /// no longer available. Use LastBody, LastBodyAsText and LastBodyAsResource to get access to the received body (if any)</remarks>
        [Obsolete]
        public override HttpWebResponse LastResponse { get { return (Requester as Requester)?.LastResponse; } }

        /// <summary>
        /// Called just before the Http call is done
        /// </summary>
        //[Obsolete]
        public override event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;

        /// <summary>
        /// Called just after the response was received
        /// </summary>
        //[Obsolete]
        public override event EventHandler<AfterResponseEventArgs> OnAfterResponse;

        /// <summary>
        /// Inspect or modify the HttpWebRequest just before the FhirClient issues a call to the server
        /// </summary>
        /// <param name="rawRequest">The request as it is about to be sent to the server</param>
        /// <param name="body">The data in the body of the request as it is about to be sent to the server</param>
        protected virtual void BeforeRequest(HttpWebRequest rawRequest, byte[] body)
        {
            // Default implementation: call event
            OnBeforeRequest?.Invoke(this, new BeforeRequestEventArgs(rawRequest, body));
        }

        /// <summary>
        /// Inspect the HttpWebResponse as it came back from the server
        /// </summary>
        /// <remarks>You cannot read the body from the HttpWebResponse, since it has
        /// already been read by the framework. Use the body parameter instead.</remarks>
        protected virtual void AfterResponse(HttpWebResponse webResponse, byte[] body)
        {
            // Default implementation: call event
            OnAfterResponse?.Invoke(this, new AfterResponseEventArgs(webResponse, body));
        }
    }

    public class BeforeRequestEventArgs : EventArgs
    {
        public BeforeRequestEventArgs(HttpWebRequest rawRequest, byte[] body)
        {
            this.RawRequest = rawRequest;
            this.Body = body;
        }

        public HttpWebRequest RawRequest { get; internal set; }
        public byte[] Body { get; internal set; }
    }

    public class AfterResponseEventArgs : EventArgs
    {
        public AfterResponseEventArgs(HttpWebResponse webResponse, byte[] body)
        {
            this.RawResponse = webResponse;
            this.Body = body;
        }

        public HttpWebResponse RawResponse { get; internal set; }
        public byte[] Body { get; internal set; }
    }
}
