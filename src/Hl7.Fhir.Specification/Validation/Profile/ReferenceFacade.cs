using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class ReferenceFacade : IReference
    {
        private readonly ResourceReference _reference;

        public ReferenceFacade(ResourceReference reference)
        {
            _reference = reference;
            Identifier = new IdentifierFacade(reference.Identifier);
        }

        public string Reference { get => _reference.Reference; set => _reference.Reference = value; }
        public string Type { get => _reference.Type; set => _reference.Type = value; }
        public IIdentifier Identifier { get; }
        public string Display { get => _reference.Display; set => _reference.Display = value; }
    }
}
