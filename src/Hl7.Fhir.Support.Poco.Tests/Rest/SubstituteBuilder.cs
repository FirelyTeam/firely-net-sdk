#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using NSubstitute;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.Rest
{
    public class SubstituteBuilder
    {
        private static readonly ModelInspector TESTINSPECTOR = ModelInfo.ModelInspector;
        private static readonly string TESTVERSION = "3.0.1";

        private TestHttpMessageHandler Instance { get; } = Substitute.For<TestHttpMessageHandler>();

        
        public SubstituteBuilder Send(HttpResponseMessage response, Action<HttpRequestMessage> callback)
        {
            Instance.SendAsyncPublic(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response).AndDoes(msg => callback(msg.Arg<HttpRequestMessage>()));

            return this;
        }
        
        public SubstituteBuilder Send(HttpResponseMessage response, Predicate<HttpRequestMessage> check)
        {
            Instance.SendAsyncPublic(Arg.Is<HttpRequestMessage>(msg => check(msg)), Arg.Any<CancellationToken>()).Returns(response);

            return this;
        }

        public SubstituteBuilder Send(HttpResponseMessage response)
        {
            Instance.SendAsyncPublic(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response);

            return this;
        }

        public BaseFhirClient AsClient(Action<FhirClientSettings>? setter = null, Uri? baseUri = null)
        {
            var settings = new FhirClientSettings() { ExplicitFhirVersion = TESTVERSION };
            setter?.Invoke(settings);

            return new BaseFhirClient(
                baseUri ?? new Uri("http://example.com"),
                Instance,
                TESTINSPECTOR,
                settings);
        }
        
        public class TestHttpMessageHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
                throw new NotSupportedException("This should never be called, this class should be mocked by a substitute. and this method should be overridden by that mock");
            public Task<HttpResponseMessage> SendAsyncPublic(HttpRequestMessage request, CancellationToken cancellationToken) => SendAsync(request, cancellationToken);
        }
    }
}

#nullable restore