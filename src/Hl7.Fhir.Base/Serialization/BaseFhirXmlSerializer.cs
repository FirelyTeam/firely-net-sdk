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

        writer.WriteStartDocument();

        // Wrap the instance with a named element if either a root name is given,
        // or we are serializing a datatype (=a subtree).
        if (rootName is not null)
            writer.WriteStartElement(rootName, XmlNs.FHIR);
        else if(instance is not Resource)
            writer.WriteStartElement(instance.TypeName, XmlNs.FHIR);

        serializeInternal(instance, writer, filter);

        if (rootName is not null) writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    private void serializeInternal(
        Base element,
        XmlWriter writer,
        SerializationFilter? filter)
    {
        if (element is Resource r)
            writer.WriteStartElement(r.TypeName, XmlNs.FHIR);

        // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
        if (Inspector.FindOrImportClassMapping(element.GetType()) is not {} mapping)
            throw new InvalidOperationException($"Encountered type {element.GetType()}, which is a support POCO for FHIR, but does not " +
                                                $"have sufficient metadata to be used by the serializer.");

        filter?.EnterObject(element, mapping);

        serializeElement(element, writer, filter, mapping);

        filter?.LeaveObject(element, mapping);

        if (element is Resource) writer.WriteEndElement();
    }

    private void serializeElement(Base element, XmlWriter writer, SerializationFilter? filter, ClassMapping? mapping)
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


    private void serializeMemberValue(string elementName, object? value, XmlWriter writer, SerializationFilter? filter)
    {
        try
        {

        switch (value)
        {
            case null:
                break;  // In error situations there may be a null in a list, just don't serialize it.
            case XHtml xhtml:
                writer.WriteRaw(xhtml.Value ?? "");
                break;
            case Base complex:
                writer.WriteStartElement(elementName, XmlNs.FHIR);
                serializeInternal(complex, writer, filter);
                writer.WriteEndElement();
                break;
            default:
                SerializePrimitiveValue(elementName, value, writer);
                break;
        }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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