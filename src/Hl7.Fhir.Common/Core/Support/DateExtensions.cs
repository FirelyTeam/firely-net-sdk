/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using System;

namespace Hl7.Fhir.Support
{
    public static class DateExtensions
    {
        /// <summary>
        /// This function will return only the date part (as a string) of the Datetime parameter. 
        /// Be aware: this function will not bother about the timezone. 21-11-2018 23:00 in Amsterdam is not the same as in Austrilia: the date 
        /// will be there already 22-11-2018.
        /// </summary>
        /// <param name="me">a Date time</param>
        /// <returns>The date part of the datetime as a string</returns>
        public static string ToFhirDate(this System.DateTime me) => me.ToString("yyyy-MM-dd");

        /// <summary>
        /// This function will return only the date part (as a string) of the Datetime parameter. 
        /// Be aware: this function will not bother about the timezone. 21-11-2018 23:00 in Amsterdam is not the same as in Austrilia: the date 
        /// will be there already 22-11-2018.
        /// </summary>
        /// <param name="me">a Date time</param>
        /// <returns>if me is empty then return null otherwie return the date part of the datetime as a string</returns>
        public static string ToFhirDate(this System.DateTime? me) => me.HasValue ? me.Value.ToString("yyyy-MM-dd") : null;

        /// <summary>
        /// Returns a FHIR datetime as a string
        /// </summary>
        /// <param name="me">A date and time.</param>
        /// <param name="offset">The time's offset from Coordinated Universal Time (UTC).</param>
        /// <returns>Returns a FHIR datetime as a string</returns>
        public static string ToFhirDateTime(this System.DateTime me, TimeSpan offset) => ToFhirDateTime(new DateTimeOffset(me, offset));

        public static string ToFhirDate(this System.DateTimeOffset me) => me.ToString("yyyy-MM-dd");

        public static string ToFhirDate(this System.DateTimeOffset? me) => me.HasValue ? me.Value.ToString("yyyy-MM-dd") : null;

        public static string ToFhirDateTime(this System.DateTimeOffset me) => PrimitiveTypeConverter.ConvertTo<string>(me);

        public static string ToFhirDateTime(this System.DateTimeOffset? me) => me.HasValue ? PrimitiveTypeConverter.ConvertTo<string>(me) : null;

        #region Obsolete members
        [Obsolete("Use ToFhirDateTime(this System.DateTimeOffset me) instead")]
        public static string ToFhirDateTime(this System.DateTime me) => PrimitiveTypeConverter.ConvertTo<string>(me);

        [Obsolete("Use ToFhirDateTime(this System.DateTimeOffset? me) instead")]
        public static string ToFhirDateTime(this System.DateTime? me) => me.HasValue ? PrimitiveTypeConverter.ConvertTo<string>(me) : null;
        #endregion

    }
}
