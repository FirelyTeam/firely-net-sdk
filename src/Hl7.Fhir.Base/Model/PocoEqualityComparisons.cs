/*
 * Copyright (c) 2024, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Model;

/// <summary>
/// An <see cref="IEqualityComparer{T}"/> that compares FHIR POCOs on exact equality of all elements.
/// </summary>
public class ExactMatchEqualityComparer: IEqualityComparer<Base>, IEqualityComparer<IReadOnlyCollection<Base>>
{
    /// <summary>
    /// A singleton instance of the <see cref="ExactMatchEqualityComparer"/>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static readonly ExactMatchEqualityComparer Instance = new();

    public bool Equals(Base? x, Base? y)
    {
        if (x == null || y == null) return x==y;
        if (ReferenceEquals(x, y)) return true;
        return x.CompareChildren(y, this);
    }

    public int GetHashCode(Base obj) => obj.GetHashCode();

    public bool Equals(IReadOnlyCollection<Base>? x, IReadOnlyCollection<Base>? y)
    {
        if (x is null)
            return y is null || !y.Any();

        if (y is null)
            return !x.Any();

        return x.Count == y.Count && x.Zip(y, (l,r) => l.CompareChildren(r,this)).All(r => r);
    }

    public int GetHashCode(IReadOnlyCollection<Base> obj) => obj.GetHashCode();
}


/// <summary>
/// An <see cref="IEqualityComparer{T}"/> that compares FHIR POCOs on pattern equality of all elements.
/// </summary>
/// <remarks>See https://www.hl7.org/fhir/R4/elementdefinition-definitions.html#ElementDefinition.pattern_x_
/// for the definition of pattern matching.</remarks>
public class PatternMatchEqualityComparer: IEqualityComparer<Base>, IEqualityComparer<IReadOnlyCollection<Base>>
{
    /// <summary>
    /// A singleton instance of the <see cref="PatternMatchEqualityComparer"/>.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static readonly PatternMatchEqualityComparer Instance = new();

    public bool Equals(Base? x, Base? pattern)
    {
        if (pattern == null) return true;
        if (ReferenceEquals(x, pattern)) return true;
        return x != null && x.CompareChildren(pattern, this);
    }

    public int GetHashCode(Base obj) => obj.GetHashCode();

    public bool Equals(IReadOnlyCollection<Base>? x, IReadOnlyCollection<Base>? pattern)
    {
        // if not present in the pattern, there's a match
        if (pattern is null || !pattern.Any()) return true;

        return x != null && x.All(src => pattern.Any(patt => src.CompareChildren(patt, this)));
    }

    public int GetHashCode(IReadOnlyCollection<Base> obj) => obj.GetHashCode();
}


public static class PocoEqualityComparisons
{
    /// <summary>
    /// Poco equality comparers consist of comparisons for Base and IEnumerable{Base}. This extension
    /// method helps to compare two lists of Base elements, given the normal IEqualityComparer{T} for Base.
    /// </summary>
    /// <exception cref="ArgumentException">Is throw when the IEqualityComparer for Base does not
    /// support comparing IEnumerable{Base}</exception>
    public static bool ListEquals<T>(this IEqualityComparer<T> comparer, IReadOnlyCollection<T>? a, IReadOnlyCollection<T>? b)
    {
        if (comparer is not IEqualityComparer<IReadOnlyCollection<T>> listComparer)
            throw new ArgumentException("The comparer does not support list comparison", nameof(comparer));

#pragma warning disable CS8604 // Possible null reference argument - incorrect signature in netstd2.1
        return listComparer.Equals(a, b);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Compares two Base instances using the <see cref="ExactMatchEqualityComparer"/>.
    /// </summary>
    public static bool IsExactly(this Base? me, Base? other) => ExactMatchEqualityComparer.Instance.Equals(me, other);

    /// <summary>
    /// Compares two lists of Base using the <see cref="ExactMatchEqualityComparer"/>.
    /// </summary>
    public static bool IsExactly(this IReadOnlyCollection<Base>? me, IReadOnlyCollection<Base>? other) =>
        ExactMatchEqualityComparer.Instance.Equals(me, other);

    /// <summary>
    /// Compares two Base instances using the <see cref="PatternMatchEqualityComparer"/>.
    /// </summary>
    public static bool Matches(this Base? me, Base? other) => PatternMatchEqualityComparer.Instance.Equals(me, other);

    /// <summary>
    /// Compares two lists of Base using the <see cref="PatternMatchEqualityComparer"/>.
    /// </summary>
    public static bool Matches(this IReadOnlyCollection<Base>? me, IReadOnlyCollection<Base>? other) =>
        PatternMatchEqualityComparer.Instance.Equals(me, other);
}