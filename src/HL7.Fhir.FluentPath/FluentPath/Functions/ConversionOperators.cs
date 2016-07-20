using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hl7.Fhir.FluentPath.Functions
{
    internal static class ConversionOperators
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

            return null;
        }
    }
}
