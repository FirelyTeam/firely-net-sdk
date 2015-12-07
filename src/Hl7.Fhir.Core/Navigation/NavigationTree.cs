/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Diagnostics;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Common interface for a <see cref="NavigationTree{T}"/>.</summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <example><code>MyTree : INavigationTree&lt;MyTree&gt; { }</code></example>
    public interface INavigationTree<T> : INamedTree<T>, IDoublyLinkedTree<T>, ITreeBuilder<T> where T : INavigationTree<T> { }

    /// <summary>Abstract base class for a navigation tree.</summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <example><code>MyTree : NavigationTree&lt;MyTree&gt; { }</code></example>
    public abstract class NavigationTree<T> : INavigationTree<T> where T : NavigationTree<T>
    {
        private readonly string _name;
        
        private readonly T _parent;             // readonly => cannot rotate or randomly move nodes
        private readonly T _previousSibling;    // readonly => can append, but not insert new nodes

        /// <summary>Create a new tree node.</summary>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <param name="name">The name of the new node.</param>
        protected NavigationTree(T parent, T previousSibling, string name)
        {
            _name = name;
            _parent = parent;
            _previousSibling = previousSibling;
        }

        #region ITree<T>

        /// <summary>The name of the tree node.</summary>
        public string Name { get { return _name; } }

        /// <summary>Indicates if the instance represents a root node.</summary>
        /// <value><c>true</c> of <see cref="Parent"/> equals <c>null</c>, or <c>false</c> otherwise.</value>
        public bool IsRoot { get { return Parent == null; } }

        /// <summary>Indicates if the instance represents an internal tree node.</summary>
        /// <value><c>true</c> if the node has at least one child, or <c>false</c> otherwise.</value>
        public bool IsInternal { get { return FirstChild != null; } }

        /// <summary>Indicates if the instance represents a tree leaf node.</summary>
        /// <value><c>true</c> if the node has no children, or <c>false</c> otherwise.</value>
        public bool IsLeaf { get { return FirstChild == null; } }

        public bool IsFirstSibling { get { return PreviousSibling == null; } }

        public bool IsLastSibling { get { return NextSibling == null; } }

        /// <summary>Returns a sequence of all descendant nodes.</summary>
        /// <returns>An <see cref="IEnumerator{T}"/> sequence.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Self.Descendants().GetEnumerator();
        }

        /// <summary>Returns a sequence of all descendant nodes.</summary>
        /// <returns>An <see cref="IEnumerator"/> sequence.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IPathIndexTree

        /// <summary>Indexer property. Returns a sequence of child nodes by name.</summary>
        /// <param name="name">An node name.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        public IEnumerable<T> this[string name] { get { return Self.Children(name); } }

        /// <summary>Indexer property. Returns a sequence of descendant nodes by path.</summary>
        /// <param name="names">A sequence of node names describing a node path relative to the current node.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        /// <example>
        /// Retrieve the descendants of the specified node matching the path "child.grandchild.greatgrandchild":
        /// <list type="number">
        ///     <item><description>Find child nodes of the current node with name "child"</description></item>
        ///     <item><description>For all nodes in the result set, find child nodes with name "grandchild"</description></item>
        ///     <item><description>For all nodes in the (2nd) result set, find child nodes with name "greatgrandchild"</description></item>
        /// </list>
        /// <code>var result = node["child", "grandchild", "greatgrandchild"];</code>
        /// </example>
        public IEnumerable<T> this[params string[] names]
        {
            get
            {
                if (names == null || names.Length == 0) { throw new ArgumentNullException("path"); } // nameof(path)
                var result = Self.ToEnumerable();
                foreach (var name in names)
                {
                    result = result.SelectMany(n => n[name]);
                }
                return result;
            }
        }

        #endregion

        #region ILinkedTree

        /// <summary>Returns a reference to the next sibling node.</summary>
        public T NextSibling { get; private set; }

        /// <summary>Returns a reference to the first child node.</summary>
        public T FirstChild { get; private set; }

        #endregion

        #region DoublyLinkedTree

        /// <summary>Returns a reference to the parent tree node.</summary>
        public T Parent { get { return _parent; } }

        /// <summary>Returns a reference to the previous sibling tree node.</summary>
        public T PreviousSibling { get { return _previousSibling; } }

        #endregion

        #region ILinkedTreeBuilder

        /// <summary>Add a new node with the specified name as the first child.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        public T AddFirstChild(string name) { return AddFirstChild(() => CreateNode(Self, null, name)); }

        /// <summary>Add a new node with the specified name as the next sibling.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        public T AddNextSibling(string name) { return AddNextSibling(() => CreateNode(Parent, Self, name)); }

        #endregion

        #region Protected members

        // Derived classes must implement the following abstract members

        // Self - allows base class to operate on the derived class using it's generic target type T (covariance)
        // Example: Self.FirstChild returns T whereas this.FirstChild returns NavigationTree<T>

        /// <summary>Returns a self-reference to the current instance of type <typeparamref name="T"/>.</summary>
        /// <example><code>protected override T Self { get { return this; } }</code></example>
        protected abstract T Self { get; } // (T)this;

        // CreateNode - allows base class to create new instances of the generic type T of the derived class

        /// <summary>Creates a new tree node of type <typeparamref name="T"/>.</summary>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A new node instance of <typeparamref name="T"/>.</returns>
        protected abstract T CreateNode(T parent, T previousSibling, string name);

        // Note: internal builder methods expect factory delegate parameter so derived classes may inject additional ctor parameters

        protected T AddFirstChild(Func<T> factory)
        {
            if (!this.IsLeaf) { throw new InvalidOperationException("Invalid operation. The current node already has a first child node."); }
            var node = factory();
            Debug.Assert(object.ReferenceEquals(node.Parent, this));
            Debug.Assert(node.IsFirstSibling);
            Debug.Assert(node.IsLastSibling);
            FirstChild = node;
            return node;
        }

        protected T AddNextSibling(Func<T> factory)
        {
            if (!this.IsLastSibling) { throw new InvalidOperationException("Invalid operation. The current node already has a sibling node."); }
            var node = factory();
            Debug.Assert(object.ReferenceEquals(node.Parent, this.Parent));
            Debug.Assert(node.IsLastSibling);
            NextSibling = node;
            return node;
        }

        #endregion

        public override string ToString() { return string.Format("({0}) {1}", ReflectionHelper.PrettyTypeName(GetType()), Name); }

    }

}
