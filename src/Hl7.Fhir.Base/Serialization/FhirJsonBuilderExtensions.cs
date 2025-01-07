/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public static class FhirJsonBuilderExtensions
{
    private static void writeTo(this JObject root, JsonWriter destination)
    {
        root.WriteTo(destination);
        destination.Flush();
    }

    private static async Task writeToAsync(this JObject root, JsonWriter destination)
    {
        await root.WriteToAsync(destination).ConfigureAwait(false);
        await destination.FlushAsync().ConfigureAwait(false);
    }

    public static void WriteTo(this ITypedElement source, JsonWriter destination) =>
        new FhirJsonBuilder().Build(source).writeTo(destination);

    public static async Task WriteToAsync(this ITypedElement source, JsonWriter destination) =>
        await new FhirJsonBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    public static void WriteTo(this ISourceNode source, JsonWriter destination) =>
        new FhirJsonBuilder().Build(source).writeTo(destination);

    public static async Task WriteToAsync(this ISourceNode source, JsonWriter destination) =>
        await new FhirJsonBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    public static JObject ToJObject(this ISourceNode source) => new FhirJsonBuilder().Build(source);

    public static JObject ToJObject(this ITypedElement source) => new FhirJsonBuilder().Build(source);

    public static string ToJson(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode { Poco: Resource resource } node)
            return SerializationUtil.WriteJsonToString(source.WriteTo, pretty);

        var inspector = node.FindInspector() ?? ModelInspector.ForType(resource.GetType());
        var ser = new BaseFhirJsonPocoSerializer(inspector);
        return ser.SerializeToString(resource, pretty);
    }

    public static async Task<string> ToJsonAsync(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode { Poco: Resource resource } node)
            return await SerializationUtil
                .WriteJsonToStringAsync(async writer => await source.WriteToAsync(writer).ConfigureAwait(false),
                    pretty).ConfigureAwait(false);

        var inspector = node.FindInspector() ?? ModelInspector.ForType(resource.GetType());
        var ser = new BaseFhirJsonPocoSerializer(inspector);
        return ser.SerializeToString(resource, pretty);
    }

    public static string ToJson(this ISourceNode source, bool pretty = false)
        => SerializationUtil.WriteJsonToString(source.WriteTo, pretty);

    public static async Task<string> ToJsonAsync(this ISourceNode source, bool pretty = false)
        => await SerializationUtil.WriteJsonToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

    public static byte[] ToJsonBytes(this ITypedElement source, bool pretty = false)
        => SerializationUtil.WriteJsonToBytes(source.WriteTo, pretty);

    public static async Task<byte[]> ToJsonBytesAsync(this ITypedElement source, bool pretty = false)
        => await SerializationUtil.WriteJsonToBytesAsync(source.WriteToAsync, pretty).ConfigureAwait(false);
}