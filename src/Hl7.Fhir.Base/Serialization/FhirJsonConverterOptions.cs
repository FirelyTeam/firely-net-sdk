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
    private SerializationFilter? _summaryFilter = null;

    /// <summary>
    /// Specifies the filter to use for summary serialization.
    /// </summary>
    [Obsolete("Use SummaryFilterFactory instead to ensure thread-safety when reusing" +
              " JsonSerializerOptions instances. This property will be removed in a future version.")]
    public SerializationFilter? SummaryFilter
    {
        get => _summaryFilter;

        set
        {
            _summaryFilter = value;
            SummaryFilterFactory = value is not null ? () => value : null;
        }
    }

    /// <summary>
    /// Specifies a factory function that creates a new filter instance for each serialization operation.
    /// This ensures thread-safety when reusing JsonSerializerOptions instances in concurrent scenarios.
    /// </summary>
    public Func<SerializationFilter>? SummaryFilterFactory { get; set; } = null;
}

[Obsolete("Use FhirJsonConverterOptions instead. This will be removed in a future version.")]
public record FhirJsonPocoDeserializerSettings : FhirJsonConverterOptions;

[Obsolete("Use FhirJsonConverterOptions instead. This will be removed in a future version.")]
public record FhirJsonPocoSerializerSettings : FhirJsonConverterOptions;