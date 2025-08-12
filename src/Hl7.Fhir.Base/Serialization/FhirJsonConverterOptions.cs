/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using System;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Specify the optional features for Json deserialization.
/// </summary>
public record FhirJsonConverterOptions : DeserializerSettings
{
    /// <summary>
    /// Specifies the filter to use for summary serialization.
    /// </summary>
    public SerializationFilter? SummaryFilter { get; init; } = null;
}

[Obsolete("Use FhirJsonConverterOptions instead. This will be removed in a future version.")]
public record FhirJsonPocoDeserializerSettings : FhirJsonConverterOptions;