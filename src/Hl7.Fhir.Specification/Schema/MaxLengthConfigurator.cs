/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Impl;
using Hl7.Fhir.Validation.Schema;

namespace Hl7.Fhir.Specification.Schema
{

    internal static class MaxLengthConfigurator
    {
        public static MaxLength ToValidatable(this ElementDefinition elementDefinition)
        {
            // TODO: Should we do this?
            if (elementDefinition.MaxLength == null)
                throw new IncorrectElementDefinitionException("Not exptected");

            var maxLength = elementDefinition.MaxLength.Value;
            if (maxLength <= 0)
                throw new IncorrectElementDefinitionException($"MaxLength was given in ElementDefinition, but it has a negative value ({maxLength})");

            return new MaxLength("TODO", maxLength);
        }
    }

}
