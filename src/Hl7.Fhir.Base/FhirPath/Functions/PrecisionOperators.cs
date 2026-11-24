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
    /// <remarks>FHIRPath dates and times are precise to the millisecond at most, so any fractional seconds count
    /// as the 3 digits of milliseconds, whatever the number of fraction digits written in the input.</remarks>
    internal static class PrecisionOperators
    {
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
        /// 10 (hour), 12 (minute), 14 (second) or 17 (millisecond).
        /// </summary>
        public static int Precision(this P.DateTime focus) => dateTimeDigits(focus.Precision);

        /// <summary>
        /// The number of digits in the time, ignoring the timezone, so 2 (hour), 4 (minute), 6 (second) or 9 (millisecond).
        /// </summary>
        public static int Precision(this P.Time focus) => focus.Precision switch
        {
            P.DateTimePrecision.Hour => 2,
            P.DateTimePrecision.Minute => 4,
            P.DateTimePrecision.Second => 6,
            P.DateTimePrecision.Fraction => 9,
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
            P.DateTimePrecision.Fraction => 17,
            _ => throw new ArgumentException($"Unexpected DateTime precision {precision}")
        };
    }
}
