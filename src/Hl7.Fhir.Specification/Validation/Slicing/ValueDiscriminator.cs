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
    internal class ValueDiscriminator : IDiscriminator
    {
        // Note: the discriminator (input) is just a string in DSTU2. Becomes a backbone element in STU3+
        public ValueDiscriminator(Element fixedValue, string path, Validator validator)
        {
            Validator = validator;
            Path = path;
            FixedValue = fixedValue ?? throw new System.ArgumentNullException(nameof(fixedValue));
        }

        public readonly string Path;
        public readonly Validator Validator;
        public readonly Element FixedValue;

        public bool Matches(ITypedElement candidate)
        {
            ITypedElement[] values = candidate.Select(Path).ToArray();

            // Don't know how to handle a discriminating element that repeats - all? any?
            if (values.Length > 1)
                throw Error.NotImplemented($"The instance has multiple elements at '{candidate.Location}' " +
                    $"for the discriminator path '{Path}'. Don't know how to handle that.");

            return matchesFixed(values[0]);
        }

        private bool matchesFixed(ITypedElement instance)
        {
            //TODO: The reason why there's no match might be interesting for debugging purposes,
            //we need to add a way to Trace() that - or make it part of the new Validation outcome plans.
            var result = Validator.ValidateFixed(FixedValue, instance);
            return result.Success;
        }
    }
}
