/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
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

namespace Hl7.Fhir.ElementModel
{
    public partial class ElementNode : IElementNode, IAnnotated, IAnnotatable
    {
        public IElementNode Parent { get; private set; }

        private List<ElementNode> _children = new List<ElementNode>();

        public IList<IElementNode> Children => _children.ToList<IElementNode>();

        public string Name { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }

        public string Location
        {
            get
            {
                if (Parent != null)
                {
                    //TODO: Slow - but since we'll change the use of this property to informational 
                    //(i.e. for error messages), it may not be necessary to improve it.
                    var basePath = Parent.Location;
                    int myIndex = Parent.Children.Where(c => c.Name == Name).ToList().IndexOf(this);
                    return $"{basePath}.{Name}[{myIndex}]";
                }
                else
                    return Name;            
            }
        }

        private ElementNode(string name, object value, string type, IEnumerable<ElementNode> children) : this(name,value,type)
        {
            if (children != null) AddRange(children);
        }

        private ElementNode(string name, object value, string type)
        {
            Name = name;
            Value = value;
            Type = type;
        }


        public void Add(ElementNode child)
        {
            AddRange(new[] { child });
        }

        public void AddRange(IEnumerable<ElementNode> children)
        {
            _children.AddRange(children);
            foreach (var c in _children) c.Parent = this;
        }

        public static ElementNode Valued(string name, object value, params ElementNode[] children)
        {
            return new ElementNode(name, value, null, children);
        }

        public static ElementNode Valued(string name, object value, string type, params ElementNode[] children)
        {
            return new ElementNode(name, value, type, children);
        }

        public static ElementNode Node(string name, params ElementNode[] children)
        {
            return new ElementNode(name, null, null, children);
        }

        public static ElementNode Node(string name, string type, params ElementNode[] children)
        {
            return new ElementNode(name, null, type, children);
        }

        public IElementNode Clone()
        {
            var copy = new ElementNode(Name, Value, Type, _children);

            if (_annotations.IsValueCreated)
                copy.annotations.AddRange(annotations);

            return copy;
        }

        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        private List<object> annotations { get { return _annotations.Value; } }

        public ChildNodes this[string name] => new ChildNodes(_children.Where(c=>c.Name == name).ToList());

        public ElementNode this[int index] => _children[index];

        public IEnumerable<object> Annotations(Type type)
        {
            return annotations.OfType(type);
        }

        public void AddAnnotation(object annotation)
        {
            annotations.Add(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            annotations.RemoveOfType(type);
        }
    }


    public class ChildNodes : IEnumerable<IElementNode>
    {
        private IList<ElementNode> _wrapped;

        internal ChildNodes(IList<ElementNode> nodes)
        {
            _wrapped = nodes;
        }

        public ElementNode this[int index] => _wrapped[index];

        public ChildNodes this[string name] => new ChildNodes(_wrapped.Children(name).Cast<ElementNode>().ToList());

        public int Count => _wrapped.Count;

        public bool Contains(ElementNode item) => _wrapped.Contains(item);

        public void CopyTo(ElementNode[] array, int arrayIndex) => _wrapped.CopyTo(array, arrayIndex);

        public IEnumerator<IElementNode> GetEnumerator() => _wrapped.GetEnumerator();

        public int IndexOf(ElementNode item) => _wrapped.IndexOf(item);

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }

}
