/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Expressions;

internal class CallSignature(string name, Type returnType, params Type[] argTypes)
{
    public string Name { get; } = name;

    public Type[] ArgumentTypes { get; } = argTypes;

    public Type ReturnType { get; private set; } = returnType;

    public bool DynamicMatches(string functionName, IEnumerable<object> arguments)
    {
        var argumentEnumerated = arguments as object[] ?? arguments.ToArray();
        return functionName == Name && argumentEnumerated.Length == ArgumentTypes.Length &&
               argumentEnumerated.Zip(ArgumentTypes, Typecasts.CanCastTo).All(r => r);
    }

    public bool DynamicExactMatches(string functionName, IEnumerable<object> arguments)
    {
        var argumentsEnumerated = arguments as object[] ?? arguments.ToArray();
        return functionName == Name && argumentsEnumerated.Length == ArgumentTypes.Length &&
               argumentsEnumerated.Zip(ArgumentTypes, Typecasts.IsOfExactType).All(r => r);
    }

    virtual public bool Matches(string functionName, int argCount) =>
        functionName == Name && ArgumentTypes.Length == argCount;
}