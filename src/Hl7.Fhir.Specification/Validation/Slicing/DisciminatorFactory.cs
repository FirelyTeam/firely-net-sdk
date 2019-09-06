/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Validation
{
    internal static class DiscriminatorFactory
    {
        public static IDiscriminator Build(ElementDefinition.DiscriminatorComponent spec, ElementDefinitionNavigator root, Validator validator)
        {
            if (spec?.Type == null) throw new ArgumentNullException(nameof(spec), "Encountered a discriminator component without a discriminator type.");

            switch (spec.Type.Value)
            {
                case ElementDefinition.DiscriminatorType.Value:
                    return new ValueDiscriminator(spec.Path, root, validator);
                default:
                    throw Error.NotImplemented($"Slicing with a '{spec.Type.Value.GetLiteral()}' discriminator is not yet supported by this validator.");
            }
        }

    }
}
