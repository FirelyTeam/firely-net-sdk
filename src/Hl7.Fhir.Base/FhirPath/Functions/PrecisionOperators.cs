/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Functions
{
    /// <summary>
    /// Implementation of the FHIRPath <c>precision()</c> function, see https://hl7.org/fhirpath/#precision--integer.
    /// </summary>
    /// <remarks>Fractional seconds count as the number of fraction digits written in the input, so <c>@T10:30:00.0</c>
    /// gives 7 and <c>@T10:30:00.000</c> gives 9. This matches the HL7 Java engine for times and for dateTimes in data;
    /// for dateTime literals the Java engine pads the fraction to at least 3 digits. A value that was not parsed from
    /// text, such as the result of <c>now()</c> or of date arithmetic, counts as having millisecond (3 digit) precision.
    /// Note that date arithmetic currently drops an all-zero fraction (see #3712), so such a result has second precision.</remarks>
    internal static class PrecisionOperators
    {
        private const int MILLISECOND_DIGITS = 3;

        /// <summary>
        /// The number of digits after the decimal point, so <c>1.58700.precision()</c> is 5.
        /// </summary>
#if NET7_0_OR_GREATER
        public static int Precision(this decimal focus) => focus.Scale;
#else
        public static int Precision(this decimal focus) => (decimal.GetBits(focus)[3] >> 16) & 0xFF;
#endif

        /// <summary>
        /// The number of digits in the date, so 4 (year), 6 (month) or 8 (day).
        /// </summary>
        public static int Precision(this P.Date focus) => dateTimeDigits(focus.Precision);

        /// <summary>
        /// The number of digits in the dateTime, ignoring the timezone, so 4 (year), 6 (month), 8 (day),
        /// 10 (hour), 12 (minute), 14 (second), or 14 plus the number of fraction digits (17 for milliseconds).
        /// </summary>
        public static int Precision(this P.DateTime focus) => focus.Precision == P.DateTimePrecision.Fraction
            ? 14 + (focus.ParsedFractionDigits ?? MILLISECOND_DIGITS)
            : dateTimeDigits(focus.Precision);

        /// <summary>
        /// The number of digits in the time, ignoring the timezone, so 2 (hour), 4 (minute), 6 (second),
        /// or 6 plus the number of fraction digits (9 for milliseconds).
        /// </summary>
        public static int Precision(this P.Time focus) => focus.Precision switch
        {
            P.DateTimePrecision.Hour => 2,
            P.DateTimePrecision.Minute => 4,
            P.DateTimePrecision.Second => 6,
            P.DateTimePrecision.Fraction => 6 + (focus.ParsedFractionDigits ?? MILLISECOND_DIGITS),
            _ => throw new ArgumentException($"Unexpected Time precision {focus.Precision}")
        };

        private static int dateTimeDigits(P.DateTimePrecision precision) => precision switch
        {
            P.DateTimePrecision.Year => 4,
            P.DateTimePrecision.Month => 6,
            P.DateTimePrecision.Day => 8,
            P.DateTimePrecision.Hour => 10,
            P.DateTimePrecision.Minute => 12,
            P.DateTimePrecision.Second => 14,
            _ => throw new ArgumentException($"Unexpected DateTime precision {precision}")
        };
    }
}
