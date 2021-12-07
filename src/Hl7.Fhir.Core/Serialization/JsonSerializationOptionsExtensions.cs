/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

#if NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER

using Hl7.Fhir.Model;
using System.Reflection;
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
        public static JsonSerializerOptions ForFhir(this JsonSerializerOptions options, SerializationFilter? filter = default)
        {
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            options.Converters.Add(new JsonFhirConverter(typeof(ModelInfo).Assembly, filter));
            return options;
        }
    }
}

#endif
#nullable restore
