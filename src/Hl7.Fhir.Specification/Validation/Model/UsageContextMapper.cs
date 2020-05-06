using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class UsageContextMapper : ITransferMapper<UsageContext, UniUsageContext>
    {
        public static readonly UsageContextMapper Current = new UsageContextMapper();

        private static readonly ElementMapper<Element> _elementMapper = new ElementMapper<Element>()
            .Add(CodeableConceptMapper.Current)
            .Add(QuantityMapper.Current)
            .Add(RangeMapper.Current)
            .Add(ResourceReferenceMapper.Current);

        public void Transfer(MappingContext context, UsageContext source, UniUsageContext target)
        {
            target.Code = source.Code.Map(context, CodingMapper.Current);
            target.Value = source.Value.Map(context, _elementMapper);
        }

        public void Transfer(MappingContext context, UniUsageContext source, UsageContext target)
        {
            target.Code = source.Code.Map(context, CodingMapper.Current);
            target.Value = source.Value.Map(context, _elementMapper);
        }
    }
}
