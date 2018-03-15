using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
