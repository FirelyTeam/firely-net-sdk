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
    /// Collects the comments encountered while advancing the reader, or <c>null</c> when
    /// <see cref="DeserializerSettings.RetainComments"/> is off, so every use of it (<c>state.Comments?.…</c>)
    /// is a no-op in the default case.
    /// </summary>
    public SourceCommentCollector? Comments { get; init; }

    private readonly Stack<BaseFhirJsonDeserializer.ObjectParsingState> objectContext = new();

    public void EnterObjectContext() =>
        objectContext.Push(new BaseFhirJsonDeserializer.ObjectParsingState());

    public void LeaveObjectContext() => objectContext.Pop();

    public BaseFhirJsonDeserializer.ObjectParsingState GetObjectContext() => objectContext.Peek();
}