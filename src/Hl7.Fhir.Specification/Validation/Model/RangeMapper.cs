using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class RangeMapper : ITransferMapper<Range, UniRange>
    {
        public static readonly RangeMapper Current = new RangeMapper();

        public void Transfer(MappingContext context, Range source, UniRange target)
        {
            target.Low = source.Low.Map(context, QuantityMapper.Current);
            target.High = source.High.Map(context, QuantityMapper.Current);
        }

        public void Transfer(MappingContext context, UniRange source, Range target)
        {
            target.Low = source.Low.Map(context, QuantityMapper.Current);
            target.High = source.High.Map(context, QuantityMapper.Current);
        }
    }
}
