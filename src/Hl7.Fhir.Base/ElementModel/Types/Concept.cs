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
using System.Collections.Generic;
using System.Linq;
using static Hl7.Fhir.Utility.Result;


namespace Hl7.Fhir.ElementModel.Types
{
    public class Concept : Any, ICqlConvertible
    {
        public Concept(IEnumerable<Code> codes, string? display = null)
        {
            Codes = codes.ToArray();
            Display = display;
        }

        public IReadOnlyCollection<Code> Codes { get; }

        public string? Display { get; }

        public static Concept Parse(string representation) => throw new NotImplementedException();
        public static bool TryParse(string representation, out Concept? value) => throw new NotImplementedException();

        public override bool Equals(object? obj) => obj is Concept c && Enumerable.SequenceEqual(Codes, c.Codes) && Display == c.Display;

        public override int GetHashCode() => (Codes, Display).GetHashCode();
        public override string ToString() => string.Join(", ", Codes) + Display != null ? $" \"{Display}\"" : "";

        Result<Concept> ICqlConvertible.TryConvertToConcept() => Ok(this);

        Result<Code> ICqlConvertible.TryConvertToCode() => CannotCastTo<Code>(this);
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


        public static bool operator ==(Concept left, Concept right) => Equals(left, right);
        public static bool operator !=(Concept left, Concept right) => !Equals(left, right);

        // Does not support equality, equivalence and ordering in the CQL sense, so no explicit implementations of these interfaces
    }
}
