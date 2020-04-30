using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Profile;

namespace Hl7.Fhir.Specification.Validation.Profile
{
    internal class CodeableConceptFacade : ICodeableConcept
    {
        private readonly CodeableConcept _codeableConcept;

        public CodeableConceptFacade(CodeableConcept codeableConcept)
        {
            _codeableConcept = codeableConcept;
            Coding = new CollectionFacade<ICodingExt, Coding>(codeableConcept.Coding, coding => new CodingFacade(coding));
        }

        public ICollectionFacade<ICodingExt> Coding { get; }

        public string Text { get => _codeableConcept.Text; set => _codeableConcept.Text = value; }
    }
}
