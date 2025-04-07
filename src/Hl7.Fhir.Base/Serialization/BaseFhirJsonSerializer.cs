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
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Serializes the contents of an IReadOnlyDictionary[string,object] according to the rules of FHIR Json serialization.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/json.html. Since all POCOs included
/// in the SDK implement IReadOnlyDictionary, these methods can be used to serialize POCOs to Json.
/// </remarks>
public class BaseFhirJsonSerializer(ModelInspector inspector)
{
    /// <summary>
    /// The <see cref="ModelInspector"/> to be used for serialization metadata.
    /// </summary>
    public ModelInspector Inspector => inspector;

    /// <summary>
    /// Serializes the given POCO with FHIR data into Json.
    /// </summary>
    /// <param name="instance">The instance to serialize.</param>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write the serialized data to.</param>
    /// <param name="filter">An optional <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    public void Serialize(Base instance, Utf8JsonWriter writer, SerializationFilter? filter = null)
    {
        // If the element is summarized, add the subsetted tags.
        if (filter is not null)
            instance = SerializationUtil.MakeSubsettedClone(instance);

        serializeInternal(instance, writer, filter);
    }

    /// <summary>
    /// Serializes the given POCO with FHIR data into Json, optionally skipping the "value" element.
    /// </summary>
    /// <remarks>Not serializing the "value" element is useful when serializing FHIR primitives into two properties, one
    /// with just the value, and one with the id/extensions.</remarks>
    private void serializeInternal(
        Base? element,
        Utf8JsonWriter writer,
        SerializationFilter? filter)
    {
        if (element is null)
        {
            // empty objects in arrays may occur in error situations.
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();

        if (element is Resource r)
            writer.WriteString("resourceType", r.TypeName);

        // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
        if (Inspector.FindOrImportClassMapping(element.GetType()) is not {} mapping)
            throw new InvalidOperationException($"Encountered type {element.GetType()}, which is a support POCO for FHIR, but does not " +
                                                $"have sufficient metadata to be used by the serializer.");

        filter?.EnterObject(element, mapping);

        foreach (var member in element.EnumerateElements())
        {
            var propertyMapping = mapping?.FindMappedElementByName(member.Key);

            if (filter?.TryEnterMember(member.Key, member.Value, propertyMapping) == false)
                continue;

            var propertyName = propertyMapping?.Choice == ChoiceType.DatatypeChoice ?
                addSuffixToElementName(member.Key, member.Value) : member.Key;

            // do we have more possible types (choiceType), then the required type is dependent on the type of the member.Value (is it Integer64 or not?),
            // otherwise we will use the Fhirtype of the propertyMapping.
            var requiredType = (propertyMapping?.FhirType.Length > 1)
                ? (member.Value is Integer64 ? typeof(Integer64) : null)
                : propertyMapping?.FhirType.FirstOrDefault();

            switch (member.Value)
            {
                case PrimitiveType pt:
                    serializeFhirPrimitive(propertyName, pt, writer, filter, requiredType);
                    break;
                case IReadOnlyList<PrimitiveType?> pts:
                    serializeFhirPrimitiveList(propertyName, pts, writer, filter, requiredType);
                    break;
                case IReadOnlyList<Base?> children:   // Not List<Base>, since that is an invariant type.
                    {
                        writer.WritePropertyName(propertyName);
                        writer.WriteStartArray();

                        foreach (var child in children)
                            serializeInternal(child, writer, filter);

                        writer.WriteEndArray();
                        break;
                    }
                case Base b:
                    {
                        writer.WritePropertyName(propertyName);
                        serializeInternal(b, writer, filter);
                        break;
                    }
                default:
                    throw new InvalidOperationException($"{nameof(element.EnumerateElements)} returned a non-Base element of type {member.Value.GetType()}.");
            }

            filter?.LeaveMember(member.Key, member.Value, propertyMapping);
        }

        filter?.LeaveObject(element, mapping);
        writer.WriteEndObject();
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

    /// <summary>
    /// Serializes a list of FHIR primitives into an array element with the given name
    /// </summary>
    /// <remarks>FHIR primitives are handled separately here since they may require
    /// serialization into two Json properties called "elementName" and "_elementName" and
    /// may use Json <c>null</c>s as placeholders.</remarks>
    private void serializeFhirPrimitiveList(
        string elementName,
        IReadOnlyList<PrimitiveType?> values,
        Utf8JsonWriter writer,
        SerializationFilter? filter,
        Type? requiredType = null)
    {
        if(values is null) throw new ArgumentNullException(nameof(values));

        // Don't serialize empty collections.
        if (values.Count == 0) return;

        // We should not write a "elementName" property until we encounter an actual
        // value. If we do, we should "catch up", by creating the property starting
        // with a json array that contains 'null' for each of the elements we encountered
        // until now that did not have a value id/extensions.
        bool wroteStartArray = false;
        int numNullsMissed = 0;

        foreach (var value in values)
        {
            if (value?.ObjectValue is not null)
            {
                if (!wroteStartArray)
                {
                    wroteStartArray = true;
                    writeStartArray(elementName, numNullsMissed, writer);
                }

                SerializePrimitiveValue(value.ObjectValue, writer, requiredType);
            }
            else
            {
                if (wroteStartArray)
                    writer.WriteNullValue();
                else
                    numNullsMissed += 1;
            }
        }

        if (wroteStartArray) writer.WriteEndArray();

        // We should not write a "_elementName" property until we encounter an actual
        // id/extension. If we do, we should "catch up", by creating the property starting
        // with a json array that contains 'null' for each of the elements we encountered
        // until now that did not have id/extensions etc.
        wroteStartArray = false;
        numNullsMissed = 0;

        foreach (var value in values)
        {
            if (value?.EnumerateElements().Any() == true)
            {
                if (!wroteStartArray)
                {
                    wroteStartArray = true;
                    writeStartArray("_" + elementName, numNullsMissed, writer);
                }

                serializeInternal(value, writer, filter);
            }
            else
            {
                if (wroteStartArray)
                    writer.WriteNullValue();
                else
                    numNullsMissed += 1;
            }
        }

        if (wroteStartArray) writer.WriteEndArray();
    }

    private static void writeStartArray(string propName, int numNulls, Utf8JsonWriter writer)
    {
        writer.WriteStartArray(propName);

        for (int i = 0; i < numNulls; i++)
            writer.WriteNullValue();
    }


    /// <summary>
    /// Serializes a FHIR primitive into an element with the given name
    /// </summary>
    /// <remarks>FHIR primitives are handled separately here since they may require
    /// serialization into two Json properties called "elementName" and "_elementName".</remarks>
    private void serializeFhirPrimitive(string elementName, PrimitiveType value, Utf8JsonWriter writer, SerializationFilter? filter, Type? requiredType = null)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (value.ObjectValue is not null)
        {
            // Write a property with 'elementName'
            writer.WritePropertyName(elementName);
            SerializePrimitiveValue(value.ObjectValue, writer, requiredType);
        }

        if (!value.EnumerateElements().Any()) return;

        // Write a property with '_elementName'
        writer.WritePropertyName("_" + elementName);
        serializeInternal(value, writer, filter);
    }

    /// <summary>
    /// Serialize a primitive .NET value that may occur in the POCOs into Json.
    /// </summary>
    /// <remarks>
    /// To allow for future additions to the POCOs the list of primitives supported here
    /// is larger than the set used by the current POCOs. Note that <c>DateTimeOffset</c>c> and
    /// <c>byte[]</c> are considered to be "primitive" values here (used as the value in
    /// <see cref="Instant"/> and <see cref="Base64Binary"/>).
    ///
    /// Note that the current version of System.Text.Json only allows numbers
    /// to be written that fit in .NET's <see cref="decimal"/> type, which may be less
    /// precision than required by the FHIR specification (http://hl7.org/fhir/json.html#primitive).
    /// </remarks>
    protected virtual void SerializePrimitiveValue(object? value, Utf8JsonWriter writer, Type? requiredType)
    {
        switch (value)
        {
            case int i32: writer.WriteNumberValue(i32); break;
            case decimal dec: writer.WriteNumberValue(dec); break;
            // A little note about trimming and whitespaces. The spec says:
            // "(...) In JSON and Turtle whitespace in string values is always significant. Primitive types other than
            // string SHALL NOT have leading or trailing whitespace."
            // Based on this, we are not trimming whitespace here. Validation is not a part of the responsibilities of
            // the serializer, and string-based types (like code and uri) should make sure their values are valid,
            // so should not have trailing spaces to begin with. strings are allowed to have trailing spaces, but should
            // not just be spaces. The serializer will, however, not serialize an element with only whitespace
            // (or an empty byte[]).
            case string s: writer.WriteStringValue(s); break;
            case bool b: writer.WriteBooleanValue(b); break;
            case null: writer.WriteNullValue(); break;
            default:
                writer.WriteStringValue(PrimitiveTypeConverter.ConvertTo<string>(value));
                break;
        }
    }
}

[Obsolete("This class has been replaced by the equivalent BaseFhirJsonSerializer class.")]
public class BaseFhirJsonPocoSerializer(ModelInspector inspector) : BaseFhirJsonSerializer(inspector);