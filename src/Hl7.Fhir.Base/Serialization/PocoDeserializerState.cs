/*
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization;

internal class PocoDeserializerState
{
    public readonly ExceptionAggregator Errors = new();

    public PathPart Path { get; internal set; } = new RootPathPart();

    /// <summary>
    /// Whether comments encountered in the source data are collected, so they can be retained as
    /// <see cref="SourceComments"/> annotations on the POCOs. See <see cref="DeserializerSettings.RetainComments"/>.
    /// </summary>
    public bool RetainComments { get; init; }

    private List<string>? _pendingComments;

    /// <summary>
    /// Records a comment encountered while moving the reader forward.
    /// </summary>
    /// <remarks>Comments are buffered until a consumer takes them: which comments belong to which POCO
    /// only becomes clear once the reader has arrived at the next node. Every buffered comment must be
    /// taken by exactly one consumer (see <see cref="TakePendingComments"/>), otherwise it will leak
    /// forward and end up annotated on an unrelated element.</remarks>
    public void AddComment(string comment)
    {
        if (!RetainComments) return;

        (_pendingComments ??= []).Add(comment);
    }

    /// <summary>
    /// Returns the comments buffered since the previous call and clears the buffer, or <c>null</c> when
    /// no comments were encountered.
    /// </summary>
    public string[]? TakePendingComments()
    {
        if (_pendingComments is not { Count: > 0 }) return null;

        var result = _pendingComments.ToArray();
        _pendingComments.Clear();
        return result;
    }

    private readonly Stack<BaseFhirJsonDeserializer.ObjectParsingState> objectContext = new();

    public void EnterObjectContext() =>
        objectContext.Push(new BaseFhirJsonDeserializer.ObjectParsingState());

    public void LeaveObjectContext() => objectContext.Pop();

    public BaseFhirJsonDeserializer.ObjectParsingState GetObjectContext() => objectContext.Peek();
}