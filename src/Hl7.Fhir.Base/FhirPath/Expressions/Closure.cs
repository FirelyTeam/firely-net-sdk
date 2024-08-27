/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */


using Hl7.Fhir.ElementModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;

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
            var newContext = ctx ?? new EvaluationContext();
            
            var node = root as ScopedNode;
            newContext.Resource ??= node != null ? getResourceFromNode(node) : root;
            newContext.RootResource ??= node != null ? getRootResourceFromNode(node) : root;
            
            var newClosure = new Closure() { EvaluationContext = ctx ?? new EvaluationContext() };

            var input = new[] { root };

            foreach (var assignment in newClosure.EvaluationContext.Environment)
            {
                newClosure.SetValue(assignment.Key, assignment.Value);
            }
            
            newClosure.SetThis(input);
            newClosure.SetThat(input);
            newClosure.SetIndex(ElementNode.CreateList(0));
            newClosure.SetOriginalContext(input);
            
            if (newContext.Resource != null) newClosure.SetResource(new[] { newContext.Resource });
            if (newContext.RootResource != null) newClosure.SetRootResource(new[] { newContext.RootResource });

            return newClosure;
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

        private static ITypedElement getResourceFromNode(ScopedNode node) => node.AtResource ? node : node.ParentResource;
        
        private static ITypedElement getRootResourceFromNode(ScopedNode node)
        {
            var resource = (ScopedNode)getResourceFromNode(node);
            return resource?.ParentResource?.ContainedResources() is {} c && c.Any() ? resource.ParentResource : resource;
        }
    }
}
