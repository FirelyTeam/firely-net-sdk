/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class BindingValidator
    {
        private ITerminologyService _service;
        private string _path;

        public BindingValidator(ITerminologyService service, string path)
        {
            _service = service;
            _path = path;
        }

        public OperationOutcome ValidateBinding(Element bindable, ElementDefinition.BindingComponent binding)
        {
            // bindable should be code, Coding or CodeableConcept

            if (binding.Strength == null)
                return Issue.PROFILE_INCOMPLETE_BINDING.NewOutcomeWithIssue($"Encountered a binding element without a binding strength", _path);

            var uri = (binding.ValueSet as FhirUri)?.Value ??
                        (binding.ValueSet as ResourceReference)?.Reference;

            if (uri == null)
                return Issue.PROFILE_INCOMPLETE_BINDING.NewOutcomeWithIssue($"Encountered a binding element without either a ValueSet reference or uri", _path);

            var abstractAllowed = true;     // no way to say otherwise in the current spec?

            OperationOutcome outcome;

            if (!hasCode(bindable))
            {
                var isCCWithJustText = bindable is CodeableConcept cc && !String.IsNullOrEmpty(cc.Text);
                if (isCCWithJustText)
                    return new OperationOutcome();   // OK
                return Issue.TERMINOLOGY_NO_CODE_IN_INSTANCE.NewOutcomeWithIssue($"No code found in instance.", _path);
            }

            switch (bindable)
            {
                case Code co:
                    outcome = callService(uri, co?.Value, system: null, display: null, abstractAllowed: abstractAllowed);
                    break;
                case Coding cd:
                    outcome = callService(uri, coding: cd, abstractAllowed: abstractAllowed);
                    break;
                case CodeableConcept cc:
                    outcome = callService(uri, cc: cc, abstractAllowed: abstractAllowed);
                    break;
                default:
                    throw Error.NotSupported($"Validating a binding against a '{bindable.TypeName}' is not supported in FHIR.");
            }

            //EK 20170605 - disabled inclusion of warnings/erros for all but required bindings since this will 
            // 1) create superfluous messages (both saying the code is not valid) coming from the validateResult + the outcome.AddIssue() 
            // 2) add the validateResult as warnings for preferred bindings, which are confusing in the case where the slicing entry is 
            //    validating the binding against the core and slices will refine it: if it does not generate warnings against the slice, 
            //    it should not generate warnings against the slicing entry.
            if (binding.Strength == BindingStrength.Required)
                return outcome;
            else
                return new OperationOutcome();
        }

        private bool hasCode(Element bindeable)
        {
            switch (bindeable)
            {
                case Code co:
                    return !String.IsNullOrEmpty(co?.Value);
                case Coding cd:
                    return !String.IsNullOrEmpty(cd?.Code);
                case CodeableConcept cc:
                    return cc?.Coding.Any(c => hasCode(c)) == true;
                default:
                    return false;
            }
        }

        private OperationOutcome callService(string canonical, string code = null, string system = null, string display = null,
                        Coding coding = null, CodeableConcept cc = null, bool? abstractAllowed = null)
        {
            var outcome = new OperationOutcome();

            try
            {
                outcome = _service.ValidateCode(canonical: canonical, code: code, system: system, display: display,
                                coding: coding, codeableConcept: cc, @abstract: abstractAllowed);
                foreach (var issue in outcome.Issue) issue.Location = new string[] { _path };
            }
            catch (TerminologyServiceException tse)
            {
                outcome.AddIssue($"Terminology service failed while validating code '{code}' (system '{system}'): {tse.Message}", Issue.TERMINOLOGY_SERVICE_FAILED, _path);
            }

            return outcome;
        }

    }
}
