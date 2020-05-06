using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class CodingMapper : ITransferMapper<Coding, UniCoding>
    {
        public static readonly CodingMapper Current = new CodingMapper();

        public void Transfer(MappingContext context, Coding source, UniCoding target)
        {
            target.System = source.System;
            target.UserSelected = source.UserSelected;
            target.Version = source.Version;
            target.Code = source.Code;
            target.Display = source.Display;
        }

        public void Transfer(MappingContext context, UniCoding source, Coding target)
        {
            target.System = source.System;
            target.UserSelected = source.UserSelected;
            target.Version = source.Version;
            target.Code = source.Code;
            target.Display = source.Display;
        }
    }
}
