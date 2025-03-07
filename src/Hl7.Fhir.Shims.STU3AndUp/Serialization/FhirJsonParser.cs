/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Newtonsoft.Json;
using System;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public class FhirJsonParser(ParserSettings? settings = null)
    : BaseFhirJsonParser(ModelInfo.ModelInspector, settings)
{
    /// <inheritdoc cref="FhirXmlParser.DEFAULT" />
    public static readonly FhirJsonParser DEFAULT = new();

    /// <inheritdoc cref="FhirXmlParser.STRICT" />
    public static readonly FhirJsonParser STRICT = new(new ParserSettings().UsingMode(DeserializationMode.Strict));

    /// <inheritdoc cref="FhirXmlParser.RECOVERABLE" />
    public static readonly FhirJsonParser RECOVERABLE = new(new ParserSettings().UsingMode(DeserializationMode.Recoverable));

    /// <inheritdoc cref="FhirXmlParser.BACKWARDSCOMPATIBLE" />
    public static readonly FhirJsonParser BACKWARDSCOMPATIBLE = new(new ParserSettings().UsingMode(DeserializationMode.BackwardsCompatible));

    /// <inheritdoc cref="FhirXmlParser.OSTRICH" />
    public static readonly FhirJsonParser OSTRICH = new(new ParserSettings().UsingMode(DeserializationMode.Ostrich));
}