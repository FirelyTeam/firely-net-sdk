using Hl7.Fhir.ElementModel.Adapters;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.ElementModel;

public static partial class SourceNodeExtensions
{
    /// <param name="source">The source node to convert.</param>
    extension(ISourceNode source)
    {
        public Base ToPoco(ModelInspector inspector, Type pocoType = null, PocoBuilderSettings settings = null) =>
            source is PocoNode { Poco: { } poco } && (pocoType is null || pocoType.IsAssignableFrom(poco.GetType()))
                ? poco
                : new NewPocoBuilder(inspector, settings ?? new() { AllowUnrecognizedEnums = true, IgnoreUnknownMembers = true }).BuildFrom(source, pocoType);

        public T ToPoco<T>(ModelInspector inspector, PocoBuilderSettings settings = null) where T : Base
        {
            if (source is PocoNode { Poco: T poco })
                return poco;

            return (T)source.ToPoco(inspector, typeof(T), settings);
        }

        /// <summary>
        /// Converts a source node to a PocoNode without first building a TypedElement wrapper.
        /// </summary>
        /// <param name="inspector">The model inspector supplying version-specific metadata.</param>
        /// <param name="rootName">Optional. The name to use for the root node.</param>
        public PocoNode ToPocoNode(ModelInspector inspector, string rootName = null)
        {
            if (source is PocoNode pocoNode && (rootName is null || pocoNode.Name == rootName))
                return pocoNode;

            return source
                .ToPoco(inspector, settings: new PocoBuilderSettings { IgnoreUnknownMembers = true, AllowUnrecognizedEnums = true })
                .ToPocoNode(inspector, rootName ?? source.Name);
        }

        /// <summary>
        /// Turns the <c>ISourceNode</c> into a <see cref="ITypedElement"/> by adding type information to it.
        /// </summary>
        /// <param name="provider">The provider which supplies type information.</param>
        /// <param name="type">Optional. The type of the element at the root.</param>
        /// <param name="settings"></param>
        /// <returns>An <see cref="ITypedElement"/> that represents the data in the node, with type information
        /// added to it.</returns>
        /// <remarks>This extension method decorates the <c>ISourceNode</c> with a new instance of
        /// an <see cref="TypedElementOnSourceNode"/>, passing on the parameters of this extension method.</remarks>
        /// <seealso cref="ITypedElement"/>
        public ITypedElement ToTypedElement(IStructureDefinitionSummaryProvider provider, string type = null, TypedElementSettings settings = null)
            => source as ITypedElement ?? new TypedElementOnSourceNode(source, type, provider, settings: settings ?? new TypedElementSettings() { ErrorMode = TypedElementSettings.TypeErrorMode.Passthrough });

        /// <summary>
        /// Turns the <c>ISourceNode</c> into a <see cref="ITypedElement"/> by adding type information from <see cref="ModelInspector.Base"/>.
        /// </summary>
        /// <returns>An <see cref="ITypedElement"/> that represents the data in the node, with type information
        /// added to it.</returns>
        /// <remarks>This extension method decorates the <c>ISourceNode</c> with a new instance of
        /// an <see cref="TypedElementOnSourceNode"/>, passing on the parameters of this extension method.</remarks>
        /// <seealso cref="ITypedElement"/>
        public ITypedElement ToTypedElement() => source.ToTypedElement(ModelInspector.Base);

        /// <summary>
        /// Adapting an <c>ISourceNode</c> to a <see cref="ITypedElement"/> without adding type information to it.
        /// </summary>
        /// <returns></returns>
        /// <remarks>In contrast to <see cref="ToTypedElement(ISourceNode, IStructureDefinitionSummaryProvider, string, TypedElementSettings)"/>,
        /// this method simulates an <c>ITypedElement</c> on top of an <c>ISourceNode</c>, without adding type information to
        /// it. This is used internally in a few places in the API, where the component using the <c>ITypedNode</c> is aware it
        /// cannot depend on type information being present, but should normally not be used.
        /// </remarks>
        [Obsolete("WARNING! For internal API use only. Turning an untyped SourceNode into an ITypedElement without providing" +
                  "type information (see other overload) will cause side-effects with components in the API that are not prepared to deal with" +
                  "missing type information. Please don't use this overload unless you know what you are doing.")]
        public ITypedElement ToTypedElementLegacy() =>
            new SourceNodeToTypedElementAdapter(source);
    }


