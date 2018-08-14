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
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    public partial class UntypedNode : ISourceNode, IAnnotated, IAnnotatable
    {
        public ISourceNode Parent { get; private set; }

        private List<UntypedNode> _children = new List<UntypedNode>();

        public IEnumerable<ISourceNode> Children(string name = null) =>
            name == null ? _children : _children.Where(c => c.Name == name);

        public string Name { get; set; }

        public string ResourceType { get; set; }

        public string Text { get; set; }

        public string Location
        {
            get
            {
                if (Parent != null)
                {
                    //TODO: Slow - but since we'll change the use of this property to informational 
                    //(i.e. for error messages), it may not be necessary to improve it.
                    var basePath = Parent.Location;
                    int myIndex = Parent.Children(Name).ToList().IndexOf(this);
                    return $"{basePath}.{Name}[{myIndex}]";
                }
                else
                    return Name;            
            }
        }

        private UntypedNode(string name, string text,  
            IEnumerable<UntypedNode> children=null, string resourceType=null)
        {
            Name = name;
            Text = text;
            ResourceType = resourceType;

            if (children != null) AddRange(children);
        }

        public void Add(UntypedNode child)
        {
            AddRange(new[] { child });
        }

        public void AddRange(IEnumerable<UntypedNode> children)
        {
            _children.AddRange(children);
            foreach (var c in _children) c.Parent = this;
        }

        public static UntypedNode Valued(string name, string value, params UntypedNode[] children)
        {
            return new UntypedNode(name, value, children);
        }

        public static UntypedNode Resource(string name, string type, params UntypedNode[] children)
        {
            return new UntypedNode(name, null, children, type);
        }

        public static UntypedNode Node(string name, params UntypedNode[] children)
        {
            return new UntypedNode(name, null, children);
        }

        public static UntypedNode FromNode(ISourceNode node)
        {
            return buildNode(node);
        }

        private static UntypedNode buildNode(ISourceNode node)
        {
            var me = new UntypedNode(node.Name, node.Text);
            me.AddRange(node.Children().Select(c => buildNode(c)));
            return me;
        }


        public ISourceNode Clone()
        {
            var copy = new UntypedNode(Name, Text, _children, ResourceType);

            if (_annotations.IsValueCreated)
                copy.annotations.AddRange(annotations);

            return copy;
        }

        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        private List<object> annotations { get { return _annotations.Value; } }

        public IElementDefinitionSummary Definition => throw new NotImplementedException();

        public ChildNodes this[string name] => new ChildNodes(_children.Where(c=>c.Name == name).ToList());

        public UntypedNode this[int index] => _children[index];

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


    public class ChildNodes : IEnumerable<ISourceNode>
    {
        private IList<UntypedNode> _wrapped;

        internal ChildNodes(IList<UntypedNode> nodes)
        {
            _wrapped = nodes;
        }

        public UntypedNode this[int index] => _wrapped[index];

        public ChildNodes this[string name] => new ChildNodes(_wrapped.Children(name).Cast<UntypedNode>().ToList());

        public int Count => _wrapped.Count;

        public bool Contains(UntypedNode item) => _wrapped.Contains(item);

        public void CopyTo(UntypedNode[] array, int arrayIndex) => _wrapped.CopyTo(array, arrayIndex);

        public IEnumerator<ISourceNode> GetEnumerator() => _wrapped.GetEnumerator();

        public int IndexOf(UntypedNode item) => _wrapped.IndexOf(item);

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }

}
