using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation.Impl;
using Hl7.Fhir.Validation.Schema;
using System;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    internal class DiscriminatorFactory
    {
        public static IAssertion Build(ElementDefinitionNavigator root, ElementDefinition.DiscriminatorComponent discriminator,
            IAsyncResourceResolver resolver, IElementDefinitionAssertionFactory assertionFactory)
        {
            if (discriminator?.Type == null) throw new ArgumentNullException(nameof(discriminator), "Encountered a discriminator component without a discriminator type.");
            if (resolver == null) throw Error.ArgumentNull(nameof(resolver));

            var condition = walkToCondition(root, discriminator.Path, resolver);
            var location = root.Current.Path;

            var discrimatorAssertion = discriminator.Type.Value switch
            {
                ElementDefinition.DiscriminatorType.Value => buildCombinedDiscriminator("value", condition.Current, assertionFactory),
                ElementDefinition.DiscriminatorType.Pattern => buildCombinedDiscriminator("pattern", condition.Current, assertionFactory),
                ElementDefinition.DiscriminatorType.Type => buildTypeDiscriminator(condition.Current, assertionFactory),
                ElementDefinition.DiscriminatorType.Profile => buildProfileDiscriminator(condition.Current, assertionFactory),
                ElementDefinition.DiscriminatorType.Exists => buildExistsDiscriminator(condition.Current, assertionFactory),
                _ => throw Error.NotImplemented($"Found a slice discriminator of type '{discriminator.Type.Value.GetLiteral()}' at '{location}' which is not yet supported by this validator."),
            };

            return new PathSelectorAssertion(discriminator.Path, discrimatorAssertion);
        }

        private static IAssertion buildExistsDiscriminator(ElementDefinition current, IElementDefinitionAssertionFactory assertionFactory)
        {
            return ResultAssertion.Success;
        }

        private static IAssertion buildCombinedDiscriminator(string name, ElementDefinition spec, IElementDefinitionAssertionFactory assertionFactory)
        {
            if (spec.Fixed == null && spec.Binding == null && spec.Pattern == null)
                throw new IncorrectElementDefinitionException($"The {name} discriminator should have a 'fixed[x]', 'pattern[x]' or binding element set on '{spec.ElementId}'.");

            return spec.ValueSlicingConditions(assertionFactory);
        }

        private static IAssertion buildTypeDiscriminator(ElementDefinition spec, IElementDefinitionAssertionFactory assertionFactory)
        {
            var codes = spec.Type.Select(tr => tr.Code).ToArray();

            if (codes.Any())
                return new AnyAssertion(codes.Select(c => new FhirTypeLabel(c)));
            else
                throw new IncorrectElementDefinitionException($"A type discriminator should have at least one 'type' element with a code set on '{spec.ElementId}'.");
        }

        private static IAssertion buildProfileDiscriminator(ElementDefinition spec, IElementDefinitionAssertionFactory assertionFactory)
        {
            throw new NotImplementedException();
        }

        private static ElementDefinitionNavigator walkToCondition(ElementDefinitionNavigator root, string discriminator, IAsyncResourceResolver resolver)
        {
            var walker = new StructureDefinitionWalker(root, resolver);
            var conditions = walker.Walk(discriminator);

            // Well, we could check whether the conditions are Equal, since that's what really matters - they should not differ.
            if (conditions.Count > 1)
                throw new IncorrectElementDefinitionException($"The discriminator path '{discriminator}' at {root.CanonicalPath()} leads to multiple ElementDefinitions, which is not allowed.");

            return conditions.Single().Current;
        }

    }
}
