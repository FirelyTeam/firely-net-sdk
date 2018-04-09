/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Support
{
    public static class DateExtensions
    {
        public static string ToFhirDate(this System.DateTime me) => me.ToString("yyyy-MM-dd");

        public static string ToFhirDate(this System.DateTime? me) => me.HasValue ? me.Value.ToString("yyyy-MM-dd") : null;

        public static string ToFhirDateTime(this System.DateTime me) => PrimitiveTypeConverter.ConvertTo<string>(me);

        public static string ToFhirDateTime(this System.DateTime? me) => me.HasValue ? PrimitiveTypeConverter.ConvertTo<string>(me) : null;
    }
}
