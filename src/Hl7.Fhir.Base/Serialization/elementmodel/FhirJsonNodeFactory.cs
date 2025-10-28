/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization
{
    public partial class FhirJsonNode
    {
        /// <inheritdoc cref="ReadAsync(JsonReader, string, FhirJsonParsingSettings)" />
        public static ISourceNode Read(JsonReader reader, string rootName = null, FhirJsonParsingSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            var doc = SerializationUtil.JObjectFromReader(reader);
            return new FhirJsonNode(doc, rootName, settings);
        }

        public static async Task<ISourceNode> ReadAsync(JsonReader reader, string rootName = null, FhirJsonParsingSettings settings = null)
        {
            if (reader == null) throw Error.ArgumentNull(nameof(reader));

            var doc = await SerializationUtil.JObjectFromReaderAsync(reader).ConfigureAwait(false);
            return new FhirJsonNode(doc, rootName, settings);
        }

        /// <inheritdoc cref="ParseAsync(string, string, FhirJsonParsingSettings)" />
        public static ISourceNode Parse(string json, string rootName = null, FhirJsonParsingSettings settings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));

            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return Read(reader, rootName, settings);
            }
        }

        public static async Task<ISourceNode> ParseAsync(string json, string rootName = null, FhirJsonParsingSettings settings = null)
        {
            if (json == null) throw Error.ArgumentNull(nameof(json));

            using (var reader = SerializationUtil.JsonReaderFromJsonText(json))
            {
                return await ReadAsync(reader, rootName, settings).ConfigureAwait(false);
            }
        }

        public static ISourceNode Create(JObject root, string rootName = null, FhirJsonParsingSettings settings = null) => 
            new FhirJsonNode(root, rootName, settings);
    }
}
