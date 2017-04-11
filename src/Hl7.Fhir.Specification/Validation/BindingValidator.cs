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


        public OperationOutcome ValidateBinding(Coding coding, string valueSetUri, BindingStrength? strength=null)
        {
            return callService(coding.Code, coding.System, coding.Display, valueSetUri, strength, _path);
        }

        public OperationOutcome ValidateBinding(CodeableConcept concept, string valueSetUri, BindingStrength? strength=null)
        {
            var outcome = new OperationOutcome();

            // Maybe just a text, but if there are no codings, that's a positive result
            if (!concept.Coding.Any()) return outcome;

            // If we have just 1 coding, we better handle this using the simpler version of ValidateBinding
            if (concept.Coding.Count == 1)
                return ValidateBinding(concept.Coding.Single(), valueSetUri, strength);

            // Else, look for one succesful match in any of the codes in the CodeableConcept
            var callResults = concept.Coding.Select(coding => ValidateBinding(coding, valueSetUri, strength));
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


        private OperationOutcome callService(string code, string system, string display, string uri, BindingStrength? strength, string path)
        {
            var outcome = new OperationOutcome();

            OperationOutcome validateResult = _service.ValidateCode(uri, code, system, display, abstractAllowed: false);
            var codeLabel = $"Code '{code}' from system '{system}'";
            if (display != null) codeLabel += $" with display '{display}'";

            if (validateResult.Where(type: OperationOutcome.IssueType.NotSupported).Any())
            {
                if (strength != BindingStrength.Example)
                {
                    outcome.AddIssue($"The terminology service is incapable of validating {codeLabel} (valueset '{uri}').", Issue.UNSUPPORTED_BINDING_NOT_SUPPORTED_BY_SERVICE, path);
                    validateResult.MakeInformational();
                    outcome.Include(validateResult);
                }
                return outcome;
            }

            if (!validateResult.Success)
            {
                if (strength == BindingStrength.Required)
                {
                    outcome.AddIssue($"{codeLabel} is not valid for required binding to valueset '{uri}'", Issue.CONTENT_INVALID_FOR_REQUIRED_BINDING, path);
                }
                else if(strength != BindingStrength.Example)
                {
                    outcome.AddIssue($"{codeLabel} is not valid for non-required binding to valueset '{uri}'", Issue.CONTENT_INVALID_FOR_NON_REQUIRED_BINDING, path);
                }

                validateResult.MakeInformational();             
            }

            outcome.Include(validateResult);
            return outcome;
        }


        public OperationOutcome ValidateBinding(IElementNavigator instance, ElementDefinition definition)
        {
            var outcome = new OperationOutcome();

            if (definition.Binding != null)
            {
                var binding = definition.Binding;

                if (binding.ValueSet != null)
                {
                    var uri = (binding.ValueSet as FhirUri)?.Value;

                    // == null, so we check whether this could NOT be casted to a FhirUri, thus is a ValueSet reference
                    if (uri == null)
                    {
                        uri = (binding.ValueSet as ResourceReference).Reference;

                        var codedType = Validator.DetermineType(definition, instance);
                        if (codedType != null)
                        {
                            if (codedType.Value.IsBindeableFhirType())
                            {
                                var bindable = instance.ParseBindable(codedType.Value);

                                if (bindable is Coding)
                                    return ValidateBinding(bindable as Coding, uri, binding.Strength);
                                else
                                    return ValidateBinding(bindable as CodeableConcept, uri, binding.Strength);
                            }
                            else
                                outcome.AddIssue($"A binding is given ('{uri}'), but the instance data is of type '{codedType.Value}', which is not bindeable.", Issue.CONTENT_TYPE_NOT_BINDEABLE, instance);
                        }
                        else
                            outcome.AddIssue($"Cannot determine type of data in instance to extract code/system information", Issue.CONTENT_ELEMENT_CANNOT_DETERMINE_TYPE, instance);
                    }
                    else
                        outcome.AddIssue($"Binding element references a valueset by uri ({uri}), which cannot be used to validate a binding",
                                Issue.UNSUPPORTED_URI_BINDING_NOT_SUPPORTED, instance);
                }
                else
                    outcome.AddIssue($"Encountered a binding element without a ValueSet reference", Issue.PROFILE_BINDING_WITHOUT_VALUESET, instance);
            }

            return outcome;
        }
    }
}
