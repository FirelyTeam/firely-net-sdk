/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.Expressions;

namespace Hl7.Fhir.FluentPath.Binding
{
    public class ParamBinding
    {
        public ParamBinding(string name, TypeInfo type)
        {
            Name = name;
            FluentPathType = type;
        }

        public string Name { get; private set; }

        public Type NativeType { get; private set; }
        public TypeInfo FluentPathType { get; private set; }

        public bool StaticMatches(Expression parameterExpression)
        {
            return true;
        }

        public T Bind<T>(object source)
        {
            try
            {
                return CastToSingleValue<T>(source);
            }
            catch (InvalidCastException ice)
            {
                throw Error.Argument(Name, ice.Message);
            }
        }

        public static T CastToSingleValue<T>(object source)
        {
            if (source is T)
                return (T)source;

            if (source is IEnumerable<IValueProvider>)
            {
                var ifp = (IEnumerable<IValueProvider>)source;

                if (ifp.Any())
                {
                    if (ifp.Skip(1).Any())
                        throw new InvalidCastException("expecting only a single value");
                    source = ifp.Single();
                }
                else
                    return default(T);
            }

            if (source is T)
                return (T)source;

            if (source is IValueProvider)
            {
                if (source == null)
                    return default(T);

                var val = (IValueProvider)source;
                source = val.Value;
            }

            if (source is T)
                return (T)source;

            if (source is long && typeof(T) == typeof(decimal))
                return (T)System.Convert.ChangeType(source, typeof(decimal));

            throw new InvalidCastException("cannot cast argument of type '{0}' to a '{1}'".FormatWith(source.GetType().Name, typeof(T).Name));
        }
    }
}