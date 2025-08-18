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
        internal int Id { get; private set; }

        public Closure(EvaluationContext ctx)
        {
            EvaluationContext = ctx;
            Id = ctx.IncrementClosuresCreatedCount();
        }

        public Closure(Closure parent, EvaluationContext ctx)
        {
            Parent = parent;
            EvaluationContext = ctx;
            Id = ctx.IncrementClosuresCreatedCount();
        }

        /// <summary>
        /// When the debug/trace is enabled this property is used to record the focus of the closure.
        /// It is set in the delegate produced for each node by the evaluator visitor.
        /// The value is set <b>immediately before</b> returning the result of the evaluation of the node,
        /// <b>after all</b> it's processing, this must be done as the same context is re-used in many
        /// cases, and thus needs to be re-set just before it returns from the delegate.
        /// The debug tracer uses this information in the wrapped delegate to report not only the
        /// result of the expression, but also the other states of the closure, such as the focus,
        /// resource, root resource, etc.
        /// The $this variable doesn't change within a closure object, so it is not set here.
        /// </summary>
        public IEnumerable<ITypedElement> focus
        {
            get => _focus;
            set
            {
                _focus = value;
            }
        }

        private IEnumerable<ITypedElement> _focus;

        public EvaluationContext EvaluationContext { get; private set; }

        public static Closure Root(ITypedElement root, EvaluationContext ctx = null)
        {
            var newContext = ctx ?? new EvaluationContext();

            var node = root as ScopedNode;

            newContext.Resource ??= node != null // if the value has been manually set, we do nothing. Otherwise, if the root is a scoped node:
                ? getResourceFromNode(node) // we infer the resource from the scoped node
                : (root?.Definition?.IsResource is true // if we do not have a scoped node, we see if this is even a resource to begin with
                    ? root // if it is, we use the root as the resource
                    : null // if not, this breaks the spec in every way (but we will still continue, hopefully we do not need %resource or %rootResource)
                );

            // Same thing, but we copy the resource into the root resource if we cannot infer it from the node.
            newContext.RootResource ??= node != null
                ? getRootResourceFromNode(node)
                : newContext.Resource;

            var newClosure = new Closure(ctx ?? new EvaluationContext());

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

        internal IEnumerable<KeyValuePair<string, IEnumerable<ITypedElement>>> Variables()
        {
            return _namedValues;
        }

        public virtual void SetValue(string name, IEnumerable<ITypedElement> value)
        {
            _namedValues.Remove(name);
            _namedValues.Add(name, value);
        }


        public Closure Parent { get; private set; }

        public virtual Closure Nest()
        {
            return new Closure(this, EvaluationContext);
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

        private static ScopedNode getResourceFromNode(ScopedNode node) => node.AtResource ? node : node.ParentResource;

        private static ScopedNode getRootResourceFromNode(ScopedNode node)
        {
            var resource = getResourceFromNode(node);
            return resource?.Name is "contained" ? resource.ParentResource : resource;
        }
    }
}
