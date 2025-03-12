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
    /// A named scope for the factory methods that use the legacy ElementModel-based (de)serializers.
    /// </summary>
    public static class Legacy
    {
        private enum Mode
        {
            Strict,
            Permissive,
            BackwardsCompatible,
            Ostrich
        }

        private static PocoBuilderSettings buildPocoBuilderSettings(Mode mode) => new()
        {
            AllowUnrecognizedEnums = mode is Mode.BackwardsCompatible or Mode.Ostrich,
            IgnoreUnknownMembers = mode is Mode.BackwardsCompatible or Mode.Ostrich,
            ExceptionHandler = mode is Mode.Ostrich
                ? (_, _) => { }
                : null
        };


        private static FhirXmlParsingSettings buildXmlParsingSettings(Mode mode) => new()
        {
            DisallowSchemaLocation = mode is Mode.Strict,
            PermissiveParsing = mode is Mode.Permissive or Mode.Ostrich,
            ValidateFhirXhtml = mode is Mode.Strict
        };

        private static FhirJsonParsingSettings buildJsonParsingSettings(Mode mode) => new()
        {
            AllowJsonComments = mode is not Mode.Strict,
            PermissiveParsing = mode is Mode.Permissive or Mode.Ostrich,
            ValidateFhirXhtml = mode is Mode.Strict
        };

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the legacy parser and serializer
        /// using <see cref="ParserSettings.PermissiveParsing"/> set to <c>true</c>.
        /// </summary>
        public static IFhirSerializationEngine FromParserSettings(ModelInspector inspector, DeserializerSettings settings) =>
            new ElementModelSerializationEngine(inspector,
                buildXmlParsingSettings(settings),
                buildJsonParserSettings(settings),
                buildPocoBuilderSettings(settings));


        private static PocoBuilderSettings buildPocoBuilderSettings(DeserializerSettings ps) =>
            new()
            {
                AllowUnrecognizedEnums = ps.AllowUnrecognizedEnums,
                IgnoreUnknownMembers = ps.AcceptUnknownMembers,
            };

        private static FhirXmlParsingSettings buildXmlParsingSettings(DeserializerSettings settings) =>
            new()
            {
                DisallowSchemaLocation = settings.DisallowXsiAttributesOnRoot,
#pragma warning disable CS0618 // Type or member is obsolete
                PermissiveParsing = settings is ParserSettings { PermissiveParsing: true }
#pragma warning restore CS0618 // Type or member is obsolete
            };

        private static FhirJsonParsingSettings buildJsonParserSettings(DeserializerSettings settings) =>
#pragma warning disable CS0618 // Type or member is obsolete
            new()
                {
                    AllowJsonComments = false,
                    PermissiveParsing = settings is ParserSettings { PermissiveParsing: true }
                };
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the legacy parser and serializer
        /// using <see cref="ParserSettings.PermissiveParsing"/> set to <c>true</c>.
        /// </summary>
        public static IFhirSerializationEngine Permissive(ModelInspector inspector) =>
            new ElementModelSerializationEngine(inspector,
                buildXmlParsingSettings(Mode.Permissive),
                buildJsonParsingSettings(Mode.Permissive),
                buildPocoBuilderSettings(Mode.Permissive));

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the legacy parser and serializer
        /// with <see cref="ParserSettings.PermissiveParsing"/> set to <c>false</c>.
        /// </summary>
        public static IFhirSerializationEngine Strict(ModelInspector inspector) =>
            new ElementModelSerializationEngine(inspector,
                buildXmlParsingSettings(Mode.Strict),
                buildJsonParsingSettings(Mode.Strict),
                buildPocoBuilderSettings(Mode.Strict));

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the legacyt parser and serializer
        /// and is configured to allow errors that could occur when reading data from newer releases of FHIR. Note that this
        /// parser may drop data that cannot be captured in the POCO model, such as new elements in future FHIR releases.
        /// </summary>
        public static IFhirSerializationEngine BackwardsCompatible(ModelInspector inspector) =>
            new ElementModelSerializationEngine(inspector,
                buildXmlParsingSettings(Mode.BackwardsCompatible),
                buildJsonParsingSettings(Mode.BackwardsCompatible),
                buildPocoBuilderSettings(Mode.BackwardsCompatible));

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the legacy parser and serializer
        /// configured to allow errors and just continue parsing. Note that this may mean data loss.
        /// </summary>
        public static IFhirSerializationEngine Ostrich(ModelInspector inspector) =>
            new ElementModelSerializationEngine(inspector,
                buildXmlParsingSettings(Mode.Ostrich),
                buildJsonParsingSettings(Mode.Ostrich),
                buildPocoBuilderSettings(Mode.Ostrich));
    }
}