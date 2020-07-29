using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Validation;
using Hl7.Fhir.Validation.Schema;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Specification.Terminology
{
    public class TerminologyServiceAdapter : ITerminologyServiceNEW
    {
        private readonly ITerminologyService _service;

        public TerminologyServiceAdapter(ITerminologyService service)
        {
            _service = service;
        }

        private Model.Coding ToFhirPrimitive(Code coding) =>
            coding is null ? null : new Model.Coding() { Code = coding?.Value, System = coding?.System, Display = coding?.Display };

        private Model.CodeableConcept ToFhirPrimitive(Concept concept) =>
            concept is null ? null :
            new Model.CodeableConcept() { Coding = concept?.Codes.Select(cd => ToFhirPrimitive(cd)).ToList(), Text = concept?.Display };


        public T.Task<Assertions> ValidateCode(string canonical = null, string context = null, string code = null, string system = null, string version = null, string display = null, Code coding = null, Concept codeableConcept = null, DateTime date = null, bool? @abstract = null, string displayLanguage = null)
        {
            var outcome = _service.ValidateCode(canonical: canonical, context: context, code: code, system: system, version: version, display: display,

                    coding: ToFhirPrimitive(coding), codeableConcept: ToFhirPrimitive(codeableConcept), date: null,  // todo convert from system types to Fhir Types

                    @abstract: @abstract, displayLanguage: displayLanguage);


            return T.Task.FromResult(outcome.ToAssertions());
        }
    }
}
