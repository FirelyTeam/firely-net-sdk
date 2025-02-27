/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using P = Hl7.Fhir.ElementModel.Types;
using System.Xml;
using Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.FhirPath.Functions
{
    internal static class ConversionOperators
    {
        /// <summary>
        /// FhirPath toBoolean() function
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool? ToBoolean(this Any focus) => 
            focus.TryConvertTo<Boolean>(out var result) ? result.Value : null;

        /// <summary>
        /// FhirPath convertsToBoolean() function
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToBoolean(this Any focus) => ToBoolean(focus) is not null;


        /// <summary>
        /// FhirPath toInteger() function
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static int? ToInteger(this Any focus) => focus.TryConvertTo<Integer>(out var result) ? result.Value : null;

        /// <summary>
        /// FhirPath convertsToInteger() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToInteger(this Any focus) => ToInteger(focus) is not null;


        /// <summary>
        /// FhirPath toDecimal() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this Any focus) =>
            focus.TryConvertTo<Decimal>(out var result) ? result.Value : null;

        /// <summary>
        /// FhirPath convertsToDecimal() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToDecimal(this Any focus) => ToDecimal(focus) is not null;


        /// <summary>
        /// FhirPath toLong() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static long? ToLong(this Any focus) => focus.TryConvertTo<Long>(out var result) ? result.Value : null;


        /// <summary>
        /// FhirPath convertsToLong() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToLong(this Any focus) => ToLong(focus) is not null;


        /// <summary>
        /// FhirPath toQuantity() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static P.Quantity? ToQuantity(this Any focus) => focus.TryConvertTo<Quantity>(out var result) ? result : null;

        /// <summary>
        /// FhirPath convertsToQuantity() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToQuantity(this Any focus) => ToQuantity(focus) is not null;


        /// <summary>
        /// FhirPath toString() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static string? ToStringRepresentation(this Any focus) =>
            focus.TryConvertTo<String>(out var result) ? result.Value : null;

        /// <summary>
        /// FhirPath convertsToString() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToString(this Any focus) => ToStringRepresentation(focus) is not null;

        /// <summary>
        /// FhirPath toDate() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static P.Date? ToDate(this Any focus) => focus.TryConvertTo<Date>(out var result) ? result : null;


        /// <summary>
        /// FhirPath convertsToDate() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToDate(this Any focus) => ToDate(focus) is not null;


        /// <summary>
        /// FhirPath toDateTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static P.DateTime? ToDateTime(this Any focus) => focus.TryConvertTo<DateTime>(out var result) ? result : null;


        /// <summary>
        /// FhirPath convertsToDateTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToDateTime(this Any focus) => ToDateTime(focus) is not null;

        /// <summary>
        /// FhirPath toTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static P.Time? ToTime(this Any focus) => focus.TryConvertTo<Time>(out var result) ? result : null;

        /// <summary>
        /// FhirPath convertsToTime() function.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool ConvertsToTime(this Any focus) => ToTime(focus) is not null;
    }
}