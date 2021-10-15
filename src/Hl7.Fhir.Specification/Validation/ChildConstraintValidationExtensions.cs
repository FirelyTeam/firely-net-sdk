/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System;
using System.Linq;


namespace Hl7.Fhir.Validation
{
    internal static class ChildConstraintValidationExtensions
    {
        internal static OperationOutcome ValidateChildConstraints(this Validator validator, ElementDefinitionNavigator definition,
            ScopedNode instance, bool allowAdditionalChildren)
        {
            var outcome = new OperationOutcome();
            if (!definition.HasChildren) return outcome;

            validator.Trace(outcome, "Start validation of inlined child constraints for '{0}'".FormatWith(definition.Path), Issue.PROCESSING_PROGRESS, instance);

            // validate the type on the parent of children. If this is a reference type, it will follow that reference as well
            outcome.Add(validator.ValidateTypeReferences(definition.Current.Type, instance, validateProfiles: false));

            var matchResult = ChildNameMatcher.Match(definition, instance);

            if (matchResult.UnmatchedInstanceElements.Any() && !allowAdditionalChildren)
            {
                var elementList = String.Join(",", matchResult.UnmatchedInstanceElements.Select(e => "'" + e.Name + "'"));
                validator.Trace(outcome, $"Encountered unknown child elements {elementList} for definition '{definition.Path}'",
                                Issue.CONTENT_ELEMENT_HAS_UNKNOWN_CHILDREN, instance);
            }

            //TODO: Give warnings for out-of order children.  Really? That's an xml artifact, no such thing in Json!
            //(with the serializationrepresentationnav we could determine the source is xml and make order matter....)

            // Recursively validate my children
            foreach (var match in matchResult.Matches)
            {
                outcome.Add(validator.validateMatch(match, instance));
            }

            return outcome;
        }

        private static OperationOutcome validateMatch(this Validator validator, Match match, ScopedNode parent)
        {
            var outcome = new OperationOutcome();

            var definition = match.Definition.Current;

            if (definition.Min == null)
                validator.Trace(outcome, $"Element definition does not specify a 'min' value, which is required. Cardinality has not been validated",
                    Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING, parent);
            else if (definition.Max == null)
                validator.Trace(outcome, $"Element definition does not specify a 'max' value, which is required. Cardinality has not been validated",
                    Issue.PROFILE_ELEMENTDEF_CARDINALITY_MISSING, parent);

            IBucket bucket;

            var resolver = validator?.Settings?.ResourceResolver ??
                throw Error.Argument("Discriminator validation needs a ResourceResolver to be set in the ValidationSettings.");

            if (isExtension(definition))
            {
                outcome.Add(validateExtensionCardinality(validator, match, parent, definition, resolver));
            }

            static bool isExtension(ElementDefinition def)
                    => def.Type.FirstOrDefault()?.Code == "Extension";

            try
            {
                bucket = BucketFactory.CreateRoot(match.Definition, resolver, validator);
            }
            catch (NotImplementedException ni)
            {
                // Will throw if a non-supported slice type is encountered
                validator.Trace(outcome, ni.Message, Issue.UNSUPPORTED_SLICING_NOT_SUPPORTED, parent);
                return outcome;
            }
            catch (Exception others)
            {
                validator.Trace(outcome, others.Message, Issue.PROFILE_ELEMENTDEF_INCORRECT, parent);
                return outcome;
            }

            foreach (var element in match.InstanceElements)
            {
                var _ = bucket.Add(element);

                // For the "root" slice group (=the original core element that was sliced, not resliced)
                // any element that does not match is an error
                // Since the ChildNameMatcher currently does the matching, this will never go wrong
            }

            outcome.Add(bucket.Validate(validator, parent));

            return outcome;
        }

        private static OperationOutcome validateExtensionCardinality(Validator validator, Match match, ScopedNode parent, ElementDefinition definition, IResourceResolver resolver)
        {
            var outcome = new OperationOutcome();

            var groups = match.InstanceElements.GroupBy(instance => getStringValue(instance, "url"));

            foreach (var group in groups)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                var extension = group.Key is not null ? resolver.FindExtensionDefinition(group.Key) : null;
#pragma warning restore CS0618 // Type or member is obsolete

                // extension could not be resolved. This error will be raised somewhere else. Continue for now.
                if (extension is null) continue;

                var nav = ElementDefinitionNavigator.ForSnapshot(extension);
                nav.MoveToFirstChild();
                var cardinality = Cardinality.FromElementDefinition(nav.Current);

                if (!cardinality.InRange(group.Count()))
                {
                    validator.Trace(outcome, $"Instance count for extension '{extension.Name}' ('{definition.Path}') is {group.Count()}, which is not within the specified cardinality of {cardinality}",
                                            Issue.CONTENT_INCORRECT_OCCURRENCE, parent);
                }
            }

            return outcome;

            static string? getStringValue(ITypedElement instance, string childName) =>
                instance.Children(childName).Select(s => s.Value).OfType<string>().SingleOrDefault();
        }
    }
}