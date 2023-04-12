/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

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
        public event EventHandler<BeforeHttpRequestEventArgs>? OnBeforeRequest;

        /// <summary>
        /// Called just after the response was received
        /// </summary>
        public event EventHandler<AfterHttpResponseEventArgs>? OnAfterResponse;

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
            var requestBody = message.Content is not null ?
#if NET6_0_OR_GREATER
                await message.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false)
#else
                await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false)
#endif
                : Array.Empty<byte>();

            BeforeRequest(message, requestBody);

            var response = await base.SendAsync(message, cancellationToken).ConfigureAwait(false);

#if NET6_0_OR_GREATER
            var body =  await response.Content.ReadAsByteArrayAsync(cancellationToken);
#else
            var body = await response.Content.ReadAsByteArrayAsync();
#endif

            AfterResponse(response, body);

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

#nullable restore