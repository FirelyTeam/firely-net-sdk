/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class CompiledConstraintAnnotation
    {
        public CompiledExpression Expression;
    }

    internal static class FpConstraintValidationExtensions
    {
        public static OperationOutcome ValidateFp(this Validator v, string structureDefinitionUrl, ElementDefinition definition, ScopedNode instance)
        {
            var outcome = new OperationOutcome();

            if (!definition.Constraint.Any()) return outcome;
            if (v.Settings.SkipConstraintValidation) return outcome;

            foreach (var constraintElement in definition.Constraint)
            {
                // 20190703 Issue 447 - rng-2 is incorrect in DSTU2 and STU3. EK
                // should be removed from STU3/R4 once we get the new normative version
                // of FP up, which could do comparisons between quantities.
                if (constraintElement.Key == "rng-2") continue;

                if (constraintElement.GetBoolExtension("http://hl7.org/fhir/StructureDefinition/elementdefinition-bestpractice") == true)
                    if (v.Settings.ConstraintBestPractices == ConstraintBestPractices.Ignore)
                        continue;
                    else if (v.Settings.ConstraintBestPractices == ConstraintBestPractices.Enabled)
                        constraintElement.Severity = ElementDefinition.ConstraintSeverity.Error;
                    else if (v.Settings.ConstraintBestPractices == ConstraintBestPractices.Disabled)
                        constraintElement.Severity = ElementDefinition.ConstraintSeverity.Warning;

                bool success = false;

                try
                {
                    var compiled = getExecutableConstraint(v, outcome, instance, constraintElement);
                    success = compiled.Predicate(instance,
                        new FhirEvaluationContext(instance)
                        { ElementResolver = callExternalResolver });
                }
                catch (Exception e)
                {
                    v.Trace(outcome, $"Evaluation of FhirPath for constraint '{constraintElement.Key}' failed: {e.Message}",
                                    Issue.PROFILE_ELEMENTDEF_INVALID_FHIRPATH_EXPRESSION, instance);
                }

                if (!success)
                {
                    var text = "Instance failed constraint " + constraintElement.ConstraintDescription();
                    var issue = constraintElement.Severity == ElementDefinition.ConstraintSeverity.Error ?
                        Issue.CONTENT_ELEMENT_FAILS_ERROR_CONSTRAINT : Issue.CONTENT_ELEMENT_FAILS_WARNING_CONSTRAINT;

                    // just use the constraint description in the error message, as this is to explain the issue
                    // to a human, the code for the error should be in the coding
                    var outcomeIssue = new OperationOutcome.IssueComponent()
                    {
                        Severity = issue.Severity,
                        Code = issue.Type,
                        Details = issue.ToCodeableConcept(text),
                        Diagnostics = constraintElement.GetFhirPathConstraint(), // Putting the fhirpath expression of the invariant in the diagnostics
                        Location = new string[] { instance.Location }
                    };
                    outcomeIssue.Details.Coding.Add(new Coding(structureDefinitionUrl, constraintElement.Key, constraintElement.Human));
                    outcome.AddIssue(outcomeIssue);
                }
            }

            return outcome;

            ITypedElement callExternalResolver(string url)
            {
                OperationOutcome o = new OperationOutcome();
                var result = v.ExternalReferenceResolutionNeeded(url, o, "dummy");

                if (o.Success && result != null) return result;

                return null;
            }
        }


        private static CompiledExpression getExecutableConstraint(Validator v, OperationOutcome outcome, ITypedElement instance,
                        ElementDefinition.ConstraintComponent constraintElement)
        {
            var compiledExpression = constraintElement.Annotation<CompiledConstraintAnnotation>()?.Expression;

            if (compiledExpression == null)
            {
                var fpExpressionText = constraintElement.GetFhirPathConstraint();

                if (fpExpressionText != null)
                {
                    try
                    {
                        compiledExpression = v.FpCompiler.Compile(fpExpressionText);
                        constraintElement.SetAnnotation(new CompiledConstraintAnnotation { Expression = compiledExpression });

                    }
                    catch (Exception e)
                    {
                        v.Trace(outcome, $"Compilation of FhirPath for constraint '{constraintElement.Key}' failed: {e.Message}",
                                        Issue.PROFILE_ELEMENTDEF_INVALID_FHIRPATH_EXPRESSION, instance);
                    }
                }
                else
                    v.Trace(outcome, $"Encountered an invariant ({constraintElement.Key}) that has no FhirPath expression, skipping validation of this constraint",
                            Issue.UNSUPPORTED_CONSTRAINT_WITHOUT_FHIRPATH, instance);
            }

            return compiledExpression;
        }
    }
}
