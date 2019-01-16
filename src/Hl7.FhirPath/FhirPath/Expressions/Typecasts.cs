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
using Hl7.Fhir.Model.Primitives;
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


        private static Cast makeNativeCast(Type to) => source =>
                                              Convert.ChangeType(source, to);

        private static object any2ValueProvider(object source) => new ConstantValue(source);

        private static object any2List(object source) => FhirValueList.Create(source);

        private static object tryQuantity(object source)
        {
            if (source is ITypedElement element)
            {
                if (element.InstanceType == "Quantity")
                {
                    return ParseQuantity(element);
                }
                else
                    throw new InvalidCastException($"Cannot convert from '{element.InstanceType}' to Quantity");
            }

            throw new InvalidCastException($"Cannot convert from '{source.GetType().Name}' to Quantity");
        }

        private static Cast getImplicitCast(Type from, Type to)
        {
            if (to == typeof(object)) return id;

            //if (to.IsAssignableFrom(from)) return id;
            if (from.CanBeTreatedAsType(to)) return id;

            //if (to == typeof(bool)) return any2bool;
            if (to == typeof(Quantity) && from.CanBeTreatedAsType(typeof(ITypedElement))) return tryQuantity;
                    
            if (to == typeof(ITypedElement) && (!from.CanBeTreatedAsType(typeof(IEnumerable<ITypedElement>)))) return any2ValueProvider;
            if (to == typeof(IEnumerable<ITypedElement>)) return any2List;
             
            if (from == typeof(long) && (to == typeof(decimal) || to == typeof(decimal?))) return makeNativeCast(typeof(decimal));
            if (from == typeof(long?) && to == typeof(decimal?)) return makeNativeCast(typeof(decimal?));
            return null;
        }

        internal enum BoxingLevel
        {
            CollectionOfTypedElements,
            TypedElement,
            NativeValue                
        }


        /// <summary>
        /// This will unpack the instance 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="to">The level to unbox to.</param>
        /// <returns></returns>
        /// <remarks>The level of unboxing is specified using a type. The highest level
        /// being an <see cref="IEnumerable{ITypedElement}"/> followed by 
        /// <see cref="ITypedElement"/> followed by a primitive runtime type.
        /// </remarks>
        internal static object UnboxTo(object instance, Type to)
        {
            if (instance == null) return null;
           
            if (instance is IEnumerable<ITypedElement> list)
            {
                if (to.CanBeTreatedAsType(typeof(IEnumerable<ITypedElement>))) return instance;

                if (!list.Any()) return null;
                if (list.Count() == 1)
                    instance = list.Single();
            }

            if (instance is ITypedElement element)
            {
                if (to.CanBeTreatedAsType(typeof(ITypedElement))) return instance;

                if (element.Value != null)
                    instance = element.Value;                   
            }

            return instance;
        }

        internal static Quantity? ParseQuantity(ITypedElement element)
        {
            var value = element.Children("value").SingleOrDefault()?.Value as decimal?;
            var unit = element.Children("code").SingleOrDefault()?.Value as string;
            return value == null ? null : (Quantity?)new Quantity(value.Value, unit);
        }

        public static bool CanCastTo(object source, Type to)
        {
            if (source == null)
                return to.IsNullable();

            var from = UnboxTo(source, to);
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

                source = UnboxTo(source, to);

                if (source != null)
                {
                    Cast cast = getImplicitCast(source.GetType(), to);

                    if (cast == null)
                    {
                        var message = "Cannot cast from '{0}' to '{1}'".FormatWith(Typecasts.ReadableFhirPathName(source),
                            Typecasts.ReadableTypeName(to));
                        throw new InvalidCastException(message);
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


        public static string ReadableTypeName(Type t)
        {
            if (t.CanBeTreatedAsType(typeof(IEnumerable<ITypedElement>)))
                return "collection";
            else if (t.CanBeTreatedAsType(typeof(ITypedElement)))
                return "any type";
            else
                return t.Name;
        }
        public static string ReadableFhirPathName(object value)
        {
            if (value is IEnumerable<ITypedElement> ete)
            {
                var values = ete.ToList();
                var types = ete.Select(te => ReadableFhirPathName(te)).Distinct();

                if (values.Count > 1)
                    return "collection of " + String.Join("/", types);
                else
                    return types.Single();
            }
            else if (value is ITypedElement te)
                return te.InstanceType;
            else
                return value.GetType().Name;
        }

    }

}
