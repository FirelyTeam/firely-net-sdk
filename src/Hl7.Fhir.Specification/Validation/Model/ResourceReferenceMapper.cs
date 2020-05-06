using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class ResourceReferenceMapper : ITransferMapper<ResourceReference, UniResourceReference>
    {
        public static readonly ResourceReferenceMapper Current = new ResourceReferenceMapper();

        public void Transfer(MappingContext context, ResourceReference source, UniResourceReference target)
        {
            target.Display = source.Display;
            target.Identifier = source.Identifier.Map(context, IdentifierMapper.Current);
            target.Reference = source.Reference;
            target.Type = source.Type;
        }

        public void Transfer(MappingContext context, UniResourceReference source, ResourceReference target)
        {
            target.Display = source.Display;
            target.Identifier = source.Identifier.Map(context, IdentifierMapper.Current);
            target.Reference = source.Reference;
            target.Type = source.Type;
        }
    }
}
