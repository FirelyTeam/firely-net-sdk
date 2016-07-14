/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath.Binding
{
    internal class CallBinding
    {
        public string Name { get; private set; }
        public Invokee Function { get; private set; }

        public IEnumerable<ParamBinding> Arguments { get; private set; }

        public CallBinding(string name, Invokee function, params ParamBinding[] arguments)
        {
            Name = name;
            Function = function;
            Arguments = arguments;
        }

        public bool StaticMatches(string functionName, IEnumerable<TypeInfo> argumentTypes)
        {
            //TODO: Match types
            return functionName == Name && argumentTypes.Count() == Arguments.Count();
        }
    }
}
