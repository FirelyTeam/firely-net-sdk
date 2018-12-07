/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
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
        public static OperationOutcome ValidateFp(this Validator v, ElementDefinition definition, ScopedNode instance)
        {
            var outcome = new OperationOutcome();

            if (!definition.Constraint.Any()) return outcome;
            if (v.Settings.SkipConstraintValidation) return outcome;

            var context = instance.ResourceContext;

            foreach (var constraintElement in definition.Constraint)
            {
                bool success = false;
               
                try
                {
                    var compiled = getExecutableConstraint(v, outcome, instance, constraintElement);
                    success = compiled.Predicate(instance, new FhirEvaluationContext(context) { ElementResolver = callExternalResolver } );
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

                    v.Trace(outcome, text, issue, instance);
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
                        constraintElement.AddAnnotation(new CompiledConstraintAnnotation { Expression = compiledExpression });
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
