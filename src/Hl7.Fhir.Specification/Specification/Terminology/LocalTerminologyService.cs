/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
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
        private readonly IResourceResolver _resolver;
        private readonly ValueSetExpander _expander;

        public LocalTerminologyService(IResourceResolver resolver, ValueSetExpanderSettings expanderSettings = null)
        {
            _resolver = resolver ?? throw Error.ArgumentNull(nameof(resolver));

            var settings = expanderSettings ?? ValueSetExpanderSettings.CreateDefault();
            if (settings.ValueSetSource == null) settings.ValueSetSource = resolver;

            _expander = new ValueSetExpander(settings);
        }

        internal ValueSet FindValueset(string canonical)
        {
// Don't want to redo ITerminologyService yet
#pragma warning disable CS0618 // Type or member is obsolete
            return _resolver.FindValueSet(canonical);
#pragma warning restore CS0618 // Type or member is obsolete
        }
        
        public Parameters ValidateCode(Parameters parameters, string typeName, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Resource Expand(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Parameters Lookup(Parameters parameters, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Parameters Translate(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Resource Subsumes(Parameters parameters, string id = null, bool useGet = false)
        {
            throw new NotImplementedException();
        }

        public Resource Closure(Parameters parameters, bool useGet = false)
        {
            throw new NotImplementedException();
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
// Don't want to redo ITerminologyService yet
#pragma warning disable CS0618 // Type or member is obsolete
                    valueSet = _resolver.FindValueSet(canonical);
#pragma warning restore CS0618 // Type or member is obsolete
                }
                catch
                {
                    // valueSet remains null
                }

                if (valueSet == null)
                    throw new ValueSetUnknownException($"Cannot retrieve valueset '{canonical}'");
            }

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
            if(code == null)
                return Issue.TERMINOLOGY_NO_CODE_IN_INSTANCE.NewOutcomeWithIssue($"No code supplied.");

            lock (vs.SyncLock)
            {
                // We might have a cached or pre-expanded version brought to us by the _source
                if (!vs.HasExpansion)
                {
                    // This will expand te vs - since we do not deepcopy() it, it will change the instance
                    // as it was passed to us from the source
// Don't want to redo ITerminologyService yet
#pragma warning disable CS0618 // Type or member is obsolete
                    _expander.Expand(vs);
#pragma warning restore CS0618 // Type or member is obsolete
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

