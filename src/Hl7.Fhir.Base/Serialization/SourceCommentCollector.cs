/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System.Collections.Generic;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Buffers the comments encountered while advancing an <see cref="System.Xml.XmlReader"/>, so they can
/// be attached as a <see cref="SourceComments"/> annotation once it becomes clear which element they
/// belong to. See <see cref="DeserializerSettings.RetainComments"/>.
/// </summary>
/// <remarks>A <see cref="PocoDeserializerState"/> only has one of these when <see cref="DeserializerSettings.RetainComments"/>
/// is set, so every call site that touches it does so through <c>state.Comments?.…</c> and is a no-op otherwise.
/// Every comment added here must be consumed by exactly one caller (see <see cref="Consume"/>) - one that
/// is never consumed leaks forward and ends up annotated on whichever element happens to consume next.</remarks>
internal sealed class SourceCommentCollector
{
    private List<string>? _pending;

    public void Add(string comment) => (_pending ??= []).Add(comment);

    /// <summary>
    /// Returns the comments buffered since the previous call and clears the buffer, or <c>null</c> when
    /// no comments were encountered.
    /// </summary>
    public string[]? Consume()
    {
        if (_pending is not { Count: > 0 }) return null;

        var result = _pending.ToArray();
        _pending.Clear();
        return result;
    }
}
