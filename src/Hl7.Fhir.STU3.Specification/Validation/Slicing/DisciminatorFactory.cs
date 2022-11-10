/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal static class DiscriminatorFactory
    {
        public static IDiscriminator Build(ElementDefinition.DiscriminatorComponent spec,
            IResourceResolver resolver,
            string location, ElementDefinitionNavigator root, Validator validator)
        {
            if (spec?.Type == null) throw new ArgumentNullException(nameof(spec), "Encountered a discriminator component without a discriminator type.");
            if (resolver == null) throw Error.ArgumentNull(nameof(resolver));

            //Context is needed by some external validators in case a system is missing.
            //See http://hl7.org/fhir/valueset-operation-validate-code.html
            var context = $"{root.StructureDefinition.Url}#{location}";

            var condition = walkToCondition(root, spec.Path, resolver);

            if (condition is null)
            {
                return new ComprehensiveDiscriminator();
            }

            switch (spec.Type.Value)
            {
                //This was the situation before implementing #1246 and #1185
                //case ElementDefinition.DiscriminatorType.Value:
                //    return buildValueDiscriminator(condition, spec.Path, validator);
                //case ElementDefinition.DiscriminatorType.Pattern:
                //    return buildPatternDiscriminator(condition, spec.Path, validator);

                case ElementDefinition.DiscriminatorType.Value:
                    return buildCombinedDiscriminator("value", condition, spec.Path, validator, context);
                case ElementDefinition.DiscriminatorType.Pattern:
                    return buildCombinedDiscriminator("pattern", condition, spec.Path, validator, context);
                case ElementDefinition.DiscriminatorType.Type:
                    return buildTypeDiscriminator(condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Profile:
                    return buildProfileDiscriminator(condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Exists:
                    return buildExistsDiscriminator(condition, spec.Path, validator);
                default:
                    throw Error.NotImplemented($"Found a slice discriminator of type '{spec.Type.Value.GetLiteral()}' at '{location}' which is not yet supported by this validator.");
            }
        }

        private static IDiscriminator buildExistsDiscriminator(ElementDefinitionNavigator condition, string path, Validator validator)
        {
            var spec = condition.Current;

            var existsMode = spec switch
            {
                { Max: "0" } => false,
                { Min: 1 } => true,
                _ => throw new IncorrectElementDefinitionException("For an exists slice, there should be one slice with a max set to 0 and one slice with a min set to 1.")
            };

            return new ExistsDiscriminator(existsMode, path);
        }

        private static IDiscriminator buildCombinedDiscriminator(string name, ElementDefinitionNavigator nav, string discriminator, Validator validator, string context)
        {
            return new CombinedDiscriminator(listDiscriminators());

            IEnumerable<IDiscriminator> listDiscriminators()
            {
                var spec = nav.Current;

                if (spec.Fixed == null && spec.Binding == null && spec.Pattern == null)
                    throw new IncorrectElementDefinitionException($"The {name} discriminator should have a 'fixed[x]', 'pattern[x]' or binding element set on '{nav.CanonicalPath()}'.");

                if (spec.Fixed != null)
                    yield return new ValueDiscriminator(spec.Fixed, discriminator, validator);

                if (spec.Binding != null)
                    yield return new BindingDiscriminator(spec.Binding, discriminator, spec.Path, validator, context);

                if (spec.Pattern != null)
                    yield return new PatternDiscriminator(spec.Pattern, discriminator, validator);
            }
        }

        private static IDiscriminator buildTypeDiscriminator(ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            var spec = nav.Current;
            if (spec.IsRootElement())
            {
                // When we are at a root of a structuredefinition, then return the type of this structuredefinition.
                return new TypeDiscriminator(new[] { nav.StructureDefinition.Type }, discriminator, validator);
            }

            var codes = spec.Type.Select(tr => tr.Code).ToArray();

            return codes.Any()
                ? new TypeDiscriminator(codes, discriminator, validator)
                : throw new IncorrectElementDefinitionException($"A type discriminator should have at least one 'type' element with a code set on '{nav.CanonicalPath()}'.");
        }

        private static IDiscriminator buildProfileDiscriminator(ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            var spec = nav.Current;

            if (spec.IsRootElement())
            {
                // Firsts case: we are at the root of a StructureDefinition, most commonly because
                // the discriminator path ended in a resolve(). We need to find the canonical url
                // of the thing we've landed on, since we're at the root of the profile we actually
                // want to validate against.
                var profile = nav.StructureDefinition?.Url ??
                    throw new InvalidOperationException($"Cannot determine the canonical url for the profile at '{nav.CanonicalPath()}' - parent StructureDefinition was not set on navigator.");

                return new ProfileDiscriminator(new[] { profile }, discriminator, validator);
            }
            else
            {
                // Second case: we are inside a structure definition (not on the root), in this case
                // the current element can only be profiled by the <profile> tag(s) on the <type> element.
                // Note that the element pointed to by the discriminator should have constrained the types
                // to a single (unique) type, but we will allow multiple <profile>s.
                if (spec.Type.Select(tr => tr.Code).Distinct().Count() != 1)   // STU3, in R4 codes are always unique
                    throw new IncorrectElementDefinitionException($"The profile discriminator '{discriminator}' should navigate to an ElementDefinition with exactly one 'type' element at '{nav.CanonicalPath()}'.");

                var profiles = spec.Type.Select(tr => tr.Profile).Distinct();
                return new ProfileDiscriminator(profiles, discriminator, validator);
            }
        }


        private static ElementDefinitionNavigator? walkToCondition(ElementDefinitionNavigator root, string discriminator, IResourceResolver resolver)
        {
            var walker = new StructureDefinitionWalker(root, resolver);
            var conditions = walker.Walk(discriminator);

            // Well, we could check whether the conditions are Equal, since that's what really matters - they should not differ.
            return conditions.Count > 1
                ? throw new IncorrectElementDefinitionException($"The discriminator path '{discriminator}' at {root.CanonicalPath()} leads to multiple ElementDefinitions, which is not allowed.")
                : (conditions.SingleOrDefault()?.Current);
        }
    }
}

#nullable restore
