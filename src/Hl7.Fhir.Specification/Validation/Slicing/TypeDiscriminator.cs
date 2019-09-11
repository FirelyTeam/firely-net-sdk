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
    internal class TypeDiscriminator : IDiscriminator
    {
        // Note: the discriminator (input) is just a string in DSTU2. Becomes a backbone element in STU3+
        public TypeDiscriminator(string[] types, string path, Validator validator)
        {
            Validator = validator;
            Path = path;
            Types = types ?? throw new System.ArgumentNullException(nameof(types));
        }

        public readonly string Path;
        public readonly Validator Validator;
        public readonly string[] Types;

        public bool Matches(ITypedElement candidate)
        {
            ITypedElement[] values = candidate.Select(Path).ToArray();

            // Don't know how to handle a discriminating element that repeats - all? any?
            if (values.Length > 1)
                throw Error.NotImplemented($"The instance has multiple elements at '{candidate.Location}' " +
                    $"for the discriminator path '{Path}'. Don't know how to handle that.");

            return matchesTypes(values[0]);
        }

        // This discriminator matches if the type in the instance is *compatible* with ANY of the 
        // possible multiple types in the ElementDefinition.Type.Code (since Type repeats).
        private bool matchesTypes(ITypedElement instance) =>
            Types.Any(type => ModelInfo.IsInstanceTypeFor(type, instance.InstanceType));
    }
}
