using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Schema
{
    internal class Binding : IValidatable, IExceptionSource
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

        public ExceptionNotificationHandler ExceptionHandler { get; set; }

        public Binding(string valueSetUri, BindingStrength strength, bool abstractAllowed = true, string description = null)
        {
            ValueSetUri = valueSetUri ?? throw Error.ArgumentNull(nameof(valueSetUri));
            Strength = strength;
            Description = description;
            AbstractAllowed = abstractAllowed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="vc"></param>
        /// <returns></returns>
        /// <remarks>ValidationContext must have TerminologyService set.</remarks>
        public ValidationOutcome Validate(ITypedElement input, ValidationContext vc)
        {
            if (input == null) throw Error.ArgumentNull(nameof(input));
            if (vc?.TerminologyService == null) throw new InvalidValidationContextException($"ValidationContext should have its {nameof(ValidationContext.TerminologyService)} property set.");

            ExceptionHandler = vc?.ExceptionSink?.ExceptionHandler;

            var outcome = VerifyIsBindable(input.InstanceType);
            if (!outcome.IsSuccessful) return outcome;

            outcome = VerifyMinimalRequirements(input, Strength);
            if (!outcome.IsSuccessful) return outcome;

            // Binds to a value set if this element is coded (code, Coding, CodeableConcept, Quantity), or the data types (string, uri).

            return validateCode(input);
        }

        private ValidationOutcome validateCode()
        {
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

        public static bool IsBindable(string instanceType)
        {
            switch (instanceType)
            {
                case "code":
                case "Coding":
                case "CodeableConcept":
                case "Quantity":
                case "string":
                case "uri":
                    return true;
                default:
                    return false;
            }
        }

        public static ValidationOutcome ValidateIsBindable(ITypedElement input, ValidationContext vc)
        {
            return null;
            //if (input.InstanceType is null) return / throw;

            //if(!IsBindable(input.InstanceType))
        }

        public static ValidationOutcome VerifyMinimalRequirements(ITypedElement input, BindingStrength strength)
        {
            var isCCWithJustText = bindable is CodeableConcept cc && !String.IsNullOrEmpty(cc.Text);
            if (isCCWithJustText)
                return new OperationOutcome();   // OK
            return Issue.TERMINOLOGY_NO_CODE_IN_INSTANCE.NewOutcomeWithIssue($"No code found in instance.", _path);

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
