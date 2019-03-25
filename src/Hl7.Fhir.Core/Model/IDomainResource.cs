using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public interface IDomainResource : IResource, IModifierExtendable
    {
        Hl7.Fhir.Model.Narrative Text { get; set; }

        List<Hl7.Fhir.Model.Resource> Contained { get; set; }
    }
}
