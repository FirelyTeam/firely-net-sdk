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
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.ElementModel.Types
{
    public class Ratio : Any, ICqlConvertible
    {
        public Ratio(Quantity numerator, Quantity denominator)
        {
            Numerator = numerator ?? throw new ArgumentNullException(nameof(numerator));
            Denominator = denominator ?? throw new ArgumentNullException(nameof(denominator));
        }

        public Quantity Numerator { get; }
        public Quantity Denominator { get; }

        public static Ratio Parse(string representation) =>
             TryParse(representation, out var result) ? result! : throw new FormatException($"String '{representation}' was not recognized as a valid ratio.");
        public static bool TryParse(string representation, out Ratio? value)
        {
            if (representation is null) throw new ArgumentNullException(nameof(representation));

            value = null;

            // Not too sure if quantities cannot contain colons themselves, but I have
            // no time to worry about that now.
            var components = representation.Split(':');
            if (components.Length != 2) return false;

            if (!Quantity.TryParse(components[0].Trim(), out var numerator)) return false;
            if (!Quantity.TryParse(components[1].Trim(), out var denumerator)) return false;

            value = new Ratio(numerator, denumerator);
            return true;
        }

        public override bool Equals(object? obj) => obj is Ratio r && Numerator == r.Numerator && Denominator == r.Denominator;

        public override int GetHashCode() => (Numerator, Denominator).GetHashCode();
        public override string ToString() => $"{Numerator}:{Denominator}";

        public static explicit operator String(Ratio r) => ((ICqlConvertible)r).TryConvertToString().ValueOrThrow();

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));

        Result<Ratio> ICqlConvertible.TryConvertToRatio() => Ok(this);

        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Boolean> ICqlConvertible.TryConvertToBoolean() => CannotCastTo<Boolean>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);
        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => CannotCastTo<Decimal>(this);
        Result<Integer> ICqlConvertible.TryConvertToInteger() => CannotCastTo<Integer>(this);
        Result<Long> ICqlConvertible.TryConvertToLong() => CannotCastTo<Long>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => CannotCastTo<Quantity>(this);

        public static bool operator ==(Ratio left, Ratio right) => left.Equals(right);
        public static bool operator !=(Ratio left, Ratio right) => !Equals(left, right);

        // Does not support equality, equivalence and ordering in the CQL sense, so no explicit implementations of these interfaces
    }
}
