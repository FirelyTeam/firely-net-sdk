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
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.Serialization;


/// <summary>
/// Serializes the contents of a POCO according to the rules of FHIR Xml serialization.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/xml.html.
/// </remarks>
public class BaseFhirXmlSerializer(ModelInspector inspector)
{
    /// <summary>
    /// The <see cref="ModelInspector"/> to be used for serialization metadata.
    /// </summary>
    public ModelInspector Inspector => inspector;

    /// <summary>
    /// Serializes the given POCO with FHIR data into Xml.
    /// </summary>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="writer">The <see cref="XmlWriter"/> to write the serialized data to.</param>
    /// <param name="filterFactory">An optional factory that creates a fresh <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    /// <param name="rootName">When serializing subtrees, the root element is named after the type of the instance.
    /// If necessary, use this parameter to override the name of the root element.</param>
    public void Serialize(
        Base instance,
        XmlWriter writer,
        Func<SerializationFilter?>? filterFactory = null,
        string? rootName = null)
    {
        // If the element is summarized, add the subsetted tags.
        var filter = filterFactory?.Invoke();
        if (filter is not null)
            instance = SerializationUtil.MakeSubsettedClone(instance);

        // Comments around the root element are not written by the loop over the members (there is no
        // parent element to write them into), so they are handled here.
        var rootComments = instance.Annotation<SourceComments>();

        // Elements that turn out to be empty must not be written at all, but we only know that after we have
        // walked their members. The PruningXmlWriter postpones the start tags for us, so the serializer below
        // can simply write, and never has to take an empty element back.
        // The root element is exempt: XmlWriter.WriteEndDocument() throws on a document without one.
        var pruningWriter = new PruningXmlWriter(writer);

        pruningWriter.WriteStartDocument();

        writeComments(rootComments?.CommentsBefore, pruningWriter);

        // Wrap the instance with a named element if either a root name is given,
        // or we are serializing a datatype (=a subtree).
        if (rootName is not null)
            pruningWriter.WriteStartElement(rootName, XmlNs.FHIR, PruningXmlWriter.OnEmpty.Keep);
        else if(instance is not Resource)
            pruningWriter.WriteStartElement(instance.TypeName, XmlNs.FHIR, PruningXmlWriter.OnEmpty.Keep);

        serializeInternal(instance, pruningWriter, filter, PruningXmlWriter.OnEmpty.Keep);

        if (rootName is not null) pruningWriter.WriteEndElement();

        // Only write these once the root element is actually closed - for a datatype without a root name
        // the wrapping element above is left open for WriteEndDocument() to close.
        if (rootName is not null || instance is Resource)
            writeComments(rootComments?.DocumentEndComments, pruningWriter);

        pruningWriter.WriteEndDocument();
    }

    private void serializeInternal(
        Base element,
        PruningXmlWriter writer,
        SerializationFilter? filter,
        PruningXmlWriter.OnEmpty onEmpty = PruningXmlWriter.OnEmpty.Omit)
    {
        // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
        // Resolved before any output is written, so a failure does not leave the writer in a broken state.
        if (Inspector.FindOrImportClassMapping(element) is not {} mapping)
            throw new InvalidOperationException($"Encountered type {element.GetType()}, which is a support POCO for FHIR, but does not " +
                                                $"have sufficient metadata to be used by the serializer.");

        if (element is Resource r)
            writer.WriteStartElement(r.TypeName, XmlNs.FHIR, onEmpty);

        filter?.EnterObject(element, mapping);

        serializeElement(element, writer, filter, mapping);

        filter?.LeaveObject(element, mapping);

        if (element is Resource) writer.WriteEndElement();
    }

