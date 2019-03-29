/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.ElementModel
{
    public class SourceNode : ISourceNode, IAnnotated, IResourceTypeSupplier, IAnnotatable
    {
        public ISourceNode Parent { get; private set; }

        private List<SourceNode> _children = new List<SourceNode>();

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

        private SourceNode(string name, string text,  
            IEnumerable<SourceNode> children=null, string resourceType=null)
        {
            Name = name;
            Text = text;
            ResourceType = resourceType;

            if (children != null) AddRange(children);
        }

        public void Add(SourceNode child)
        {
            AddRange(new[] { child });
        }

        public void AddRange(IEnumerable<SourceNode> children)
        {
            _children.AddRange(children);
            foreach (var c in _children) c.Parent = this;
        }

        public static SourceNode Valued(string name, string value, params SourceNode[] children)
        {
            return new SourceNode(name, value, children);
        }

        public static SourceNode Resource(string name, string type, params SourceNode[] children)
        {
            return new SourceNode(name, null, children, type);
        }

        public static SourceNode Node(string name, params SourceNode[] children)
        {
            return new SourceNode(name, null, children);
        }

        public static SourceNode FromNode(ISourceNode node)
        {
            return buildNode(node);
        }

        private static SourceNode buildNode(ISourceNode node)
        {
            var me = new SourceNode(node.Name, node.Text);
            me.AddRange(node.Children().Select(c => buildNode(c)));
            return me;
        }


        public ISourceNode Clone()
        {
            var copy = new SourceNode(Name, Text, _children, ResourceType);

            if (_annotations.IsValueCreated)
                copy.annotations.AddRange(annotations);

            return copy;
        }

        private Lazy<List<object>> _annotations = new Lazy<List<object>>(() => new List<object>());
        private List<object> annotations { get { return _annotations.Value; } }

        public IElementDefinitionSummary Definition => throw new NotImplementedException();

        public ChildNodes this[string name] => new ChildNodes(_children.Where(c=>c.Name == name).ToList());

        public SourceNode this[int index] => _children[index];

        public IEnumerable<object> Annotations(Type type)
        {
            return type == typeof(SourceNode) || type == typeof(ISourceNode) || type == typeof(IResourceTypeSupplier)
                ? (new[] { this })
                : annotations.OfType(type);
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
        private IList<SourceNode> _wrapped;

        internal ChildNodes(IList<SourceNode> nodes)
        {
            _wrapped = nodes;
        }

        public SourceNode this[int index] => _wrapped[index];

        public ChildNodes this[string name] => new ChildNodes(_wrapped.Children(name).Cast<SourceNode>().ToList());

        public int Count => _wrapped.Count;

        public bool Contains(SourceNode item) => _wrapped.Contains(item);

        public void CopyTo(SourceNode[] array, int arrayIndex) => _wrapped.CopyTo(array, arrayIndex);

        public IEnumerator<ISourceNode> GetEnumerator() => _wrapped.GetEnumerator();

        public int IndexOf(SourceNode item) => _wrapped.IndexOf(item);

        IEnumerator IEnumerable.GetEnumerator() => _wrapped.GetEnumerator();
    }

}
