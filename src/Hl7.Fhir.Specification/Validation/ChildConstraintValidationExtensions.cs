/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal static class ChildConstraintValidationExtensions
    {
        internal static OperationOutcome ValidateChildConstraints(this Validator validator, ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            if (!definition.HasChildren) return outcome;

            validator.Trace(outcome, "Start validation of inlined child constraints for '{0}'".FormatWith(definition.Path), Issue.PROCESSING_PROGRESS, instance);

            var matchResult = ChildNameMatcher.Match(definition, instance);

            if (matchResult.UnmatchedInstanceElements.Any())
            {
                var elementList = String.Join(",", matchResult.UnmatchedInstanceElements.Select(e => "'" + e.Name + "'"));
                validator.Trace(outcome, $"Encountered unknown child elements {elementList} for definition '{definition.Path}'",
                                Issue.CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN, instance);
            }

            //TODO: Give warnings for out-of order children.  Really? That's an xml artifact, no such thing in Json!

            outcome.Add(validator.ValidateCardinality(matchResult, instance));

            // Recursively validate my children
            foreach (Match match in matchResult.Matches)
            {
                foreach (IElementNavigator element in match.InstanceElements)
                {
                    outcome.Include(validator.Validate(element, match.Definition));
                }
            }

            return outcome;
        }

        internal static OperationOutcome ValidateCardinality(this Validator validator, MatchResult matchResult, IElementNavigator parent)
        {
            var outcome = new OperationOutcome();

            foreach (var match in matchResult.Matches)
            {
                var definition = match.Definition.Current;
                var occurs = match.InstanceElements.Count;
                var cardinality = Cardinality.FromElementDefinition(definition);

                if (definition.Min != null && definition.Max != null)
                {
                    if(!cardinality.InRange(occurs))
                        validator.Trace(outcome, $"Element '{match.Definition.PathName}' occurs {occurs} times, which is not within the specified cardinality of {cardinality.ToString()}",
                                Issue.CONTENT_ELEMENT_INCORRECT_OCCURRENCE, parent);
                }
                else
                    validator.Trace(outcome, "ElementDefinition does not specify cardinality", Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING, parent);
            }

            return outcome;
        }


    }
}
