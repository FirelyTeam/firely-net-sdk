using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class StructureDefElementMapper : ITransferMapper<ElementDefinition, UniStructureDefElement>
    {
        public static readonly StructureDefElementMapper Current = new StructureDefElementMapper();

        public void Transfer(MappingContext context, ElementDefinition source, UniStructureDefElement target)
        {
            throw new System.NotImplementedException();
        }

        public void Transfer(MappingContext context, UniStructureDefElement source, ElementDefinition target)
        {
            throw new System.NotImplementedException();
        }
    }
}
