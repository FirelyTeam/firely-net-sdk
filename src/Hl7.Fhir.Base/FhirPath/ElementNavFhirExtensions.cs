/* 
 * Copyright (c) 2023, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.FhirPath
{
    public static class ElementNavFhirExtensions
    {
        internal static bool _fhirSymbolTableExtensionsAdded = false;
        public static void PrepareFhirSymbolTableFunctions()
        {
            if (!_fhirSymbolTableExtensionsAdded)
            {
                _fhirSymbolTableExtensionsAdded = true;
                Hl7.FhirPath.FhirPathCompiler.DefaultSymbolTable.AddFhirExtensions();
            }
        }

        public static SymbolTable AddFhirExtensions(this SymbolTable t)
        {
            t.Add("hasValue", (PocoNode f) => f.HasValue(), doNullProp: false);
            t.Add("resolve", (PocoNode f, EvaluationContext ctx) => resolver(f, ctx), doNullProp: false);
            t.Add("resolve", (IEnumerable<PocoNode> f, EvaluationContext ctx) => f.Select(fi => resolver(fi, ctx)).OfType<PocoNode>(), doNullProp: false);

            t.Add("memberOf", (PocoNode input, string valueset, EvaluationContext ctx) => MemberOf(input, valueset, ctx), doNullProp: false);

            // Pre-normative this function was called htmlchecks, normative is htmlChecks
            // lets keep both to keep everyone happy.
            t.Add("htmlchecks", (PocoNode f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlChecks", (PocoNode f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlchecks", (IEnumerable<PocoNode> f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlChecks", (IEnumerable<PocoNode> f) => f.HtmlChecks(), doNullProp: false);

            // An integer input is converted to a decimal, so its boundaries are those of the decimal (1 gives 0.5 and 1.5).
            t.Add("lowBoundary", (decimal d, long precision) => BoundaryDecimal(d, precision, high: false), doNullProp: false);
            t.Add("lowBoundary", (decimal d) => BoundaryDecimal(d, null, high: false), doNullProp: false);
            t.Add("lowBoundary", (P.Any a, long precision) => LowBoundary(a, precision), doNullProp: false);
            t.Add("lowBoundary", (P.Any a) => LowBoundary(a, null), doNullProp: false);

            t.Add("highBoundary", (decimal d, long precision) => BoundaryDecimal(d, precision, high: true), doNullProp: false);
            t.Add("highBoundary", (decimal d) => BoundaryDecimal(d, null, high: true), doNullProp: false);
            t.Add("highBoundary", (P.Any a, long precision) => HighBoundary(a, precision), doNullProp: false);
            t.Add("highBoundary", (P.Any a) => HighBoundary(a, null), doNullProp: false);

            //https://github.com/hapifhir/org.hl7.fhir.core/blob/master/org.hl7.fhir.r5/src/main/java/org/hl7/fhir/r5/utils/FHIRPathEngine.java
            t.Add("comparable", (P.Quantity l, P.Quantity r) => Comparable(l, r), doNullProp: false);

            return t;

            static PocoNode? resolver(PocoNode f, EvaluationContext ctx)
            {
                return ctx is FhirEvaluationContext fctx ? f.Resolve(fctx.ElementResolver) : f.Resolve();
            }
        }


        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HasValue(this PocoNode focus) => focus is PrimitiveNode {Value: not null};

        /// <summary>
        /// Check whether the node contains valid Xhtml narrative.
        /// </summary>
        /// <remarks>When invoked on an xhtml element, the contents are validated against the rules around HTML usage.
        /// When invoked on a string, the contents of the string are parsed as the content of a div and validated
        /// against those same rules. For any other kind of element, the result is <c>null</c> (empty).</remarks>
        public static bool? HtmlChecks(this PocoNode focus) => focus switch
        {
            PrimitiveNode { Primitive: XHtml { JsonValue: string xhtml } } => XHtml.IsValidNarrativeXhtml(xhtml, out _, out _),
            PrimitiveNode { Value: string s } => XHtml.IsValidNarrativeXhtml($"<div xmlns=\"{XmlNs.XHTML}\">{s}</div>", out _, out _),
            _ => null
        };

        public static bool? HtmlChecks(this IEnumerable<PocoNode> focus)
        {
            var single = focus.Take(2).ToArray();
            return single.Length == 1 ? single[0].HtmlChecks() : null;
        }

        public static IEnumerable<Base?> ToFhirValues(this IEnumerable<PocoNode> results)
        {
            return results.Select(r => r.Poco);
        }

        /// <summary>
        /// The default number of decimal places of the result of lowBoundary() and highBoundary() on a decimal.
        /// </summary>
        private const int DEFAULT_DECIMAL_BOUNDARY_PRECISION = 8;

        /// <summary>
        /// The largest number of decimal places a <see cref="decimal"/> can hold.
        /// </summary>
        private const int MAX_DECIMAL_SCALE = 28;

        /// <summary>
        /// Calculates the low or high boundary of a decimal, as done by lowBoundary() and highBoundary().
        /// </summary>
        /// <remarks>
        /// <para>The precision of the input is given by its digits: 1.587 stands for any value from 1.5865 up to 1.5875, so
        /// half a unit of the last digit is subtracted (low) or added (high). An integer like 120 has the boundaries 119.5 and
        /// 120.5. For a negative number the boundaries mirror those of its magnitude: the low boundary of -1.587 is -1.5875.</para>
        /// <para>The result has exactly <paramref name="precision"/> decimal places (default 8). Where that drops digits, the
        /// magnitude of the boundary that lies further from zero is rounded half up, and the one closer to zero is
        /// truncated. This follows the FHIRPath conformance tests and the reference implementation:
        /// 1.587.lowBoundary(2) is 1.58, 1.587.highBoundary(2) is 1.59, 9.95.highBoundary(1) is 10.0 and
        /// 0.0034.highBoundary(1) is 0.0.</para>
        /// <para>Zero is half a unit of its last digit away from both its boundaries, so 0.0 gives -0.05 and 0.05. Where
        /// digits are dropped, its high boundary is rounded half up and its low boundary truncated (0.lowBoundary(0) is 0),
        /// as in the reference implementation. (That implementation uses 0.5 for every zero, ignoring its digits.)</para>
        /// </remarks>
        /// <returns>The boundary, or <c>null</c> (an empty result) when it does not fit in a decimal at the requested
        /// precision, such as a value above about 7.9e20 at the default precision of 8. The spec gives an empty result when
        /// the precision is beyond what the implementation supports.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The precision is negative or larger than a decimal can hold.</exception>
        internal static decimal? BoundaryDecimal(decimal input, long? precision, bool high)
        {
            var resultScale = precision ?? DEFAULT_DECIMAL_BOUNDARY_PRECISION;
            if (resultScale is < 0 or > MAX_DECIMAL_SCALE)
                throw new ArgumentOutOfRangeException(nameof(precision), precision,
                    $"The precision of a decimal boundary must be between 0 and {MAX_DECIMAL_SCALE}.");

            // Split the input into its digits (as an integer) and its scale, so 1.587 becomes 1587 and 3, and 120 becomes
            // 120 and 0. Trailing zeroes count: 12.500 is 12500 and 3.
            var bits = decimal.GetBits(input);
            var scale = (bits[3] >> 16) & 0xFF;
            var isNegative = input < 0;
            var digits = (new BigInteger((uint)bits[2]) << 64) | (new BigInteger((uint)bits[1]) << 32) | (uint)bits[0];

            // Work with one more decimal place, where half a unit of the last digit of the input is 5.
            var magnitude = digits * 10;
            var workScale = scale + 1;

            // The boundary that lies further from zero is the high boundary of a positive number and the low boundary of a
            // negative one. Zero has no "closer to zero" side: both its boundaries lie half a unit away from it.
            var furtherFromZero = digits.IsZero || high != isNegative;
            var boundary = furtherFromZero ? magnitude + 5 : magnitude - 5;
            var negativeResult = digits.IsZero ? !high : isNegative;

            // As in the reference implementation, the low boundary of zero is truncated: 0.lowBoundary(0) is 0, not -1.
            var roundHalfUp = digits.IsZero ? high : furtherFromZero;
            var result = rescale(boundary, workScale, (int)resultScale, roundHalfUp);
            return toDecimal(result, (byte)resultScale, negativeResult);

            static BigInteger rescale(BigInteger value, int fromScale, int toScale, bool roundHalfUp)
            {
                if (toScale >= fromScale) return value * BigInteger.Pow(10, toScale - fromScale);

                var divisor = BigInteger.Pow(10, fromScale - toScale);
                var quotient = BigInteger.DivRem(value, divisor, out var remainder);
                return roundHalfUp && remainder * 2 >= divisor ? quotient + 1 : quotient;
            }

            static decimal? toDecimal(BigInteger value, byte scale, bool isNegative)
            {
                // A decimal holds 96 bits of digits.
                if (value.GetByteCount(isUnsigned: true) > 12) return null;

                var bits = decimal.GetBits((decimal)value);

                // Never produce a negative zero: (-0.0034).lowBoundary(1) is 0.0.
                return new decimal(bits[0], bits[1], bits[2], isNegative && !value.IsZero, scale);
            }
        }

        internal static P.Any? LowBoundary(P.Any input, long? precision)
        {
            return input switch
            {
                P.Quantity q => BoundaryQuantity(q, precision, high: false),
                P.Date d => BoundaryDateTime(d, precision, 1, 1, 0, 0, 0, 0),
                P.DateTime dt => BoundaryDateTime(dt, precision, 1, 1, 0, 0, 0, 0),
                P.Time t => BoundaryTime(t, precision, 0, 0, 0),
                _ => null
            };
        }

        internal static P.Any? HighBoundary(P.Any input, long? precision)
        {
            return input switch
            {
                P.Quantity q => BoundaryQuantity(q, precision, high: true),
                P.Date d => BoundaryDateTime(d, precision, 12, 31, 23, 59, 59, 999),
                P.DateTime dt => BoundaryDateTime(dt, precision, 12, 31, 23, 59, 59, 999),
                P.Time t => BoundaryTime(t, precision, 59, 59, 999),
                _ => null
            };
        }

        internal static P.Quantity? BoundaryQuantity(P.Quantity input, long? precision, bool high) =>
            BoundaryDecimal(input.Value, precision, high) is { } value ? new(value, input.Unit, input.System) : null;

        internal static P.Any BoundaryDateTime(P.DateTime dt, long? precision, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            TimeSpan offset = dt.HasOffset ? dt.Offset!.Value : TimeSpan.Zero;

            DateTimeOffset dto = dt.Precision switch
            {
                P.DateTimePrecision.Year => new(dt.Years!.Value, months, days, hours, minutes, seconds, milliseconds, offset),
                P.DateTimePrecision.Month => new(dt.Years!.Value, dt.Months!.Value, days == 1 ? days : DateTime.DaysInMonth(dt.Years.Value, dt.Months.Value), hours, minutes, seconds, milliseconds, offset),
                P.DateTimePrecision.Day => new(dt.Years!.Value, dt.Months!.Value, dt.Days!.Value, hours, minutes, seconds, milliseconds, offset),
                P.DateTimePrecision.Hour => new(dt.Years!.Value, dt.Months!.Value, dt.Days!.Value, dt.Hours!.Value, minutes, seconds, milliseconds, offset),
                P.DateTimePrecision.Minute => new(dt.Years!.Value, dt.Months!.Value, dt.Days!.Value, dt.Hours!.Value, dt.Minutes!.Value, seconds, milliseconds, offset),
                P.DateTimePrecision.Second => new(dt.Years!.Value, dt.Months!.Value, dt.Days!.Value, dt.Hours!.Value, dt.Minutes!.Value, dt.Seconds!.Value, milliseconds, offset),
                P.DateTimePrecision.Fraction => new(dt.Years!.Value, dt.Months!.Value, dt.Days!.Value, dt.Hours!.Value, dt.Minutes!.Value, dt.Seconds!.Value, dt.Millis!.Value, offset),
                _ => throw new ArgumentException("Unexpected DateTime precision found")
            };

            P.DateTimePrecision dtPrecision = precision switch
            {
                4 => P.DateTimePrecision.Year,
                6 => P.DateTimePrecision.Month,
                8 => P.DateTimePrecision.Day,
                14 => P.DateTimePrecision.Second,
                >= 17 or null => P.DateTimePrecision.Fraction,
                _ => throw new ArgumentException($"Unsupported DateTime precision for boundary operation: {precision}")
            };

            return
                (dtPrecision <= P.DateTimePrecision.Day) ?
                    P.Date.FromDateTimeOffset(dto, dtPrecision, dt.HasOffset) :
                    P.DateTime.FromDateTimeOffset(dto, dtPrecision, dt.HasOffset);
        }

        internal static P.Time BoundaryTime(P.Time time, long? precision, int minutes, int seconds, int milliseconds)
        {
            const int defaultYear = 2023;
            const int defaultMonth = 1;
            const int defaultDay = 1;

            TimeSpan offset = time.HasOffset ? time.Offset!.Value : TimeSpan.Zero;

            DateTimeOffset dto = time.Precision switch
            {
                P.DateTimePrecision.Hour => new(defaultYear, defaultMonth, defaultDay, time.Hours!.Value, minutes, seconds, milliseconds, offset),
                P.DateTimePrecision.Minute => new(defaultYear, defaultMonth, defaultDay, time.Hours!.Value, time.Minutes!.Value, seconds, milliseconds, offset),
                P.DateTimePrecision.Second => new(defaultYear, defaultMonth, defaultDay, time.Hours!.Value, time.Minutes!.Value, time.Seconds!.Value, milliseconds, offset),
                P.DateTimePrecision.Fraction => new(defaultYear, defaultMonth, defaultDay, time.Hours!.Value, time.Minutes!.Value, time.Seconds!.Value, time.Millis!.Value, offset),
                _ => throw new ArgumentException("Unexpected Time precision found")
            };

            P.DateTimePrecision dtPrecision = precision switch
            {
                2 => P.DateTimePrecision.Hour,
                4 => P.DateTimePrecision.Minute,
                6 => P.DateTimePrecision.Second,
                9 or null => P.DateTimePrecision.Fraction,
                _ => throw new ArgumentException($"Unsupported Time precision for boundary operation: {precision}")
            };

            return P.Time.FromDateTimeOffset(dto, dtPrecision);
        }

        /// <summary>
        /// Compares the singleton Quantity with the singleton other Quantity and determine their relationship to each other. Comparable means that both 
        /// have values and that the code and system for the units are the same (irrespective of system) or both have code + system, system is recognized 
        /// by the FHIRPath implementation and the codes are comparable within that code system. E.g. days and hours or inches and cm
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        internal static bool Comparable(P.Quantity l, P.Quantity r) => l.TryCompareTo(r, out _);

        /// <summary>
        /// When invoked on a single code-valued element, returns true if the code is a member of the given valueset. 
        /// When invoked on a single concept-valued element, returns true if any code in the concept is a member of the given valueset. 
        /// When invoked on a single string, returns true if the string is equal to a code in the valueset, so long as the valueset only contains one codesystem. 
        /// If the valueset in this case contains more than one codesystem, the return value is empty.
        /// 
        /// If the valueset cannot be resolved as a uri to a value set, or the input is empty or has more than one value, the return value is empty.
        /// </summary>
        /// <param name="input">The input element for the function 'memberOf()'</param>
        /// <param name="valueset">The valueset</param>
        /// <param name="ctx">EvaluationContext of the FhirPath compiler</param>
        /// <returns>See summary</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static bool? MemberOf(PocoNode input, string valueset, EvaluationContext ctx)
        {
            var service = (ctx is FhirEvaluationContext fctx ? fctx.TerminologyService : null)
                ?? throw new ArgumentNullException(nameof(ctx), "The 'memberOf' function cannot be executed because the FhirEvaluationContext does not include a TerminologyService.");

            ValidateCodeParameters? inParams = new ValidateCodeParameters()
                        .WithValueSet(valueset);

            inParams = input switch
            {
                { Poco: Code code } => inParams.WithCode(code: code.Value, context: input.GetLocalLocation(), inferSystem: true),
                { Poco: Coding coding } => inParams.WithCoding(coding),
                { Poco: CodeableConcept cc } => inParams.WithCodeableConcept(cc),
                PrimitiveNode { Value: string s } => inParams.WithCode(code: s, context: "No context available", inferSystem: true),
                _ => null,
            };

            if (inParams is null)
            {
                // the memberOf function has an invalid input element
                return null;
            }

            try
            {
                var outParams = TaskHelper.Await(() => service.ValueSetValidateCode(inParams));
                return outParams.GetSingleValue<FhirBoolean>("result")?.Value ?? false;
            }
            catch (FhirOperationException)
            {
                // something happened in the service, like ValueSet was not found, or duplicate parameters, etc. Then return null (undefined)
                return null;
            }
        }
    }
}
#nullable restore