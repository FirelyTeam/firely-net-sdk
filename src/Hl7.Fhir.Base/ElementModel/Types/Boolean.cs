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
    public class Boolean : Any, ICqlEquatable, ICqlConvertible
    {
        public static Boolean True = new Boolean(true);
        public static Boolean False = new Boolean(false);
        public const string TRUE_LITERAL = "true";
        public const string FALSE_LITERAL = "false";

        public Boolean() : this(default) { }
        public Boolean(bool value) => Value = value;

        public bool Value { get; }

        public static Boolean Parse(string value) =>
            TryParse(value, out var result) ? result! : throw new FormatException($"String '{value}' was not recognized as a valid boolean.");

        public static bool TryParse(string representation, out Boolean? value)
        {
            if (representation is null) throw new ArgumentNullException(nameof(representation));

            if (representation == TRUE_LITERAL)
            {
                value = True;
                return true;
            }
            else if (representation == FALSE_LITERAL)
            {
                value = False;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value ? TRUE_LITERAL : FALSE_LITERAL;
        public override bool Equals(object? obj) => obj is Boolean b && Value == b.Value;

        public static bool operator ==(Boolean a, Boolean b) => Equals(a, b);
        public static bool operator !=(Boolean a, Boolean b) => !Equals(a, b);

        public static implicit operator bool(Boolean b) => b.Value;
        public static explicit operator Boolean(bool b) => new Boolean(b);
        public static explicit operator Decimal(Boolean b) => ((ICqlConvertible)b).TryConvertToDecimal().ValueOrThrow();
        public static explicit operator Integer(Boolean b) => ((ICqlConvertible)b).TryConvertToInteger().ValueOrThrow();
        public static explicit operator Long(Boolean b) => ((ICqlConvertible)b).TryConvertToLong().ValueOrThrow();
        public static explicit operator Quantity(Boolean b) => ((ICqlConvertible)b).TryConvertToQuantity().ValueOrThrow();
        public static explicit operator String(Boolean b) => ((ICqlConvertible)b).TryConvertToString().ValueOrThrow();

        bool? ICqlEquatable.IsEqualTo(Any other) => other is { } ? (bool?)Equals(other) : null;
        bool ICqlEquatable.IsEquivalentTo(Any other) => Equals(other);

        Result<Boolean> ICqlConvertible.TryConvertToBoolean() => Ok(this);

        Result<Decimal> ICqlConvertible.TryConvertToDecimal() =>
            Value switch
            {
                true => Ok(new Decimal(1m)),
                false => Ok(new Decimal(0m)),
            };


        Result<Integer> ICqlConvertible.TryConvertToInteger() =>
            Value switch
            {
                true => Ok(new Integer(1)),
                false => Ok(new Integer(0)),
            };

        Result<Long> ICqlConvertible.TryConvertToLong() =>
            Value switch
            {
                true => Ok(new Long(1)),
                false => Ok(new Long(0)),
            };

        Result<Quantity> ICqlConvertible.TryConvertToQuantity() =>
            Value switch
            {
                true => Ok(new Quantity(1.0m)),
                false => Ok(new Quantity(0.0m)),
            };

        Result<String> ICqlConvertible.TryConvertToString() => Ok(new String(ToString()));

        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => CannotCastTo<Concept>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);
    }
}
