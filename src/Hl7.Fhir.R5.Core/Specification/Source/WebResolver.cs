/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Rest;
using System;

namespace Hl7.Fhir.Specification.Source
{
    /// <inheritdoc/>
    public class WebResolver : CommonWebResolver
    {
        /// <summary>
        /// Create a new <see cref="WebResolver"/> instance that uses default <see cref="FhirClient"/> instance.
        /// </summary>
        public WebResolver() : base((uri) => new FhirClient(uri))
        {

        }

        /// <summary>Create a new <see cref="WebResolver"/> instance that supports a custom <see cref="FhirClient"/> implementation.</summary>
        /// <param name="fhirClientFactory">
        /// Factory function that should create a new <see cref="FhirClient"/> instance for the specified <see cref="Uri"/>.
        /// If this parameter equals <c>null</c>, then the new instance creates a default <see cref="FhirClient"/> instance.
        /// </param>
        public WebResolver(Func<Uri, FhirClient> fhirClientFactory) : base(fhirClientFactory)
        {
        }
    }
}
#nullable restore