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
    internal static class ChildConstraintExtensions
    {
        internal static OperationOutcome ValidateChildConstraints(this Validator validator, ElementDefinitionNavigator definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();
            if (!definition.HasChildren) return outcome;

            outcome.Info("Start validation of inlined child constraints for '{0}'".FormatWith(definition.Path), Issue.PROCESSING_PROGRESS, instance);

            var matchResult = ChildNameMatcher.Match(definition, instance);

            // If this is an abstract class, we have only a minimal definition, since concrete subclasses
            // may have introduced additional members...
            bool mayHaveUnknownChildren = false;

            if (outcome.Verify(() => definition.StructureDefinition != null, "Navigator does not provide information about the StructureDefinition - cannot determin whether structure is abstract",
                    Issue.UNAVAILABLE_ELEMENTDEF_WITHOUT_STRUCTDEF, instance))
            {
                mayHaveUnknownChildren = definition.StructureDefinition.Abstract != false;
            }

            outcome.Verify(() => mayHaveUnknownChildren || !matchResult.UnmatchedInstanceElements.Any(), "Encountered unknown child elements {0}".
                            FormatWith(String.Join(",", matchResult.UnmatchedInstanceElements.Select(e => "'" + e.Name + "'"))),
                            Issue.CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN, instance);

            //TODO: Give warnings for out-of order children.  Really? That's an xml artifact, no such thing in Json!

            outcome.Add(validator.ValidateCardinality(matchResult, instance));

            // Recursively validate my children
            foreach (Match match in matchResult.Matches)
            {
                foreach (IElementNavigator element in match.InstanceElements)
                {
                    outcome.Include(validator.ValidateElement(match.Definition, element));
                }
            }

            return outcome;
        }

        internal static OperationOutcome ValidateCardinality(this Validator validator, MatchResult matchResult, INamedNode parent)
        {
            var outcome = new OperationOutcome();

            foreach (var match in matchResult.Matches)
            {
                var definition = match.Definition.Current;
                var occurs = match.InstanceElements.Count;
                var cardinality = Cardinality.FromElementDefinition(definition);

                if (outcome.Verify(() => definition.Min != null && definition.Max != null, "ElementDefinition does not specify cardinality", Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING, parent))
                {
                    outcome.Verify(() => cardinality.InRange(occurs), "Element '{0}' occurs {1} times, which is not within the specified cardinality of {2}"
                                .FormatWith(match.Definition.PathName, occurs, cardinality.ToString()), Issue.CONTENT_ELEMENT_INCORRECT_OCCURRENCE, parent);
                }
            }

            return outcome;
        }


    }
}
