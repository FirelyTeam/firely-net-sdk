/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    public class ElementNode : ITypedElement, IAnnotated, IAnnotatable
    {
        public ElementNode Parent { get; private set; }

        private List<ElementNode> _children = new List<ElementNode>();

        public IEnumerable<ITypedElement> Children(string name = null) =>
            name == null ? _children : _children.Where(c => c.Name == name);

        public string Name { get; set; }

        private ElementNode(string name, object value, string instanceType, IElementDefinitionSummary definition,
             IEnumerable<ElementNode> children = null)
        {
            Name = name;
            InstanceType = instanceType;
            Value = value;
            Definition = definition;

            if (children != null) AddRange(children);
        }

        public void Add(ElementNode child)
        {
            AddRange(new[] { child });
        }

        public void AddRange(IEnumerable<ElementNode> children)
        {
            throw new NotImplementedException();

            //_children.AddRange(children);
            //foreach (var c in _children) c.Parent = this;
        }

        public static ElementNode Valued(string name, string value, params ElementNode[] children)
        {
            throw new NotImplementedException();
            //            return new ElementNode(name, value, children);
        }

        public static ElementNode Valued(string name, string value, string instanceType, params ElementNode[] children)
        {
            throw new NotImplementedException();
            //            return new ElementNode(name, value, children);
        }

        public static ElementNode Node(string name, params SourceNode[] children)
        {
            throw new NotImplementedException();
            //            return new SourceNode(name, null, children);
        }

        public static ElementNode Node(string name, string instanceType, params SourceNode[] children)
        {
            throw new NotImplementedException();
            //            return new SourceNode(name, null, children);
        }

        public static ElementNode FromNode(ITypedElement node, bool justRoot = false) => buildNode(node, justRoot);
        // base this in TypedElementOnSourceNode.NewChild("name",....) which creates a new typed child,
        // probably making now-private code that connects the source+element world re-useable.

        public static ElementNode FromElement(ITypedElement node, bool justRoot = false) => buildNode(node, justRoot);

        private static ElementNode buildNode(ITypedElement node, bool justRoot)
        {
            throw new NotImplementedException();
            //var me = new SourceNode(node.Name, node.Text);
            //me.AddRange(node.Children().Select(c => buildNode(c)));
            //return me;
        }

        public ElementNode Clone()
        {
            var copy = new ElementNode(Name, Value, InstanceType, Definition, _children);

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

        public ChildElements this[string name] => new ChildElements(_children.Where(c=>c.Name == name).ToList());

        public ElementNode this[int index] => _children[index];

        public IEnumerable<object> Annotations(Type type)
        {
            return (type == typeof(ElementNode) || type == typeof(ITypedElement) )
                ? (new[] { this })
                : annotations.OfType(type);
        }

        public void AddAnnotation(object annotation) => annotations.Add(annotation);

        public void RemoveAnnotations(Type type) => annotations.RemoveOfType(type);
    }


    public class ChildElements : IEnumerable<ITypedElement>
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

        public void CopyTo(ElementNode[] array, int arrayIndex) => _wrapped.CopyTo(array, arrayIndex);

        public IEnumerator<ITypedElement> GetEnumerator() => _wrapped.GetEnumerator();

        public int IndexOf(ElementNode item) => _wrapped.IndexOf(item);

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }

}
