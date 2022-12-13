using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public class HttpClientEventHandler : HttpClientHandler
    {
        /// <summary>
        /// Called just before the Http call is done
        /// </summary>
        public event EventHandler<BeforeHttpRequestEventArgs> OnBeforeRequest;

        /// <summary>
        /// Called just after the response was received
        /// </summary>
        public event EventHandler<AfterHttpResponseEventArgs> OnAfterResponse;

        /// <summary>
        /// Inspect or modify the HttpRequestMessage just before the FhirClient issues a call to the server
        /// </summary>
        /// <param name="rawRequest">The request as it is about to be sent to the server</param>
        /// <param name="body">The data in the body of the request as it is about to be sent to the server</param>
        protected virtual void BeforeRequest(HttpRequestMessage rawRequest, byte[] body)
        {
            // Default implementation: call event
            OnBeforeRequest?.Invoke(this, new BeforeHttpRequestEventArgs(rawRequest, body));
        }

        /// <summary>
        /// Inspect the HttpResponseMessage as it came back from the server
        /// </summary>
        /// <remarks>You cannot read the body from the HttpResponseMessage, since it has
        /// already been read by the framework. Use the body parameter instead.</remarks>
        protected virtual void AfterResponse(HttpResponseMessage webResponse, byte[] body)
        {
            // Default implementation: call event
            OnAfterResponse?.Invoke(this, new AfterHttpResponseEventArgs(webResponse, body));
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
        {
            var requestBody = message.Content != null ? await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false) : new byte[0];
            BeforeRequest(message, requestBody);

            var response = await base.SendAsync(message, cancellationToken).ConfigureAwait(false);

            AfterResponse(response, (await response.Content?.ReadAsByteArrayAsync() ?? new byte[0]));

            return response;
        }
    }

    public class BeforeHttpRequestEventArgs : EventArgs
    {
        public BeforeHttpRequestEventArgs(HttpRequestMessage rawRequest, byte[] body)
        {
            this.RawRequest = rawRequest;
            this.Body = body;
        }

        public HttpRequestMessage RawRequest { get; internal set; }
        public byte[] Body { get; internal set; }
    }

    public class AfterHttpResponseEventArgs : EventArgs
    {
        public AfterHttpResponseEventArgs(HttpResponseMessage webResponse, byte[] body)
        {
            this.RawResponse = webResponse;
            this.Body = body;
        }

        public HttpResponseMessage RawResponse { get; internal set; }
        public byte[] Body { get; internal set; }
    }
}