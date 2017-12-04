using System;
using System.Net;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Rest
{
    public interface IFhirClient : IFhirCompatibleClient
    {
        byte[] LastBody { get; }
        Resource LastBodyAsResource { get; }
        string LastBodyAsText { get; }
        HttpWebRequest LastRequest { get; }
        HttpWebResponse LastResponse { get; }
        Bundle.ResponseComponent LastResult { get; }

        event EventHandler<AfterResponseEventArgs> OnAfterResponse;
        event EventHandler<BeforeRequestEventArgs> OnBeforeRequest; 
    }
}