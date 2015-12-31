/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Support
{
    internal static class DateExtensions
    {
        public static DateTime? ToDateTime(this Model.FhirDateTime me)
        {
            if (me == null)
                return null;
            DateTime result;
            if (DateTime.TryParse(me.Value, out result))
                return result;
            if (!string.IsNullOrEmpty(me.Value))
            {
                // the date didn't parse, one of the common mistakes
                // with dates is not to include the - symbols
                // so lets put them in and proceed
                if (me.Value.Length == 8 && !me.Value.Contains("-"))
                {
                    string newValue = me.Value.Insert(4, "-").Insert(7, "-");
#if !PORTABLE45
                    System.Diagnostics.Trace.WriteLine(String.Format("Invalid Date [{0}] was encountered, processing it as though it was [{1}]", me.Value, newValue));
#endif
                    if (DateTime.TryParse(newValue, out result))
                        return result;
                }
            }
            return null;
        }

        public static DateTime? ToDateTime(this Model.Date me)
        {
            if (me == null)
                return null;
            DateTime result;
            if (DateTime.TryParse(me.Value, out result))
                return result;
            if (!string.IsNullOrEmpty(me.Value))
            {
                // the date didn't parse, one of the common mistakes
                // with dates is not to include the - symbols
                // so lets put them in and proceed
                if (me.Value.Length == 8 && !me.Value.Contains("-"))
                {
                    string newValue = me.Value.Insert(4, "-").Insert(7, "-");
#if !PORTABLE45
                    System.Diagnostics.Trace.WriteLine(String.Format("Invalid Date [{0}] was encountered, processing it as though it was [{1}]", me.Value, newValue));
#endif
                    if (DateTime.TryParse(newValue, out result))
                        return result;
                }
            }
            return null;
        }

        public static string ToFhirDate(this System.DateTime me)
        {
            return me.ToString("yyyy-MM-dd");
        }

        public static string ToFhirDate(this System.DateTime? me)
        {
            if (me.HasValue)
                return me.Value.ToString("yyyy-MM-dd");
            return null;
        }

        public static string ToFhirDateTime(this System.DateTime me)
        {
            return me.ToString("yyyy-MM-ddTHH:mm:ss.sss");
        }

        public static string ToFhirDateTime(this System.DateTime? me)
        {
            if (me.HasValue)
                return me.Value.ToString("yyyy-MM-ddTHH:mm:ss.sss");
            return null;
        }
    }
}
