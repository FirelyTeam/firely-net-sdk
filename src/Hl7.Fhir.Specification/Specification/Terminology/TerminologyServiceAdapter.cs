using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation.Schema;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;
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

        private Model.Coding ToFhirPrimitive(P.Code coding) =>
            coding is null ? null : new Model.Coding() { Code = coding?.Value, System = coding?.System, Display = coding?.Display };

        private Model.CodeableConcept ToFhirPrimitive(P.Concept concept) =>
            concept is null ? null :
            new Model.CodeableConcept() { Coding = concept?.Codes.Select(cd => ToFhirPrimitive(cd)).ToList(), Text = concept?.Display };

        private static IssueSeverity? ConvertToSeverity(OperationOutcome.IssueSeverity? severity) => severity switch
        {
            OperationOutcome.IssueSeverity.Fatal => IssueSeverity.Fatal,
            OperationOutcome.IssueSeverity.Error => IssueSeverity.Error,
            OperationOutcome.IssueSeverity.Warning => IssueSeverity.Warning,
            _ => IssueSeverity.Information,
        };

        public async T.Task<Assertions> ValidateCode(string canonical = null, string context = null, string code = null, string system = null, string version = null, string display = null, P.Code coding = null, P.Concept codeableConcept = null, P.DateTime date = null, bool? @abstract = null, string displayLanguage = null)
        {
            var parameters = new ValidateCodeParameters()
               .WithValueSet(url: canonical, context: context)
               .WithCode(code: code, system: system, systemVersion: version, display: display, displayLanguage: displayLanguage)
               .WithCoding(ToFhirPrimitive(coding))
               .WithCodeableConcept(ToFhirPrimitive(codeableConcept))
               .WithAbstract(@abstract)
               .Build();

            var resultParms = await _service.ValueSetValidateCode(parameters);


            var result = resultParms.GetSingleValue<FhirBoolean>("result")?.Value ?? false;
            var message = resultParms.GetSingleValue<FhirString>("message")?.Value;

            var assertions = Assertions.Empty;

            if (message is { })
            {
                var severity = result ? OperationOutcome.IssueSeverity.Warning : OperationOutcome.IssueSeverity.Error;
                var issue = new IssueAssertion(-1, null, message, ConvertToSeverity(severity));
                assertions += issue;
            }

            return assertions;
        }
    }
}
