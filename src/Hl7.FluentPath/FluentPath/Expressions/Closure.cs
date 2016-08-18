/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System.Collections.Generic;
using Hl7.ElementModel;

namespace Hl7.FluentPath.Expressions
{
    internal class Closure
    {
        public Closure()
        {
        }

        public static Closure Root(IEnumerable<IValueProvider> input)
        {
            var newContext = new Closure();

            newContext.SetThis(input);
            newContext.SetThat(input);
            newContext.SetOriginalContext(input);

            return newContext;
        }

        public static Closure Root(IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource)
        {
            var newContext = new Closure();

            newContext.SetThis(input);
            newContext.SetThat(input);
            newContext.SetOriginalContext(input);
            if(resource != null) newContext.SetResource(resource);

            return newContext;
        }

        private Dictionary<string, IEnumerable<IValueProvider>> _namedValues = new Dictionary<string, IEnumerable<IValueProvider>>();

        public virtual void SetValue(string name, IEnumerable<IValueProvider> value)
        {
            _namedValues.Remove(name);
            _namedValues.Add(name, value);
        }


        public Closure Parent { get; private set; }

        public virtual Closure Nest()
        {
            var newContext = new Closure();
            newContext.Parent = this;

            return newContext;
        }


        public virtual IEnumerable<IValueProvider> ResolveValue(string name)
        {
            // First, try to directly get "normal" values
            IEnumerable<IValueProvider> result = null;
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
