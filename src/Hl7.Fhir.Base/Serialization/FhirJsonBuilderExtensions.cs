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
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Hl7.Fhir.Serialization;

public static class FhirJsonBuilderExtensions
{
    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance to FHIR Json.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="writer">The <see cref="JsonWriter"/> to write the serialized data to.</param>
    public static void WriteTo(this ITypedElement source, JsonWriter writer) =>
        new FhirJsonBuilder().Build(source).writeTo(writer);

    /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ITypedElement,Newtonsoft.Json.JsonWriter)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task WriteToAsync(this ITypedElement source, JsonWriter destination) =>
        await new FhirJsonBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    /// <summary>
    /// Serializes an <see cref="ISourceNode"/> instance into FHIR Json.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="writer">The <see cref="JsonWriter"/> to write the serialized data to.</param>
    /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
    /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirJsonNode"/>.</remarks>
    public static void WriteTo(this ISourceNode source, JsonWriter writer) =>
        new FhirJsonBuilder().Build(source).writeTo(writer);

    /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ISourceNode,Newtonsoft.Json.JsonWriter)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task WriteToAsync(this ISourceNode source, JsonWriter destination) =>
        await new FhirJsonBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    /// <summary>
    /// Serializes an <see cref="ISourceNode"/> instance into a <see cref="JObject"/>.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
    /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirJsonNode"/>.</remarks>
    public static JObject ToJObject(this ISourceNode source) => new FhirJsonBuilder().Build(source);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a <see cref="JObject"/>
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    public static JObject ToJObject(this ITypedElement source) => new FhirJsonBuilder().Build(source);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a FHIR Json string.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    public static string ToJson(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode { Poco: Resource resource } node)
            return SerializationUtil.WriteJsonToString(source.WriteTo, pretty);

        var inspector = node.FindInspector() ?? ModelInspector.ForType(resource.GetType());
        var ser = new BaseFhirJsonSerializer(inspector);
        return ser.SerializeToString(resource, pretty);
    }

    /// <inheritdoc cref="ToJson(Hl7.Fhir.ElementModel.ITypedElement,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<string> ToJsonAsync(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode { Poco: Resource resource } node)
            return await SerializationUtil
                .WriteJsonToStringAsync(async writer => await source.WriteToAsync(writer).ConfigureAwait(false),
                    pretty).ConfigureAwait(false);

        var inspector = node.FindInspector() ?? ModelInspector.ForType(resource.GetType());
        var ser = new BaseFhirJsonSerializer(inspector);
        return ser.SerializeToString(resource, pretty);
    }

    /// <summary>
    /// Serializes an <see cref="ISourceNode"/> instance into a FHIR Json string.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
    /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirJsonNode"/>.</remarks>
    public static string ToJson(this ISourceNode source, bool pretty = false)
        => SerializationUtil.WriteJsonToString(source.WriteTo, pretty);

    /// <inheritdoc cref="ToJson(Hl7.Fhir.ElementModel.ISourceNode,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<string> ToJsonAsync(this ISourceNode source, bool pretty = false)
        => await SerializationUtil.WriteJsonToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a FHIR Json byte array.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Json.</param>
    public static byte[] ToJsonBytes(this ITypedElement source, bool pretty = false)
        => SerializationUtil.WriteJsonToBytes(source.WriteTo, pretty);

    /// <inheritdoc cref="ToJsonBytes"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<byte[]> ToJsonBytesAsync(this ITypedElement source, bool pretty = false)
        => await SerializationUtil.WriteJsonToBytesAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

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
}