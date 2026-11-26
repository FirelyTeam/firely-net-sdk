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
    /// <remarks>
    /// <para>Fractional seconds count as the number of fraction digits written in the input, so <c>@T10:30:00.0</c>
    /// gives 7 and <c>@T10:30:00.000</c> gives 9. A value that was not parsed from text, such as the result of
    /// <c>now()</c> or of date arithmetic, counts as having millisecond (3 digit) precision.</para>
    /// <para>This matches the HL7 Java engine for times and for dateTimes in data; for dateTime literals the Java
    /// engine pads the fraction to at least 3 digits.</para>
    /// <para>Date arithmetic currently drops an all-zero fraction (see #3712), so such a result has second precision.</para>
    /// </remarks>
    internal static class PrecisionOperators
    {
        private const int MILLISECOND_DIGITS = 3;

        /// <summary>The digits of the date part (yyyyMMdd), which a time does not have.</summary>
        private const int DATE_DIGITS = 8;

        /// <summary>
        /// The number of digits after the decimal point, so <c>1.58700.precision()</c> is 5.
        /// </summary>
        public static int PrecisionDigits(this decimal focus) => P.Decimal.Scale(focus, ignoreTrailingZeroes: false);

        /// <summary>
        /// The number of digits in the date, so 4 (year), 6 (month) or 8 (day).
        /// </summary>
        public static int PrecisionDigits(this P.Date focus) => dateTimeDigits(focus.Precision, null);

        /// <summary>
        /// The number of digits in the dateTime, ignoring the timezone, so 4 (year), 6 (month), 8 (day),
        /// 10 (hour), 12 (minute), 14 (second), or 14 plus the number of fraction digits (17 for milliseconds).
        /// </summary>
        public static int PrecisionDigits(this P.DateTime focus) => dateTimeDigits(focus.Precision, focus.ParsedFractionDigits);

        /// <summary>
        /// The number of digits in the time, ignoring the timezone, so 2 (hour), 4 (minute), 6 (second),
        /// or 6 plus the number of fraction digits (9 for milliseconds).
        /// </summary>
        public static int PrecisionDigits(this P.Time focus) => dateTimeDigits(focus.Precision, focus.ParsedFractionDigits) - DATE_DIGITS;

        private static int dateTimeDigits(P.DateTimePrecision precision, int? parsedFractionDigits) => precision switch
        {
            P.DateTimePrecision.Year => 4,
            P.DateTimePrecision.Month => 6,
            P.DateTimePrecision.Day => DATE_DIGITS,
            P.DateTimePrecision.Hour => 10,
            P.DateTimePrecision.Minute => 12,
            P.DateTimePrecision.Second => 14,
            P.DateTimePrecision.Fraction => 14 + (parsedFractionDigits ?? MILLISECOND_DIGITS),
            _ => throw new InvalidOperationException($"Unexpected date/time precision {precision}")
        };
    }
}
