#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Moq;
using Moq.Protected;
using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Core.Tests.Rest
{
    internal class MoqBuilder
    {
        private static readonly ModelInspector TESTINSPECTOR = ModelInspector.ForType(typeof(TestPatient));
        private static readonly string TESTVERSION = "3.0.1";

        /// <summary>
        /// Used only for intelligence help
        /// </summary>
        private interface IHttpResponseMessage
        {
            Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token);
        }


        public Mock<HttpMessageHandler> Mock { get; } = new();

        public HttpMessageHandler Instance => Mock.Object;

        public MoqBuilder Send(HttpResponseMessage response, Expression<Func<HttpRequestMessage, bool>> check, Action<HttpRequestMessage> callback)
        {
            Mock.Protected()
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(It.Is(check), It.IsAny<CancellationToken>()))
                       .Callback<HttpRequestMessage,CancellationToken>((msg,_) => callback(msg))
                       .ReturnsAsync(response);

            return this;
        }

        public MoqBuilder Send(HttpResponseMessage response, Expression<Func<HttpRequestMessage, bool>> check)
        {
            Mock.Protected()
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(It.Is(check), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(response);

            return this;
        }

        public MoqBuilder Send(HttpResponseMessage response)
        {
            Mock.Protected()
                       .As<IHttpResponseMessage>()
                       .Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(response);

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
    }
}

#nullable restore