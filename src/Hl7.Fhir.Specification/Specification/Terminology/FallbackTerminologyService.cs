using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Terminology
{
    public class FallbackTerminologyService : ITerminologyService
    {
        private LocalTerminologyService _localService;
        private ITerminologyService _fallbackService;

        public FallbackTerminologyService(LocalTerminologyService local, ITerminologyService fallback)
        {
            _localService = local;
            _fallbackService = fallback;
        }

        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null, 
            string code = null, string system = null, string version = null, string display = null, 
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null, 
            bool? @abstract = default(bool?), string displayLanguage = null)
        {

            try
            {
                // First, try the local service
                return _localService.ValidateCode(canonical, context, valueSet, code, system, version, display,
                    coding, codeableConcept, date, @abstract, displayLanguage);
            }
            catch (TerminologyServiceException)
            {
                // If that fails, call the fallback
                try
                {
                    return _fallbackService.ValidateCode(canonical, context, valueSet, code, system, version, display,
                        coding, codeableConcept, date, @abstract, displayLanguage);
                }
                catch (ValueSetUnknownException vse)
                {
                    // The fall back service does not know the valueset. If our local service
                    // does, try get the VS from there, and retry by sending the vs inline
                    valueSet = _localService.FindValueset(canonical);
                    if (valueSet == null) throw vse;

                    return _fallbackService.ValidateCode(null, context, valueSet, code, system, version, display,
                        coding, codeableConcept, date, @abstract, displayLanguage);
                }
            }
        }
    }
}
