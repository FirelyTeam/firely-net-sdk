/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.Introspection;
using System;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Factory methods for creating the default implementation of <see cref="IFhirSerializationEngine"/>, as used by the
/// FhirClient.
/// </summary>
public static partial class FhirSerializationEngineFactory
{
    /// <summary>
    /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to flag all parsing errors,
    /// which uses the new Poco-based parser and serializer.
    /// </summary>
    public static IFhirSerializationEngine Strict(ModelInspector inspector,
        FhirJsonConverterOptions? converterOptions = null,
        DeserializerSettings? xmlSettings = null) =>
        createEngine(inspector, converterOptions, xmlSettings, DeserializationMode.Strict);

    /// <summary>
    /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to ignore recoverable errors,
    /// which uses the new Poco-based parser and serializer.
    /// </summary>
    public static IFhirSerializationEngine Recoverable(ModelInspector inspector,
        FhirJsonConverterOptions? converterOptions = null,
        DeserializerSettings? xmlSettings = null) =>
        createEngine(inspector, converterOptions, xmlSettings, DeserializationMode.Recoverable);

    /// <summary>
    /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the new Poco-based parser and
    /// uses <see cref="DeserializationMode.SyntaxOnly"/> mode.
    /// </summary>
    public static IFhirSerializationEngine SyntaxOnly(ModelInspector inspector,
        FhirJsonConverterOptions? converterOptions = null,
        DeserializerSettings? xmlSettings = null) =>
        createEngine(inspector, converterOptions, xmlSettings, DeserializationMode.SyntaxOnly);

    /// <summary>
    /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the new Poco-based parser and
    /// uses <see cref="DeserializationMode.BackwardsCompatible"/> mode.
    /// </summary>
    public static IFhirSerializationEngine BackwardsCompatible(ModelInspector inspector,
        FhirJsonConverterOptions? converterOptions = null,
        DeserializerSettings? xmlSettings = null) =>
        createEngine(inspector, converterOptions, xmlSettings, DeserializationMode.BackwardsCompatible);

    /// <summary>
    /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to allow errors
    /// and just continue parsing. Note that this may mean data loss.
    /// </summary>
    public static IFhirSerializationEngine Ostrich(ModelInspector inspector,
        FhirJsonConverterOptions? converterOptions = null,
        DeserializerSettings? xmlSettings = null) =>
        createEngine(inspector, converterOptions, xmlSettings, DeserializationMode.Ostrich);

    /// <summary>
    /// Create an implementation of <see cref="IFhirSerializationEngine"/> which allows for manual configuration
    /// of most behaviour. See parameters for more information.
    /// </summary>
    /// <param name="inspector"></param>
    /// <param name="converterOptions">The settings to be used by the engine to deserialize JSON sources</param>
    /// <param name="xmlSerializerSettings">The settings to be used by the engine to deserialize XML sources</param>
    /// <returns></returns>
    public static IFhirSerializationEngine Custom(ModelInspector inspector,
        FhirJsonConverterOptions converterOptions,
        DeserializerSettings xmlSerializerSettings)
    {
        var jsonDeserializer = new BaseFhirJsonDeserializer(inspector, converterOptions);
        var xmlDeserializer = new BaseFhirXmlDeserializer(inspector, xmlSerializerSettings);

        return new PocoSerializationEngine(jsonDeserializer,
            new BaseFhirJsonSerializer(inspector),
            xmlDeserializer,
            new BaseFhirXmlSerializer(inspector));
    }

    private static IFhirSerializationEngine createEngine(ModelInspector inspector,
        FhirJsonConverterOptions? converterOptions, DeserializerSettings? xmlSettings, DeserializationMode mode)
    {
        var jsonOptions = (FhirJsonConverterOptions)(converterOptions ?? new FhirJsonConverterOptions()).UsingMode(mode);
        var xmlOptions = (xmlSettings ?? new DeserializerSettings()).UsingMode(mode);

        return Custom(inspector, jsonOptions, xmlOptions);
    }
}