/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization;

/// <inheritdoc />
public class FhirJsonDeserializer(DeserializerSettings? settings = null)
    : BaseFhirJsonDeserializer(ModelInfo.ModelInspector, settings)
{
    /// <inheritdoc cref="FhirXmlDeserializer.DEFAULT" />
    public static readonly FhirJsonDeserializer DEFAULT = new();

    /// <inheritdoc cref="FhirXmlDeserializer.STRICT" />
    public static readonly FhirJsonDeserializer STRICT = new(new DeserializerSettings().UsingMode(DeserializationMode.Strict));

    /// <inheritdoc cref="FhirXmlDeserializer.RECOVERABLE" />
    public static readonly FhirJsonDeserializer RECOVERABLE = new(new DeserializerSettings().UsingMode(DeserializationMode.Recoverable));

    /// <inheritdoc cref="FhirXmlDeserializer.BACKWARDSCOMPATIBLE" />
    public static readonly FhirJsonDeserializer BACKWARDSCOMPATIBLE = new(new DeserializerSettings().UsingMode(DeserializationMode.BackwardsCompatible));

    /// <inheritdoc cref="FhirXmlDeserializer.OSTRICH" />
    public static readonly FhirJsonDeserializer OSTRICH = new(new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));
}