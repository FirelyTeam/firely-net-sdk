/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Validation
{
    internal static class BindingValidationExtensions
    {
        internal static OperationOutcome ValidateBinding(this Validator me, ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.Binding != null)
            {
                var binding = definition.Binding;

                if (binding.ValueSet == null)
                {
                    me.Trace(outcome, $"Encountered a binding element without a ValueSet reference", Issue.PROFILE_BINDING_WITHOUT_VALUESET, instance);
                    return outcome;
                }

                var uri = (binding.ValueSet as FhirUri)?.Value;

                if (uri != null)
                {
                    me.Trace(outcome, $"Binding element references a valueset by uri ({uri}), which cannot be used to validate a binding",
                                Issue.UNSUPPORTED_URI_BINDING_NOT_SUPPORTED, instance);
                    return outcome;
                }

                uri = (binding.ValueSet as ResourceReference).Reference;
                var shouldCheck = binding.Strength == BindingStrength.Required || binding.Strength == BindingStrength.Preferred;
                var ts = me.Settings.TerminologyService;

                if (ts == null)
                {
                    if (me.Settings.ResourceResolver == null)
                    {
                        me.Trace(outcome, $"Cannot resolve binding reference '{uri}' since neither TerminologyService nor ResourceResolver is given in the settings",
                            Issue.UNAVAILABLE_TERMINOLOGY_SERVER, instance);
                        return outcome;
                    }

                    ts = new LocalTerminologyServer(me.Settings.ResourceResolver);
                }

                try
                {
                    OperationOutcome validateResult = ts.ValidateCode(uri, "code", "system", "display");
                    var codeLabel = $"Code '{"code"}' from system '{"system"}'";

                    if (!validateResult.Success)
                    {
                        if (binding.Strength == BindingStrength.Required)
                            me.Trace(outcome, $"{codeLabel} is not a valid value for required binding to valueset '{uri}'", Issue.CONTENT_INVALID_FOR_REQUIRED_BINDING, instance);
                        else
                            me.Trace(outcome, $"{codeLabel} is not valid in non-required binding to valueset '{uri}'", Issue.CONTENT_INVALID_FOR_NON_REQUIRED_BINDING, instance);
                    }
                }
                catch (Exception e)
                {
                    me.Trace(outcome, $"Terminology failed while validating code X (system Y): {e.Message}", Issue.UNAVAILABLE_VALIDATE_CODE_FAILED, instance);
                    return outcome;
                }
            }

            return outcome;
        }
    }
}
