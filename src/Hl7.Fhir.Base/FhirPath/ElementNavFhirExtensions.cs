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
            t.Add("hasValue", (ITypedElement f) => f.HasValue(), doNullProp: false);
            t.Add("resolve", (ITypedElement f, EvaluationContext ctx) => resolver(f, ctx), doNullProp: false);

            t.Add("memberOf", (ITypedElement input, string valueset, EvaluationContext ctx) => MemberOf(input, valueset, ctx), doNullProp: false);

            // Pre-normative this function was called htmlchecks, normative is htmlChecks
            // lets keep both to keep everyone happy.
            t.Add("htmlchecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlChecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);

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

            static ITypedElement? resolver(ITypedElement f, EvaluationContext ctx)
            {
                return ctx is FhirEvaluationContext fctx ? f.Resolve(fctx.ElementResolver) : f.Resolve();
            }
        }


        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HasValue(this ITypedElement focus) => focus?.Value is not null;

        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HtmlChecks(this ITypedElement focus)
        {
            if (focus?.Value is null) return false;

            // Perform the checking of the content for valid html content
            return XHtml.IsValidNarrativeXhtml(focus.Value.ToString()!);
        }

        public static IEnumerable<Base?> ToFhirValues(this IEnumerable<ITypedElement> results)
        {
            return results.Select(r =>
            {
                if (r is null)
                    return null;

                var fhirValue = r.Annotation<IFhirValueProvider>();
                if (fhirValue != null)
                {
                    return fhirValue.FhirValue;
                }

                object result = r.Value;

                return result switch
                {
                    bool b => new FhirBoolean(b),
                    long l => new Integer64(l),
                    int i => new Integer(i),
                    decimal dec => new FhirDecimal(dec),
                    string s => new FhirString(s),
                    P.Date d => new Date(d.ToString()),
                    P.Time t => new Time(t.ToString()),
                    P.DateTime dt => new FhirDateTime(dt.ToDateTimeOffset(TimeSpan.Zero).ToUniversalTime()),
                    _ => (Base)result
                };
            });
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
                P.Quantity q => AdjustBoundaryQuantity(q, precision, add),
                P.Date d => BoundaryDateTime(d, precision, 12, 31, 23, 59, 59, 999),
                P.DateTime dt => BoundaryDateTime(dt, precision, 12, 31, 23, 59, 59, 999),
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
                    new P.DateTime(dto.ToString(), dto, dtPrecision, dt.HasOffset);
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
        internal static bool Comparable(P.Quantity l, P.Quantity r) => l.TryCompareTo(r).Success;

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
        internal static bool? MemberOf(ITypedElement input, string valueset, EvaluationContext ctx)
        {
            var service = (ctx is FhirEvaluationContext fctx ? fctx.TerminologyService : null)
                ?? throw new ArgumentNullException("The 'memberOf' function cannot be executed because the FhirEvaluationContext does not include a TerminologyService.");

            ValidateCodeParameters? inParams = new ValidateCodeParameters()
                        .WithValueSet(valueset);

            inParams = input.InstanceType switch
            {
                "code" when input is ScopedNode sn => inParams.WithCode(code: sn.Value as string, context: sn.LocalLocation),
                "Coding" => inParams.WithCoding(input.ParseCoding()),
                "CodeableConcept" => inParams.WithCodeableConcept(input.ParseCodeableConcept()),
                "System.String" => inParams.WithCode(code: input.Value as string, context: "No context available"),
                _ => null,
            };

            if (inParams is null)
            {
                // the memberOf function has an invalid input element
                return null;
            }

            try
            {
                var outParams = TaskHelper.Await(() => service.ValueSetValidateCode(inParams.Build()));
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