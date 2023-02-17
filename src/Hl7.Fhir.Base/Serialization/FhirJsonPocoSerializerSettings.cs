/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Text.Json;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Specify the optional features for Json serialization.
    /// </summary>
    public record FhirJsonPocoSerializerSettings
    {
        /// <summary>
        /// Specifies the filter to use for summary serialization.
        /// </summary>
        public SerializationFilter? SummaryFilter { get; set; } = default;
    }
}

#nullable restore
#endif