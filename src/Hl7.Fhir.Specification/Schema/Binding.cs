using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal class Binding : IValidatable, IAssertion
    {
        /// <summary>
        /// Instance type is not one of the bindable types
        /// </summary>
        public const int TYPE_NOT_BINDEABLE = 1025;

        /// <summary>
        /// Bindable element does not have enough coded content (codes, text).
        /// </summary>
        /// <remarks>Depends on binding strength.</remarks>
        public const int INSUFFICIENT_CODED_CONTENT = 6100;


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
            if (input.InstanceType == null) throw Error.Argument(nameof(input), "Binding validation requires input to have an instance type.");

            if (!IsBindable(input.InstanceType))
            {
                // TODO: Find a way to conditionally evaluate this depending on tracing level
                return new ValidationOutcome(this, ValidationResult.Valid, input,
                    ValidationDetail.Single(TYPE_NOT_BINDEABLE, Weight.Note, $"Non-bindable instance type '{input.InstanceType}'"));
            }

            var outcome = VerifyContentRequirements(input);
            if (outcome.Result != ValidationResult.Valid) return outcome;

            return ValidateCode(input, vc);
        }

        private static Element parseBindable(ITypedElement input)
        {
            var bindable = input.ParseBindable();
            if (bindable == null)    // should never happen, since we already checked IsBindable
                throw Error.NotSupported($"Type '{input.InstanceType}' is bindable, but could not be parsed by ParseBindable().");

            return bindable;
        }

        internal ValidationOutcome ValidateCode(ITypedElement input, ValidationContext vc)
        {
            var bindable = parseBindable(input);

            ValidationOutcome outcome;
            return outcome;
            //switch (bindable)
            //{
            //    case Code co:
            //        outcome = callService(uri, co?.Value, system: null, display: null, abstractAllowed: abstractAllowed);
            //        break;
            //    case Coding cd:
            //        outcome = callService(uri, coding: cd, abstractAllowed: abstractAllowed);
            //        break;
            //    case CodeableConcept cc:
            //        outcome = callService(uri, cc: cc, abstractAllowed: abstractAllowed);
            //        break;
            //    default:
            //        throw Error.NotSupported($"Validating a binding against a '{bindable.TypeName}' is not supported in FHIR.");
            //}

            ////EK 20170605 - disabled inclusion of warnings/erros for all but required bindings since this will 
            //// 1) create superfluous messages (both saying the code is not valid) coming from the validateResult + the outcome.AddIssue() 
            //// 2) add the validateResult as warnings for preferred bindings, which are confusing in the case where the slicing entry is 
            ////    validating the binding against the core and slices will refine it: if it does not generate warnings against the slice, 
            ////    it should not generate warnings against the slicing entry.
            //if (Strength == BindingStrength.Required)
            //    return outcome;
            //else
            //    return new OperationOutcome();
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

        /// <summary>
        /// Validates whether the instance has the minimum required coded content, depending on the binding.
        /// </summary>
        internal ValidationOutcome VerifyContentRequirements(ITypedElement input)
        {
            var bindable = parseBindable(input);

            switch (bindable)
            {
                case Code co when String.IsNullOrEmpty(co.Value) && Strength == BindingStrength.Required:
                case Coding cd when String.IsNullOrEmpty(cd.Code) && Strength == BindingStrength.Required:
                case CodeableConcept cc when !codeableConceptHasCode(cc) && Strength == BindingStrength.Required:
                case Quantity q when String.IsNullOrEmpty(q.Code) && Strength == BindingStrength.Required:
                case FhirString fs when String.IsNullOrEmpty(fs.Value) && Strength == BindingStrength.Required:
                case FhirUri fu when String.IsNullOrEmpty(fu.Value) && Strength == BindingStrength.Required:
                    return new ValidationOutcome(this, ValidationResult.Invalid, input,
                        ValidationDetail.Single(INSUFFICIENT_CODED_CONTENT, Weight.Error,
                        $"No code found in {input.InstanceType} with a required binding."));
                case CodeableConcept cc when !codeableConceptHasCode(cc) && String.IsNullOrEmpty(cc.Text) && Strength == BindingStrength.Extensible:
                    return new ValidationOutcome(this, ValidationResult.Invalid, input,
                        ValidationDetail.Single(INSUFFICIENT_CODED_CONTENT, Weight.Error,
                        $"Extensible binding requires code or text."));
                default:
                    return false;

                // TODO: This list differs from STU to STU (i.e. canonical). Fix.
            }
        }

        private bool codeableConceptHasCode(CodeableConcept cc) =>
            cc.Coding.Any(cd => !String.IsNullOrEmpty(cd.Code));

        //private OperationOutcome callService(string canonical, string code = null, string system = null, string display = null,
        //                Coding coding = null, CodeableConcept cc = null, bool? abstractAllowed = null)
        //{
        //    var outcome = new OperationOutcome();

        //    try
        //    {
        //        outcome = _service.ValidateCode(canonical: canonical, code: code, system: system, display: display,
        //                        coding: coding, codeableConcept: cc, @abstract: abstractAllowed);
        //        foreach (var issue in outcome.Issue) issue.Location = new string[] { _path };
        //    }
        //    catch (TerminologyServiceException tse)
        //    {
        //        outcome.AddIssue($"Terminology service failed while validating code '{code}' (system '{system}'): {tse.Message}", Issue.TERMINOLOGY_SERVICE_FAILED, _path);
        //    }

        //    return outcome;
        //}

    }
}
