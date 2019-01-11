/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;
using System.Xml;

namespace Hl7.FhirPath.Functions
{
    internal static class ConversionOperators
    {
        /// <summary>
        /// FhirPath toBoolean() function
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool? ToBoolean(this ITypedElement focus)
        {
            var val = focus?.Value;
            if (val == null) return null;

            switch (val)
            {
                case bool b:
                    return b;
                case string s:
                    return convertString(s);
                case long l:
                    return l == 1 ? true :
                        l == 0 ? false : (bool?)null;
                case decimal d:
                    return d == 1.0m ? true :
                        d == 0.0m ? false : (bool?)null;
                default:
                    return null;
            }

            bool? convertString(string si)
            {
                switch (si.ToLower())
                {
                    case "true":
                    case "t":
                    case "yes":
                    case "y":
                    case "1":
                    case "1.0":
                        return true;
                    case "false":
                    case "f":
                    case "no":
                    case "n":
                    case "0":
                    case "0.0":
                        return false;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// FhirPath convertsToBoolean() function
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToBoolean(this ITypedElement focus) => ToBoolean(focus) != null;


        /// <summary>
        /// FhirPath toInteger() function
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static long? ToInteger(this ITypedElement focus)
        {
            var val = focus?.Value;
            if (val == null) return null;

            switch (val)
            {
                case long l:
                    return l;
                case string s:
                    return convertString(s);
                case bool b:
                    return b ? 1L : 0L;
                default:
                    return null;
            }

            long? convertString(string si)
            {
                try
                {
                    return XmlConvert.ToInt64(si);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// FhirPath convertsToInteger() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToInteger(this ITypedElement focus) => ToInteger(focus) != null;


        /// <summary>
        /// FhirPath toDecimal() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this ITypedElement focus)
        {
            var val = focus?.Value;
            if (val == null) return null;

            switch (val)
            {
                case decimal d:
                    return d;
                case long l:
                    return l;
                case string s:
                    return convertString(s);
                case bool b:
                    return b ? 1m : 0m;
                default:
                    return null;
            }

            decimal? convertString(string si)
            {
                try
                {
                    return XmlConvert.ToDecimal(si);
                }
                catch
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// FhirPath convertsToDecimal() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToDecimal(this ITypedElement focus) => ToDecimal(focus) != null;


        /// <summary>
        /// FhirPath toDateTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static PartialDateTime? ToDateTime(this ITypedElement focus)
        {
            var val = focus?.Value;
            if (val == null) return null;

            switch (val)
            {
                case PartialDateTime pdt:
                    return pdt;
                case string s:
                    return convertString(s);
                default:
                    return null;
            }

            PartialDateTime? convertString(string si) =>
                   PartialDateTime.TryParse(si, out var result) ?
                        result : (PartialDateTime?)null;
        }


        /// <summary>
        /// FhirPath convertsToDateTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToDateTime(this ITypedElement focus) => ToDateTime(focus) != null;


        /// <summary>
        /// FhirPath toTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static PartialTime? ToTime(this ITypedElement focus)
        {
            var val = focus?.Value;
            if (val == null) return null;

            switch (val)
            {
                case PartialTime pt:
                    return pt;
                case string s:
                    return convertString(s);
                default:
                    return null;
            }

            PartialTime? convertString(string si)
            {
                // Inconsistenty, the format for a time requires the 'T' prefix, while
                // convertsToDateTime() does not expect a '@'.
                if (!si.StartsWith("T")) return null;

                return PartialTime.TryParse(si.Substring(1), out var result) ?
                     result : (PartialTime?)null;
            }
        }


        /// <summary>
        /// FhirPath convertsToTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToTime(this ITypedElement focus) => ToTime(focus) != null;


        /// <summary>
        /// FhirPath toString() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static string ToStringRepresentation(this ITypedElement focus)
        {
            var val = focus?.Value;
            if (val == null) return null;

            switch (val)
            {
                case string s:
                    return s;
                case long l:
                    return XmlConvert.ToString(l);
                case decimal d:
                    return XmlConvert.ToString(d);
                case PartialDateTime pdt:
                    return pdt.ToString();
                case PartialTime pt:
                    return "T" + pt.ToString();   // again, this inconsistency.
                case bool b:
                    return b ? "true" : "false";
                case Quantity q:
                    throw Error.NotImplemented("Quantities are not yet supported");
                default:
                    return null;
            }
        }

        /// <summary>
        /// FhirPath convertsToString() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToString(this ITypedElement focus) => ToStringRepresentation(focus) != null;
    }
}
