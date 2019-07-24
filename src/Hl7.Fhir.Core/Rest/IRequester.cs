using Hl7.Fhir.Model;
using System.Net;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public interface IRequester
    {
        Task<Bundle.EntryComponent> ExecuteAsync(Bundle.EntryComponent interaction);

        Bundle.EntryComponent LastResult { get; }
        HttpStatusCode? LastStatusCode { get; }
    }
}
