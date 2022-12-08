/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System;
using System.Xml;
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.ElementModel.Types
{
    public class Long : Any, IComparable, ICqlEquatable, ICqlOrderable, ICqlConvertible
    {
        public Long() : this(default) { }

        public Long(long value) => Value = value;

        public long Value { get; }

        public static Long Parse(string value) =>
            TryParse(value, out var result) ? result : throw new FormatException($"String '{value}' was not recognized as a valid long integer.");

        public static bool TryParse(string representation, out Long value)
        {
            if (representation == null) throw new ArgumentNullException(nameof(representation));

            (var succ, var val) = Any.DoConvert(() => XmlConvert.ToInt64(representation));
            value = new Long(val);
            return succ;
        }

        /// <summary>
        /// Determines if two 64-bit integers are equal according to CQL equality rules.
        /// </summary>
        /// <remarks>For 64-bits integers, CQL and .NET equality rules are aligned.
        /// </remarks>
        public override bool Equals(object? obj) => obj is Long i && Value == i.Value;
        public static bool operator ==(Long a, Long b) => Equals(a, b);
        public static bool operator !=(Long a, Long b) => !Equals(a, b);

        /// <summary>
        /// Compares two 64-bit integers according to CQL equality rules
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks>For 64-bit integers, CQL and .NET comparison rules are aligned.</remarks>
        public int CompareTo(object? obj)
        {
            return obj switch
            {
                null => 1,
                Long i => Value.CompareTo(i.Value),
                _ => throw NotSameTypeComparison(this, obj)
            };
        }

        public static bool operator <(Long a, Long b) => a.CompareTo(b) < 0;
        public static bool operator <=(Long a, Long b) => a.CompareTo(b) <= 0;
        public static bool operator >(Long a, Long b) => a.CompareTo(b) > 0;
        public static bool operator >=(Long a, Long b) => a.CompareTo(b) >= 0;

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => XmlConvert.ToString(Value);

        public static implicit operator long(Long i) => i.Value;
        public static implicit operator Decimal(Long i) => new Decimal(i.Value);
        public static implicit operator Quantity(Long i) => new Quantity(i.Value, Quantity.UCUM_UNIT);

        public static explicit operator Long(long i) => new Long(i);
        public static explicit operator Boolean(Long l) => ((ICqlConvertible)l).TryConvertToBoolean().ValueOrThrow();
        public static explicit operator String(Long l) => ((ICqlConvertible)l).TryConvertToString().ValueOrThrow();
        public static explicit operator Integer(Long l) => ((ICqlConvertible)l).TryConvertToInteger().ValueOrThrow();

        bool? ICqlEquatable.IsEqualTo(Any other) => other is { } ? Equals(other) : (bool?)null;
        bool ICqlEquatable.IsEquivalentTo(Any other) => Equals(other);
        int? ICqlOrderable.CompareTo(Any other) => other is { } ? CompareTo(other) : (int?)null;

        Result<Boolean> ICqlConvertible.TryConvertToBoolean() =>
              Value switch
              {
                  1 => Ok((Boolean)true),
                  0 => Ok((Boolean)false),
                  _ => CannotCastTo<Boolean>(this)
              };

        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => Ok(new Decimal(Value));
        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => Ok(new Quantity(Value));
        Result<Long> ICqlConvertible.TryConvertToLong() => Ok(this);

        Result<Integer> ICqlConvertible.TryConvertToInteger() =>
            Value >= int.MinValue && Value <= int.MaxValue ?
                Ok(new Integer((int)Value)) : CannotCastTo<Integer>(this);

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);
        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);

    }
}