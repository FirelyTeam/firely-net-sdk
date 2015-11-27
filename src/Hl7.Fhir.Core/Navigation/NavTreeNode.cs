/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Abstract base class for a generic navigable tree node.</summary>
    /// <typeparam name="TNode">Represents the type of the derived class.</typeparam>
    public abstract class BaseNavTreeNode<TNode> : INavTreeLeafNode<TNode> where TNode : BaseNavTreeNode<TNode>, INavTreeLeafBuilder<TNode>
    {
        protected BaseNavTreeNode(string name) : this(null, name) { }

        protected BaseNavTreeNode(TNode parent, string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentException("Must specify a node name.", "name"); } // nameof(name)
            Name = name;
            Parent = parent;
        }

        /// <summary>Gets the name of the node.</summary>
        public string Name { get; protected set; }

        /// <summary>Gets a reference to the parent node.</summary>
        public TNode Parent { get; protected set; }

        /// <summary>Gets a reference to the preceding sibling node.</summary>
        public TNode PreviousSibling { get; protected set; }

        /// <summary>Gets a reference to the following sibling node.</summary>
        public TNode NextSibling { get; protected set; }

        /// <summary>Appends a new sibling node.</summary>
        public T AppendSibling<T>(T node) where T : TNode
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            if (node.Parent != null) { throw new InvalidOperationException("The specified node is already attached to a parent node."); }
            if (node.PreviousSibling != null) { throw new InvalidOperationException("The specified node is already attached to a preceding sibling node."); }

            var last = this.LastSibling();
            last.NextSibling = node;
            node.PreviousSibling = (TNode)last;
            node.Parent = Parent;

            return node;
        }

        public override string ToString() => string.Format("({0}) {1}", PrettyTypeName(GetType()), Name);

        // http://stackoverflow.com/questions/1533115/get-generictype-name-in-good-format-using-reflection-on-c-sharp#answer-25287378
        protected static string PrettyTypeName(Type t)
            => t.IsGenericType
            ? string.Format(
                    "{0}<{1}>",
                    t.Name.Substring(0, t.Name.LastIndexOf("`", StringComparison.InvariantCulture)),
                    string.Join(", ", t.GetGenericArguments().Select(PrettyTypeName)))
            : t.Name;
    }

    /// <summary>A node in a navigable tree structure.</summary>
    public class NavTreeNode : BaseNavTreeNode<NavTreeNode>, INavTreeNode<NavTreeNode>, INavTreeBuilder<NavTreeNode>
    {
        public NavTreeNode(string name) : base(name) { }

        public NavTreeNode(NavTreeNode parent, string name) : base(parent, name) { }

        /// <summary>Gets a reference to the first child node.</summary>
        public virtual NavTreeNode FirstChild { get; protected set; }

        /// <summary>Appends a new child node.</summary>
        public TNode AppendChild<TNode>(TNode node) where TNode : NavTreeNode
        {
            if (node == null) { throw new ArgumentNullException("node"); } // nameof(node)
            if (node.Parent != null) { throw new InvalidOperationException("The specified node is already attached to a parent node."); }
            if (node.PreviousSibling != null) { throw new InvalidOperationException("The specified node is already attached to a preceding sibling node."); }

            var last = this.FirstChild;
            if (last == null)
            {
                FirstChild = node;
            }
            else
            {
                last = last.LastSibling();
                last.NextSibling = node;
                node.PreviousSibling = last;
            }
            node.Parent = this;

            return node;
        }

        public NavTreeNode AppendChild(string name) => AppendChild(new NavTreeNode(name));

        public NavTreeLeafNode<TValue> AppendChild<TValue>(string name, TValue value) => AppendChild(new NavTreeLeafNode<TValue>(name, value));

        //public TNode AppendSibling<TNode>(TNode node) where TNode : NavTreeNode
        //{
        //    if (node == null) { throw new ArgumentNullException(nameof(node)); }
        //    if (node.Parent != null) { throw new InvalidOperationException("The specified node is already attached to a parent node."); }
        //    if (node.PreviousSibling != null) { throw new InvalidOperationException("The specified node is already attached to a preceding sibling node."); }
        //
        //    var last = this.LastSibling();
        //    last.NextSibling = node;
        //    node.PreviousSibling = last;
        //    node.Parent = Parent;
        //
        //    return node;
        //}

        public NavTreeNode AppendSibling(string name) => AppendSibling(new NavTreeNode(name));

        public NavTreeLeafNode<TValue> AppendSibling<TValue>(string name, TValue value) => AppendSibling(new NavTreeLeafNode<TValue>(name, value));
    }

    /// <summary>A leaf node in a navigable tree structure. A leaf node has a typed value and no child nodes.</summary>
    /// <typeparam name="TValue">The type of the node value.</typeparam>
    public class NavTreeLeafNode<TValue> : NavTreeNode, INavTreeValueLeafNode<NavTreeNode, TValue>
    {
        public NavTreeLeafNode(string name) : base(name) { }

        public NavTreeLeafNode(string name, TValue value) : base(name) { Value = value; }

        public NavTreeLeafNode(NavTreeNode parent, string name) : base(parent, name) { }

        public NavTreeLeafNode(NavTreeNode parent, string name, TValue value) : base(parent, name) { Value = value; }

        /// <summary>Warning! A leaf node cannot have children. This property always returns <c>null</c> and you cannot assign a value.</summary>
        public override NavTreeNode FirstChild {
            get { return null; }
            protected set { throw new InvalidOperationException("Cannot add children to a leaf node."); }
        }

        /// <summary>Gets or sets the value of the leaf node.</summary>
        public TValue Value { get; set; }

        public override string ToString() => string.Format("{0} = '{1}'", base.ToString(), Value);
    }
}
