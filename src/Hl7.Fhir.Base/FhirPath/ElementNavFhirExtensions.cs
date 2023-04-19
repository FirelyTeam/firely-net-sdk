using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
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

            t.Add("memberOf", (Func<object, string, bool>)memberOf, doNullProp: false);

            // Pre-normative this function was called htmlchecks, normative is htmlChecks
            // lets keep both to keep everyone happy.
            t.Add("htmlchecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlChecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);

            t.Add("lowBoundary", (decimal d, long precision) => AdjustBoundaryDecimal(d, precision, substract), doNullProp: false);
            t.Add("lowBoundary", (decimal d) => AdjustBoundaryDecimal(d, null, substract), doNullProp: false);
            t.Add("lowBoundary", (P.Any a, long precision) => LowBoundary(a, precision), doNullProp: false);
            t.Add("lowBoundary", (P.Any a) => LowBoundary(a, null), doNullProp: false);

            t.Add("highBoundary", (decimal d, long precision) => AdjustBoundaryDecimal(d, precision, add), doNullProp: false);
            t.Add("highBoundary", (decimal d) => AdjustBoundaryDecimal(d, null, add), doNullProp: false);
            t.Add("highBoundary", (P.Any a, long precision) => HighBoundary(a, precision), doNullProp: false);
            t.Add("highBoundary", (P.Any a) => HighBoundary(a, null), doNullProp: false);

            return t;

            static ITypedElement resolver(ITypedElement f, EvaluationContext ctx)
            {
                return ctx is FhirEvaluationContext fctx ? f.Resolve(fctx.ElementResolver) : f.Resolve();
            }

            static bool memberOf(object focus, string valueset) => throw new NotImplementedException("Terminology functions in FhirPath are unsupported in the .NET FhirPath engine.");
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
            return XHtml.IsValidNarrativeXhtml(focus.Value.ToString());
        }

        public static IEnumerable<Base> ToFhirValues(this IEnumerable<ITypedElement> results)
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

        internal static P.Any LowBoundary(P.Any input, long? precision)
        {
            return input switch
            {
                P.Quantity q => AdjustBoundaryQuantity(q, precision, substract),
                P.Date d => LowBoundaryDateTime(d, precision),
                P.DateTime dt => LowBoundaryDateTime(dt, precision),
                P.Time t => LowBoundaryTime(t, precision),
                _ => null
            };
        }

        internal static P.Any HighBoundary(P.Any input, long? precision)
        {
            return input switch
            {
                P.Quantity q => AdjustBoundaryQuantity(q, precision, add),
                P.Date d => HighBoundaryDateTime(d, precision),
                P.DateTime dt => HighBoundaryDateTime(dt, precision),
                P.Time t => HighBoundaryTime(t, precision),
                _ => null
            };
        }

        internal static P.Quantity AdjustBoundaryQuantity(P.Quantity input, long? precision, Func<decimal, decimal, decimal> op)
        {
            var v = AdjustBoundaryDecimal(input.Value, precision, op);

            return new P.Quantity(v.Value, input.Unit, input.System);
        }

        private static P.DateTimePrecision getNext(P.DateTimePrecision value)
        {
            return (from P.DateTimePrecision val in Enum.GetValues(typeof(P.DateTimePrecision))
                    where val > value
                    orderby val
                    select val).DefaultIfEmpty().First();
        }

        internal static P.DateTime LowBoundaryDateTime(P.DateTime dateTime, long? precision)
        {
            P.DateTimePrecision dtPrecision = precision switch
            {
                <= 6 => P.DateTimePrecision.Month,
                <= 8 => P.DateTimePrecision.Day,
                <= 10 => P.DateTimePrecision.Hour,
                <= 12 => P.DateTimePrecision.Minute,
                <= 14 => P.DateTimePrecision.Second,
                <= 17 => P.DateTimePrecision.Fraction,

                null => getNext(dateTime.Precision),
                _ => getNext(dateTime.Precision),
            };


            return new(dateTime.ToString(), dateTime.ToDateTimeOffset(TimeSpan.Zero), dtPrecision, dateTime.HasOffset);
        }

        internal static P.Time LowBoundaryTime(P.Time time, long? precision)
        {
            P.DateTimePrecision dtPrecision = precision switch
            {
                2 => P.DateTimePrecision.Hour,
                5 => P.DateTimePrecision.Minute,
                8 => P.DateTimePrecision.Second,
                12 => P.DateTimePrecision.Fraction,

                null => getNext(time.Precision),
                _ => getNext(time.Precision),
            };

            return P.Time.FromDateTimeOffset(time.ToDateTimeOffset(2023, 04, 18, TimeSpan.Zero), dtPrecision);
        }

        internal static P.Any HighBoundaryDateTime(P.DateTime dt, long? precision)
        {
            TimeSpan offset = dt.HasOffset ? dt.Offset.Value : TimeSpan.Zero;

            DateTimeOffset dto = dt.Precision switch
            {
                P.DateTimePrecision.Year => new(dt.Years.Value, 12, 31, 23, 59, 59, 999, offset),
                P.DateTimePrecision.Month => new(dt.Years.Value, dt.Months.Value, DateTime.DaysInMonth(dt.Years.Value, dt.Months.Value), 23, 59, 59, 999, offset),
                P.DateTimePrecision.Day => new(dt.Years.Value, dt.Months.Value, dt.Days.Value, 23, 59, 59, 999, offset),
                P.DateTimePrecision.Hour => new(dt.Years.Value, dt.Months.Value, dt.Days.Value, dt.Hours.Value, 59, 59, 999, offset),
                P.DateTimePrecision.Minute => new(dt.Years.Value, dt.Months.Value, dt.Days.Value, dt.Hours.Value, dt.Minutes.Value, 59, 999, offset),
                P.DateTimePrecision.Second => new(dt.Years.Value, dt.Months.Value, dt.Days.Value, dt.Hours.Value, dt.Minutes.Value, dt.Seconds.Value, 999, offset),
                _ => DateTimeOffset.Now
            };

            P.DateTimePrecision dtPrecision = precision switch
            {
                4 => P.DateTimePrecision.Year,
                6 => P.DateTimePrecision.Month,
                8 => P.DateTimePrecision.Day,
                14 => P.DateTimePrecision.Second,
                17 or null => P.DateTimePrecision.Fraction,
                _ => throw new Exception("")
            };

            return
                (dtPrecision <= P.DateTimePrecision.Day) ?
                    P.Date.FromDateTimeOffset(dto, dtPrecision, dt.HasOffset) :
                    new P.DateTime(dto.ToString(), dto, dtPrecision, dt.HasOffset);
        }

        internal static P.Time HighBoundaryTime(P.Time time, long? precision)
        {
            TimeSpan offset = time.HasOffset ? time.Offset.Value : TimeSpan.Zero;

            DateTimeOffset dto = time.Precision switch
            {
                P.DateTimePrecision.Hour => new(2023, 4, 18, time.Hours.Value, 59, 59, 9999, offset),
                P.DateTimePrecision.Minute => new(2023, 4, 18, time.Hours.Value, time.Minutes.Value, 59, 999, offset),
                P.DateTimePrecision.Second => new(2023, 4, 18, time.Hours.Value, time.Minutes.Value, time.Seconds.Value, 999, offset),
                _ => DateTimeOffset.Now
            };

            P.DateTimePrecision dtPrecision = precision switch
            {
                2 => P.DateTimePrecision.Hour,
                4 => P.DateTimePrecision.Minute,
                6 => P.DateTimePrecision.Second,
                9 or null => P.DateTimePrecision.Fraction,
                _ => throw new Exception("")
            };

            return P.Time.FromDateTimeOffset(dto, dtPrecision);
        }
    }
}