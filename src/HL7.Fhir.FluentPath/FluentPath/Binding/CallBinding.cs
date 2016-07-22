/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath.Binding
{
    internal class CallBinding
    {
        public string Name { get; private set; }
        public Invokee Function { get; private set; }

        public Type[] ArgumentTypes { get; private set; }

        public CallBinding(string name, Invokee function, params Type[] argTypes )
        {
            Name = name;
            Function = function;
            ArgumentTypes = argTypes;
        }

        public bool StaticMatches(string functionName, IEnumerable<Type> argumentTypes)
        {
            //TODO: Match types
            return functionName == Name && argumentTypes.Count() == ArgumentTypes.Count();
        }

        public bool DynamicMatches(string functionName, IEnumerable<object> argumentValues)
        {
            if (ArgumentTypes.Count() != argumentValues.Count())
                return false;

            var casts = argumentValues.Zip(ArgumentTypes, (aa, ea) => Typecasts.CanCastTo(aa, ea));

            return casts.All(c => c == true);
        }

        public static CallBinding Create<R>(string name, Func<R> func)
        {
            return new CallBinding(name, InvokeeFactory.Wrap(func), new Type[] { });
        }

        public static CallBinding Create<A, R>(string name, Func<A, R> func)
        {
            return new CallBinding(name, InvokeeFactory.Wrap(func), new[] { typeof(A) });
        }

        public static CallBinding Create<A,B, R>(string name, Func<A,B,R> func)
        {
            return new CallBinding(name, InvokeeFactory.Wrap(func), new[] { typeof(A), typeof(B) });
        }

        public static CallBinding Create<A, B, C, R>(string name, Func<A, B, C, R> func)
        {
            return new CallBinding(name, InvokeeFactory.Wrap(func), new[] { typeof(A), typeof(B), typeof(C) });
        }

    }
}
