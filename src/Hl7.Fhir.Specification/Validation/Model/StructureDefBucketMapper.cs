using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class StructureDefBucketMapper : ITransferMapper<ElementDefinition, UniStructureDefElementBucket>
    {
        public static readonly StructureDefBucketMapper Current = new StructureDefBucketMapper();

        public void Transfer(MappingContext context, ElementDefinition source, UniStructureDefElementBucket target)
        {
            throw new System.NotImplementedException();
        }

        public void Transfer(MappingContext context, UniStructureDefElementBucket source, ElementDefinition target)
        {
            throw new System.NotImplementedException();
        }
    }
}
