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
using Hl7.Fhir.Utility;
using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Serializes the contents of an instance of Base, according to the rules of FHIR Json serialization.
/// </summary>
/// <remarks>The serializer uses the format documented in https://www.hl7.org/fhir/json.html. Since all POCOs included
/// in the SDK implement Base, these methods can be used to serialize POCOs to Json.
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
    /// <param name="filterFactory">An optional factory that creates a fresh <see cref="SerializationFilter"/> to use to serialize summaries.</param>
    public void Serialize(Base instance, Utf8JsonWriter writer, Func<SerializationFilter?>? filterFactory = null)
    {
        // If the element is summarized, add the subsetted tags.
        var filter = filterFactory?.Invoke();
        if (filter is not null)
            instance = SerializationUtil.MakeSubsettedClone(instance);

        var deferredWriter = new DeferredJsonWriter(writer);

        // This handles an edge-case where we are asked to serialize just a primitive value.
        // For compatibility with SDK5 logic, we emit object with pseudo-property 'value' and value of the fhir primitive.
        // Issue for context: https://github.com/FirelyTeam/firely-net-sdk/issues/3286
        if (instance is not PrimitiveType val)
        {
            serializeInternal(instance, deferredWriter, filter, required: true);
        }
        else
        {
            using var root = deferredWriter.BeginObject(required: true);
            serializeFhirPrimitive("value", val, deferredWriter, filter);
        }
    }

    /// <summary>
    /// Serializes the given POCO with FHIR data into Json, optionally skipping the "value" element.
    /// </summary>
    /// <remarks>Not serializing the "value" element is useful when serializing FHIR primitives into two properties, one
    /// with just the value, and one with the id/extensions.</remarks>
    private void serializeInternal(
        Base? element,
        DeferredJsonWriter writer,
        SerializationFilter? filter,
        string? containerPropertyName = null,
        bool required = false,
        bool writeNullIfEmpty = false)
    {
        if (element is null)
        {
            // empty objects in arrays may occur in error situations.
            writer.WriteNullValue();
            return;
        }

        // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
        if (Inspector.FindOrImportClassMapping(element) is not {} mapping)
            throw new InvalidOperationException($"Encountered type {element.GetType()}, which is a support POCO for FHIR, but does not " +
                                                $"have sufficient metadata to be used by the serializer.");

        using var objectScope = writer.BeginObject(containerPropertyName, required, writeNullIfEmpty);

        if (element is Resource r and not DynamicResource { DynamicTypeName: null })
            writer.WriteString("resourceType", r.TypeName);

        filter?.EnterObject(element, mapping);

        foreach (var member in element.EnumerateElements())
        {
            var propertyMapping = mapping?.FindMappedElementByName(member.Key);

            if (filter?.TryEnterMember(member.Key, member.Value, propertyMapping) == false)
                continue;

            var propertyName = propertyMapping switch
            {
                { Choice: ChoiceType.DatatypeChoice } => addSuffixToElementName(member.Key, member.Value),
                null when member.Value is DataType annotatable && annotatable.HasAnnotation<ChoiceElementAnnotation>()
                    => addSuffixToElementName(member.Key, member.Value),
                _ => member.Key
            };

            switch (member.Value)
            {
                case PrimitiveType pt:
                    serializeFhirPrimitive(propertyName, pt, writer, filter);
                    break;
                case IReadOnlyList<PrimitiveType?> pts:
                    serializeFhirPrimitiveList(propertyName, pts, writer, filter);
                    break;
                case IReadOnlyList<Base?> children:   // Not List<Base>, since that is an invariant type.
                    {
                        using var arrayScope = writer.BeginArray(propertyName);

                        foreach (var child in children)
                            serializeInternal(child, writer, filter);
                        break;
                    }
                case Base b:
                    {
                        serializeInternal(b, writer, filter, propertyName);
                        break;
                    }
                default:
                    throw new InvalidOperationException($"{nameof(element.EnumerateElements)} returned a non-Base element of type {member.Value.GetType()}.");
            }

            filter?.LeaveMember(member.Key, member.Value, propertyMapping);
        }

        filter?.LeaveObject(element, mapping);
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
        DeferredJsonWriter writer,
        SerializationFilter? filter)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

        // Don't serialize empty collections.
        if (values.Count == 0) return;

        // Nulls that precede the first actual value are deferred. This lets us omit the property
        // when every item lacks a value while preserving positional alignment once one is found.
        using (writer.BeginArray(elementName))
        {
            foreach (var value in values)
                if (value?.JsonValue is not null)
                {
                    SerializePrimitiveValue(value, writer.PrepareArrayValue());
                }
                else
                {
                    writer.DeferNullValue();
                }
        }

        // Empty metadata objects become deferred null placeholders. If a later object survives
        // filtering, the placeholders are emitted before it; if none survive, the array is omitted.
        using (writer.BeginArray("_" + elementName))
        {
            foreach (var value in values)
            {
                if (value is not null && value.EnumerateElements().Any())
                    serializeInternal(value, writer, filter, writeNullIfEmpty: true);
                else
                    writer.DeferNullValue();
            }
        }
    }


    /// <summary>
    /// Serializes a FHIR primitive into an element with the given name
    /// </summary>
    /// <remarks>FHIR primitives are handled separately here since they may require
    /// serialization into two Json properties called "elementName" and "_elementName".</remarks>
    private void serializeFhirPrimitive(string elementName, PrimitiveType value, DeferredJsonWriter writer, SerializationFilter? filter)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (value.JsonValue is not null)
        {
            SerializePrimitiveValue(value, writer.PreparePropertyValue(elementName));
        }

        if (!value.EnumerateElements().Any()) return;

        serializeInternal(value, writer, filter, "_" + elementName);
    }
    
    private static void tryWriteBase64(Utf8JsonWriter writer, string text)
    {
        var maxSize = Base64.GetMaxDecodedFromUtf8Length(text.Length);
        using var pool = MemoryPool<byte>.Shared.Rent(maxSize);
        if (Convert.TryFromBase64Chars(text, pool.Memory.Span, out var written))
            writer.WriteBase64StringValue(pool.Memory.Span[..written]);
        else
            writer.WriteStringValue(text);
    }

    /// <summary>
    /// Serialize a primitive POCO into Json.
    /// </summary>
    /// <remarks>
    /// For <see cref="Base64Binary"/> values, this method decodes the stored base64 string into a
    /// pooled byte buffer and writes it via <see cref="Utf8JsonWriter.WriteBase64StringValue(ReadOnlySpan{byte})"/>,
    /// bypassing the ~125 MB string-size limit in System.Text.Json (see
    /// https://github.com/FirelyTeam/firely-net-sdk/issues/3501). If the stored value is not valid
    /// base64, it falls back to writing the raw string so that error information is preserved.
    /// All other primitive types are delegated to <see cref="SerializePrimitiveValue(object?, Utf8JsonWriter)"/>.
    ///
    /// To allow for future additions to the POCOs the list of primitives supported here
    /// is larger than the set used by the current POCOs. Note that <c>DateTimeOffset</c> and
    /// <c>byte[]</c> are considered to be "primitive" values here (used as the value in
    /// <see cref="Instant"/> and <see cref="Base64Binary"/>).
    ///
    /// Note that the current version of System.Text.Json only allows numbers
    /// to be written that fit in .NET's <see cref="decimal"/> type, which may be less
    /// precision than required by the FHIR specification (http://hl7.org/fhir/json.html#primitive).
    /// </remarks>
    protected virtual void SerializePrimitiveValue(PrimitiveType value, Utf8JsonWriter writer)
    {
        // due to System.Text.Json limitations described in https://github.com/FirelyTeam/firely-net-sdk/issues/3501
        // Base64 strings need to be < 125MB, but the overload accepting byte array does not carry such limitation
        // Accessing the Value property might result in CodedValidationException, so we need to 
        if (value is Base64Binary { JsonValue: string { Length: > 0 } text })
            tryWriteBase64(writer, text);
        else
            SerializePrimitiveValue(value.JsonValue, writer);
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
    protected virtual void SerializePrimitiveValue(object? value, Utf8JsonWriter writer)
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