    private void serializeElement(Base element, PruningXmlWriter writer, SerializationFilter? filter, ClassMapping? mapping)
    {
        static int attributeSorter(PropertyMapping? mapping, Base? value)
        {
            // Make sure that known attributes are serialized first.
            if (mapping?.SerializationHint == XmlRepresentation.XmlAttr)
                return mapping.Order is { } number ? Int32.MinValue + number : -1;
            if (value?.Annotation<XmlRepresentationAnnotation>()?.Value == XmlRepresentation.XmlAttr)
                return -1;

            // Order elements by order after the attributes, unknown elements at the end.
            return mapping?.Order ?? Int32.MaxValue;
        }

        // Make sure that elements with attributes are serialized first.
        // Add the special "value" attribute if this is a FhirPrimitive.
        var orderedMembers = element
            .EnumerateElements()
            .Concat(element is PrimitiveType { JsonValue: {} ptValue } ? [KeyValuePair.Create("value", ptValue)] : [])
            .Select(m => (m, mapping: mapping?.FindMappedElementByName(m.Key)))
            .OrderBy(p => attributeSorter(p.mapping, p.m.Value as Base));

        foreach (var ((mKey, mValue), propertyMapping) in orderedMembers)
        {
            if (filter?.TryEnterMember(mKey, mValue, propertyMapping) == false)
                continue;

            var serializeValue = mValue!;

            if (serializeValue is PrimitiveType primitive && 
                (propertyMapping?.SerializationHint ?? primitive.Annotation<XmlRepresentationAnnotation>()?.Value) == XmlRepresentation.XmlAttr)
            {
                // If this is a FHIR primitive element marked as XmlAttr,
                // take the primitive's value (e.g. Extension.url, Element.id)
                serializeValue = primitive.JsonValue!;
            }

            var elementName = propertyMapping?.Choice == ChoiceType.DatatypeChoice ?
                addSuffixToElementName(mKey, serializeValue) : mKey;

            if (serializeValue is IReadOnlyList<Base?> coll)
            {
                foreach (var value in coll)
                    serializeMemberValue(elementName, value, writer, filter);
            }
            else
                serializeMemberValue(elementName, serializeValue, writer, filter);

            filter?.LeaveMember(mKey, serializeValue, propertyMapping);
        }

        // Comments that were the last content of this element in the source data. Written after the children,
        // so they end up just before the closing tag written by our caller.
        writeComments(element.Annotation<SourceComments>()?.ClosingComments, writer);
    }

    /// <summary>
    /// Writes the comments retained from the source data (see <see cref="DeserializerSettings.RetainComments"/>).
    /// </summary>
    /// <remarks>May only be called when the writer is not writing the attributes of a start tag: a comment
    /// cannot be written into a start tag. Since a comment is only ever annotated onto an element that was
    /// itself serialized as an element, that is guaranteed by writing them at the element branches only.
    ///
    /// Note that a comment counts as content, so an element that holds nothing but a retained comment is
    /// written rather than pruned. Dropping a comment the caller asked us to retain would be worse than
    /// emitting an element that the source data had in that shape to begin with.</remarks>
    private static void writeComments(string[]? comments, PruningXmlWriter writer)
    {
        if (comments is null) return;

        foreach (var comment in comments)
            writer.WriteComment(comment);
    }

    private static string addSuffixToElementName(string elementName, object? elementValue)
    {
        var typeName = elementValue switch
        {
            IEnumerable<Base> ib => ib.FirstOrDefault()?.TypeName,
            Base b => b.TypeName,
            _ => null
        };

        return typeName is null ? elementName : elementName + char.ToUpperInvariant(typeName[0]) + typeName[1..];
    }


    private void serializeMemberValue(string elementName, object? value, PruningXmlWriter writer, SerializationFilter? filter)
    {
        switch (value)
        {
            case null:
                break;  // In error situations there may be a null in a list, just don't serialize it.
            case XHtml xhtml:
                writeComments(xhtml.Annotation<SourceComments>()?.CommentsBefore, writer);
                writer.WriteRaw(xhtml.Value);
                break;
            case Base complex:
                writeComments(complex.Annotation<SourceComments>()?.CommentsBefore, writer);
                writer.WriteStartElement(elementName, XmlNs.FHIR);
                serializeInternal(complex, writer, filter);
                writer.WriteEndElement();
                break;
            default:
                SerializePrimitiveValue(elementName, value, writer.Value);
                break;
        }
    }

    /// <summary>
    /// Serialize a primitive .NET value that may occur in the POCOs into XML.
    /// </summary>
    /// <remarks>
    /// To allow for future additions to the POCOs the list of primitives supported here
    /// is larger than the set used by the current POCOs. Note that <c>DateTimeOffset</c>c> and
    /// <c>byte[]</c> are considered to be "primitive" values here (used as the value in
    /// <see cref="Instant"/> and <see cref="Base64Binary"/>).
    /// </remarks>
    protected virtual void SerializePrimitiveValue(string elementName, object value, XmlWriter writer)
    {
        var literal = value switch
        {
            int i32 => XmlConvert.ToString(i32),
            decimal dec => XmlConvert.ToString(dec),
            // A little note about trimming and whitespaces. The spec says:
            // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading
            // and trailing whitespace when reading attribute values (for XML schema conformance)"
            string s => s.Trim(),
            bool b => XmlConvert.ToString(b),
            _ => PrimitiveTypeConverter.ConvertTo<string>(value)
        };

        writer.WriteAttributeString(elementName, ns: null, value: literal);
    }
}

[Obsolete("This class has been replaced by the equivalent BaseFhirXmlSerializer class.")]
public class BaseFhirXmlPocoSerializer(ModelInspector inspector) : BaseFhirXmlSerializer(inspector);