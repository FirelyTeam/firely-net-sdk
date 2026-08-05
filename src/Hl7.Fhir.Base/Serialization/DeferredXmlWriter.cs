/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Xml;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Delays writing XML elements until they contain an attribute, child element, or raw value.
/// This allows the serializer to omit empty FHIR elements without buffering their serialized form.
/// </summary>
internal sealed class DeferredXmlWriter(XmlWriter destination)
{
    private ElementState[] _elements = new ElementState[8];
    private int _depth;

    public ElementScope BeginElement(
        string localName,
        string namespaceUri,
        bool required = false,
        string[]? commentsBefore = null)
    {
        if (_depth == _elements.Length)
            Array.Resize(ref _elements, _elements.Length * 2);

        var index = _depth++;
        _elements[index] = new ElementState(localName, namespaceUri, commentsBefore);

        if (required) commit(index);

        return new ElementScope(this, index);
    }

    /// <summary>
    /// Returns the underlying writer positioned to write an attribute on the current element.
    /// </summary>
    public XmlWriter PrepareAttribute()
    {
        var current = requireCurrent();
        commit(current);
        return destination;
    }

    /// <summary>
    /// Writes raw element content and any comments that preceded it. Empty raw content does not
    /// materialize the current element.
    /// </summary>
    public void WriteRaw(string value, string[]? commentsBefore = null)
    {
        if (string.IsNullOrEmpty(value)) return;

        commit(requireCurrent());
        writeComments(commentsBefore);
        destination.WriteRaw(value);
    }

    /// <summary>
    /// Writes comments that followed the current element's last child. Comments are not FHIR
    /// content, so they are discarded when the element has not otherwise materialized.
    /// </summary>
    public void WriteClosingComments(string[]? comments)
    {
        var current = requireCurrent();
        if (!_elements[current].Committed) return;

        writeComments(comments);
    }

    private int requireCurrent()
    {
        if (_depth == 0)
            throw new InvalidOperationException("An XML element must be open for this operation.");

        return _depth - 1;
    }

    private void commit(int index)
    {
        if (_elements[index].Committed) return;

        if (index > 0) commit(index - 1);

        writeComments(_elements[index].CommentsBefore);
        destination.WriteStartElement(_elements[index].LocalName, _elements[index].NamespaceUri);
        _elements[index].Committed = true;
    }

    private void endElement(int index)
    {
        if (index != _depth - 1)
            throw new InvalidOperationException("XML elements must be closed in reverse order.");

        if (_elements[index].Committed)
            destination.WriteEndElement();

        _elements[index] = default;
        _depth--;
    }

    private void writeComments(string[]? comments)
    {
        if (comments is null) return;

        foreach (var comment in comments)
            destination.WriteComment(comment);
    }

    private struct ElementState(
        string localName,
        string namespaceUri,
        string[]? commentsBefore)
    {
        public string LocalName { get; } = localName;
        public string NamespaceUri { get; } = namespaceUri;
        public string[]? CommentsBefore { get; } = commentsBefore;
        public bool Committed { get; set; }
    }

    internal struct ElementScope(DeferredXmlWriter owner, int index) : IDisposable
    {
        private DeferredXmlWriter? _owner = owner;
        private readonly int _index = index;

        public void Dispose()
        {
            _owner?.endElement(_index);
            _owner = null;
        }
    }
}
