/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System.Collections.Generic;
using System.Xml;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// A thin wrapper around <see cref="XmlWriter"/> that delays writing a start element until actual content -
/// an attribute, a child element or raw markup - is written inside it. An element that turns out to be empty
/// is therefore never written at all.
/// </summary>
/// <remarks>
/// The FHIR specification does not allow elements without a value, children or extensions (see
/// https://www.hl7.org/fhir/xml.html), but a serializer only discovers that an element is empty <i>after</i>
/// it has walked its members - by which time the start tag would already have been written. Postponing that
/// start tag until content arrives, and discarding it when the matching close comes first, avoids the problem
/// without any need to buffer output.
///
/// Because the tokens still reach the underlying writer in document order, attributes always land on the
/// element they belong to: a postponed <c>&lt;a&gt;&lt;b&gt;&lt;c</c> is flushed in one go, leaving the
/// writer positioned on the innermost element, which is the one the attribute was written for.
///
/// This writer only knows about the calls made by <see cref="BaseFhirXmlSerializer"/>; it is not a
/// general-purpose <see cref="XmlWriter"/> facade. See <see cref="PruningJsonWriter"/> for the equivalent
/// over <see cref="System.Text.Json.Utf8JsonWriter"/>, which additionally needs to deal with the
/// <c>null</c> placeholders of Json's primitive arrays.
/// </remarks>
internal sealed class PruningXmlWriter(XmlWriter writer)
{
    /// <summary>
    /// What to do with an element that turns out to have no content.
    /// </summary>
    public enum OnEmpty
    {
        /// <summary>Write nothing at all.</summary>
        Omit,

        /// <summary>Write the element even when it is empty. Used for the root of the document.</summary>
        Keep
    }

    private readonly record struct Pending(string LocalName, string? Ns, OnEmpty OnEmpty);

    /// <summary>
    /// The start elements written so far that the underlying writer has not seen yet, in document order.
    /// Contains only elements that are still empty.
    /// </summary>
    private readonly List<Pending> _pending = [];

    /// <summary>
    /// Hands out the underlying writer, ready to accept an attribute or value: any postponed start elements
    /// are written first, since we now know they are not empty.
    /// </summary>
    /// <remarks>Deliberately a method rather than a property: getting it changes the state of this writer,
    /// and a property with that side effect would fire when a debugger evaluates it.</remarks>
    public XmlWriter PrepareContent()
    {
        commit();
        return writer;
    }

    /// <inheritdoc cref="XmlWriter.WriteStartDocument()"/>
    public void WriteStartDocument() => writer.WriteStartDocument();

    /// <summary>
    /// Closes the document, writing out any element still postponed at that point.
    /// </summary>
    /// <remarks><see cref="BaseFhirXmlSerializer"/> deliberately leaves the element wrapping a datatype open
    /// for <see cref="XmlWriter.WriteEndDocument()"/> to close, so we cannot wait for a
    /// <see cref="WriteEndElement"/> that will never come. Writing it is correct: the only elements left open
    /// here are the ones holding the document together.</remarks>
    public void WriteEndDocument()
    {
        commit();
        writer.WriteEndDocument();
    }

    /// <summary>
    /// Writes a comment. Since this is content, any postponed start elements are written first.
    /// </summary>
    public void WriteComment(string comment) => PrepareContent().WriteComment(comment);

    /// <summary>
    /// Postpones writing a start element until it gets content.
    /// </summary>
    public void WriteStartElement(string localName, string? ns, OnEmpty onEmpty = OnEmpty.Omit) =>
        _pending.Add(new Pending(localName, ns, onEmpty));

    /// <summary>
    /// Closes the element opened by the most recent <see cref="WriteStartElement"/>, or drops it when it
    /// never received any content and its <see cref="OnEmpty"/> policy allows.
    /// </summary>
    public void WriteEndElement()
    {
        if (_pending.Count > 0 && _pending[^1].OnEmpty == OnEmpty.Omit)
        {
            // Our own start element is necessarily the last postponed one: a nested element would have been
            // resolved by its own WriteEndElement, and any content would have committed the whole list.
            _pending.RemoveAt(_pending.Count - 1);
            return;
        }

        commit();
        writer.WriteEndElement();
    }

    /// <summary>
    /// Writes raw markup. Since this is content, any postponed start elements are written first - unless
    /// there is nothing to write, in which case this call is ignored altogether.
    /// </summary>
    public void WriteRaw(string? data)
    {
        if (string.IsNullOrEmpty(data)) return;

        PrepareContent().WriteRaw(data);
    }

    /// <summary>
    /// Writes all postponed start elements to the underlying writer, in the order they were written.
    /// </summary>
    private void commit()
    {
        if (_pending.Count == 0) return;

        // Writing to the underlying writer cannot re-enter this class, so iterating the list in place and
        // clearing it afterwards is safe (and avoids a copy on what is a hot path).
        foreach (var (localName, ns, _) in _pending)
            writer.WriteStartElement(localName, ns);

        _pending.Clear();
    }
}
