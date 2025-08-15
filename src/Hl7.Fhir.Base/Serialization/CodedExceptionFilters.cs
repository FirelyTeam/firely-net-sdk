#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hl7.Fhir.Serialization;

/// <summary>
/// Provides utility methods for building and combining <see cref="Predicate{T}"/>s used to filter <see cref="CodedException"/>s.
/// </summary>
public static class CodedExceptionFilters
{
    /// <summary>
    /// A predicate that returns true if a <see cref="CodedException"/> is recoverable,
    /// which means that all data is represented in the POCO, so there is no data loss.
    /// See <see cref="DeserializationMode.Recoverable"/>.
    /// </summary>
    public static readonly Predicate<CodedException> FilterRecoverableIssues =
        ce => ce is ExtendedCodedException ece && ece.IssueSeverity != OperationOutcome.IssueSeverity.Fatal;

    /// <summary>
    /// A predicate that returns true if an error recoverable and also does not require
    /// overflow to capture the data.
    /// </summary>
    public static readonly Predicate<CodedException> FilterNoOverflowIssues =
        ce => FilterRecoverableIssues(ce) &&
              !CodedValidationException.ISSUES_CAUSED_BY_OVERFLOW.Contains(ce.ErrorCode);
    /// <summary>
    /// A predicate that returns true if a <see cref="CodedException"/> signifies a backwards compatibility issue.
    /// See <see cref="DeserializationMode.BackwardsCompatible"/>.
    /// </summary>
    public static readonly Predicate<CodedException> FilterBackwardsCompatibilityIssues =
        ce => FhirJsonException.BACKWARDS_COMPATIBILITY_ALLOWED_ISSUES.Contains(ce.ErrorCode) ||
              FhirXmlException.BACKWARDS_COMPATIBILITY_ALLOWED_ISSUES.Contains(ce.ErrorCode);

    /// <summary>
    /// Combines two predicates for a <see cref="CodedException"/> with a logical AND.
    /// </summary>
    [return: NotNullIfNotNull(nameof(a))]
    [return: NotNullIfNotNull(nameof(b))]
    public static Predicate<CodedException>? And(this Predicate<CodedException>? a, Predicate<CodedException>? b) =>
        (a, b) switch
            {
                (a: null, b: not null) => b,
                (a: not null, b: null) => a,
                (a: not null, b: not null) => ce => a(ce) && b(ce),
                _ => null
            };

    /// <summary>
    /// Combines two predicates for a <see cref="CodedException"/> with a logical OR.
    /// </summary>
    [return: NotNullIfNotNull(nameof(a))]
    [return: NotNullIfNotNull(nameof(b))]
    public static Predicate<CodedException>? Or(this Predicate<CodedException>? a, Predicate<CodedException>? b) =>
        (a, b) switch
        {
            (a: null, b: not null) => b,
            (a: not null, b: null) => a,
            (a: not null, b: not null) => ce => a(ce) || b(ce),
            _ => null
        };

    /// <summary>
    /// Negates a predicate for a <see cref="CodedException"/>.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    [return: NotNullIfNotNull(nameof(a))]
    public static Predicate<CodedException>? Negate(this Predicate<CodedException>? a) =>
        a is not null ? ce => !a(ce) : null;

    /// <summary>
    /// Will restrict the predicate to only return true for the given list of error codes.
    /// </summary>
    public static Predicate<CodedException> Enforce(this Predicate<CodedException>? a, IEnumerable<string> toEnforce)
    {
        var enforcer = toEnforce.IsInList().Negate();
        return a is null ? enforcer : a.And(enforcer);
    }

    /// <summary>
    /// Will relax the pedicate to also return true for the given list of error codes.
    /// </summary>
    public static Predicate<CodedException> Ignore(this Predicate<CodedException>? a, IEnumerable<string> toIgnore)
    {
        var ignorer = toIgnore.IsInList();
        return a is null ? ignorer : a.Or(ignorer);
    }

    /// <summary>
    /// Returns a list of CodedException that do not match the filter.
    /// </summary>
    public static IEnumerable<CodedException> Remove(this IEnumerable<CodedException> issues, Predicate<CodedException> filter) =>
        issues.Where(ce => !filter(ce));

    internal static Predicate<CodedException> IsInList(this IEnumerable<string> ignoreList) =>
        ce => ignoreList.Contains(ce.ErrorCode);
}