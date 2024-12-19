/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using Hl7.Fhir.Model;
using System.Text.Json;

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
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options) => options.ForFhir(ModelInfo.ModelInspector);

        /// <summary>
        /// Initialize the options to serialize using the CDS Hooks specific (de)serializers. Note that this also adds the FHIR specific converters, since FHIR is expected in the CDS Hooks messages.
        /// </summary>
#if NET8_0_OR_GREATER
        [System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
        [System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
        public static JsonSerializerOptions ForCdsHooks(this JsonSerializerOptions options,
            FhirJsonConverterOptions? converterOptions = null) =>
            options.ForCdsHooks(ModelInfo.ModelInspector,
                converterOptions ?? new FhirJsonConverterOptions());

        public static JsonSerializerOptions ForFhir(
            this JsonSerializerOptions options,
            FhirJsonConverterOptions converterOptions) =>
            options.ForFhir(ModelInfo.ModelInspector, converterOptions);
    }
}

#nullable restore