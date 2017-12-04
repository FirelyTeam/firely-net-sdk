using System;
using System.Net;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.Net.Http;

namespace Hl7.Fhir.Rest
{
    public interface IFhirClient : IDisposable, IFhirCompatibleClient
    {
        byte[] LastBody { get; }
        Resource LastBodyAsResource { get; }
        string LastBodyAsText { get; }
        HttpRequestMessage LastRequest { get; }
        HttpResponseMessage LastResponse { get; }
        Bundle.ResponseComponent LastResult { get; }

        event EventHandler<AfterResponseEventArgs> OnAfterResponse;
        event EventHandler<BeforeRequestEventArgs> OnBeforeRequest;        
    }
}