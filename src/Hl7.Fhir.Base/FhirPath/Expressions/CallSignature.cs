/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    internal class CallSignature
    {
        public string Name { get; private set; }

        public Type[] ArgumentTypes { get; private set; }

        public Type ReturnType { get; private set; }

        public CallSignature(string name, Type returnType, params Type[] argTypes)
        {
            Name = name;
            ArgumentTypes = argTypes;
            ReturnType = returnType;
        }

        //public bool Matches(string functionName, IEnumerable<Type> argumentTypes)
        //{
        //    return functionName == Name && argumentTypes.Count() == ArgumentTypes.Count() &&
        //           argumentTypes.Zip(ArgumentTypes, (call, sig) => Typecasts.CanCastTo(call,sig)).All(r => r == true);
        //}

        public bool DynamicMatches(string functionName, IEnumerable<object> arguments)
        {
            return functionName == Name && arguments.Count() == ArgumentTypes.Length &&
                   arguments.Zip(ArgumentTypes, Typecasts.CanCastTo).All(r => r == true);
        }

        public bool DynamicExactMatches(string functionName, IEnumerable<object> arguments)
        {
            return functionName == Name && arguments.Count() == ArgumentTypes.Length &&
                   arguments.Zip(ArgumentTypes, Typecasts.IsOfExactType).All(r => r == true);
        }

        virtual public bool Matches(string functionName, int argCount)
        {
            return functionName == Name && ArgumentTypes.Length == argCount;
        }
    }
}
