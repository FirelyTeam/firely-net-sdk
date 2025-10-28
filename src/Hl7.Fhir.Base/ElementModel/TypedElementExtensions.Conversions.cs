using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.ElementModel;

#nullable enable

public static partial class TypedElementExtensions
{
#if NETSTANDARD2_1
    [Obsolete("ToScopedNode should only be used in combination with ToTypedElementLegacy. PocoNode should implement all functionality of ScopedNode with some additional benefits." +
              "ToTypedElement().ToScopedNode() should in most cases be substituted with ToPocoNode()")]
#else
    [Experimental("SDK0002")]
#endif
    public static ScopedNode ToScopedNode(this ITypedElement node) =>
        node as ScopedNode ?? new ScopedNode(node);
    
    public static ISourceNode ToSourceNode(this ITypedElement node) => node as ISourceNode ?? new TypedElementToSourceNodeAdapter(node);

    public static T ToPoco<T>(this ITypedElement element, ModelInspector inspector, PocoBuilderSettings? settings = null) where T : Base
    {
        if (element is PocoNode { Poco: T poco })
            return poco;
        
        return (T)new NewPocoBuilder(inspector, settings ?? new PocoBuilderSettings() { AllowUnrecognizedEnums = true, IgnoreUnknownMembers = true }).BuildFrom(element, typeof(T));
    }

    public static Base ToPoco(this ITypedElement element, ModelInspector inspector, Type? pocoType = null, PocoBuilderSettings? settings = null) =>
        new NewPocoBuilder(inspector, settings ?? new PocoBuilderSettings() { AllowUnrecognizedEnums = true, IgnoreUnknownMembers = true }).BuildFrom(element, pocoType);

    /// <summary>
    /// Converts a typed element to a PocoNode.
    /// </summary>
    /// <remarks>Will produce significantly more accurate results if a modelinspector is provided, or if the input is already a PocoNode</remarks>
    public static PocoNode ToPocoNode(this ITypedElement node, ModelInspector? inspector = null, string? rootName = null)
    {
        if (node is PocoNode pn)
            return pn;
        
        var model = inspector ?? node.Annotation<ModelInspector>() ?? ModelInspector.Base;
        return node.ToPoco(model, null, new() { IgnoreUnknownMembers = true, AllowUnrecognizedEnums = true }).ToPocoNode(model, rootName: rootName);
    }

    #region Json

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

        return PocoNodeExtensions.ToJson(node, pretty);
    }

    /// <inheritdoc cref="ToJson(Hl7.Fhir.ElementModel.ITypedElement,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<string> ToJsonAsync(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode { Poco: Resource resource } node)
            return await SerializationUtil
                .WriteJsonToStringAsync(async writer => await source.WriteToAsync(writer).ConfigureAwait(false),
                    pretty).ConfigureAwait(false);

        return PocoNodeExtensions.ToJson(node, pretty);
    }

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

    #endregion

    #region Xml
    
    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a FHIR Xml byte array.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    public static byte[] ToXmlBytes(this ITypedElement source, bool pretty = false)
        => SerializationUtil.WriteXmlToBytes(source.WriteTo, pretty);

    /// <inheritdoc cref="ToXmlBytes(Hl7.Fhir.ElementModel.ITypedElement,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<byte[]> ToXmlBytesAsync(this ITypedElement source, bool pretty = false)
        => await SerializationUtil.WriteXmlToBytesAsync(source.WriteToAsync, pretty).ConfigureAwait(false);
    
    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a <see cref="XDocument"/>
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    public static XDocument ToXDocument(this ITypedElement source) =>
        new FhirXmlBuilder().Build(source);

    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance into a FHIR Xml string.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="pretty">Formats and indents the serialized Xml.</param>
    public static string ToXml(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode node)
            return SerializationUtil.WriteXmlToString(source.WriteTo, pretty);

        return PocoNodeExtensions.ToXml(node, pretty);
    }
    
    /// <summary>
    /// Serializes an <see cref="ITypedElement"/> instance to FHIR Xml.
    /// </summary>
    /// <param name="source">The instance to serialize.</param>
    /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
    public static void WriteTo(this ITypedElement source, XmlWriter writer) =>
        new FhirXmlBuilder().Build(source).writeTo(writer);

    /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ITypedElement,System.Xml.XmlWriter)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task WriteToAsync(this ITypedElement source, XmlWriter destination) =>
        await new FhirXmlBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);

    /// <inheritdoc cref="ToXml(Hl7.Fhir.ElementModel.ITypedElement,bool)"/>
    [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
    public static async Task<string> ToXmlAsync(this ITypedElement source, bool pretty = false)
    {
        if (source is not PocoNode node)
            return await SerializationUtil.WriteXmlToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

        return PocoNodeExtensions.ToXml(node, pretty);
    }

    #endregion
}