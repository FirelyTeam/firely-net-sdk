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

    private readonly Stack<BaseFhirJsonDeserializer.ObjectParsingState> objectContext = new();

    public void EnterObjectContext() =>
        objectContext.Push(new BaseFhirJsonDeserializer.ObjectParsingState());

    public void LeaveObjectContext() => objectContext.Pop();

    public BaseFhirJsonDeserializer.ObjectParsingState GetObjectContext() => objectContext.Peek();
}