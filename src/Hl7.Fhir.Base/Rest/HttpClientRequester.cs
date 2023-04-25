/*
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal class HttpClientRequester : IDisposable
    {
        public Uri BaseUrl { get; private set; }
        public HttpClient Client { get; private set; }
        private readonly bool _disposeHttpClient = true;

        public HttpClientRequester(Uri baseUrl, int timeout, HttpMessageHandler messageHandler, bool disposeHandler = true)
        {
            BaseUrl = baseUrl;

            Client = new HttpClient(messageHandler, disposeHandler)
            {
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };
        }

        public HttpClientRequester(Uri baseUrl, HttpClient client)
        {
            BaseUrl = baseUrl;

            Client = client;
            _disposeHttpClient = false;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage message, CancellationToken ct)
        {         
#if NET6_0_OR_GREATER
            return await Client.SendAsync(message,ct).ConfigureAwait(false);
#else
            return await Client.SendAsync(message).ConfigureAwait(false);
#endif
        }

#region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing && _disposeHttpClient)
                {
                    // Only dispose the httpclient if was created here
                    this.Client.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
#endregion
    }

}

#nullable restore
