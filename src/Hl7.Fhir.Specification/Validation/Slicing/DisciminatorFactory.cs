/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

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
            if(resolver == null) throw Error.ArgumentNull(nameof(resolver));

            var condition = walkToCondition(root, spec.Path, resolver);

            switch (spec.Type.Value)
            {
                //This was the situation before implementing #1246 and #1185
                //case ElementDefinition.DiscriminatorType.Value:
                //    return buildValueDiscriminator(condition, spec.Path, validator);
                //case ElementDefinition.DiscriminatorType.Pattern:
                //    return buildPatternDiscriminator(condition, spec.Path, validator);
                
                case ElementDefinition.DiscriminatorType.Value:
                    return buildCombinedDiscriminator("value", condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Pattern:
                    return buildCombinedDiscriminator("pattern", condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Type:
                    return buildTypeDiscriminator(condition, spec.Path, validator);
                case ElementDefinition.DiscriminatorType.Profile:
                    return buildProfileDiscriminator(condition, spec.Path, validator);
                default:
                    throw Error.NotImplemented($"Found a slice discriminator of type '{spec.Type.Value.GetLiteral()}' at '{location}' which is not yet supported by this validator.");
            }
        }

        private static IDiscriminator buildValueDiscriminator(ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            var spec = nav.Current;

            if (spec.Fixed != null)
                return new ValueDiscriminator(spec.Fixed, discriminator, validator);
            else if (spec.Binding != null)
                return new BindingDiscriminator(spec.Binding, discriminator, spec.Path, validator);
            else if(spec.Fixed != null && spec.Binding != null)
                throw new IncorrectElementDefinitionException($"The value discriminator has both a 'fixed[x]' AND 'binding' element set on '{nav.CanonicalPath()}'.");
            else
                throw new IncorrectElementDefinitionException($"The value discriminator should have either a 'fixed[x]' or 'binding' element set on '{nav.CanonicalPath()}'.");
        }

        private static IDiscriminator buildPatternDiscriminator(ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            var spec = nav.Current;

            if (spec.Pattern != null)
                return new PatternDiscriminator(spec.Pattern, discriminator, validator);
            else
                throw new IncorrectElementDefinitionException($"The pattern discriminator should have a 'pattern[x]' element set on '{nav.CanonicalPath()}'.");
        }

        private static IDiscriminator buildCombinedDiscriminator(string name, ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            return new CombinedDiscriminator(listDiscriminators());

            IEnumerable<IDiscriminator> listDiscriminators()
            {
                var spec = nav.Current;

                if(spec.Fixed == null && spec.Binding == null && spec.Pattern == null)
                    throw new IncorrectElementDefinitionException($"The {name} discriminator should have a 'fixed[x]', 'pattern[x]' or binding element set on '{nav.CanonicalPath()}'.");

                if (spec.Fixed != null)
                    yield return new ValueDiscriminator(spec.Fixed, discriminator, validator);
                
                if (spec.Binding != null)
                    yield return new BindingDiscriminator(spec.Binding, discriminator, spec.Path, validator);
                
                if (spec.Pattern != null)
                    yield return new PatternDiscriminator(spec.Pattern, discriminator, validator);                
            }
        }

        private static IDiscriminator buildTypeDiscriminator(ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            var spec = nav.Current;
            var codes = spec.Type.Select(tr => tr.Code).ToArray();

            if (codes.Any())
                return new TypeDiscriminator(codes, discriminator, validator);
            else
                throw new IncorrectElementDefinitionException($"A type discriminator should have at least one 'type' element with a code set on '{nav.CanonicalPath()}'.");
        }

        private static IDiscriminator buildProfileDiscriminator(ElementDefinitionNavigator nav, string discriminator, Validator validator)
        {
            var spec = nav.Current;
            
            if(spec.IsRootElement())
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
                if(spec.Type.Count != 1)   // in R4 codes are always unique
                    throw new IncorrectElementDefinitionException($"The profile discriminator '{discriminator}' should navigate to an ElementDefinition with exactly one 'type' element at '{nav.CanonicalPath()}'.");

                var profiles = spec.Type.Single().Profile;
                return new ProfileDiscriminator(profiles, discriminator, validator);
            }
        }


        private static ElementDefinitionNavigator walkToCondition(ElementDefinitionNavigator root, string discriminator, IResourceResolver resolver)
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
