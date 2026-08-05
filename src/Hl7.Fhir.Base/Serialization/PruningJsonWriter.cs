/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System.Collections.Generic;
using System.Text.Json;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// A thin wrapper around <see cref="Utf8JsonWriter"/> that delays writing the tokens that <i>open</i> a
/// structure - a property name plus its <c>{</c> or <c>[</c> - until actual content is written inside it.
/// A structure that turns out to be empty is therefore never written at all.
/// </summary>
/// <remarks>
/// The FHIR specification does not allow empty objects (see https://www.hl7.org/fhir/json.html), but a
/// serializer only discovers that an object is empty <i>after</i> it has walked its members - by which time
/// the opening brace would already have been written. Rather than serializing into a side buffer and
/// inspecting the result, this writer simply postpones the opening tokens: they are handed to the underlying
/// writer the moment a value is written, and discarded if the matching close arrives first.
///
/// Because the tokens still reach the underlying writer in document order, it keeps track of depth,
/// separators and indentation itself, so pruned output needs no re-indentation or raw splicing.
///
/// This writer only knows about the calls made by <see cref="BaseFhirJsonSerializer"/>; it is not a
/// general-purpose <see cref="Utf8JsonWriter"/> facade. See <see cref="PruningXmlWriter"/> for the
/// equivalent over <see cref="System.Xml.XmlWriter"/>.
/// </remarks>
internal sealed class PruningJsonWriter(Utf8JsonWriter writer)
{
    /// <summary>
    /// What to do with a structure that turns out to have no content.
    /// </summary>
    public enum OnEmpty
    {
        /// <summary>Write nothing at all - the structure and its property name are both dropped.</summary>
        Omit,

        /// <summary>
        /// Write a <c>null</c> in its place, but only if the enclosing array is (or becomes) non-empty.
        /// Used for the items of a <c>_elementName</c> array, where the indices must stay aligned with
        /// those of the corresponding <c>elementName</c> array.
        /// </summary>
        NullPlaceholder,

        /// <summary>Write the structure even when it is empty. Used for the root of the document.</summary>
        Keep
    }

    private enum PendingKind { Object, Array, Null }

    private readonly record struct Pending(PendingKind Kind, string? PropertyName, OnEmpty OnEmpty);

    /// <summary>
    /// The tokens written so far that the underlying writer has not seen yet, in document order. Contains
    /// only structures that are still empty, and <c>null</c> placeholders that may yet be dropped.
    /// </summary>
    private readonly List<Pending> _pending = [];

    /// <summary>
    /// Hands out the underlying writer, ready to accept a value: any postponed opening tokens are written
    /// first, since we now know the structures they open are not empty.
    /// </summary>
    public Utf8JsonWriter Value
    {
        get
        {
            commit();
            return writer;
        }
    }

    /// <summary>
    /// Postpones writing an object, and the property name it belongs to (if any), until it gets content.
    /// </summary>
    public void WriteStartObject(string? propertyName = null, OnEmpty onEmpty = OnEmpty.Omit) =>
        _pending.Add(new Pending(PendingKind.Object, propertyName, onEmpty));

    /// <summary>
    /// Postpones writing an array, and the property name it belongs to (if any), until it gets content.
    /// </summary>
    public void WriteStartArray(string? propertyName = null, OnEmpty onEmpty = OnEmpty.Omit) =>
        _pending.Add(new Pending(PendingKind.Array, propertyName, onEmpty));

    /// <summary>
    /// Closes the object opened by the most recent <see cref="WriteStartObject"/>, or drops it according to
    /// its <see cref="OnEmpty"/> policy when it never received any content.
    /// </summary>
    public void WriteEndObject()
    {
        if (!tryResolveEmpty(PendingKind.Object))
        {
            commit();
            writer.WriteEndObject();
        }
    }

    /// <summary>
    /// Closes the array opened by the most recent <see cref="WriteStartArray"/>, or drops it according to
    /// its <see cref="OnEmpty"/> policy when it never received any content.
    /// </summary>
    public void WriteEndArray()
    {
        if (!tryResolveEmpty(PendingKind.Array))
        {
            // The array is already open, so it has content and its length is significant: write out any
            // trailing placeholders before closing it.
            commit();
            writer.WriteEndArray();
        }
    }

    /// <summary>
    /// Writes a <c>null</c> as the next item of the current array, but only if that array is (or becomes)
    /// non-empty. Used to keep the indices of an <c>elementName</c>/<c>_elementName</c> pair aligned without
    /// emitting an array that holds nothing but placeholders.
    /// </summary>
    public void WriteNullPlaceholder() => _pending.Add(new Pending(PendingKind.Null, null, OnEmpty.Omit));

    /// <summary>
    /// Writes a property with a string value. Since this is content, any postponed opening tokens are
    /// written first.
    /// </summary>
    public void WriteString(string propertyName, string? value) => Value.WriteString(propertyName, value);

    /// <summary>
    /// Writes a property name. Since a name is only ever written when a value follows, this counts as
    /// content and any postponed opening tokens are written first.
    /// </summary>
    public void WritePropertyName(string propertyName) => Value.WritePropertyName(propertyName);

    /// <summary>
    /// Handles <see cref="WriteEndObject"/>/<see cref="WriteEndArray"/> for a structure that is still
    /// postponed, and so never received content. Returns <c>false</c> when the structure has already been
    /// written and must be closed for real.
    /// </summary>
    private bool tryResolveEmpty(PendingKind kind)
    {
        // Our own opening token is the last postponed structure: anything added after it can only be a
        // placeholder, since a nested structure would have been resolved by its own WriteEnd... and any
        // real content would have committed the whole list.
        var index = _pending.Count - 1;
        while (index >= 0 && _pending[index].Kind == PendingKind.Null) index -= 1;

        if (index < 0) return false;

        var pending = _pending[index];
        if (pending.Kind != kind) return false;

        if (pending.OnEmpty == OnEmpty.Keep) return false;

        // Drop the structure, plus the placeholders it contains - they were only there to pad it out.
        _pending.RemoveRange(index, _pending.Count - index);

        // Keep the slot filled if the structure stood at a significant position in an array.
        if (pending.OnEmpty == OnEmpty.NullPlaceholder)
            _pending.Add(new Pending(PendingKind.Null, null, OnEmpty.Omit));

        return true;
    }

    /// <summary>
    /// Hands all postponed tokens to the underlying writer, in the order they were written.
    /// </summary>
    private void commit()
    {
        if (_pending.Count == 0) return;

        // Writing to the underlying writer cannot re-enter this class, so iterating the list in place and
        // clearing it afterwards is safe (and avoids a copy on what is a hot path).
        foreach (var (kind, propertyName, _) in _pending)
        {
            switch (kind)
            {
                case PendingKind.Object when propertyName is not null:
                    writer.WriteStartObject(propertyName);
                    break;
                case PendingKind.Object:
                    writer.WriteStartObject();
                    break;
                case PendingKind.Array when propertyName is not null:
                    writer.WriteStartArray(propertyName);
                    break;
                case PendingKind.Array:
                    writer.WriteStartArray();
                    break;
                case PendingKind.Null:
                    writer.WriteNullValue();
                    break;
            }
        }

        _pending.Clear();
    }
}
