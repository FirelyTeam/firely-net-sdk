using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class QuantityMapper : ITransferMapper<Quantity, UniQuantity>
    {
        public static readonly QuantityMapper Current = new QuantityMapper();

        private static readonly EnumMapper<Quantity.QuantityComparator, UniQuantityComparator> _comparatorMapper = new EnumMapper<Quantity.QuantityComparator, UniQuantityComparator>(
            (Quantity.QuantityComparator.GreaterOrEqual, UniQuantityComparator.GreaterOrEqual),
            (Quantity.QuantityComparator.GreaterThan, UniQuantityComparator.GreaterThen),
            (Quantity.QuantityComparator.LessOrEqual, UniQuantityComparator.LessOrEqual),
            (Quantity.QuantityComparator.LessThan, UniQuantityComparator.LessThan)
            );

        public void Transfer(MappingContext context, Quantity source, UniQuantity target)
        {
            target.System = source.System;
            target.Unit = source.Unit;
            target.Value = source.Value;
            target.Code = source.Code;
            target.Comparator = source.Comparator.Map(context, _comparatorMapper);
        }

        public void Transfer(MappingContext context, UniQuantity source, Quantity target)
        {
            target.System = source.System;
            target.Unit = source.Unit;
            target.Value = source.Value;
            target.Code = source.Code;
            target.Comparator = source.Comparator.Map(context, _comparatorMapper);
        }
    }
}
