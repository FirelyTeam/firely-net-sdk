using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class PeriodFacade : IPeriod
    {
        private readonly Period _period;

        public PeriodFacade(Period period)
        {
            _period = period;
        }

        public string Start { get => _period.Start; set => _period.Start = value; }
        public string End { get => _period.End; set => _period.End = value; }
    }
}
