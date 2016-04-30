using Hl7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.FluentPath.FluentPath
{
    internal class Binder
    {
        private Dictionary<string, Delegate> _functions = new Dictionary<string, Delegate>();

        public void Add<P, Q, R>(System.Linq.Expressions.Expression<Func<P, Q, R>> func, string name)
        {
            var compiledFunc = func.Compile();
            var funcName = name + ":" + String.Join("/", func.Parameters.Select(p => p.Name));

            _functions.Add(funcName, compiledFunc);
        }

        public Delegate Bind(string name, IEnumerable<IEnumerable<IFluentPathValue>> parameters)
        {

        } 
    }
}
