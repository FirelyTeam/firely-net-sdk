/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
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
                return new OperationOutcome().AddIssue($"Encountered a binding element without a binding strength", Issue.PROFILE_INCOMPLETE_BINDING, _path);

            var uri = (binding.ValueSet as FhirUri)?.Value ??
                        (binding.ValueSet as ResourceReference)?.Reference;

            if (uri == null)
                return new OperationOutcome().AddIssue($"Encountered a binding element without either a ValueSet reference or uri", Issue.PROFILE_INCOMPLETE_BINDING, _path);
            
            switch(bindable)
            {
                case Code co:
                    return validateBinding(co, uri, binding.Strength);
                case Coding cd:
                    return validateBinding(cd, uri, binding.Strength);
                case CodeableConcept cc:
                    return validateBinding(cc, uri, binding.Strength);
                default:
                    throw Error.NotSupported($"Validating a binding against a '{bindable.TypeName}' is not supported in FHIR.");
            }
        }


        private OperationOutcome validateBinding(Code code, string valueSetUri, BindingStrength? strength = null)
        {
            return callService(code.Value, system: null, display: null, uri: valueSetUri, strength: strength);
        }

        private OperationOutcome validateBinding(Coding coding, string valueSetUri, BindingStrength? strength = null)
        {
            return callService(coding.Code, coding.System, coding.Display, valueSetUri, strength);
        }

        private OperationOutcome validateBinding(CodeableConcept concept, string valueSetUri, BindingStrength? strength = null)
        {
            var outcome = new OperationOutcome();

            // Maybe just a text, but if there are no codings, that's a positive result
            if (!concept.Coding.Any()) return outcome;

            // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
            if (concept.Coding.Count == 1)
                return validateBinding(concept.Coding.Single(), valueSetUri, strength);

            // Else, look for one succesful match in any of the codes in the CodeableConcept
            var callResults = concept.Coding.Select(coding => validateBinding(coding, valueSetUri, strength));
            var successOutcome = callResults.Where(r => r.Success).OrderBy(oo => oo.Warnings).FirstOrDefault();

            if (successOutcome == null)
            {
                outcome.AddIssue("None of the Codings in the CodeableConcept were valid for the binding. Details follow.", Issue.CONTENT_INVALID_FOR_REQUIRED_BINDING, _path);
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


        private OperationOutcome callService(string code, string system, string display, string uri, BindingStrength? strength)
        {
            var outcome = new OperationOutcome();

            if (string.IsNullOrEmpty(code))
            {
                //HACK: we only can see the empty code element this low in the call tree that we can just report success
                //from here. In fact, we should not even go into validating a binding at all if the instance data does
                //not provide anything bindeable.
                return outcome;
            }

            try
            {
                var validateResult = _service.ValidateCode(uri, code, system, display, abstractAllowed: false);
                foreach (var issue in validateResult.Issue) issue.Location = new string[] { _path };

                //EK 20170605 - disabled inclusion of warnings/erros for all but required bindings since this will 
                // 1) create superfluous messages (both saying the code is not valid) coming from the validateResult + the outcome.AddIssue() 
                // 2) add the validateResult as warnings for preferred bindings, which are confusing in the case where the slicing entry is 
                //    validating the binding against the core and slices will refine it: if it does not generate warnings against the slice, 
                //    it should not generate warnings against the slicing entry.
                if (strength == BindingStrength.Required)
                    outcome.Include(validateResult);
            }
            catch (TerminologyServiceException tse)
            {
                outcome.AddIssue($"Terminology service failed while validating code '{code}' (system '{system}'): {tse.Message}", Issue.TERMINOLOGY_SERVICE_FAILED, _path);
            }

            return outcome;
        }

    }
}
