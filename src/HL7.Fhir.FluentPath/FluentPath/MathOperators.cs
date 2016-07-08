using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Binding;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.FluentPath.FluentPath
{
    public static class MathOperators
    {
        public static object DynaMul(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            try
            {
                var l = ParamBinding.CastToSingleValue<long>(left);
                var r = ParamBinding.CastToSingleValue<long>(right);
                return l * r;
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<decimal>(left);
                var r = ParamBinding.CastToSingleValue<decimal>(right);
                return l * r;
            }
            catch { }

            throw Error.Argument("Can only multiply values of type integer or decimal");
        }

        public static object DynaDiv(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            try
            {
                var l = ParamBinding.CastToSingleValue<long>(left);
                var r = ParamBinding.CastToSingleValue<long>(right);
                return l / r;
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<decimal>(left);
                var r = ParamBinding.CastToSingleValue<decimal>(right);
                return l / r;
            }
            catch( Exception e) { throw e; }

            throw Error.Argument("Can only divide values of type integer or decimal");
        }

        public static object DynaAdd(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            try
            {
                var l = ParamBinding.CastToSingleValue<long>(left);
                var r = ParamBinding.CastToSingleValue<long>(right);
                return l + r;
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<decimal>(left);
                var r = ParamBinding.CastToSingleValue<decimal>(right);
                return l + r;
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<string>(left);
                var r = ParamBinding.CastToSingleValue<string>(right);
                return l + r;
            }
            catch { }

            throw Error.Argument("Can only add values of type string, integer or decimal");
        }

        public static object DynaMod(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            try
            {
                var l = ParamBinding.CastToSingleValue<long>(left);
                var r = ParamBinding.CastToSingleValue<long>(right);
                return l % r;
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<decimal>(left);
                var r = ParamBinding.CastToSingleValue<decimal>(right);
                return l % r;
            }
            catch { }

            throw Error.Argument("Can only subtract values of type integer or decimal");
        }


        public static object DynaSub(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            try
            {
                var l = ParamBinding.CastToSingleValue<long>(left);
                var r = ParamBinding.CastToSingleValue<long>(right);
                return l - r;
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<decimal>(left);
                var r = ParamBinding.CastToSingleValue<decimal>(right);
                return l - r;
            }
            catch { }

            throw Error.Argument("Can only subtract values of type integer or decimal");
        }


        public static object DynaTruncDiv(this IEnumerable<IValueProvider> left, IEnumerable<IValueProvider> right)
        {
            try
            {
                var l = ParamBinding.CastToSingleValue<long>(left);
                var r = ParamBinding.CastToSingleValue<long>(right);
                return (long)Math.Truncate((decimal)l - r);
            }
            catch { }

            try
            {
                var l = ParamBinding.CastToSingleValue<decimal>(left);
                var r = ParamBinding.CastToSingleValue<decimal>(right);
                return Math.Truncate((decimal)l - r);
            }
            catch { }

            throw Error.Argument("Can only subtract values of type integer or decimal");
        }

    }
}
