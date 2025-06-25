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

namespace Hl7.Fhir.Rest;

internal class HttpClientRequester : IDisposable
{
    public Uri BaseUrl { get; private set; }
    public HttpClient Client { get; }
    private readonly bool _hasInternalClient;

    public HttpClientRequester(Uri baseUrl, HttpMessageHandler messageHandler, bool disposeHandler = true)
    {
        BaseUrl = baseUrl;

        Client = new HttpClient(messageHandler, disposeHandler);
        _hasInternalClient = true;
    }

    public HttpClientRequester(Uri baseUrl, HttpClient client)
    {
        BaseUrl = baseUrl;

        Client = client;
        _hasInternalClient = false;
    }

    public async Task<HttpResponseMessage> ExecuteAsync(int timeout, HttpRequestMessage message, CancellationToken ct)
    {
        // Only overwrite the client's timeout if it is our, created internally in the constructor.
        if(_hasInternalClient)
            Client.Timeout = TimeSpan.FromMilliseconds(timeout);

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
            if (disposing && _hasInternalClient)
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