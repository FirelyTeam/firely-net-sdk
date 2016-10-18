using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LocalTerminologyServer : ITerminologyService
    {
        private IConformanceSource _source;
        private IResourceResolver _resolver;

        public LocalTerminologyServer(IConformanceSource localSource)
        {
            if (localSource == null) throw Error.ArgumentNull(nameof(localSource));

            _source = localSource;
            _resolver = localSource;
        }

        public LocalTerminologyServer(IResourceResolver resolver)
        {
            if (resolver == null) throw Error.ArgumentNull(nameof(resolver));

            _resolver = resolver;
        }


        public OperationOutcome ValidateCode(string uri, string code, string system, string display = null, bool abstractAllowed = false)
        {
            var result = new OperationOutcome();
            var vs = _resolver.FindValueSet(uri);

            if (vs == null)
            {
                result.AddIssue($"Cannot retrieve valueset '{uri}'", Issue.UNAVAILABLE_VALUESET);
                return result;
            }

            // We might have a cached or pre-expanded version brought to us by the _source
            if (!vs.HasExpansion)
            {
                var expander = new ValueSetExpander();
                // set the settings to use the _source

                // This will expand te vs - since we do not deepcopy() it, it will change the instance
                // as it was passed to us from the source
                try
                {
                    expander.Expand(vs);
                }
                catch(NotSupportedException nse)
                {
                    result.AddIssue($"ValueSet cannot be expanded: {nse.Message}", Issue.TERMINOLOGY_VALUESET_TOO_COMPLEX);
                    return result;
                }
            }

            // No fallback necessary, just do a direct check for now

            var component = vs.FindInExpansion(code, system);
            var codeLabel = $"Code '{code}' from system '{system}'";
            if (component == null)
                result.AddIssue($"{codeLabel} does not exists in valueset '{uri}'", Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
            else
            {
                if (component.Abstract == true && !abstractAllowed)
                    result.AddIssue($"{codeLabel} is abstract, which is not allowed here", Issue.TERMINOLOGY_ABSTRACT_CODE_NOT_ALLOWED);

                if (display != null && component.Display != null && display != component.Display)
                    result.AddIssue($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'", Issue.TERMINOLOGY_INCORRECT_DISPLAY);
            }

            return result;
        }
    }
}
