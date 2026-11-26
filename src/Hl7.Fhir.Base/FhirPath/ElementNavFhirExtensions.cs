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
using System.Globalization;
using System.Linq;
using System.Text;
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

            t.Add("lowBoundary", (decimal d, long precision) => AdjustBoundaryDecimal(d, precision, substract), doNullProp: false);
            t.Add("lowBoundary", (decimal d) => AdjustBoundaryDecimal(d, null, substract), doNullProp: false);
            t.Add("lowBoundary", (int i, long precision) => i, doNullProp: false);
            t.Add("lowBoundary", (int i) => i, doNullProp: false);
            t.Add("lowBoundary", (P.Any a, long precision) => LowBoundary(a, precision), doNullProp: false);
            t.Add("lowBoundary", (P.Any a) => LowBoundary(a, null), doNullProp: false);

            t.Add("highBoundary", (decimal d, long precision) => AdjustBoundaryDecimal(d, precision, add), doNullProp: false);
            t.Add("highBoundary", (decimal d) => AdjustBoundaryDecimal(d, null, add), doNullProp: false);
            t.Add("highBoundary", (int i, long precision) => i, doNullProp: false);
            t.Add("highBoundary", (int i) => i, doNullProp: false);
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

        internal static decimal? AdjustBoundaryDecimal(decimal? input, long? precision, Func<decimal, decimal, decimal> op)
        {
            if (input is null) return null;

            var decimalParts = input.Value.ToString(CultureInfo.InvariantCulture).Split('.');

            // how many digits after the decimal point?
            var fractionalDigits = decimalParts.Length > 1 ? decimalParts.Last().Length : 0;

            decimal adjustment = 5 * (decimal)Math.Pow(10, -(fractionalDigits + 1)); // 0.5, 0.05, 0.005, etc

            input = op(input.Value, adjustment);

            if (precision is not null)
            {
                StringBuilder precisionAsString = new("0.");
                precisionAsString.Append('0', (int)precision);
                input += decimal.Parse(precisionAsString.ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
            }

            return input;
        }

        private static decimal substract(decimal operand1, decimal operand2) => operand1 - operand2;
        private static decimal add(decimal operand1, decimal operand2) => operand1 + operand2;

        internal static P.Any? LowBoundary(P.Any input, long? precision)
        {
            return input switch
            {
                P.Quantity q => AdjustBoundaryQuantity(q, precision, substract),
                P.Date d => BoundaryDateTime(d, precision ?? DATE_DEFAULT_BOUNDARY_PRECISION, high: false),
                P.DateTime dt => BoundaryDateTime(dt, precision, high: false),
                P.Time t => BoundaryTime(t, precision, 0, 0, 0),
                _ => null
            };
        }

        internal static P.Any? HighBoundary(P.Any input, long? precision)
        {
            return input switch
            {
                P.Quantity q => AdjustBoundaryQuantity(q, precision, add),
                P.Date d => BoundaryDateTime(d, precision ?? DATE_DEFAULT_BOUNDARY_PRECISION, high: true),
                P.DateTime dt => BoundaryDateTime(dt, precision, high: true),
                P.Time t => BoundaryTime(t, precision, 59, 59, 999),
                _ => null
            };
        }

        internal static P.Quantity AdjustBoundaryQuantity(P.Quantity input, long? precision, Func<decimal, decimal, decimal> op)
        {
            var value = AdjustBoundaryDecimal(input.Value, precision, op);

            return (value is null)
                ? throw new ArgumentException($"Invalid input element: {input}")
                : new P.Quantity(value.Value, input.Unit, input.System);
        }

        // Without a precision, the boundary of a date is a date (YYYYMMDD); that of a dateTime has milliseconds.
        private const long DATE_DEFAULT_BOUNDARY_PRECISION = 8;

        // A dateTime without a timezone could be in any timezone, so its boundaries are taken at the
        // extremes: the earliest moment is in the most eastern zone (+14:00), the latest in the most western one (-12:00).
        private static readonly TimeSpan LOW_BOUNDARY_OFFSET = TimeSpan.FromHours(14);
        private static readonly TimeSpan HIGH_BOUNDARY_OFFSET = TimeSpan.FromHours(-12);

        internal static P.Any BoundaryDateTime(P.DateTime dt, long? precision, bool high)
        {
            P.DateTimePrecision dtPrecision = precision switch
            {
                4 => P.DateTimePrecision.Year,
                6 => P.DateTimePrecision.Month,
                8 => P.DateTimePrecision.Day,
                14 => P.DateTimePrecision.Second,
                >= 17 or null => P.DateTimePrecision.Fraction,
                _ => throw new ArgumentException($"Unsupported DateTime precision for boundary operation: {precision}")
            };

            // An input timezone written as Z is kept as Z.
            var zulu = dt.HasOffset && dt.ToString().EndsWith("Z", StringComparison.Ordinal);

            // A dateTime with only an hour is not valid in FHIR, which requires at least minutes, so it is read as
            // hh:00 (e.g. @2014-01-01T08 is 08:00) and its boundaries are those of that minute.
            if (dt.Precision == P.DateTimePrecision.Hour)
                dt = P.DateTime.FromDateTimeOffset(dt.ToDateTimeOffset(TimeSpan.Zero), P.DateTimePrecision.Minute, dt.HasOffset);

            // The boundaries are the first and the last instant of the range the value covers. A value without a timezone
            // could be in any timezone, so its range is taken at the extremes: the earliest moment is in the most eastern
            // zone (+14:00), the latest in the most western one (-12:00). A date result carries no offset, so the zone
            // does not matter there. The range keeps the input's own offset, and its fraction digits: the high boundary
            // of .5 is .599.
            var assumedOffset = dtPrecision <= P.DateTimePrecision.Day ? TimeSpan.Zero
                : high ? HIGH_BOUNDARY_OFFSET : LOW_BOUNDARY_OFFSET;
            var range = dt.GetRange(assumedOffset);

            // A bound beyond what DateTimeOffset can represent (e.g. the end of 9999-12-31T13:00 at -12:00) leaves that
            // side of the range unbounded. The boundary is then the latest/earliest instant the input's offset can express
            // (its wall clock cannot pass 9999-12-31T23:59:59.9999999 or precede 0001-01-01T00:00), or, without an
            // offset, the latest/earliest representable instant in UTC.
            var instant = high ? range.LastInstant ?? latest() : range.Start ?? earliest();

            if (dtPrecision <= P.DateTimePrecision.Day)
                return P.Date.FromDateTimeOffset(instant, dtPrecision, includeOffset: false);

            // A boundary with a time part always has a timezone, and is written with exactly three fraction digits (e.g. .000)
            // so that it spans exactly one millisecond; finer digits are truncated. Parsing the formatted value keeps both
            // the text and that span.
            var format = dtPrecision == P.DateTimePrecision.Fraction ? "yyyy-MM-dd'T'HH:mm:ss.fff" : "yyyy-MM-dd'T'HH:mm:ss";
            var zone = zulu ? "Z" : instant.ToString("zzz", CultureInfo.InvariantCulture);
            return P.DateTime.Parse(instant.ToString(format, CultureInfo.InvariantCulture) + zone);

            DateTimeOffset latest() =>
                !dt.HasOffset ? DateTimeOffset.MaxValue
                : dt.Offset!.Value >= TimeSpan.Zero ? new DateTimeOffset(System.DateTime.MaxValue, dt.Offset.Value)
                : DateTimeOffset.MaxValue.ToOffset(dt.Offset.Value);

            DateTimeOffset earliest() =>
                !dt.HasOffset ? DateTimeOffset.MinValue
                : dt.Offset!.Value <= TimeSpan.Zero ? new DateTimeOffset(System.DateTime.MinValue, dt.Offset.Value)
                : DateTimeOffset.MinValue.ToOffset(dt.Offset.Value);
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

            // A millisecond boundary is written with exactly three fraction digits (e.g. .000), so it spans exactly one millisecond.
            // As before, the boundary of a time never carries an offset.
            return dtPrecision == P.DateTimePrecision.Fraction
                ? P.Time.Parse(dto.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture))
                : P.Time.FromDateTimeOffset(dto, dtPrecision);
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