    #region Xml

    /// <param name="source">The instance to serialize.</param>
    extension(ISourceNode source)
    {
        /// <summary>
        /// Serializes an <see cref="ISourceNode"/> instance into a FHIR Xml string.
        /// </summary>
        /// <param name="pretty">Formats and indents the serialized Xml.</param>
        /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
        /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirXmlNode"/>.</remarks>
        public string ToXml(bool pretty = false)
            => SerializationUtil.WriteXmlToString(source.WriteTo, pretty);

        /// <inheritdoc cref="ToXml(Hl7.Fhir.ElementModel.ISourceNode,bool)"/>
        [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
        public async Task<string> ToXmlAsync(bool pretty = false)
            => await SerializationUtil.WriteXmlToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

        /// <summary>
        /// Serializes an <see cref="ISourceNode"/> instance into a <see cref="XDocument"/>.
        /// </summary>
        /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
        /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirXmlNode"/>.</remarks>
        public XDocument ToXDocument() =>
            new FhirXmlBuilder().Build(source);

        /// <summary>
        /// Serializes an <see cref="ISourceNode"/> instance into FHIR Xml.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
        /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
        /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirXmlNode"/>.</remarks>
        public void WriteTo(XmlWriter writer) =>
            new FhirXmlBuilder().Build(source).writeTo(writer);

        /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ISourceNode,System.Xml.XmlWriter)"/>
        public async Task WriteToAsync(XmlWriter destination) =>
            await new FhirXmlBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);
    }

    #endregion

    #region Json

    /// <param name="source">The instance to serialize.</param>
    extension(ISourceNode source)
    {
        /// <summary>
        /// Serializes an <see cref="ISourceNode"/> instance into a FHIR Json string.
        /// </summary>
        /// <param name="pretty">Formats and indents the serialized Json.</param>
        /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
        /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirJsonNode"/>.</remarks>
        public string ToJson(bool pretty = false)
            => SerializationUtil.WriteJsonToString(source.WriteTo, pretty);

        /// <inheritdoc cref="ToJson(Hl7.Fhir.ElementModel.ISourceNode,bool)"/>
        [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
        public async Task<string> ToJsonAsync(bool pretty = false)
            => await SerializationUtil.WriteJsonToStringAsync(source.WriteToAsync, pretty).ConfigureAwait(false);

        /// <summary>
        /// Serializes an <see cref="ISourceNode"/> instance into a <see cref="JObject"/>.
        /// </summary>
        /// <param name="preserveWhiteSpaceInValues">Whether to preserve whitespace in string values when serializing</param>
        /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
        /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirJsonNode"/>.</remarks>
        public JObject ToJObject(bool preserveWhiteSpaceInValues = false) => new FhirJsonBuilder(preserveWhiteSpaceInValues).Build(source);

        /// <summary>
        /// Serializes an <see cref="ISourceNode"/> instance into FHIR Json.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter"/> to write the serialized data to.</param>
        /// <remarks>Since <see cref="ISourceNode"/> has no type information, this function will throw unless
        /// the <see cref="ISourceNode"/> originated from parsing using <see cref="FhirJsonNode"/>.</remarks>
        public void WriteTo(JsonWriter writer) =>
            new FhirJsonBuilder().Build(source).writeTo(writer);

        /// <inheritdoc cref="WriteTo(Hl7.Fhir.ElementModel.ISourceNode,Newtonsoft.Json.JsonWriter)"/>
        [Obsolete("Async support will be removed in the next major release, please use the non-async version instead")]
        public async Task WriteToAsync(JsonWriter destination) =>
            await new FhirJsonBuilder().Build(source).writeToAsync(destination).ConfigureAwait(false);
    }

    #endregion
}