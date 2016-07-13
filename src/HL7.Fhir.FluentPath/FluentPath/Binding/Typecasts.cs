using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.FluentPath.Binding
{
    internal class Typecasts
    {
        public delegate object Cast(object source);

        private static object id(object source)
        {
            return source;
        }

        private static object any2bool(object source)
        {
            if (source == null) return false;

            if (source is IEnumerable<IValueProvider>)
            {
                var list = (IEnumerable<IValueProvider>)source;
                if (!list.Any()) return false;

                if (list.Count() == 1)
                    source = list.Single();
            }

            if (source is IValueProvider)
            {
                var vp = (IValueProvider)source;
                if (vp.Value is bool)
                    return (bool)vp.Value;
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            return true;
        }

        private static object long2decimal(object source)
        {
            return Convert.ChangeType(source, typeof(decimal));
        }

        private static object any2ValueProvider(object source)
        {
            return new ConstantValue(source);
        }

        private static object any2List(object source)
        {
            return FhirValueList.Create(source);
        }

        public static Cast GetImplicitCast(Type from, Type to)
        {
            if (to.IsAssignableFrom(from)) return id;
            if (to == typeof(bool)) return any2bool;
            if (from == typeof(long) && to == typeof(decimal)) return long2decimal;
            if (to == typeof(IValueProvider)) return any2ValueProvider;
            if (to == typeof(IEnumerable<IValueProvider>)) return any2List;

            return null;
        }

        public static bool CanCastImplicitly(Type from, Type to)
        {
            return GetImplicitCast(from, to) != null;
        }

        public static object Unbox(object instance)
        {
            if (instance == null) instance = FhirValueList.Empty;

            if (instance is IEnumerable<IValueProvider>)
            {
                var list = (IEnumerable<IValueProvider>)instance;
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


        public static T CastTo<T>(object source)
        {
            if (source == null) source = FhirValueList.Empty;
            if (typeof(T).IsAssignableFrom(source.GetType())) return (T)source;  // for efficiency

            return (T)CastTo(source, typeof(T));
        }

        public static object CastTo(object source, Type t)
        {
            if (source == null) source = FhirValueList.Empty;
            if (t.IsAssignableFrom(source.GetType())) return source;  // for efficiency

            object unboxed = Unbox(source);
            Cast cast = GetImplicitCast(unboxed.GetType(), t);

            if(cast == null)
            {
                //TODO: Spell out why a little bit more explicit than...
                throw new InvalidCastException("Cannot cast from '{0}' to '{1}'".FormatWith(source.GetType().Name, t.Name));
            }

            return cast(unboxed);
        }

        public static IEnumerable<IValueProvider> WrapNative<F>(ParamBinding b1, Func<F,object> f, IEnumerable<IValueProvider> p1)
        {
            return CastTo<IEnumerable<IValueProvider>>(f(b1.Bind<F>(p1)));
        }

        public static IEnumerable<IValueProvider> WrapNative<F,A>(ParamBinding b1, ParamBinding b2, Func<F, A, object> f, IEnumerable<IValueProvider> p1, IEnumerable<IValueProvider> p2)
        {
            return CastTo<IEnumerable<IValueProvider>>(f(b1.Bind<F>(p1), b2.Bind<A>(p2)));
        }

        public static IEnumerable<IValueProvider> WrapNative<F, A, B>(ParamBinding b1, ParamBinding b2, ParamBinding b3, 
                        Func<F, A, B, object> f, IEnumerable<IValueProvider> p1, IEnumerable<IValueProvider> p2, IEnumerable<IValueProvider> p3)
        {
            return CastTo<IEnumerable<IValueProvider>>(f(b1.Bind<F>(p1), b2.Bind<A>(p2), b3.Bind<B>(p3)  ));
        }

    }
}
