using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.FluentPath.Binding
{
    internal static class Typecasts
    {
        public delegate object Cast(object source);

        private static object id(object source)
        {
            return source;
        }

        //private static object any2bool(object source)
        //{
        //    if (source == null) return false;

        //    if (source is IEnumerable<IValueProvider>)
        //    {
        //        var list = (IEnumerable<IValueProvider>)source;
        //        if (!list.Any()) return false;

        //        if (list.Count() == 1)
        //            source = list.Single();
        //    }

        //    if (source is IValueProvider)
        //    {
        //        var vp = (IValueProvider)source;
        //        if (vp.Value is bool)
        //            return (bool)vp.Value;
        //    }

        //    // Otherwise, we have "some" content, which we'll consider "true"
        //    return true;
        //}

        private static Cast makeNativeCast(Type to)
        {
            return source =>
                Convert.ChangeType(source, to);
        }

        private static object any2ValueProvider(object source)
        {
            return new ConstantValue(source);
        }

        private static object any2List(object source)
        {
            return FhirValueList.Create(source);
        }

        private static Cast getImplicitCast(Type from, Type to)
        {
            if (to == typeof(object)) return id;
            if (to.IsAssignableFrom(from)) return id;
            //if (to == typeof(bool)) return any2bool;
            if (to == typeof(IValueProvider) && (!typeof(IEnumerable<IValueProvider>).IsAssignableFrom(from))) return any2ValueProvider;
            if (to == typeof(IEnumerable<IValueProvider>)) return any2List;
             
            if (from == typeof(long) && (to == typeof(decimal) || to == typeof(decimal?))) return makeNativeCast(typeof(decimal));
            if (from == typeof(long?) && to == typeof(decimal?)) return makeNativeCast(typeof(decimal?));
            return null;
        }

        internal static object Unbox(object instance)
        {
            if (instance == null) return null;

            if (instance is IEnumerable<IValueProvider>)
            {
                var list = (IEnumerable<IValueProvider>)instance;
                if (!list.Any()) return null;
                if (list.Count() == 1)
                    instance = list.Single();
            }

            if(instance is IValueProvider)
            {
                var element = (IValueProvider)instance;

                if (element.Value != null)
                    instance = element.Value;
            }

            return instance;
        }

        public static bool CanCastTo(object source, Type to)
        {
            if (source == null)
                return to.IsNullable();

            var from = Unbox(source);
            if (from == null)
                return to.IsNullable();

            return getImplicitCast(from.GetType(),to) != null;
        }


        public static T CastTo<T>(object source)
        {
            return (T)CastTo(source, typeof(T));
        }


        public static object CastTo(object source, Type to)
        {
            if (source != null)
            {
                if (to.IsAssignableFrom(source.GetType())) return source;  // for efficiency

                source = Unbox(source);

                if (source != null)
                {
                    Cast cast = getImplicitCast(source.GetType(), to);

                    if (cast == null)
                    {
                        //TODO: Spell out why a little bit more explicit than...
                        throw new InvalidCastException("Cannot cast from '{0}' to '{1}'".FormatWith(source.GetType().Name, to.Name));
                    }

                    return cast(source);
                }
            }

            //if source == null, or unboxed source == null....
            if (to == typeof(IEnumerable<IValueProvider>))
                return FhirValueList.Empty;
            if (to.IsNullable())
                return null;
            else
                throw new InvalidCastException("Cannot cast a null value to non-nullable type '{0}'".FormatWith(to.Name));
        }                  

        public static bool IsNullable(this Type t)
        {
           if (!t.IsValueType) return true; // ref-type
           if (Nullable.GetUnderlyingType(t) != null) return true; // Nullable<T>
           return false; // value-type
        }
    }
}
