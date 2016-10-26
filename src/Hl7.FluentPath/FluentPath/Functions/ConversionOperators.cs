/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Xml;
using Hl7.ElementModel;
using Furore.Support;

namespace Hl7.FluentPath.Functions
{
    public static class ConversionOperators
    {
        private static T getValue<T>(this IValueProvider val, string name)
        {
            if (val == null) throw Error.ArgumentNull(name);
            if (val.Value == null) throw Error.ArgumentNull(name + ".Value");
            if (!(val.Value is T)) throw Error.Argument(name + " must be of type " + typeof(T).Name);

            return (T)val.Value;
        }

        // FluentPath toInteger() function
        public static long? ToInteger(this IValueProvider focus)
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

        // FluentPath toDecimal() function
        public static decimal? ToDecimal(this IValueProvider focus)
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


        // FluentPath toString() function
        public static string ToStringRepresentation(this IValueProvider focus)
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
            else if (val is Time)
                return ((Time)val).ToString();
            else if (val is PartialDateTime)
                return ((PartialDateTime)val).ToString();

            return null;
        }
    }
}
