using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class IdentifierFacade: IIdentifier, ICommitable
    {
        private static readonly EnumMapper<IdentifierUse, Identifier.IdentifierUse> _useMap = new EnumMapper<IdentifierUse, Identifier.IdentifierUse>
            (
                (IdentifierUse.Official, Identifier.IdentifierUse.Official),
                (IdentifierUse.Old, Identifier.IdentifierUse.Old),
                (IdentifierUse.Secondary, Identifier.IdentifierUse.Secondary),
                (IdentifierUse.Temporary, Identifier.IdentifierUse.Temp),
                (IdentifierUse.Usual, Identifier.IdentifierUse.Usual)
            );

        private readonly Identifier _identifier;

        public IdentifierFacade(Identifier identifier)
        {
            _identifier = identifier;
            Type = new CodeableConceptFacade(identifier.Type);
            Period = new PeriodFacade(_identifier.Period);
            Assigner = new ReferenceFacade(_identifier.Assigner);
        }

        public IdentifierUse? Use { get => _useMap.Export(_identifier.Use); set => _identifier.Use = _useMap.Import(value); }

        public ICodeableConcept Type { get; }

        public string System { get => _identifier.System; set => _identifier.System = value; }
        public string Value { get => _identifier.Value; set => _identifier.Value = value; }

        public IPeriod Period { get; }

        public IReference Assigner { get; }
    }
}
