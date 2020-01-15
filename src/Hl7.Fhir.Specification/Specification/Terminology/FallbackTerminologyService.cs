using System;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Terminology
{
    public class FallbackTerminologyService : ITerminologyService
    {
        private readonly LocalTerminologyService _localService;
        private readonly ITerminologyService _fallbackService;

        public FallbackTerminologyService(LocalTerminologyService local, ITerminologyService fallback)
        {
            _localService = local;
            _fallbackService = fallback;
        }

        public Parameters ValidateCode(Parameters parameters, string typeName, string id = null, bool useGet = false)
        {
            // TODO: KM: Talk to EK about fallback sequence
            throw new NotImplementedException();
        }

        public Resource Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            // TODO: KM: Talk to EK about fallback sequence
            throw new NotImplementedException();
        }

        public Parameters Lookup(Parameters parameters, bool useGet = false)
        {
            // TODO: KM: Talk to EK about fallback sequence
            throw new NotImplementedException();
        }

        public Parameters Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            // TODO: KM: Talk to EK about catch sequence
            throw new NotImplementedException();
        }

        public Resource Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            // TODO: KM: Talk to EK about fallback sequence
            throw new NotImplementedException();
        }

        public Resource Closure(Parameters parameters, bool useGet = false)
        {
            // TODO: KM: Talk to EK about fallback sequence
            throw new NotImplementedException();
        }

        [Obsolete("This method is obsolete, use method with signature 'ValidateCode(Parameters, string, string, bool)'")]
        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null, 
            string code = null, string system = null, string version = null, string display = null, 
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null, 
            bool? @abstract = default, string displayLanguage = null)
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
