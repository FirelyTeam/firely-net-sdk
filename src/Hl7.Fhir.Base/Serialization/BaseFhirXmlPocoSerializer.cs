/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Serializes the contents of a POCO according to the rules of FHIR Xml serialization.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/xml.html.
/// </remarks>
public class BaseFhirXmlPocoSerializer
{
    /// <summary>
    /// The release of FHIR for which this serializer is configured.
    /// </summary>
    public FhirRelease Release { get; }

    /// <summary>
    /// Construct a new serializer for a specific release of FHIR.
    /// </summary>
    public BaseFhirXmlPocoSerializer(FhirRelease release)
    {
        Release = release;
    }

    /// <summary>
    /// Serializes the given dictionary with FHIR data into Json.
    /// </summary>
    public void Serialize(
        Base element,
        XmlWriter writer,
        SerializationFilter? summary = default)
    {
        writer.WriteStartDocument();

        // If we are serializing a non-resource, or we are serializing a nested resource,
        // we need to pick a name for the root element.
        var pickElementName = element is not Resource or IScopedNode { Parent: not null };
        if (pickElementName)
        {
            // If we are an element with a name, pick that, otherwise us the name of the type.
            var nodeName = element is ITypedElement ite ? ite.Name : element.TypeName;

            writer.WriteStartElement(nodeName, XmlNs.FHIR);
        }

        serializeInternal(element, writer, summary);

        if (pickElementName) writer.WriteEndElement();
        writer.WriteEndDocument();
    }

    /// <summary>
    /// Serializes the given dictionary with FHIR data into UTF8 encoded Json.
    /// </summary>
    public string SerializeToString(
        Base element,
        SerializationFilter? summary = default) =>
        SerializationUtil.WriteXmlToString(element, (o,w) => Serialize(o, w, summary));

    /// <summary>
    /// Serializes the given dictionary with FHIR data into Json, optionally skipping the "value" element.
    /// </summary>
    /// <remarks>Not serializing the "value" element is useful when serializing FHIR primitives into two properties, one
    /// with just the value, and one with the id/extensions.</remarks>
    private void serializeInternal(
        Base element,
        XmlWriter writer,
        SerializationFilter? filter)
    {
        if (element is Resource r)
            writer.WriteStartElement(r.TypeName, XmlNs.FHIR);

        // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
        if (!ClassMapping.TryGetMappingForType(element.GetType(), Release, out var mapping))
            throw new InvalidOperationException($"Encountered type {element.GetType()}, which is a support POCO for FHIR, but does not " +
                                                $"have sufficient metadata to be used by the serializer.");

        filter?.EnterObject(element, mapping);

        serializeElement(element, writer, filter, mapping);

        filter?.LeaveObject(element, mapping);

        if (element is Resource) writer.WriteEndElement();
    }

    private void serializeElement(Base element, XmlWriter writer, SerializationFilter? filter, ClassMapping? mapping)
    {
        // Make sure that elements with attributes are serialized first.
        var orderedMembers = element
            .Select(m => (m, mapping: mapping?.FindMappedElementByName(m.Key)))
            .OrderBy(p => p.mapping?.SerializationHint != XmlRepresentation.XmlAttr);

        foreach (var ((mKey, mValue), propertyMapping) in orderedMembers)
        {
            if (filter?.TryEnterMember(mKey, mValue, propertyMapping) == false)
                continue;

            var elementName = propertyMapping?.Choice == ChoiceType.DatatypeChoice ?
                addSuffixToElementName(mKey, mValue) : mKey;

            if (mValue is ICollection coll and not byte[])
            {
                foreach (var value in coll)
                    serializeMemberValue(elementName, value, writer, filter);
            }
            else
                serializeMemberValue(elementName, mValue, writer, filter);

            filter?.LeaveMember(mKey, mValue, propertyMapping);
        }
    }

    private static string addSuffixToElementName(string elementName, object elementValue)
    {
        var typeName = elementValue switch
        {
            IEnumerable<Base> ib => ib.FirstOrDefault()?.TypeName,
            Base b => b.TypeName,
            _ => null
        };

        return typeName is null ? elementName : elementName + char.ToUpperInvariant(typeName[0]) + typeName[1..];
    }


    private void serializeMemberValue(string elementName, object value, XmlWriter writer, SerializationFilter? filter)
    {
        switch (value)
        {
            case XHtml xhtml:
                writer.WriteRaw(xhtml.Value);
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
            uint ui32 => XmlConvert.ToString(ui32),
            long i64 => XmlConvert.ToString(i64),
            ulong ui64 => XmlConvert.ToString(ui64),
            float si => XmlConvert.ToString(si),
            double dbl => XmlConvert.ToString(dbl),
            decimal dec => XmlConvert.ToString(dec),
            // A little note about trimming and whitespaces. The spec says:
            // "Implementers SHOULD trim leading and trailing whitespace before writing and SHOULD trim leading
            // and trailing whitespace when reading attribute values (for XML schema conformance)"
            string s => s.Trim(),
            bool b => XmlConvert.ToString(b),
            DateTimeOffset dto => ElementModel.Types.DateTime.FormatDateTimeOffset(dto),
            byte[] bytes => Convert.ToBase64String(bytes),
            _ => throw new FormatException($"There is no known serialization for type {value.GetType()} into an Xml primitive property value.")
        };

        writer.WriteAttributeString(elementName, ns: null, value: literal);
    }
}