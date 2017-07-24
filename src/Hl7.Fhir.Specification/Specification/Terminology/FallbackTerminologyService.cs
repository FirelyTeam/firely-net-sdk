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

        public OperationOutcome ValidateCode(string canonical, string code, string system, string display = null, bool abstractAllowed = false)
        {
            try
            {
                return _localService.ValidateCode(canonical, code, system, display, abstractAllowed);
            }
            catch (TerminologyServiceException)
            {
                try
                {
                    return _fallbackService.ValidateCode(canonical, code, system, display, abstractAllowed);
                }
                catch (ValueSetUnknownException vse)
                {
                    // The fall back service does not know the valueset. If our local service
                    // does, try get the VS from there, and retry by sending the vs inline
                    var vs = _localService.FindValueset(canonical);
                    if (vs == null) throw vse;

                    return _fallbackService.ValidateCode(vs, code, system, display, abstractAllowed);
                }
            }
        }

        public OperationOutcome ValidateCode(ValueSet vs, string code, string system, string display = null, bool abstractAllowed = false)
        {
            try
            {
                return _localService.ValidateCode(vs, code, system, display, abstractAllowed);
            }
            catch (TerminologyServiceException)
            {
                return _fallbackService.ValidateCode(vs, code, system, display, abstractAllowed);
            }
        }
    }
}
