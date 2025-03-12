/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization;

/// <inheritdoc />
public class FhirXmlDeserializer(DeserializerSettings? settings = null)
    : BaseFhirXmlDeserializer(ModelInfo.ModelInspector, settings)
{
    /// <summary>
    /// A parser with default settings: strict validation, only XML is not validated.
    /// </summary>
    public static readonly FhirXmlDeserializer DEFAULT = new();

    /// <summary>
    /// A parser with the most strict settings, will detect all issues we know about.
    /// </summary>
    public static readonly FhirXmlDeserializer STRICT = new(new DeserializerSettings().UsingMode(DeserializationMode.Strict));

    /// <summary>
    /// A parser that allows all errors that will not lead to dataloss when roundtripping.
    /// </summary>
    public static readonly FhirXmlDeserializer RECOVERABLE = new(new DeserializerSettings().UsingMode(DeserializationMode.Recoverable));

    /// <summary>
    /// A parser that allows all errors that result from reading data from other FHIR versions: it allows
    /// unknown elements and coded values. This will be roundtrippable.
    /// </summary>
    public static readonly FhirXmlDeserializer BACKWARDSCOMPATIBLE = new(new DeserializerSettings().UsingMode(DeserializationMode.BackwardsCompatible));

    /// <summary>
    /// A parser that continues to parse, ignoring all errors. May result in data loss.
    /// </summary>
    public static readonly FhirXmlDeserializer OSTRICH = new(new DeserializerSettings().UsingMode(DeserializationMode.Ostrich));
}

[Obsolete("FhirXmlParser is obsolete, use FhirXmlDeserializer instead.")]
#pragma warning disable CS0618 // Type or member is obsolete
public class FhirXmlParser(ParserSettings? settings = null) : FhirXmlDeserializer(settings);
#pragma warning restore CS0618 // Type or member is obsolete