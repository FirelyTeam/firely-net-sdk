/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.ElementModel
{
    public class ElementNode : ITypedElement, IAnnotated, IAnnotatable
    {
        public ElementNode Parent { get; private set; }

        private List<ElementNode> _children = new List<ElementNode>();

        public IEnumerable<ITypedElement> Children(string name = null) =>
            name == null ? _children : _children.Where(c => c.Name == name);

        public string Name { get; set; }

        internal ElementNode(string name, object value, string instanceType, IElementDefinitionSummary definition)
        {
            Name = name;
            InstanceType = instanceType;
            Value = value;
            Definition = definition;
        }

        private IReadOnlyCollection<IElementDefinitionSummary> _childDefinitions = null;

        private IReadOnlyCollection<IElementDefinitionSummary> getChildDefinitions(IStructureDefinitionSummaryProvider provider)
        {
            LazyInitializer.EnsureInitialized(ref _childDefinitions, () => this.ChildDefinitions(provider));

            return _childDefinitions;
        }

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, ElementNode child)
        {
            if (child.Name == null) throw Error.Argument($"The ElementNode given should have its Name property set.");

            child.Parent = this;

            // If we add a child, we better overwrite it's definition with what
            // we think it should be - this way you can safely first create a node representing
            // an independently created root for a resource of datatype, and then add it to the tree.
            var childDefs = getChildDefinitions(provider ?? throw Error.ArgumentNull(nameof(provider)));
            var childDef = childDefs.Where(cd => cd.ElementName == child.Name).SingleOrDefault();

            child.Definition = childDef ?? child.Definition;    // if we don't know about the definition, stick with the old one (if any)
            
            if(child.InstanceType == null)
            {
                if (child.Definition.IsResource || child.Definition.Type.Length > 1)
                    throw Error.Argument("The ElementNode given should have its InstanceType property set, since the element is a choice or resource.");

                child.InstanceType = child.Definition.Type.Single().GetTypeName();
            }
            _children.Add(child);

            return this;
        }

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, string name, object value=null, string instanceType = null)
        {
            var child = new ElementNode(name, value, instanceType, null);

            // Add() will supply the definition and the instanceType (if necessary)
            Add(provider, child); 

            return this;
        }

        public static ElementNode Root(IStructureDefinitionSummaryProvider provider, string type, string name=null)
        {
            if (provider == null) throw Error.ArgumentNull(nameof(provider));
            if (type == null) throw Error.ArgumentNull(nameof(type));

            var sd = provider.Provide(type);
            IElementDefinitionSummary definition = null;

            // Should we throw if type is not found?
            if (sd != null)
                definition = new TypeRootDefinitionSummary(sd);

            return new ElementNode(name ?? type, null, type, definition);
        }

        public static ElementNode FromElement(ITypedElement node, bool recursive = true, IEnumerable<Type> annotationsToCopy = null)
            => buildNode(node, recursive, annotationsToCopy, null);

        private static ElementNode buildNode(ITypedElement node, bool recursive, IEnumerable<Type> annotationsToCopy, ElementNode parent)
        {
            var me = new ElementNode(node.Name, node.Value, node.InstanceType, node.Definition)
            {
                Parent = parent
            };

            foreach (var t in annotationsToCopy ?? Enumerable.Empty<Type>())
                foreach (var ann in node.Annotations(t))
                    me.AddAnnotation(ann);

            if (recursive)
                me._children.AddRange(node.Children().Select(c => buildNode(c, recursive: true, annotationsToCopy: annotationsToCopy, me)));

            return me;
        }

        private static ElementNode buildNode(ISourceNode node, bool recursive, IEnumerable<Type> annotationsToCopy, ElementNode parent)
        {
            // call some new method on TypedElementOnSourceNode to do the initial conversion 
            throw new NotImplementedException();
        }

        public ElementNode Clone()
        {
            var copy = new ElementNode(Name, Value, InstanceType, Definition)
            {
                Parent = Parent,
                _children = _children
            };

            if (_annotations.IsValueCreated)
                copy.annotations.AddRange(annotations);

            return copy;
        }

        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        private List<object> annotations { get { return _annotations.Value; } }

        public IElementDefinitionSummary Definition { get; private set; }

        public string InstanceType { get; private set; }

        public object Value { get; set; }

        public string Location => "dummy";

        public ChildElements this[string name] => new ChildElements(_children.Where(c => c.Name == name).ToList());

        public ElementNode this[int index] => _children[index];

        public IEnumerable<object> Annotations(Type type)
        {
            return (type == typeof(ElementNode) || type == typeof(ITypedElement))
                ? (new[] { this })
                : annotations.OfType(type);
        }

        public void AddAnnotation(object annotation) => annotations.Add(annotation);

        public void RemoveAnnotations(Type type) => annotations.RemoveOfType(type);
    }


    public class ChildElements : IEnumerable<ElementNode>
    {
        private IList<ElementNode> _wrapped;

        internal ChildElements(IList<ElementNode> nodes)
        {
            _wrapped = nodes;
        }

        public ElementNode this[int index] => _wrapped[index];

        public ChildElements this[string name] => new ChildElements(_wrapped.Children(name).Cast<ElementNode>().ToList());

        public int Count => _wrapped.Count;

        public bool Contains(ElementNode item) => _wrapped.Contains(item);

        public IEnumerator<ElementNode> GetEnumerator() => _wrapped.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }

}
