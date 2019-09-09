/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal class PatternDiscriminator : IDiscriminator
    {
        public PatternDiscriminator(Element pattern, string path, Validator validator)
        {
            Validator = validator;
            Path = path;
            Pattern = pattern;
        }

        public readonly string Path;
        public readonly Validator Validator;
        public readonly Element Pattern;

        public bool Matches(ITypedElement candidate)
        {
            ITypedElement[] values = candidate.Select(Path).ToArray();

            // Don't know how to handle a discriminating element that repeats - all? any?
            if (values.Length > 1)
                throw Error.NotImplemented($"The instance has multiple elements at '{candidate.Location}' " +
                    $"for the pattern-based discriminator path '{Path}'. Don't know how to handle that.");

            return matchesPattern(values[0]);
        }

        private bool matchesPattern(ITypedElement instance)
        {
            //TODO: The reason why there's no match might be interesting for debugging purposes,
            //we need to add a way to Trace() that - or make it part of the new Validation outcome plans.
            var result = Validator.ValidatePattern(Pattern, instance);
            return result.Success;
        }
    }
}
