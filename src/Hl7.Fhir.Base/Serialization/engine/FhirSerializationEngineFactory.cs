﻿/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Factory methods for creating the default implementation of <see cref="IFhirSerializationEngine"/>, as used by the
    /// FhirClient.
    /// </summary>
    public static class FhirSerializationEngineFactory
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
            public static IFhirSerializationEngine FromParserSettings(ModelInspector inspector, ParserSettings settings) =>
                new ElementModelSerializationEngine(inspector,
                    BaseFhirParser.BuildXmlParsingSettings(settings),
                    BaseFhirParser.BuildJsonParserSettings(settings),
                    BaseFhirParser.BuildPocoBuilderSettings(settings));

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


        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to flag all parsing errors, 
        /// which uses the new Poco-based parser and serializer.
        /// </summary>
        public static IFhirSerializationEngine Strict(ModelInspector inspector, FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null) =>
            new PocoSerializationEngine(inspector, jsonDeserializerSettings: deserializerSettings, jsonSerializerSettings: serializerSettings);

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to ignore recoverable errors, 
        /// which uses the new Poco-based parser and serializer.
        /// </summary>
        public static IFhirSerializationEngine Recoverable(ModelInspector inspector, FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null) =>
            new PocoSerializationEngine(inspector, FilterPredicateExtensions.IsRecoverableIssue, deserializerSettings, serializerSettings);

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which uses the new Poco-based parser and 
        /// serializers and is configured to allow errors that could occur when reading data from newer releases of FHIR.
        /// Note that this parser may drop data that cannot be captured in the POCO model, such as new elements in future
        /// FHIR releases.
        /// </summary>
        public static IFhirSerializationEngine BackwardsCompatible(ModelInspector inspector, FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null) =>
            new PocoSerializationEngine(inspector, FilterPredicateExtensions.IsBackwardsCompatibilityIssue, deserializerSettings, serializerSettings);

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> configured to allow errors
        /// and just continue parsing. Note that this may mean data loss.
        /// </summary>
        public static IFhirSerializationEngine Ostrich(ModelInspector inspector, FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null) =>
            new PocoSerializationEngine(
                inspector, 
                _ => true, 
                (deserializerSettings ?? new FhirJsonPocoDeserializerSettings()) with {Validator = null}, 
                xmlSettings: new FhirXmlPocoDeserializerSettings {Validator = null});

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which allows for manual configuration
        /// of most behaviour. See parameters for more information.
        /// </summary>
        /// <param name="inspector"></param>
        /// <param name="ignoreFilter">A predicate specifying which errors to ignore when parsing</param>
        /// <param name="jsonDeserializerSettings">The settings to be used by the engine to deserialize JSON sources</param>
        /// <param name="jsonSerializerSettings">The settings to be used by the engine to serialize resources to JSON</param>
        /// <param name="xmlSerializerSettings">The settings to be used by the engine to deserialize XML sources</param>
        /// <returns></returns>
        public static IFhirSerializationEngine Custom(ModelInspector inspector, Predicate<CodedException> ignoreFilter,
            FhirJsonPocoDeserializerSettings? jsonDeserializerSettings = null,
            FhirJsonPocoSerializerSettings? jsonSerializerSettings = null, FhirXmlPocoDeserializerSettings? xmlSerializerSettings = null)
        {
            return new PocoSerializationEngine(inspector, ignoreFilter, jsonDeserializerSettings, jsonSerializerSettings, xmlSerializerSettings);
        }

        /// <summary>
        /// Create an implementation of <see cref="IFhirSerializationEngine"/> which allows for manual specification of the json serializer and deserializer.
        /// The Xml parser in this serialization engine is completely default.
        /// </summary>
        /// <param name="jsonDeserializer">A preconfigured json deserializer</param>
        /// <param name="jsonSerializer">A preconfigured json serializer</param>
        /// <returns></returns>
        internal static IFhirSerializationEngine WithCustomJsonSerializers(BaseFhirJsonPocoDeserializer jsonDeserializer,
            BaseFhirJsonPocoSerializer jsonSerializer)
        {
            return new PocoSerializationEngine(jsonDeserializer, jsonSerializer);
        }
    }
}

#nullable restore
