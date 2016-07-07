using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
