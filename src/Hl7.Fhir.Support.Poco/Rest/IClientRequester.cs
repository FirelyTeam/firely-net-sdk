using Hl7.Fhir.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    public interface IClientRequester
    {
        Task<EntryResponse> ExecuteAsync(EntryRequest interaction);
    }
}
