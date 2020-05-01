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

        // Original
        private TResource execute<TResource>(Bundle tx, HttpStatusCode expect) where TResource : Model.Resource
        {
            return executeAsync<TResource>(tx, new[] { expect }).WaitResult();
        }
        public Task<TResource> executeAsync<TResource>(Model.Bundle tx, HttpStatusCode expect) where TResource : Model.Resource
        {
            return executeAsync<TResource>(tx, new[] { expect });
        }
        // Original
        private TResource execute<TResource>(Bundle tx, IEnumerable<HttpStatusCode> expect) where TResource : Resource
        {
            return executeAsync<TResource>(tx,  expect).WaitResult();
        }

        private async Task<TResource> executeAsync<TResource>(Bundle tx, IEnumerable<HttpStatusCode> expect) where TResource : Resource
        {
            verifyServerVersion();

            var request = tx.Entry[0];
            var response = await _requester.ExecuteAsync(request).ConfigureAwait(false);

            if (!expect.Select(sc => ((int)sc).ToString()).Contains(response.Response.Status))
            {
                Enum.TryParse<HttpStatusCode>(response.Response.Status, out HttpStatusCode code);
                throw new FhirOperationException("Operation concluded successfully, but the return status {0} was unexpected".FormatWith(response.Response.Status), code);
            }

            Resource result;

            // Special feature: if ReturnFullResource was requested (using the Prefer header), but the server did not return the resource
            // (or it returned an OperationOutcome) - explicitly go out to the server to get the resource and return it. 
            // This behavior is only valid for PUT and POST requests, where the server may device whether or not to return the full body of the alterend resource.
            var noRealBody = response.Resource == null || (response.Resource is OperationOutcome && string.IsNullOrEmpty(response.Resource.Id));
            if (noRealBody && isPostOrPut(request) 
                && PreferredReturn == Prefer.ReturnRepresentation && response.Response.Location != null
                && new ResourceIdentity(response.Response.Location).IsRestResourceIdentity()) // Check that it isn't an operation too
            {
                result = await GetAsync(response.Response.Location).ConfigureAwait(false);
            }
            else
                result = response.Resource;

            if (result == null) return null;

            // We have a success code (2xx), we have a body, but the body may not be of the type we expect.
            if (!(result is TResource))
            {
                // If this is an operationoutcome, that may still be allright. Keep the OperationOutcome in 
                // the LastResult, and return null as the result. Otherwise, throw.
                if (result is OperationOutcome)
                    return null;

                var message = String.Format("Operation {0} on {1} expected a body of type {2} but a {3} was returned", request.Request.Method,
                    request.Request.Url, typeof(TResource).Name, result.GetType().Name);

                Enum.TryParse(response.Response.Status, out HttpStatusCode code);
                throw new FhirOperationException(message, code);
            }
            else
                return result as TResource;
        }
        private bool isPostOrPut(Bundle.EntryComponent interaction)
        {
            var method = interaction.Request.Method;
            return method == Bundle.HTTPVerb.POST || method == Bundle.HTTPVerb.PUT;
        }


        private bool versionChecked = false;

        private void verifyServerVersion()
        {
            if (!VerifyFhirVersion) return;

            if (versionChecked) return;
            versionChecked = true;      // So we can now start calling Conformance() without getting into a loop

            CapabilityStatement conf = null;
            try
            {
                conf = CapabilityStatement(SummaryType.True); // don't get the full version as its huge just to read the fhir version
            }
            catch (FormatException)
            {
                // Mmmm...cannot even read the body. Probably not so good.
                throw Error.NotSupported("Cannot read the conformance statement of the server to verify FHIR version compatibility");
            }

            if (conf.Version == null)
            {
                throw Error.NotSupported($"This CapabilityStatement of the server doesn't state its FHIR version");
            }
            else if (!ModelInfo.CheckMinorVersionCompatibility(conf.Version))
            {
                throw Error.NotSupported($"This client supports FHIR version {ModelInfo.Version} but the server uses version {conf.Version}");
            }         
           
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
