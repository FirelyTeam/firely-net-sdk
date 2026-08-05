/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Text.Json;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Delays writing JSON containers until they contain a value. This allows the serializer to
/// omit empty objects and arrays without buffering their serialized representation.
/// </summary>
internal sealed class DeferredJsonWriter(Utf8JsonWriter destination)
{
    private ContainerState[] _scopes = new ContainerState[8];
    private int _depth;

    public ContainerScope BeginObject(string? propertyName = null, bool required = false, bool writeNullIfEmpty = false) =>
        beginContainer(ContainerKind.Object, propertyName, required, writeNullIfEmpty);

    public ContainerScope BeginArray(string propertyName) =>
        beginContainer(ContainerKind.Array, propertyName, required: false, writeNullIfEmpty: false);

    public void WriteString(string propertyName, string value)
    {
        var current = requireCurrent(ContainerKind.Object);
        commit(current);
        destination.WriteString(propertyName, value);
    }

    /// <summary>
    /// Returns the underlying writer positioned to write a property value after committing all
    /// pending containers.
    /// </summary>
    public Utf8JsonWriter PreparePropertyValue(string propertyName)
    {
        var current = requireCurrent(ContainerKind.Object);
        commit(current);
        destination.WritePropertyName(propertyName);
        return destination;
    }

    /// <summary>
    /// Returns the underlying writer positioned to write an array item after committing all
    /// pending containers and deferred null placeholders.
    /// </summary>
    public Utf8JsonWriter PrepareArrayValue()
    {
        var current = requireCurrent(ContainerKind.Array);
        commit(current);
        flushDeferredNulls(current);
        return destination;
    }

    public void WriteNullValue() => PrepareArrayValue().WriteNullValue();

    /// <summary>
    /// Defers an array null until a later value causes the array to be written. If the array has
    /// already been written, the null is written immediately. Deferred trailing nulls are omitted
    /// when no value ever materializes the array.
    /// </summary>
    public void DeferNullValue()
    {
        var current = requireCurrent(ContainerKind.Array);
        deferNull(current);
    }

    private ContainerScope beginContainer(ContainerKind kind, string? propertyName, bool required, bool writeNullIfEmpty)
    {
        if (_depth == 0 && propertyName is not null)
            throw new InvalidOperationException("A root JSON container cannot have a property name.");

        if (_depth > 0 && _scopes[_depth - 1].Kind == ContainerKind.Object && propertyName is null)
            throw new InvalidOperationException("A JSON container inside an object must have a property name.");

        if (_depth > 0 && _scopes[_depth - 1].Kind == ContainerKind.Array && propertyName is not null)
            throw new InvalidOperationException("A JSON container inside an array cannot have a property name.");

        if (writeNullIfEmpty && (_depth == 0 || _scopes[_depth - 1].Kind != ContainerKind.Array))
            throw new InvalidOperationException("Only an array item can be replaced by null when empty.");

        if (_depth == _scopes.Length)
            Array.Resize(ref _scopes, _scopes.Length * 2);

        var index = _depth++;
        _scopes[index] = new ContainerState(kind, propertyName, required, writeNullIfEmpty);
        return new ContainerScope(this, index);
    }

    private int requireCurrent(ContainerKind kind)
    {
        if (_depth == 0 || _scopes[_depth - 1].Kind != kind)
            throw new InvalidOperationException($"A JSON {kind.ToString().ToLowerInvariant()} must be open for this operation.");

        return _depth - 1;
    }

    private void commit(int index)
    {
        if (_scopes[index].Committed) return;

        if (index > 0)
        {
            var parent = index - 1;
            commit(parent);
            if (_scopes[parent].Kind == ContainerKind.Array) flushDeferredNulls(parent);
        }

        if (_scopes[index].PropertyName is { } propertyName)
            destination.WritePropertyName(propertyName);

        if (_scopes[index].Kind == ContainerKind.Object)
            destination.WriteStartObject();
        else
            destination.WriteStartArray();

        _scopes[index].Committed = true;
    }

    private void flushDeferredNulls(int array)
    {
        while (_scopes[array].DeferredNullCount > 0)
        {
            destination.WriteNullValue();
            _scopes[array].DeferredNullCount--;
        }
    }

    private void deferNull(int array)
    {
        if (_scopes[array].Committed)
            destination.WriteNullValue();
        else
            _scopes[array].DeferredNullCount++;
    }

    private void endContainer(int index)
    {
        if (index != _depth - 1)
            throw new InvalidOperationException("JSON containers must be closed in reverse order.");

        if (_scopes[index].Required && !_scopes[index].Committed)
            commit(index);

        var committed = _scopes[index].Committed;

        if (committed)
        {
            if (_scopes[index].Kind == ContainerKind.Object)
                destination.WriteEndObject();
            else
                destination.WriteEndArray();
        }

        var writeNullIfEmpty = _scopes[index].WriteNullIfEmpty;
        _scopes[index] = default;
        _depth--;

        if (!committed && writeNullIfEmpty)
            deferNull(index - 1);
    }

    private enum ContainerKind
    {
        Object,
        Array
    }

    private struct ContainerState(ContainerKind kind, string? propertyName, bool required, bool writeNullIfEmpty)
    {
        public ContainerKind Kind { get; } = kind;
        public string? PropertyName { get; } = propertyName;
        public bool Required { get; } = required;
        public bool WriteNullIfEmpty { get; } = writeNullIfEmpty;
        public bool Committed { get; set; }
        public int DeferredNullCount { get; set; }
    }

    internal struct ContainerScope(DeferredJsonWriter owner, int index) : IDisposable
    {
        private DeferredJsonWriter? _owner = owner;
        private readonly int _index = index;

        public void Dispose()
        {
            _owner?.endContainer(_index);
            _owner = null;
        }
    }
}
