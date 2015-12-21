using Hl7.Fhir.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    public class EvaluationContext
    {
        // [WMR] Store variables as Memo<T>, i.e. evaluate expression and save result on first call
        // Polymorphic collection - each variable has it's own value type T
        // Important: each Evaluator instance should receive a new EvaluationContext instance - for variable scoping
        // => Copy ctor

        // Useful pointers:
        // http://blogs.msdn.com/b/wesdyer/archive/2008/01/11/the-marvels-of-monads.aspx
        // https://github.com/louthy/csharp-monad
        // http://www.codeproject.com/Articles/649989/Monad-like-programming-with-Csharp

            // Later we can make this nicer with an Option type or so, just need to go on now.
        public IEnumerable<FhirNavigationTree> Nodes { get; private set; }
        public IEnumerable<object> Values { get; private set; }

        public bool IsAtomic
        {
            get
            {
                return Values != null;
            }
        }

        public bool IsNodeSet
        {
            get
            {
                return Nodes != null;
            }
        }

        public static EvaluationContext NewContext(EvaluationContext parent, params object[] values)
        {
            return NewContext(parent, (IEnumerable<object>)values);
        }
        public static EvaluationContext NewContext(EvaluationContext parent, IEnumerable<object> values)
        {
            //copy stuff from parent
            return new EvaluationContext() { Values = values };
        }

        public static EvaluationContext NewContext(EvaluationContext parent, IEnumerable<FhirNavigationTree> nodes)
        {
            // copy stuff from parent
            return new EvaluationContext() { Nodes = nodes };
        }
    }
}
