/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model
{
    public static class ElementDefinitionExtensions
    {
        public static ElementDefinition Unbounded(this ElementDefinition ed)
        {
            ed.Max = "*";
            return ed;
        }

        public static ElementDefinition Prohibited(this ElementDefinition ed)
        {
            ed.Min = 0;
            ed.Max = "0";
            return ed;
        }

        public static ElementDefinition Required(this ElementDefinition ed, int min = 1, string max = "1")
        {
            ed.Min = min;
            ed.Max = max;
            return ed;
        }

        public static ElementDefinition OfType(this ElementDefinition ed, FHIRAllTypes type, string[] profile = null)
        {
            ed.Type.Clear();
            ed.OrType(type, profile);

            return ed;
        }

        public static ElementDefinition OfReference(this ElementDefinition ed, string targetProfile, IEnumerable<ElementDefinition.AggregationMode> aggregation = null, string[] profile = null)
        {
            ed.OrReference(new[] { targetProfile }, aggregation, profile);

            return ed;
        }

        public static ElementDefinition OfReference(this ElementDefinition ed, string[] targetProfile, IEnumerable<ElementDefinition.AggregationMode> aggregation = null, string[] profile = null)
        {
            ed.Type.Clear();
            ed.OrReference(targetProfile, aggregation, profile);

            return ed;
        }

        public static ElementDefinition OrType(this ElementDefinition ed, FHIRAllTypes type, string[] profiles = null)
        {
            if (type == FHIRAllTypes.Reference)
                throw Error.InvalidOperation("Use OfReference/OrReference instead of OfType/OrType for references");

            var newType = new ElementDefinition.TypeRefComponent { Code = type.GetLiteral() };

            if (profiles != null)
            {
                foreach (var profile in profiles)
                {
                    newType.ProfileElement.Add(new Canonical(profile));
                }
            }

            ed.Type.Add(newType);

            return ed;
        }

        public static ElementDefinition OrReference(this ElementDefinition ed, string[] targetProfiles, IEnumerable<ElementDefinition.AggregationMode> aggregation = null, string[] profiles = null)
        {
            var newType = new ElementDefinition.TypeRefComponent { Code = FHIRAllTypes.Reference.GetLiteral() };

            if (targetProfiles != null)
            {
                foreach (var targetProfile in targetProfiles)
                {
                    newType.TargetProfileElement.Add(new Canonical(targetProfile));
                }
            }
            if (profiles != null)
            {
                foreach (var profile in profiles)
                {
                    newType.ProfileElement.Add(new Canonical(profile));
                }
            }
            if (aggregation != null) newType.Aggregation = aggregation.Cast<ElementDefinition.AggregationMode?>();

            ed.Type.Add(newType);

            return ed;
        }

        public static ElementDefinition Value(this ElementDefinition ed, DataType fix = null, DataType pattern = null)
        {
            ed.Fixed = fix;
            ed.Pattern = pattern;

            return ed;
        }

        public static ElementDefinition WithBinding(this ElementDefinition ed, string valueSetUri, BindingStrength strength)
        {
            var binding = new ElementDefinition.ElementDefinitionBindingComponent
            {
                ValueSet = valueSetUri,
                Strength = strength
            };

            ed.Binding = binding;

            return ed;
        }

        public static ElementDefinition WithSlicingIntro(this ElementDefinition ed, ElementDefinition.SlicingRules rules,
            params (ElementDefinition.DiscriminatorType type, string path)[] discriminators)

        {
            ed.Slicing = new ElementDefinition.SlicingComponent
            {
                Rules = rules,
                Discriminator = discriminators.Select(t =>
                    new ElementDefinition.DiscriminatorComponent { Path = t.path, Type = t.type })
                    .ToList()
            };

            return ed;
        }
    }
}
