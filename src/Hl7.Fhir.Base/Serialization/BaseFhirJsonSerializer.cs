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
using System.Collections.Concurrent;
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
    private static readonly JsonEncodedText RESOURCE_TYPE_PROPERTY_NAME = JsonEncodedText.Encode("resourceType"u8);
    private static readonly JsonEncodedText VALUE_PROPERTY_NAME = JsonEncodedText.Encode("value"u8);

    // FHIR element names form a small closed set, so pre-encoding them (UTF-8 + escaping) once
    // and reusing the result on every write is considerably cheaper than having Utf8JsonWriter
    // encode the name on each call. Names of dynamic properties end up in these caches too,
    // but they are bounded by the model(s) in use.
    private static readonly ConcurrentDictionary<string, JsonEncodedText> _encodedNames = new();
    private static readonly ConcurrentDictionary<string, JsonEncodedText> _encodedUnderscoreNames = new();
    private static readonly ConcurrentDictionary<(string name, string type), string> _suffixedNames = new();

    private static JsonEncodedText encodedName(string elementName) =>
        _encodedNames.GetOrAdd(elementName, static n => JsonEncodedText.Encode(n));

    private static JsonEncodedText encodedUnderscoreName(string elementName) =>
        _encodedUnderscoreNames.GetOrAdd(elementName, static n => JsonEncodedText.Encode("_" + n));

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

        // This handles an edge-case where we are asked to serialize just a primitive value.
        // For compatibility with SDK5 logic, we emit object with pseudo-property 'value' and value of the fhir primitive.
        // Issue for context: https://github.com/FirelyTeam/firely-net-sdk/issues/3286
        if (instance is not PrimitiveType val)
        {
            serializeInternal(instance, writer, filter);
        }
        else
        {
            writer.WriteStartObject();
            serializeFhirPrimitive(VALUE_PROPERTY_NAME, val, writer, filter);
            writer.WriteEndObject();
        }
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

        // Only throw if we don't have a mapping where we are expected to: when this is a subclass of Base.
        // Resolved before any output is written, so a failure does not leave the writer in a broken state.
        if (Inspector.FindOrImportClassMapping(element) is not {} mapping)
            throw new InvalidOperationException($"Encountered type {element.GetType()}, which is a support POCO for FHIR, but does not " +
                                                $"have sufficient metadata to be used by the serializer.");

        writer.WriteStartObject();

        if (element is Resource r)
            writer.WriteString(RESOURCE_TYPE_PROPERTY_NAME, r.TypeName);

        filter?.EnterObject(element, mapping);

        foreach (var member in element.EnumerateElements())
        {
            var propertyMapping = mapping.FindMappedElementByName(member.Key);

            if (filter?.TryEnterMember(member.Key, member.Value, propertyMapping) == false)
                continue;

            var propertyName = propertyMapping switch
            {
                { Choice: ChoiceType.DatatypeChoice } => addSuffixToElementName(member.Key, member.Value),
                null when member.Value is DataType annotatable && annotatable.HasAnnotation<ChoiceElementAnnotation>()
                    => addSuffixToElementName(member.Key, member.Value),
                _ => member.Key
            };

            var encodedPropertyName = encodedName(propertyName);

            switch (member.Value)
            {
                case PrimitiveType pt:
                    serializeFhirPrimitive(encodedPropertyName, pt, writer, filter);
                    break;
                case IReadOnlyList<PrimitiveType?> pts:
                    serializeFhirPrimitiveList(encodedPropertyName, propertyName, pts, writer, filter);
                    break;
                case IReadOnlyList<Base?> children:   // Not List<Base>, since that is an invariant type.
                    serializeComplexList(encodedPropertyName, children, writer, filter);
                    break;
                case Base b:
                    serializeComplex(encodedPropertyName, b, writer, filter);
                    break;
                default:
                    throw new InvalidOperationException($"{nameof(element.EnumerateElements)} returned a non-Base element of type {member.Value.GetType()}.");
            }

            filter?.LeaveMember(member.Key, member.Value, propertyMapping);
        }

        filter?.LeaveObject(element, mapping);
        writer.WriteEndObject();
    }

    /// <summary>
    /// Serializes a single complex (non-primitive) member, omitting the property entirely
    /// when it would serialize to an empty object, which is not allowed by the FHIR spec.
    /// </summary>
    private void serializeComplex(JsonEncodedText propertyName, Base element, Utf8JsonWriter writer, SerializationFilter? filter)
    {
        if (filter is null)
        {
            if (!hasContent(element)) return;

            writer.WritePropertyName(propertyName);
            serializeInternal(element, writer, filter);
        }
        else if (trySerializeToBuffer(element, writer.Options, writer.CurrentDepth, filter) is { } payload)
        {
            // With an active filter we cannot predict up-front whether any members survive,
            // so serialize to a buffer first and only emit the property if it is non-empty.
            writer.WritePropertyName(propertyName);
            writeBufferedValue(payload, writer);
        }
    }

    /// <summary>
    /// Serializes a list of complex (non-primitive) members, skipping children that would
    /// serialize to an empty object and omitting the property when no children remain.
    /// </summary>
    private void serializeComplexList(JsonEncodedText propertyName, IReadOnlyList<Base?> children, Utf8JsonWriter writer, SerializationFilter? filter)
    {
        var wroteStartArray = false;

        foreach (var child in children)
        {
            if (child is null)
            {
                // empty objects in arrays may occur in error situations; keep the placeholder.
                ensureStartArray();
                writer.WriteNullValue();
            }
            else if (filter is null)
            {
                if (!hasContent(child)) continue;

                ensureStartArray();
                serializeInternal(child, writer, filter);
            }
            // Until the array is open, items sit one level deeper than the writer's current depth.
            else if (trySerializeToBuffer(child, writer.Options, writer.CurrentDepth + (wroteStartArray ? 0 : 1), filter, asArrayItem: true) is { } payload)
            {
                ensureStartArray();
                writeBufferedValue(payload, writer);
            }
        }

        if (wroteStartArray) writer.WriteEndArray();

        void ensureStartArray()
        {
            if (wroteStartArray) return;
            wroteStartArray = true;
            writer.WriteStartArray(propertyName);
        }
    }

    /// <summary>
    /// Determines whether an element would produce any output when serialized (without a filter).
    /// Elements without content must be omitted entirely according to the FHIR spec.
    /// </summary>
    private static bool hasContent(Base? element) => element switch
    {
        // nulls are serialized as placeholders (error situations), so they count as content.
        null => true,
        // resources always serialize their resourceType property.
        Resource => true,
        // primitive can have a value and complex parts
        PrimitiveType { JsonValue: not null } => true,
        _ => hasElementContent(element)
    };

    /// <summary>
    /// Determines whether an element's children (id/extensions, not its value) would produce
    /// any output when serialized without a filter - i.e. whether a '_elementName' property
    /// would be non-empty.
    /// </summary>
    private static bool hasElementContent(Base element) =>
        element.EnumerateElements().Any(m => memberHasContent(m.Value));

    private static bool memberHasContent(object value) => value switch
    {
        PrimitiveType pt => hasContent(pt),
        IReadOnlyList<Base?> list => list.Any(hasContent),
        Base b => hasContent(b),
        _ => true
    };

    private static string addSuffixToElementName(string elementName, object elementValue)
    {
        var typeName = elementValue switch
        {
            IEnumerable<Base> ib => ib.FirstOrDefault()?.TypeName,
            Base b => b.TypeName,
            _ => null
        };

        return typeName is null
            ? elementName
            : _suffixedNames.GetOrAdd((elementName, typeName),
                static key => key.name + char.ToUpperInvariant(key.type[0]) + key.type[1..]);
    }

    /// <summary>
    /// Serializes a list of FHIR primitives into an array element with the given name
    /// </summary>
    /// <remarks>FHIR primitives are handled separately here since they may require
    /// serialization into two Json properties called "elementName" and "_elementName" and
    /// may use Json <c>null</c>s as placeholders.</remarks>
    private void serializeFhirPrimitiveList(
        JsonEncodedText encodedElementName,
        string elementName,
        IReadOnlyList<PrimitiveType?> values,
        Utf8JsonWriter writer,
        SerializationFilter? filter)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));

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
            if (value?.JsonValue is not null)
            {
                if (!wroteStartArray)
                {
                    wroteStartArray = true;
                    writeStartArray(encodedElementName, numNullsMissed, writer);
                }

                SerializePrimitiveValue(value, writer);
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
            // Empty objects are not allowed in the _elementName array. Without a filter we can
            // predict emptiness cheaply; with an active filter id/extensions may be removed, so
            // we serialize into a buffer first and only write the result when it is non-empty.
            ReadOnlyMemory<byte>? payload = null;

            var hasOutput = filter is null
                ? value is not null && hasElementContent(value)
                : value?.EnumerateElements().Any() == true
                  // Until the array is open, items sit one level deeper than the writer's current depth.
                  && (payload = trySerializeToBuffer(value, writer.Options, writer.CurrentDepth + (wroteStartArray ? 0 : 1), filter, asArrayItem: true)) is not null;

            if (hasOutput)
            {
                if (!wroteStartArray)
                {
                    wroteStartArray = true;
                    writeStartArray(encodedUnderscoreName(elementName), numNullsMissed, writer);
                }

                if (payload is { } p)
                    writeBufferedValue(p, writer);
                else
                    serializeInternal(value!, writer, filter);
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

    private static void writeStartArray(JsonEncodedText propName, int numNulls, Utf8JsonWriter writer)
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
    private void serializeFhirPrimitive(
        JsonEncodedText encodedElementName,
        PrimitiveType value,
        Utf8JsonWriter writer,
        SerializationFilter? filter)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (value.JsonValue is not null)
        {
            // Write a property with 'elementName'
            writer.WritePropertyName(encodedElementName);
            SerializePrimitiveValue(value, writer);
        }

        // An empty object is not allowed as the value of the '_elementName' property. Without a
        // filter we can predict emptiness cheaply; with an active filter id/extensions may be
        // removed, so we serialize into a buffer first and only write the result when non-empty.
        if (filter is null)
        {
            if (!hasElementContent(value)) return;

            writer.WritePropertyName(encodedUnderscoreName(encodedElementName.Value));
            serializeInternal(value, writer, filter);
        }
        else
        {
            if (!value.EnumerateElements().Any()) return;
            if (trySerializeToBuffer(value, writer.Options, writer.CurrentDepth, filter) is not { } payload) return;

            writer.WritePropertyName(encodedUnderscoreName(encodedElementName.Value));
            writeBufferedValue(payload, writer);
        }
    }
    
    /// <summary>
    /// Splices a buffered payload into the target writer. The payload was produced by
    /// <see cref="trySerializeToBuffer"/> at the correct nesting depth, so its indentation
    /// already matches the target writer and it can be copied verbatim.
    /// </summary>
    private static void writeBufferedValue(ReadOnlyMemory<byte> payload, Utf8JsonWriter writer) =>
        writer.WriteRawValue(payload.Span, skipInputValidation: true);

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
    /// Serializes the given element into a buffer, returning <c>null</c> when the result is an
    /// empty object (either because the element was empty, or the filter removed all its members).
    /// </summary>
    private ReadOnlyMemory<byte>? trySerializeToBuffer(Base element, JsonWriterOptions options, int targetDepth, SerializationFilter? filter, bool asArrayItem = false)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using (var defer = new Utf8JsonWriter(buffer, options))
        {
            // Utf8JsonWriter offers no way to seed its starting depth, so for indented output we
            // simulate the depth the payload will be spliced at by opening dummy arrays. The
            // element then serializes with the indentation of its final nesting level, and the
            // payload can be copied into the target writer verbatim (see writeBufferedValue).
            // The prologue consists solely of '[' and whitespace while the element itself always
            // serializes as an object, so it is sliced off by looking for the first '{'.
            if (options.Indented)
                for (var i = 0; i < targetDepth; i++)
                    defer.WriteStartArray();

            serializeInternal(element, defer, filter);
        }

        var written = buffer.WrittenMemory;
        var start = written.Span.IndexOf((byte)'{');

        // An empty object serializes to "{}" (2 bytes): either the element was empty, or the
        // filter removed all its members. Note that a null element serializes to "null"
        // (4 bytes) and is deliberately kept as a placeholder.
        // Note: the explicit cast is required. Without it, the conditional's natural type is
        // ReadOnlyMemory<byte> (via the implicit byte[] conversion for the null literal), which
        // turns 'null' into an empty-but-non-null memory instead of a null Nullable.
        const int emptyObjectLength = 2;
        if (written.Length - start <= emptyObjectLength) return null;

        // WriteRawValue emits the list separator but not the newline + indentation that normally
        // precedes an array item, so for array items we keep the payload's own leading
        // "\n<indent>" (the last newline of the dummy prologue sits right in front of the element).
        if (asArrayItem && options.Indented)
            start = written.Span[..start].LastIndexOf((byte)'\n');

        return written[start..];
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
        // pattern-match on the raw JsonValue instead of reading the parsed Value.
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
    /// is larger than the set used by the current POCOs. Note that <c>DateTimeOffset</c> and
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