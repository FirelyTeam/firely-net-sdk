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
            _debugTracerActive = ctx.DebugTracer != null;
        }

        public Closure(Closure parent, EvaluationContext ctx)
        {
            Parent = parent;
            EvaluationContext = ctx;
            Id = ctx.IncrementClosuresCreatedCount();
            _debugTracerActive = ctx.DebugTracer != null;
        }

        /// <summary>
        /// When the debug/trace is enabled this property is used to record the focus of the closure.
        /// <br/>VALUE IS NOT USED OUTSIDE DEBUG - without debug/tracer, the value is not consistent.
        /// </summary>
        /// <remarks>
        /// It is set in the delegate produced for each node by the evaluator visitor.
        /// The debug tracer will reset the focus in the closure after calling the delegate it's wrapping.
        /// ensuring that argument evaluation doesn't impact the focus logged in the debug trace in other
        /// calls.
        /// </remarks>
        public IEnumerable<ITypedElement> focus
        {
            get
            {
                if (!_debugTracerActive)
                    return ElementNode.EmptyList;
                return _focus;
            }
            set
            {
                if (!_debugTracerActive)
                    return;
                _focus = value;
            }
        }

        private IEnumerable<ITypedElement> _focus;
        private bool _debugTracerActive = false;

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
