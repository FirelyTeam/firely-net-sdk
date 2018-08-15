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

namespace Hl7.Fhir.Serialization
{
    public static class FhirJsonWriterExtensions
    {
        public static void WriteTo(this ITypedElement source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            new FhirJsonWriter(settings).Write(source, destination);

        public static void WriteTo(this ISourceNode source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            new FhirJsonWriter(settings).Write(source, destination);

#pragma warning disable 612, 618
        public static void WriteTo(this IElementNavigator source, JsonWriter destination, FhirJsonWriterSettings settings = null) =>
            source.ToTypedElement().WriteTo(destination, settings);
#pragma warning restore 612, 618
        public static string ToJson(this ITypedElement source, FhirJsonWriterSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings));

        public static string ToJson(this ISourceNode source, FhirJsonWriterSettings settings = null)
            => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings));

#pragma warning disable 612, 618
        public static string ToJson(this IElementNavigator source, FhirJsonWriterSettings settings = null)
              => SerializationUtil.WriteJsonToString(writer => source.WriteTo(writer, settings));
#pragma warning restore 612, 618

        public static byte[] ToJsonBytes(this ITypedElement source, FhirJsonWriterSettings settings = null)
                => SerializationUtil.WriteJsonToBytes(writer => source.WriteTo(writer, settings));
    }
}
