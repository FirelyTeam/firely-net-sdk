using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Validation;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    public static class PocoValidationExtensions
    {
        public static void ValidateInvariants(this Resource resource, List<ValidationResult> result)
            => resource.ValidateInvariants(DotNetAttributeValidation.BuildContext(null), result);

        public static void ValidateInvariants(this Resource resource, ValidationContext context, List<ValidationResult> result)
        {
            OperationOutcome results = new OperationOutcome();
            resource.ValidateInvariants(context, results);
            foreach (var item in results.Issue)
            {
                if (item.Severity == OperationOutcome.IssueSeverity.Error
                    || item.Severity == OperationOutcome.IssueSeverity.Fatal)
                    result.Add(new ValidationResult(item.Details.Coding[0].Code + ": " + item.Details.Text));
            }
        }

        public static void ValidateInvariants(this Resource resource, OperationOutcome result)
            => resource.ValidateInvariants(DotNetAttributeValidation.BuildContext(new object()), result);

        public static void ValidateInvariants(this Resource resource, ValidationContext context, OperationOutcome result)
        {
            var res = resource as IElementConstraintContainer;
            if (res != null && res.InvariantConstraints != null && res.InvariantConstraints.Count > 0)
            {
                var tree = resource.ToTypedElement();
                foreach (var invariantRule in res.InvariantConstraints)
                {
                    ValidateInvariantRule(context, invariantRule, tree, result);
                }
            }
        }

        /// <summary>
        /// Perform the Invariant based validation for this rule
        /// </summary>
        /// <param name="invariantRule"></param>
        /// <param name="model"></param>
        /// <param name="result">The OperationOutcome that will have the validation results appended</param>
        /// <param name="context">Describes the context in which a validation check is performed.</param>
        /// <returns></returns>
        public static bool ValidateInvariantRule(ValidationContext context, ElementDefinition.ConstraintComponent invariantRule, ITypedElement model, OperationOutcome result)
        {
            string expression = invariantRule.Expression;
            try
            {
                // No FhirPath extension
                if (string.IsNullOrEmpty(expression))
                {
                    result.Issue.Add(new OperationOutcome.IssueComponent()
                    {
                        Code = OperationOutcome.IssueType.Invariant,
                        Severity = OperationOutcome.IssueSeverity.Warning,
                        Details = new CodeableConcept(null, invariantRule.Key, "Unable to validate without a FhirPath expression"),
                        Diagnostics = expression
                    });
                    return true;
                }

                // Ensure the FHIR extensions are registered
                Hl7.Fhir.FhirPath.ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

                if (model.Predicate(expression, new EvaluationContext(model)))
                    return true;

                result.Issue.Add(new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.Invariant,
                    Severity = OperationOutcome.IssueSeverity.Error,
                    Details = new CodeableConcept(null, invariantRule.Key, invariantRule.Human),
                    Diagnostics = expression
                });
                return false;
            }
            catch (Exception ex)
            {
                result.Issue.Add(new OperationOutcome.IssueComponent()
                {
                    Code = OperationOutcome.IssueType.Invariant,
                    Severity = OperationOutcome.IssueSeverity.Fatal,
                    Details = new CodeableConcept(null, invariantRule.Key, "FATAL: Unable to process the invariant rule: " + invariantRule.Key + " " + expression),
                    Diagnostics = String.Format("FhirPath: {0}\r\nError: {1}", expression, ex.Message)
                });
                return false;
            }
        }

        #region Obsolete members
        [Obsolete("Use ValidateInvariantRule(ValidationContext context, ConstraintComponent invariantRule, ITypedElement model, OperationOutcome result) instead. Obsolete since 2018-10-17")]
        public static bool ValidateInvariantRule(this Resource resource, ValidationContext context, ElementDefinition.ConstraintComponent invariantRule, IElementNavigator model, OperationOutcome result)
        {
            return ValidateInvariantRule(context, invariantRule, model.ToTypedElement(), result);
        }
        #endregion
    }
}
