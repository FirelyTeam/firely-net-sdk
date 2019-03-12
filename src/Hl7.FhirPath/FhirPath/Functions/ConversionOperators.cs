/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System.Xml;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class ConversionOperators
    {
        private static T getValue<T>(this ITypedElement val, string name)
        {
            if (val == null) throw Error.ArgumentNull(name);
            if (val.Value == null) throw Error.ArgumentNull(name + ".Value");
            if (!(val.Value is T)) throw Error.Argument(name + " must be of type " + typeof(T).Name);

            return (T)val.Value;
        }

        // FhirPath toInteger() function
        public static long? ToInteger(this ITypedElement focus)
        {
            var val = focus.getValue<object>("focus");

            if (val is long)
                return (long)val;
            else if (val is string)
            {
                try
                {
                    return XmlConvert.ToInt64((string)val);
                }
                catch
                {
                    return null;
                }
            }
            else if(val is bool)
            {
                return (bool)val ? 1L : 0L;
            }

            return null;
        }

        // FhirPath toDecimal() function
        public static decimal? ToDecimal(this ITypedElement focus)
        {
            var val = focus.getValue<object>("focus");

            if (val is decimal)
                return (decimal)val;
            else if (val is string)
            {
                try
                {
                    return XmlConvert.ToDecimal((string)val);
                }
                catch
                {
                    return null;
                }
            }
            else if (val is bool)
            {
                return (bool)val ? 1m : 0m;
            }

            return null;
        }


        // FhirPath toString() function
        public static string ToStringRepresentation(this ITypedElement focus)
        {
            var val = focus.getValue<object>("focus");

            if (val is string)
                return (string)val;
            else if (val is long)
                return XmlConvert.ToString((long)val);
            else if (val is decimal)
                return XmlConvert.ToString((decimal)val);
            else if (val is bool)
                return (bool)val ? "true" : "false";
            else if (val is PartialTime)
                return ((PartialTime)val).ToString();
            else if (val is PartialDateTime)
                return ((PartialDateTime)val).ToString();

            return null;
        }
    }
}
