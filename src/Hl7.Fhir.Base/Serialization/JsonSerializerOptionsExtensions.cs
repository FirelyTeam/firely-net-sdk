/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Utility extension method to initialize the <see cref="JsonSerializerOptions"/> to use the System.Text.Json
    /// based (de)serializers.
    /// </summary>
    public static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        /// Initialize the options to serialize using the JsonFhirConverter, producing compact output without whitespace.
        /// </summary>
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, Assembly modelAssembly) =>
            options.ForFhir(modelAssembly, new(), new());

        /// <inheritdoc cref="ForFhir(JsonSerializerOptions, Assembly)"/>
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, Assembly modelAssembly, FhirJsonPocoSerializerSettings serializerSettings) =>
            options.ForFhir(modelAssembly, serializerSettings, new());

        /// <inheritdoc cref="ForFhir(JsonSerializerOptions, Assembly)"/>
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, Assembly modelAssembly, FhirJsonPocoDeserializerSettings deserializerSettings) =>
        options.ForFhir(modelAssembly, new(), deserializerSettings);

        /// <inheritdoc cref="ForFhir(JsonSerializerOptions, Assembly)"/>
        public static JsonSerializerOptions ForFhir(
                this JsonSerializerOptions options,
                Assembly modelAssembly,
                FhirJsonPocoSerializerSettings serializerSettings,
                FhirJsonPocoDeserializerSettings deserializerSettings
                )
        {
            var converter = new FhirJsonConverterFactory(modelAssembly, serializerSettings, deserializerSettings);
            return options.ForFhir(converter);
        }

        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, ModelInspector inspector) =>
            options.ForFhir(inspector, new(), new());

        /// <inheritdoc cref="ForFhir(JsonSerializerOptions, Assembly)"/>
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, ModelInspector inspector, FhirJsonPocoSerializerSettings serializerSettings) =>
            options.ForFhir(inspector, serializerSettings, new());

        /// <inheritdoc cref="ForFhir(JsonSerializerOptions, Assembly)"/>
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, ModelInspector inspector, FhirJsonPocoDeserializerSettings deserializerSettings) =>
            options.ForFhir(inspector, new(), deserializerSettings);

        /// <inheritdoc cref="ForFhir(JsonSerializerOptions, Assembly)"/>
        public static JsonSerializerOptions ForFhir(
                this JsonSerializerOptions options,
                ModelInspector inspector,
                FhirJsonPocoSerializerSettings serializerSettings,
                FhirJsonPocoDeserializerSettings deserializerSettings
                )
        {
            var converter = new FhirJsonConverterFactory(inspector, serializerSettings, deserializerSettings);
            return options.ForFhir(converter);
        }

        /// <summary>
        /// Initialize the options to serialize using the JsonFhirConverterFactory, producing compact output without whitespace.
        /// </summary>
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, FhirJsonConverterFactory converter)
        {
            options.Converters.Add(converter);
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            return options;
        }

        /// <summary>
        /// Initialize the options to serialize using the JsonFhirConverter, producing compact output without whitespace.
        /// </summary>
        public static JsonSerializerOptions ForFhir<F>(this JsonSerializerOptions options, FhirJsonConverter<F> converter) where F : Base
        {
            options.Converters.Add(converter);
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            return options;
        }

        public static JsonSerializerOptions UsingMode(this JsonSerializerOptions options, DeserializerModes mode)
        {
            var factory = getCustomFactoryFromList(options.Converters);
            factory.Mode = mode;
            return options;
        }

        public static JsonSerializerOptions Enforcing(this JsonSerializerOptions options, IEnumerable<string> toEnforce)
        {
            var factory = getCustomFactoryFromList(options.Converters);
            factory.IgnoreFilter = factory.IgnoreFilter.And(toEnforce.ToPredicate().Negate());
            return options;
        }

        public static JsonSerializerOptions Ignoring(this JsonSerializerOptions options, IEnumerable<string> toIgnore)
        {
            var factory = getCustomFactoryFromList(options.Converters);
            factory.IgnoreFilter = factory.IgnoreFilter.Or(toIgnore.ToPredicate());
            return options;
        }

        /// <summary>
        /// Initialize the options to serialize using the JsonFhirConverter, producing compact output without whitespace.
        /// </summary>
        public static JsonSerializerOptions Compact(this JsonSerializerOptions options)
        {
            options.WriteIndented = false;
            return options;
        }

        /// <summary>
        /// Initialize the options to serialize using the JsonFhirConverter, producing pretty output.
        /// </summary>
        public static JsonSerializerOptions Pretty(this JsonSerializerOptions options)
        {
            options.WriteIndented = true;
            return options;
        }

        private static JsonConverter? findCustomConverter(IEnumerable<JsonConverter> converters)
        {
            return converters.FirstOrDefault(jsonConverter => jsonConverter.CanConvert(typeof(Resource)));
        }

        private static FhirJsonConverterFactory getCustomFactoryFromList(IEnumerable<JsonConverter> converters)
        {
            var converter = findCustomConverter(converters);
            return converter switch
            {
                FhirJsonConverterFactory factory => factory,
                _ => throw new NotSupportedException(
                    "Defining constraints for a FHIR serializer can only be done after it was created. Try calling .ForFhir first")
            };
        }
    }

    public enum DeserializerModes
    {
        Custom,
        Strict,
        Recoverable,
        BackwardsCompatible,
        Ostrich,
    }
}

#endif
#nullable restore