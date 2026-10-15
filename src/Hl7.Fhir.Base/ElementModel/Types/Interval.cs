/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;

namespace Hl7.Fhir.ElementModel.Types;

/// <summary>
/// An interval of ordered values. Each side is either bounded, and then closed (inclusive) or open (exclusive),
/// or unbounded.
/// </summary>
/// <remarks>
/// <para>An unbounded side means the interval extends indefinitely in that direction. Unlike CQL, there is no notion
/// of an <i>unknown</i> bound, and the operations return definite answers rather than three-valued logic; CQL semantics
/// can be layered on top.</para>
/// <para>The operations treat <typeparamref name="T"/> as a dense domain: they compare bounds only and never
/// look for a value between them. For a discrete type such as <see cref="int"/>, use closed bounds, so <c>[1, 1]</c>
/// rather than <c>(0, 2)</c>; with open bounds, <c>(1, 2)</c> is accepted although it holds no integer, and
/// <c>[1, 2)</c> is reported to overlap <c>(1, 2]</c>.</para>
/// <para>Empty intervals cannot be created: the low bound must lie before the high bound, or equal it when both are closed.</para>
/// <para>This is a value type to avoid an allocation per interval. <c>default(Interval&lt;T&gt;)</c> is the interval
/// unbounded on both sides, which contains every value.</para>
/// </remarks>
public readonly struct Interval<T> : IEquatable<Interval<T>> where T : IComparable<T>
{
    private readonly T _low;
    private readonly T _high;

    private Interval(bool hasLow, T low, bool lowClosed, bool hasHigh, T high, bool highClosed)
    {
        if (hasLow && low is null) throw new ArgumentNullException(nameof(low));
        if (hasHigh && high is null) throw new ArgumentNullException(nameof(high));

        if (hasLow && hasHigh)
        {
            var c = low.CompareTo(high);
            if (c > 0 || (c == 0 && !(lowClosed && highClosed)))
                throw new ArgumentException("The interval is empty: its high bound does not lie after its low bound.");
        }

        HasLow = hasLow;
        _low = low;
        LowClosed = hasLow && lowClosed;
        HasHigh = hasHigh;
        _high = high;
        HighClosed = hasHigh && highClosed;
    }

    /// <summary>
    /// Creates an interval bounded on both sides.
    /// </summary>
    /// <exception cref="ArgumentException">The interval would be empty.</exception>
    public Interval(T low, T high, bool lowClosed = true, bool highClosed = true)
        : this(true, low, lowClosed, true, high, highClosed)
    {
    }

    /// <summary>
    /// Creates an interval from <paramref name="low"/>, unbounded above.
    /// </summary>
    public static Interval<T> From(T low, bool closed = true) => new(true, low, closed, false, default!, false);

    /// <summary>
    /// Creates an interval up to <paramref name="high"/>, unbounded below.
    /// </summary>
    public static Interval<T> Until(T high, bool closed = true) => new(false, default!, false, true, high, closed);

    /// <summary>
    /// Creates an interval from optional bounds; a side without a bound is unbounded.
    /// </summary>
    /// <exception cref="ArgumentException">Neither side is bounded, or the interval would be empty.</exception>
    internal static Interval<T> Create(bool hasLow, T low, bool lowClosed, bool hasHigh, T high, bool highClosed)
    {
        if (!hasLow && !hasHigh) throw new ArgumentException("An interval must have at least one bound.");
        return new(hasLow, low, lowClosed, hasHigh, high, highClosed);
    }

    /// <summary>Whether the interval has a low bound.</summary>
    public bool HasLow { get; }

    /// <summary>The low bound.</summary>
    /// <exception cref="InvalidOperationException">The interval is unbounded below.</exception>
    public T Low => HasLow ? _low : throw new InvalidOperationException("The interval is unbounded below.");

    /// <summary>Whether the low bound is part of the interval. Always <c>false</c> when unbounded below.</summary>
    public bool LowClosed { get; }

    /// <summary>Whether the interval has a high bound.</summary>
    public bool HasHigh { get; }

    /// <summary>The high bound.</summary>
    /// <exception cref="InvalidOperationException">The interval is unbounded above.</exception>
    public T High => HasHigh ? _high : throw new InvalidOperationException("The interval is unbounded above.");

    /// <summary>Whether the high bound is part of the interval. Always <c>false</c> when unbounded above.</summary>
    public bool HighClosed { get; }

    /// <summary>
    /// Whether <paramref name="point"/> lies within this interval.
    /// </summary>
    public bool Contains(T point)
    {
        if (point is null) throw new ArgumentNullException(nameof(point));

        return (!HasLow || compare(_low, point, LowClosed))
            && (!HasHigh || compare(point, _high, HighClosed));

        static bool compare(T a, T b, bool inclusive)
        {
            var c = a.CompareTo(b);
            return c < 0 || (c == 0 && inclusive);
        }
    }

    /// <summary>
    /// Whether this interval and <paramref name="other"/> share at least one point.
    /// </summary>
    public bool Overlaps(Interval<T> other)
    {
        return startsBeforeEnd(this, other) && startsBeforeEnd(other, this);

        static bool startsBeforeEnd(Interval<T> a, Interval<T> b)
        {
            if (!a.HasLow || !b.HasHigh) return true;
            var c = a._low.CompareTo(b._high);
            return c < 0 || (c == 0 && a.LowClosed && b.HighClosed);
        }
    }

    /// <summary>
    /// Whether every point of <paramref name="other"/> lies within this interval (CQL <c>includes</c>).
    /// </summary>
    public bool Covers(Interval<T> other)
    {
        var lowOk = !HasLow || (other.HasLow && boundCovers(_low, LowClosed, other._low, other.LowClosed, below: true));
        var highOk = !HasHigh || (other.HasHigh && boundCovers(_high, HighClosed, other._high, other.HighClosed, below: false));
        return lowOk && highOk;

        static bool boundCovers(T outer, bool outerClosed, T inner, bool innerClosed, bool below)
        {
            var c = outer.CompareTo(inner);
            if (c == 0) return outerClosed || !innerClosed;
            return below ? c < 0 : c > 0;
        }
    }

    /// <summary>
    /// Two intervals are equal when they have the same bounds (by <see cref="EqualityComparer{T}.Default"/>) and closedness.
    /// </summary>
    public bool Equals(Interval<T> other) =>
        HasLow == other.HasLow && LowClosed == other.LowClosed
        && HasHigh == other.HasHigh && HighClosed == other.HighClosed
        && (!HasLow || EqualityComparer<T>.Default.Equals(_low, other._low))
        && (!HasHigh || EqualityComparer<T>.Default.Equals(_high, other._high));

    public override bool Equals(object? obj) => obj is Interval<T> other && Equals(other);

    public static bool operator ==(Interval<T> left, Interval<T> right) => left.Equals(right);

    public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);

    public override int GetHashCode() =>
        HashCode.Combine(HasLow ? _low : default, LowClosed, HasHigh ? _high : default, HighClosed);

    public override string ToString() => ToString(v => v.ToString() ?? "");

    internal string ToString(Func<T, string> format) =>
        (HasLow ? (LowClosed ? "[" : "(") + format(_low) : "(-∞")
        + ", "
        + (HasHigh ? format(_high) + (HighClosed ? "]" : ")") : "+∞)");
}
