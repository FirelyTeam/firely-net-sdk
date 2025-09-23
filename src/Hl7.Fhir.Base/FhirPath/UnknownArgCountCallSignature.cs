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
    internal class UnknownArgCountCallSignature : CallSignature
    {
        public UnknownArgCountCallSignature(string name, Type returnType)
            : base(name, returnType)
        {
        }

        override public bool Matches(string functionName, int argCount)
        {
            return functionName == Name;
        }
    }
}
