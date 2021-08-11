/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Validation
{
    /// <summary>
    /// Not really a discriminator, but a helper discriminator in the case of multiple discriminators for slicing. 
    /// It will match on any candidate.
    /// </summary>
    internal class ComprehensiveDiscriminator : IDiscriminator
    {
        public bool Matches(ITypedElement candidate) => true;
    }
}

#nullable restore
