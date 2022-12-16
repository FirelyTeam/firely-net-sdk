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
    public class Code : Any, ICqlConvertible
    {
        public Code(string? system, string code, string? display = null, string? version = null)
        {
            System = system;
            Value = code ?? throw new ArgumentNullException(nameof(code));
            Display = display;
            Version = version;
        }

        public string? System { get; }
        public string Value { get; }
        public string? Display { get; }
        public string? Version { get; }

        public static Code Parse(string value) => throw new NotImplementedException();
        public static bool TryParse(string representation, out Code? value) => throw new NotImplementedException();

        public override int GetHashCode() => (System, Value, Display, Version).GetHashCode();
        public override string ToString() => $"{Value}@{System} " + Display ?? "";
        public override bool Equals(object? obj) => obj is Code c
            && System == c.System && Value == c.Value && Display == c.Display && Version == c.Version;

        public static implicit operator Concept(Code c) => ((ICqlConvertible)c).TryConvertToConcept().ValueOrThrow();


        Result<Code> ICqlConvertible.TryConvertToCode() => Ok(this);
        Result<Concept> ICqlConvertible.TryConvertToConcept() => Ok(new Concept(new[] { this }, Display));

        Result<Boolean> ICqlConvertible.TryConvertToBoolean() => CannotCastTo<Boolean>(this);
        Result<Date> ICqlConvertible.TryConvertToDate() => CannotCastTo<Date>(this);
        Result<DateTime> ICqlConvertible.TryConvertToDateTime() => CannotCastTo<DateTime>(this);
        Result<Decimal> ICqlConvertible.TryConvertToDecimal() => CannotCastTo<Decimal>(this);
        Result<Integer> ICqlConvertible.TryConvertToInteger() => CannotCastTo<Integer>(this);
        Result<Long> ICqlConvertible.TryConvertToLong() => CannotCastTo<Long>(this);
        Result<Quantity> ICqlConvertible.TryConvertToQuantity() => CannotCastTo<Quantity>(this);
        Result<Ratio> ICqlConvertible.TryConvertToRatio() => CannotCastTo<Ratio>(this);
        Result<String> ICqlConvertible.TryConvertToString() => CannotCastTo<String>(this);
        Result<Time> ICqlConvertible.TryConvertToTime() => CannotCastTo<Time>(this);

        public static bool operator ==(Code left, Code right) => left.Equals(right);
        public static bool operator !=(Code left, Code right) => !left.Equals(right);

        // Does not support equality, equivalence and ordering in the CQL sense, so no explicit implementations of these interfaces
    }
}
