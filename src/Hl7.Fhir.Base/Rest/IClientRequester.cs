using System.Threading.Tasks;

namespace Hl7.Fhir.Rest
{
    internal interface IClientRequester
    {
        Task<EntryResponse> ExecuteAsync(EntryRequest interaction);
    }
}
