using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Model;

namespace Hl7.Fhir.Specification.Validation.Model
{
    internal class IdentifierMapper : ITransferMapper<Identifier, UniIdentifier>
    {
        public static readonly IdentifierMapper Current = new IdentifierMapper();

        private static readonly EnumMapper<Identifier.IdentifierUse, UniIdentifierUse> _identifierUseMapper
            = new EnumMapper<Identifier.IdentifierUse, UniIdentifierUse>(
                (Identifier.IdentifierUse.Official, UniIdentifierUse.Official),
                (Identifier.IdentifierUse.Old, UniIdentifierUse.Old),
                (Identifier.IdentifierUse.Secondary, UniIdentifierUse.Secondary),
                (Identifier.IdentifierUse.Temp, UniIdentifierUse.Temporary),
                (Identifier.IdentifierUse.Usual, UniIdentifierUse.Usual)
                );

        public void Transfer(MappingContext context, Identifier source, UniIdentifier target)
        {
            target.Use = source.Use.Map(context, _identifierUseMapper);
            target.Type = source.Type.Map(context, CodeableConceptMapper.Current);
            target.System = source.System;
            target.Value = source.Value;
            target.Period = source.Period.Map(context, PeriodMapper.Current);
            target.Assigner = source.Assigner.Map(context, ResourceReferenceMapper.Current);
        }

        public void Transfer(MappingContext context, UniIdentifier source, Identifier target)
        {
            target.Use = source.Use.Map(context, _identifierUseMapper);
            target.Type = source.Type.Map(context, CodeableConceptMapper.Current);
            target.System = source.System;
            target.Value = source.Value;
            target.Period = source.Period.Map(context, PeriodMapper.Current);
            target.Assigner = source.Assigner.Map(context, ResourceReferenceMapper.Current);
        }
    }
}
