using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class PeriodMapper : ITransferMapper<Period, UniPeriod>
    {
        public static readonly PeriodMapper Current = new PeriodMapper();

        public void Transfer(MappingContext context, Period source, UniPeriod target)
        {
            target.Start = source.Start;
            target.End = source.End;
        }

        public void Transfer(MappingContext context, UniPeriod source, Period target)
        {
            target.Start = source.Start;
            target.End = source.End;
        }
    }
}
