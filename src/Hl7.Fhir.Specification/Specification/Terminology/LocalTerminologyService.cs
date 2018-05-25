/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System.Linq;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Specification.Terminology
{
    public class LocalTerminologyService : ITerminologyService
    {
        private IResourceResolver _resolver;
        private ValueSetExpander _expander;

        public LocalTerminologyService(IResourceResolver resolver, ValueSetExpanderSettings expanderSettings = null)
        {
            _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

            var settings = expanderSettings ?? ValueSetExpanderSettings.Default;
            if (settings.ValueSetSource == null) settings.ValueSetSource = resolver;

            _expander = new ValueSetExpander(settings);
        }

        internal ValueSet FindValueset(string canonical)
        {
            return _resolver.FindValueSet(canonical);
        }

        public OperationOutcome ValidateCode(string canonical = null, string context = null, ValueSet valueSet = null, 
            string code = null, string system = null, string version = null, string display = null, 
            Coding coding = null, CodeableConcept codeableConcept = null, FhirDateTime date = null, 
            bool? @abstract = null, string displayLanguage = null)
        {
            if(valueSet == null)
            {
                if(canonical == null)
                    throw Error.Argument("Have to supply either a canonical url or a valueset.");

                try
                {
                    valueSet = _resolver.FindValueSet(canonical);
                }
                catch
                {
                    // valueSet remains null
                }

                if (valueSet == null)
                    throw new ValueSetUnknownException($"Cannot retrieve valueset '{canonical}'");
            }

            var outcome = new OperationOutcome();

            if (codeableConcept != null)
                return validateCodeVS(valueSet, codeableConcept, @abstract);
            else if (coding != null)
                return validateCodeVS(valueSet, coding, @abstract);
            else
                return validateCodeVS(valueSet, code, system, display, @abstract);
        }


        private OperationOutcome validateCodeVS(ValueSet vs, CodeableConcept cc, bool? abstractAllowed)
        {
            var outcome = new OperationOutcome();

            // Maybe just a text, but if there are no codings, that's a positive result
            if (!cc.Coding.Any()) return outcome;

            // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
            if (cc.Coding.Count == 1)
                return validateCodeVS(vs, cc.Coding.Single(), abstractAllowed);

            // Else, look for one succesful match in any of the codes in the CodeableConcept
            var callResults = cc.Coding.Select(coding => validateCodeVS(vs, coding, abstractAllowed));
            var successOutcome = callResults.Where(r => r.Success).OrderBy(oo => oo.Warnings).FirstOrDefault();

            if (successOutcome == null)
            {
                outcome.AddIssue("None of the Codings in the CodeableConcept were valid for the binding. Details follow.", Issue.CONTENT_INVALID_FOR_REQUIRED_BINDING);
                foreach (var cr in callResults)
                {
                    cr.MakeInformational();
                    outcome.Include(cr);
                }
            }
            else
            {
                outcome.Add(successOutcome);
            }

            return outcome;
        }


        private OperationOutcome validateCodeVS(ValueSet vs, Coding coding, bool? abstractAllowed)
        {
            return validateCodeVS(vs, coding.Code, coding.System, coding.Display, abstractAllowed);
        }

        private OperationOutcome validateCodeVS(ValueSet vs, string code, string system, string display, bool? abstractAllowed)
        {
            if (string.IsNullOrEmpty(code)) throw Error.ArgumentNullOrEmpty(nameof(code));
            
            lock (vs.SyncLock)
            {
                // We might have a cached or pre-expanded version brought to us by the _source
                if (!vs.HasExpansion)
                {
                    // This will expand te vs - since we do not deepcopy() it, it will change the instance
                    // as it was passed to us from the source
                    _expander.Expand(vs);
                }
            }

            var component = vs.FindInExpansion(code, system);
            var codeLabel = $"Code '{code}' from system '{system}'";
            var result = new OperationOutcome();

            if (component == null)
                result.AddIssue($"{codeLabel} does not exist in valueset '{vs.Url}'", Issue.TERMINOLOGY_CODE_NOT_IN_VALUESET);
            else
            {
                if (component.Abstract == true && abstractAllowed == false)  // will be ignored if abstractAllowed == null
                    result.AddIssue($"{codeLabel} is abstract, which is not allowed here", Issue.TERMINOLOGY_ABSTRACT_CODE_NOT_ALLOWED);

                if (display != null && component.Display != null && display != component.Display)
                    result.AddIssue($"{codeLabel} has incorrect display '{display}', should be '{component.Display}'", Issue.TERMINOLOGY_INCORRECT_DISPLAY);
            }

            return result;
        }

    }
}

