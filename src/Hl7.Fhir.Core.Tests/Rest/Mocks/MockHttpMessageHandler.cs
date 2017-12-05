using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.Rest.Mocks
{
    public class MockHttpMessageHandler : HttpClientHandler
    {
        /// <summary>
        /// Called just before the Http call is done
        /// </summary>
        public event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;

        /// <summary>
        /// Called just after the response was received
        /// </summary>
        public event EventHandler<AfterResponseEventArgs> OnAfterResponse;

        /// <summary>
        /// Inspect or modify the HttpRequestMessage just before the FhirClient issues a call to the server
        /// </summary>
        /// <param name="rawRequest">The request as it is about to be sent to the server</param>
        /// <param name="body">The data in the body of the request as it is about to be sent to the server</param>
        protected virtual void BeforeRequest(HttpRequestMessage rawRequest, byte[] body)
        {
            // Default implementation: call event
            OnBeforeRequest?.Invoke(this, new BeforeRequestEventArgs(rawRequest, body));
        }

        /// <summary>
        /// Inspect the HttpResponseMessage as it came back from the server
        /// </summary>
        /// <remarks>You cannot read the body from the HttpResponseMessage, since it has
        /// already been read by the framework. Use the body parameter instead.</remarks>
        protected virtual void AfterResponse(HttpResponseMessage webResponse, byte[] body)
        {
            // Default implementation: call event
            OnAfterResponse?.Invoke(this, new AfterResponseEventArgs(webResponse, body));
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
        {
            var requestBody = message.Content != null ? await message.Content.ReadAsByteArrayAsync() : new byte[0];
            BeforeRequest(message, requestBody);

            var response = await base.SendAsync(message, cancellationToken);

            AfterResponse(response, (await response.Content?.ReadAsByteArrayAsync() ?? new byte[0]));

            return response;
        }
    }

    public class BeforeRequestEventArgs : EventArgs
    {
        public BeforeRequestEventArgs(HttpRequestMessage rawRequest, byte[] body)
        {
            this.RawRequest = rawRequest;
            this.Body = body;
        }

        public HttpRequestMessage RawRequest { get; internal set; }
        public byte[] Body { get; internal set; }
    }

    public class AfterResponseEventArgs : EventArgs
    {
        public AfterResponseEventArgs(HttpResponseMessage webResponse, byte[] body)
        {
            this.RawResponse = webResponse;
            this.Body = body;
        }

        public HttpResponseMessage RawResponse { get; internal set; }
        public byte[] Body { get; internal set; }
    }
}
