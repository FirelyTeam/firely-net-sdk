#if USE_CODE_GEN

/*
 * This code is based on the article by Mariano Omar Rodiguez found here: 
 * http://weblogs.asp.net/marianor/archive/2009/04/10/using-expression-trees-to-get-property-getter-and-setters.aspx
 */

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Hl7.Fhir.Introspection
{
    internal static class PropertyInfoExtensions
    {
        public static Func<T, object> GetValueGetter<T>(this PropertyInfo propertyInfo)
        {
            if (typeof(T) != propertyInfo.DeclaringType)
            {
                throw new ArgumentException();
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, typeof(object));
            return (Func<T, object>)Expression.Lambda(convert, instance).Compile();
        }

        public static Func<object, object> GetValueGetter(this PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(typeof(object), "i");
            var convertIn = Expression.Convert(instance, propertyInfo.DeclaringType);
            var property = Expression.Property(convertIn, propertyInfo);
            var convertOut = Expression.TypeAs(property, typeof(object));

            return (Func<object, object>)Expression.Lambda(convertOut, instance).Compile();
        }


        public static Action<T, object> GetValueSetter<T>(this PropertyInfo propertyInfo)
        {
            if (typeof(T) != propertyInfo.DeclaringType)
            {
                throw new ArgumentException();
            }

            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var argument = Expression.Parameter(typeof(object), "a");
            var setterCall = Expression.Call(
                instance,
                propertyInfo.SetMethod,
                Expression.Convert(argument, propertyInfo.PropertyType));
            return (Action<T, object>)Expression.Lambda(setterCall, instance, argument).Compile();
        }

        public static Action<object, object> GetValueSetter(this PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(typeof(object), "i");
            var convertIn = Expression.Convert(instance, propertyInfo.DeclaringType);
            var argument = Expression.Parameter(typeof(object), "a");
            var setterCall = Expression.Call(
                convertIn,
                propertyInfo.SetMethod,
                Expression.Convert(argument, propertyInfo.PropertyType));
            return (Action<object, object>)Expression.Lambda(setterCall, instance, argument).Compile();
        }
    }
}

#endif