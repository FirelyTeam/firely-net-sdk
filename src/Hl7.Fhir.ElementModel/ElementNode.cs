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

        public ElementNode Add(ElementNode child) => AddRange(new[] { child });

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, string name, object value, string instanceType = null)
        {
            // TODO: cache the definitions somehow - this is very slow
            // we need an internal method that does NewChild which caches the definitions of the children
            // the extension method NewChild should call that when it detects the implementation is ElementNode
            // we should call that internal method here too
            var child = this.NewChild(provider, name, value, instanceType);
            Add(child);

            return this;
        }

        public ElementNode Add(IStructureDefinitionSummaryProvider provider, string name, string instanceType = null)
             => Add(provider, name, null, instanceType);

        public ElementNode AddRange(IEnumerable<ElementNode> children)
        {
            // Not so fast....if we add a child, we better overwrite it's definition with what
            // we think it should be - this way you can safely first create a node representing
            // an independently created root for  a resource of datatype, and then add it to the tree.
            _children.AddRange(children);
            foreach (var c in _children) c.Parent = this;

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
            => buildNode(node, recursive, annotationsToCopy);

        private static ElementNode buildNode(ITypedElement node, bool recursive, IEnumerable<Type> annotationsToCopy)
        {
            var me = new ElementNode(node.Name, node.Value, node.InstanceType, node.Definition);

            foreach (var t in annotationsToCopy ?? Enumerable.Empty<Type>())
                foreach (var ann in node.Annotations(t))
                    me.AddAnnotation(ann);

            if (recursive)
                me.AddRange(node.Children().Select(c => buildNode(c, recursive: true, annotationsToCopy: annotationsToCopy)));

            return me;
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

        public string Location => throw new NotImplementedException();

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
