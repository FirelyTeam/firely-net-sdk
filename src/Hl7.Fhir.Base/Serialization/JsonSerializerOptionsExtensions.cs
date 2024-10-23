/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.CdsHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Hl7.Fhir.Serialization
{
    /// <summary>
    /// Utility extension method to initialize the <see cref="JsonSerializerOptions"/> to use the System.Text.Json
    /// based (de)serializers.
    /// </summary>
    public static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        /// Initialize the options to serialize using the CDS Hooks specific (de)serializers. Note that this also adds the FHIR specific converters, since FHIR is expected in the CDS Hooks messages.
        /// </summary>
#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
        [System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
        public static JsonSerializerOptions ForCdsHooks(this JsonSerializerOptions options, ModelInspector inspector, FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null) =>
            options.ForFhir(inspector, serializerSettings ?? new FhirJsonPocoSerializerSettings(), deserializerSettings ?? new FhirJsonPocoDeserializerSettings()).addCdsHooks();


        /// <inheritdoc cref="ForCdsHooks(JsonSerializerOptions, ModelInspector, FhirJsonPocoSerializerSettings, FhirJsonPocoDeserializerSettings)"/>
#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
        [System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
        public static JsonSerializerOptions ForCdsHooks(this JsonSerializerOptions options, Assembly modelAssembly, FhirJsonPocoSerializerSettings? serializerSettings = null,
            FhirJsonPocoDeserializerSettings? deserializerSettings = null) =>
            options.ForFhir(modelAssembly, serializerSettings ?? new FhirJsonPocoSerializerSettings(), deserializerSettings ?? new FhirJsonPocoDeserializerSettings())
                .addCdsHooks();

        private static JsonSerializerOptions addCdsHooks(this JsonSerializerOptions options)
        {
            options.TypeInfoResolver = (options.TypeInfoResolver ?? new DefaultJsonTypeInfoResolver()).WithAddedModifier(changeCdsHookPropertyNames);

            return options;

            static void changeCdsHookPropertyNames(JsonTypeInfo ti)
            {
                if (ti.Type.GetCustomAttribute<CdsHookElementAttribute>() is null) return;

                foreach (var p in ti.Properties)
                    p.Name = char.ToLower(p.Name.First()) + p.Name.Substring(1);
            }
        }

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

        /// <summary>
        /// Modify the options to use a preset list of errors to ignore by specifying a mode. This can be any member of <see cref="DeserializerModes"/>
        /// </summary>
        /// <remarks>
        /// Modifying the options is always left-associative. This means that defining custom constraints should probably be done AFTER setting the mode.
        /// </remarks>
        public static JsonSerializerOptions UsingMode(this JsonSerializerOptions options, DeserializerModes mode)
        {
            getCustomFactoryFromList(options.Converters).SetMode(mode);
            return options;
        }

        /// <summary>
        /// Modify the options to use a custom list of errors to enforce. 
        /// </summary>
        /// <remarks>
        /// - Modifying the options is always left-associative. This means that defining custom constraints should probably be done AFTER setting the mode.
        /// - This also means that enforcing, then ignoring an error has the opposite behaviour as ignoring it, then enforcing it.
        /// </remarks>
        public static JsonSerializerOptions Enforcing(this JsonSerializerOptions options, IEnumerable<string> toEnforce)
        {
            getCustomFactoryFromList(options.Converters).SetEnforcedErrors(toEnforce);
            return options;
        }

        /// <summary>
        /// Modify the options to use a custom list of errors to ignore. 
        /// </summary>
        /// <remarks>
        /// - Modifying the options is always left-associative. This means that defining custom constraints should probably be done AFTER setting the mode.
        /// - This also means that enforcing, then ignoring an error has the opposite behaviour as ignoring it, then enforcing it.
        /// </remarks>
        public static JsonSerializerOptions Ignoring(this JsonSerializerOptions options, IEnumerable<string> toIgnore)
        {
            getCustomFactoryFromList(options.Converters).SetIgnoredErrors(toIgnore);
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

        // internal for testing purposes
        internal static JsonConverter? FindCustomConverter(this IEnumerable<JsonConverter> converters)
        {
            return converters.FirstOrDefault(jsonConverter => jsonConverter.CanConvert(typeof(Resource)));
        }

        private static FhirJsonConverterFactory getCustomFactoryFromList(IEnumerable<JsonConverter> converters)
        {
            return FindCustomConverter(converters) as FhirJsonConverterFactory ?? throw new NotSupportedException(
                "Customizing a FHIR serializer can only be done after it was created. Try calling .ForFhir first");
        }
    }

    /// <summary>
    /// Enumerates the modes with which a deserializer can be configured
    /// </summary>
    public enum DeserializerModes
    {
        /// <summary>
        /// Do not ignore any errors (default behaviour for most implementations)
        /// </summary>
        Strict,

        /// <summary>
        /// An issue is recoverable if all data present in the parsed data could be retrieved and
        /// captured in the POCO model, even if the syntax or the data was not fully FHIR compliant.
        /// </summary>
        Recoverable,

        /// <summary>
        /// An issue is allowable for backwards compatibility if it could be caused because an older parser encounters data coming from a newer 
        /// FHIR release. This means allowing unknown elements, attributes, codes and types in a choice element. Note that the POCO model cannot capture
        /// these newer elements and data, so this means data loss may occur.
        /// </summary>
        BackwardsCompatible,

        /// <summary>
        /// Ignore all errors. Useful for debugging and/or when you know the data to be parsed is a correct instance.
        /// </summary>
        Ostrich,
    }
}

#nullable restore