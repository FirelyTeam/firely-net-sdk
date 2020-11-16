/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Implements a discriminator which matches an instance if all component discriminators match the instance.
    /// </summary>
    internal class CombinedDiscriminator : IDiscriminator
    {
        public IDiscriminator[] Components { get; private set; }

        public CombinedDiscriminator(IEnumerable<IDiscriminator> components) =>
                Components = components.ToArray();

        public bool Matches(ITypedElement candidate) => Components.All(c => c.Matches(candidate));
    }
}
