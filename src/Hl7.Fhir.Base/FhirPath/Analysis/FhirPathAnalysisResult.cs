/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Language.Debugging;
using Hl7.FhirPath.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Analysis;

/// <summary>
/// The kinds of semantic error the <see cref="FhirPathAnalyzer"/> reports.
/// </summary>
public enum FhirPathDiagnosticCode
{
    /// <summary>A path refers to an element that does not exist on any of the possible types of its focus.</summary>
    UnknownElement,

    /// <summary>A path uses a type-suffixed name for a choice element (e.g. <c>valueQuantity</c>).</summary>
    ChoiceElementName,

    /// <summary>A path starts with a type name that the focus can never be (e.g. <c>Encounter.id</c> on a Patient).</summary>
    WrongTypeName,

    /// <summary>A function is used on a focus none of whose possible types it supports.</summary>
    InvalidFocusType,

    /// <summary>An argument can never have a type the function accepts.</summary>
    InvalidArgumentType,

    /// <summary>An order-dependent function is used on a collection that has no defined order.</summary>
    UnorderedCollection,
}

/// <summary>
/// A semantic error found by the <see cref="FhirPathAnalyzer"/>.
/// </summary>
public sealed class FhirPathDiagnostic
{
    internal FhirPathDiagnostic(FhirPathDiagnosticCode code, string message, Expression expression)
    {
        Code = code;
        Message = message;
        Expression = expression;
    }

    /// <summary>The kind of error.</summary>
    public FhirPathDiagnosticCode Code { get; }

    /// <summary>A human-readable description of the error.</summary>
    public string Message { get; }

    /// <summary>The (sub)expression the error was found in.</summary>
    public Expression Expression { get; }

    /// <summary>
    /// The position of the (sub)expression in the source text, if known.
    /// </summary>
    public ISourcePositionInfo? Location => Expression.Location;

    /// <inheritdoc />
    public override string ToString() =>
        Location is { } l ? $"{Message} (at line {l.LineNumber}, position {l.LinePosition})" : Message;
}

/// <summary>
/// The outcome of a static analysis of a FHIRPath expression by the <see cref="FhirPathAnalyzer"/>.
/// </summary>
public sealed class FhirPathAnalysisResult
{
    internal FhirPathAnalysisResult(IReadOnlyList<FhirPathDiagnostic> diagnostics, IReadOnlyCollection<string>? resultTypes)
    {
        Diagnostics = diagnostics;
        ResultTypes = resultTypes;
    }

    /// <summary>
    /// The semantic errors found in the expression. Empty if none were found.
    /// </summary>
    public IReadOnlyList<FhirPathDiagnostic> Diagnostics { get; }

    /// <summary>
    /// Whether any semantic errors were found.
    /// </summary>
    public bool HasErrors => Diagnostics.Any();

    /// <summary>
    /// The possible types of the items in the result of the expression, or <c>null</c> if they could not be determined.
    /// FHIR types are given by their FHIR type name, FHIRPath System types are qualified with <c>System.</c>.
    /// </summary>
    public IReadOnlyCollection<string>? ResultTypes { get; }
}
