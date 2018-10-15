using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Tests.Rest
{
    public class TestHttpHandler : HttpClientHandler
    {
        public HttpRequestMessage LastRequest { get; private set; }
        public HttpResponseMessage LastResponse { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            var response = await base.SendAsync(request, cancellationToken);
            LastResponse = response;
            return response;
        }
    }
}
