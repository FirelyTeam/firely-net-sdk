/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Expressions
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

            //if (to.IsAssignableFrom(from)) return id;
            if (from.CanBeTreatedAsType(to)) return id;

            //if (to == typeof(bool)) return any2bool;
            if (to == typeof(ITypedElement) && (!from.CanBeTreatedAsType(typeof(IEnumerable<ITypedElement>)))) return any2ValueProvider;
            if (to == typeof(IEnumerable<ITypedElement>)) return any2List;
             
            if (from == typeof(long) && (to == typeof(decimal) || to == typeof(decimal?))) return makeNativeCast(typeof(decimal));
            if (from == typeof(long?) && to == typeof(decimal?)) return makeNativeCast(typeof(decimal?));
            return null;
        }

        internal static object Unbox(object instance, Type to)
        {
            if (instance == null) return null;

            if (to.CanBeTreatedAsType(typeof(IEnumerable<ITypedElement>))) return instance;

            if (instance is IEnumerable<ITypedElement>)
            {
                var list = (IEnumerable<ITypedElement>)instance;
                if (!list.Any()) return null;
                if (list.Count() == 1)
                    instance = list.Single();
            }

            if (to.CanBeTreatedAsType(typeof(ITypedElement))) return instance;

            if (instance is ITypedElement)
            {
                var element = (ITypedElement)instance;

                if (element.Value != null)
                    instance = element.Value;
            }

            return instance;
        }

        public static bool CanCastTo(object source, Type to)
        {
            if (source == null)
                return to.IsNullable();

            var from = Unbox(source, to);
            if (from == null)
                return to.IsNullable();

            return getImplicitCast(from.GetType(),to) != null;
        }

        public static bool CanCastTo(Type from, Type to)
        {
            return getImplicitCast(from, to) != null;
        }


        public static T CastTo<T>(object source)
        {
            return (T)CastTo(source, typeof(T));
        }


        public static object CastTo(object source, Type to)
        {
            if (source != null)
            {
                if (source.GetType().CanBeTreatedAsType(to)) return source;  // for efficiency

                source = Unbox(source, to);

                if (source != null)
                {
                    Cast cast = getImplicitCast(source.GetType(), to);

                    if (cast == null)
                    {
                        //TODO: Spell out why a little bit more explicit than...
                        throw new InvalidCastException("Cannot cast from '{0}' to '{1}'".FormatWith(Typecasts.ReadableFhirPathName(source.GetType()),
                            Typecasts.ReadableFhirPathName(to)));
                    }

                    return cast(source);
                }
            }

            //if source == null, or unboxed source == null....
            if (to == typeof(IEnumerable<ITypedElement>))
                return FhirValueList.Empty;
            if (to.IsNullable())
                return null;
            else
                throw new InvalidCastException("Cannot cast a null value to non-nullable type '{0}'".FormatWith(to.Name));
        }                  

        public static bool IsNullable(this Type t)
        {
           if (!t.IsAValueType()) return true; // ref-type
           if (Nullable.GetUnderlyingType(t) != null) return true; // Nullable<T>
           return false; // value-type
        }

        public static string ReadableFhirPathName(Type t)
        {
            if (t.CanBeTreatedAsType(typeof(IEnumerable<ITypedElement>)))
                return "collection";
            else if (t.CanBeTreatedAsType(typeof(ITypedElement)))
                return "any single value";
            else
                return t.Name;
        }

    }

}
