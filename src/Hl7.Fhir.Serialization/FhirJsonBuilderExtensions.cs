/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Hl7.Fhir.Serialization
{
    public static class FhirJsonBuilderExtensions
    {
        private static void writeTo(this JObject root, JsonWriter destination, string rootName = null)
        {
            root.WriteTo(destination);
            destination.Flush();
        }

        public static void WriteTo(this ITypedElement source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source).writeTo(destination);

        public static void WriteTo(this ISourceNode source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source).writeTo(destination);

        public static JObject ToJObject(this ISourceNode source, FhirJsonWriterSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source);

        public static JObject ToJObject(this ITypedElement source, FhirJsonWriterSettings settings = null) =>
            new FhirJsonBuilder(settings).Build(source);

#pragma warning disable 612, 618
        [Obsolete("Please consider switching to ITypedElement (which is what the new parsers return).")]
        public static void WriteTo(this IElementNavigator source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);
#pragma warning restore 612, 618
        public static string ToJson(this ITypedElement source, FhirJsonWriterSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false);

        public static string ToJson(this ISourceNode source, FhirJsonWriterSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false);

#pragma warning disable 612, 618
        [Obsolete("Please consider switching to ITypedElement (which is what the new parsers return).")]
        public static string ToJson(this IElementNavigator source, FhirJsonWriterSettings settings = null)
              => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings), settings?.Pretty ?? false);
#pragma warning restore 612, 618

        public static byte[] ToJsonBytes(this ITypedElement source, FhirJsonWriterSettings settings = null)
                => SerializationUtil.WriteJsonToBytes(writer => source.WriteTo(writer, settings));
    }
}
