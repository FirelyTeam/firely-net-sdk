/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Schema
{
    internal class Binding : IValidatable, IAssertion
    {
        public enum BindingStrength
        {
            /// <summary>
            /// To be conformant, instances of this element SHALL include a code from the specified value set.<br/>
            /// (system: http://hl7.org/fhir/binding-strength)
            /// </summary>
            [EnumLiteral("required", "http://hl7.org/fhir/binding-strength"), Description("Required")]
            Required,

            /// <summary>
            /// To be conformant, instances of this element SHALL include a code from the specified value set if any of the codes within the value set can apply to the concept being communicated.  If the valueset does not cover the concept (based on human review), alternate codings (or, data type allowing, text) may be included instead.<br/>
            /// (system: http://hl7.org/fhir/binding-strength)
            /// </summary>
            [EnumLiteral("extensible", "http://hl7.org/fhir/binding-strength"), Description("Extensible")]
            Extensible,

            /// <summary>
            /// Instances are encouraged to draw from the specified codes for interoperability purposes but are not required to do so to be considered conformant.<br/>
            /// (system: http://hl7.org/fhir/binding-strength)
            /// </summary>
            [EnumLiteral("preferred", "http://hl7.org/fhir/binding-strength"), Description("Preferred")]
            Preferred,

            /// <summary>
            /// Instances are not expected or even encouraged to draw from the specified value set.  The value set merely provides examples of the types of concepts intended to be included.<br/>
            /// (system: http://hl7.org/fhir/binding-strength)
            /// </summary>
            [EnumLiteral("example", "http://hl7.org/fhir/binding-strength"), Description("Example")]
            Example,
        }

        public readonly string ValueSetUri;
        public readonly BindingStrength Strength;
        public readonly string Description;
        public readonly bool AbstractAllowed;
        public readonly string Context;

        public Binding(string valueSetUri, BindingStrength strength, bool abstractAllowed = true, string description = null, string context = null)
        {
            ValueSetUri = valueSetUri ?? throw Error.ArgumentNull(nameof(valueSetUri));
            Strength = strength;
            Description = description;
            AbstractAllowed = abstractAllowed;
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="vc"></param>
        /// <returns></returns>
        /// <remarks>ValidationContext must have TerminologyService set.</remarks>
        public OperationOutcome Validate(ITypedElement input, ValidationContext vc)
        {
            if (input == null) throw Error.ArgumentNull(nameof(input));
            if (vc?.TerminologyService == null) throw new InvalidValidationContextException($"ValidationContext should have its {nameof(ValidationContext.TerminologyService)} property set.");
            if (input.InstanceType == null) throw Error.Argument(nameof(input), "Binding validation requires input to have an instance type.");

            // This would give informational messages even if the validation was run on a choice type with a binding, which is then
            // only applicable to an instance which is bindable. So instead of a warning, we should just return as validation is
            // not applicable to this instance.
            //if (!ModelInfo.IsBindable(input.InstanceType))
            //{
            //    return Issue.CONTENT_TYPE_NOT_BINDEABLE 
            //        .NewOutcomeWithIssue($"Validation of binding with non-bindable instance type '{input.InstanceType}' always succeeds.", input);
            //}
            if (!ModelInfo.IsBindable(input.InstanceType))
                return new OperationOutcome();  // success

            var bindable = parseBindable(input);
            var outcome = VerifyContentRequirements(input, bindable);
            if (!outcome.Success) return outcome;

            return TaskHelper.Await(() => ValidateCode(input, bindable, vc));
        }

        private static Element parseBindable(ITypedElement input)
        {
            var bindable = input.ParseBindable();
            if (bindable == null)    // should never happen, since we already checked IsBindable
                throw Error.NotSupported($"Type '{input.InstanceType}' is bindable, but could not be parsed by ParseBindable().");

            return bindable;
        }

        internal async Task<OperationOutcome> ValidateCode(ITypedElement source, Element bindable, ValidationContext vc)
        {
            OperationOutcome outcome;

            switch (bindable)
            {
                case Code co:
                    outcome = await callService(vc.TerminologyService, source.Location, ValueSetUri, co?.Value, system: null, display: null, abstractAllowed: AbstractAllowed, context: Context).ConfigureAwait(false);
                    break;
                case Coding cd:
                    outcome = await callService(vc.TerminologyService, source.Location, ValueSetUri, coding: cd, abstractAllowed: AbstractAllowed, context: Context).ConfigureAwait(false);
                    break;
                case CodeableConcept cc:
                    outcome = await callService(vc.TerminologyService, source.Location, ValueSetUri, cc: cc, abstractAllowed: AbstractAllowed, context: Context).ConfigureAwait(false);
                    break;
                default:
                    throw Error.InvalidOperation($"Parsed bindable was of unexpected instance type '{bindable.TypeName}'.");
            }

            //EK 20170605 - disabled inclusion of warnings/errors for all but required bindings since this will 
            // 1) create superfluous messages (both saying the code is not valid) coming from the validateResult + the outcome.AddIssue() 
            // 2) add the validateResult as warnings for preferred bindings, which are confusing in the case where the slicing entry is 
            //    validating the binding against the core and slices will refine it: if it does not generate warnings against the slice, 
            //    it should not generate warnings against the slicing entry.
            return Strength == BindingStrength.Required ? outcome : new OperationOutcome();
        }

        private async Task<OperationOutcome> callService(ITerminologyService svc, string location, string canonical, string code = null, string system = null, string display = null,
                Coding coding = null, CodeableConcept cc = null, bool? abstractAllowed = null, string context = null)
        {
            try
            {                
                var parameters = new ValidateCodeParameters()
                    .WithValueSet(canonical)
                    .WithCode(code: code, system: system, display: display, context: context)
                    .WithCoding(coding)
                    .WithCodeableConcept(cc)
                    .WithAbstract(abstractAllowed)
                    .Build();


                var outcome = (await svc.ValueSetValidateCode(parameters).ConfigureAwait(false)).ToOperationOutcome();
                foreach (var issue in outcome.Issue) issue.Location = new string[] { location };
                return outcome;
            }
            catch (FhirOperationException tse)
            {
                string message = (cc?.Coding == null || cc.Coding.Count == 1) 
                    ? $"Terminology service failed while validating code '{code ?? coding?.Code ?? cc?.Coding[0]?.Code}' (system '{system ?? coding?.System ?? cc?.Coding[0]?.System}'): {tse.Message}"
                    : $"Terminology service failed while validating the codes: {tse.Message}";            

                return Issue.TERMINOLOGY_SERVICE_FAILED
                        .NewOutcomeWithIssue(message, location);
            }
        }


        /// <summary>
        /// Validates whether the instance has the minimum required coded content, depending on the binding.
        /// </summary>
        /// <remarks>Will throw an <c>InvalidOperationException</c> when the input is not of a bindeable type.</remarks>
        internal OperationOutcome VerifyContentRequirements(ITypedElement source, Element bindable)
        {
            switch (bindable)
            {
                // Note: parseBindable with translate all bindable types to just code/Coding/CodeableConcept,
                // so that's all we need to expect here.
                case Code co when String.IsNullOrEmpty(co.Value) && Strength == BindingStrength.Required:
                case Coding cd when String.IsNullOrEmpty(cd.Code) && Strength == BindingStrength.Required:
                case CodeableConcept cc when !codeableConceptHasCode(cc) && Strength == BindingStrength.Required:
                    return Issue.TERMINOLOGY_NO_CODE_IN_INSTANCE
                        .NewOutcomeWithIssue($"No code found in {source.InstanceType} with a required binding.", source);
                case CodeableConcept cc when !codeableConceptHasCode(cc) && String.IsNullOrEmpty(cc.Text) &&
                                Strength == BindingStrength.Extensible:
                    return Issue.TERMINOLOGY_NO_CODE_IN_INSTANCE
                        .NewOutcomeWithIssue($"Extensible binding requires code or text.", source);
                default:
                    return new OperationOutcome();      // nothing wrong then
            }
        }

        private bool codeableConceptHasCode(CodeableConcept cc) =>
            cc.Coding.Any(cd => !String.IsNullOrEmpty(cd.Code));


        public JToken ToJson()
        {
            var props = new JObject(
                    new JProperty("valueSet", ValueSetUri),
                    new JProperty("strength", Strength.GetLiteral()),
                    new JProperty("abstractAllowed", AbstractAllowed));
            if (Description != null)
                props.Add(new JProperty("description", Description));

            return new JProperty("binding", props);
        }
    }
}
