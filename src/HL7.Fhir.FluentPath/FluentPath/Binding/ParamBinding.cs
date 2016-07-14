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
using Hl7.Fhir.FluentPath.Expressions;

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
                return Typecasts.CastTo<T>(source);
            }
            catch (InvalidCastException ice)
            {
                throw Error.Argument(Name, ice.Message);
            }
        }

        public static T CastToSingleValue<T>(object source)
        {
            return Typecasts.CastTo<T>(source);
        }
    
    }
}