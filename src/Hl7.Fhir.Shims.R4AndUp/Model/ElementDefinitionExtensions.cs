/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    public static class ElementDefinitionExtensions
    {
        /// <summary>
        /// Sets <see cref="ElementDefinition.Type"/> to a single <see cref="ElementDefinition.TypeRefComponent"/>.
        /// </summary>
        public static ElementDefinition OfType(this ElementDefinition ed, FHIRAllTypes type, IEnumerable<string>? profiles = null)
            => ed.OfType(type.GetLiteral(), profiles);

        /// <inheritdoc cref="OfType(ElementDefinition, FHIRAllTypes, IEnumerable{string}?)"/>
        public static ElementDefinition OfType(this ElementDefinition ed, FHIRAllTypes type, string profile)
            => ed.OfType(type.GetLiteral(), new[] { profile });


        /// <inheritdoc cref="OrType(ElementDefinition, FHIRAllTypes, IEnumerable{string}?)"/>
        public static ElementDefinition OrType(this ElementDefinition ed, FHIRAllTypes type, string profile)
            => ed.OrType(type.GetLiteral(), new[] { profile });

        /// <summary>
        /// Adds a <see cref="ElementDefinition.TypeRefComponent"/> to the given <see cref="ElementDefinition"/>.
        /// </summary>
        public static ElementDefinition OrType(this ElementDefinition ed, FHIRAllTypes type, IEnumerable<string>? profiles = null)
            => ed.OrType(type.GetLiteral(), profiles);

    }
}

#nullable restore