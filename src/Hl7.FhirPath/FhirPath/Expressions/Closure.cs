/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using System.Collections.Generic;

namespace Hl7.FhirPath.Expressions
{
    internal class Closure
    {
        public Closure()
        {
        }

        public EvaluationContext EvaluationContext { get; private set; }

        public static Closure Root(ITypedElement root, EvaluationContext ctx = null)
        {
            var newContext = new Closure() { EvaluationContext = ctx ?? EvaluationContext.CreateDefault() };

            var input = new[] { root };
            newContext.SetThis(input);
            newContext.SetThat(input);
            newContext.SetIndex(ElementNode.CreateList(0));
            newContext.SetOriginalContext(input);
            if (ctx.Resource != null) newContext.SetResource(new[] { ctx.Resource });
            if (ctx.RootResource != null) newContext.SetRootResource(new[] { ctx.RootResource });

            return newContext;
        }

        private Dictionary<string, IEnumerable<ITypedElement>> _namedValues = new Dictionary<string, IEnumerable<ITypedElement>>();

        public virtual void SetValue(string name, IEnumerable<ITypedElement> value)
        {
            _namedValues.Remove(name);
            _namedValues.Add(name, value);
        }


        public Closure Parent { get; private set; }

        public virtual Closure Nest()
        {
            return new Closure()
            {
                Parent = this,
                EvaluationContext = this.EvaluationContext
            };
        }


        public virtual IEnumerable<ITypedElement> ResolveValue(string name)
        {
            // First, try to directly get "normal" values
            _namedValues.TryGetValue(name, out IEnumerable<ITypedElement> result);

            if (result != null) return result;

            // If that failed, try to see if the parent has it
            if (Parent != null)
            {
                result = Parent.ResolveValue(name);
                if (result != null) return result;
            }

            return null;
        }
    }
}
