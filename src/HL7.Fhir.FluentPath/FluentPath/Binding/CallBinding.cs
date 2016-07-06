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

        public bool StaticMatches(string functionName, params TypeInfo[] argumentTypes)
        {
            return functionName == Name && argumentTypes.Count() == Arguments.Count();
        }

        //public void Verify(FunctionCallExpression expression)
        //{
        //    var minPar = Arguments.Count(a => a.IsOptional == false);
        //    var maxPar = Arguments.Count();
        //    var numPar = expression.Arguments.Count();

        //    if (expression.FunctionName != Name)
        //        throw Error.InvalidOperation("Expression does not match the name of the function");

        //    var correctParNum = numPar >= minPar && numPar <= maxPar;

        //    if (!correctParNum)
        //        throw Error.Argument("Incorrect number of arguments passed to function");
        //}
    }
}
