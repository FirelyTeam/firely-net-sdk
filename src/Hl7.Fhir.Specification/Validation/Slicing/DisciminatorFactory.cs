/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal static class DiscriminatorFactory
    {
        public static IDiscriminator Build(ElementDefinition.DiscriminatorComponent spec, string location, ElementDefinitionNavigator root, Validator validator)
        {
            if (spec?.Type == null) throw new ArgumentNullException(nameof(spec), "Encountered a discriminator component without a discriminator type.");
            var resolver = validator?.Settings?.ResourceResolver ??
                throw Error.Argument("Discriminator validation needs a ResourceResolver to be set in the ValidationSettings.");

            var condition = walkToCondition(root, spec.Path, resolver);

            switch (spec.Type.Value)
            {
                case ElementDefinition.DiscriminatorType.Value:
                    return buildValueDiscriminator(condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Pattern:
                    return buildPatternDiscriminator(condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Type:
                    return buildTypeDiscriminator(condition, spec.Path, validator);
                default:
                    throw Error.NotImplemented($"Found a slice discriminator of type '{spec.Type.Value.GetLiteral()}' at '{location}' which is not yet supported by this validator.");
            }
        }

        private static IDiscriminator buildValueDiscriminator(ElementDefinition spec, string discriminator, Validator validator)
        {
            if (spec.Fixed != null)
                return new ValueDiscriminator(spec.Fixed, discriminator, validator);
            else if (spec.Binding != null)
                return new BindingDiscriminator(spec.Binding, discriminator, spec.Path, validator);
            else if(spec.Fixed != null && spec.Binding != null)
                throw new IncorrectElementDefinitionException($"The value discriminator has both a 'fixed[x]' AND 'binding' element set on '{spec.Path}'.");
            else
                throw new IncorrectElementDefinitionException($"The value discriminator should have either a 'fixed[x]' or 'binding' element set on '{spec.Path}'.");
        }

        private static IDiscriminator buildPatternDiscriminator(ElementDefinition spec, string discriminator, Validator validator)
        {
            if (spec.Pattern != null)
                return new PatternDiscriminator(spec.Pattern, discriminator, validator);
            else
                throw new IncorrectElementDefinitionException($"The pattern discriminator should have a 'pattern[x]' element set on 'spec.Path'.");
        }

        private static IDiscriminator buildTypeDiscriminator(ElementDefinition spec, string discriminator, Validator validator)
        {
            var codes = spec.Type.Select(tr => tr.Code).ToArray();

            if (codes.Any())
                return new TypeDiscriminator(codes, discriminator, validator);
            else
                throw new IncorrectElementDefinitionException($"A type discriminator should have at least one 'type' element with a code set on '{spec.Path}'.");
        }

        private static ElementDefinition walkToCondition(ElementDefinitionNavigator root, string discriminator, IResourceResolver resolver)
        {
            var walker = new StructureDefinitionWalker(root, resolver);
            var conditions = walker.Walk(discriminator);

            // Well, we could check whether the conditions are Equal, since that's what really matters - they should not differ.
            if (conditions.Count > 1)
                throw new IncorrectElementDefinitionException($"The discriminator path '{discriminator}' at {root.CanonicalPath()} leads to multiple ElementDefinitions, which is not allowed.");

            return conditions.Single().Current.Current;
        }

    }
}
