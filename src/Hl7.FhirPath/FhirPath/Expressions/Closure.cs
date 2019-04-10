/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */


using System.Collections.Generic;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath.Expressions
{
    internal class Closure
    {
        public Closure()
        {
        }

        public EvaluationContext EvaluationContext { get; private set; }

        public static Closure Root(ITypedElement root, EvaluationContext ctx=null)
        {
            var newContext = new Closure() { EvaluationContext = ctx ?? EvaluationContext.CreateDefault() };

            var input = new[] { root };
            newContext.SetThis(input);
            newContext.SetThat(input);
            newContext.SetOriginalContext(input);
            if(ctx.Container != null) newContext.SetResource(new[] { ctx.Container } );

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
            IEnumerable<ITypedElement> result = null;
            _namedValues.TryGetValue(name, out result);

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
