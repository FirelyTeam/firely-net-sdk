/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

#define PRIVATE_SUBCLASS

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    // Advantages of a doubly linked tree:
    // - O(N) navigation on all axes in both directions
    // - O(N) insert/remove

    /// <summary>Common interface for a doubly linked tree item.</summary>
    public interface IDoublyLinkedTree : IDoublyLinkedTree<DoublyLinkedTree> { }

    /// <summary>Abstract base class for doubly linked tree items.</summary>
    public abstract class DoublyLinkedTree : IDoublyLinkedTree, ILinkedTreeBuilder<DoublyLinkedTree>, IValue
    {
        /// <summary>Static factory method. Creates a new tree node with the specified name.</summary>
        /// <param name="name">The name of the node.</param>
        /// <returns>A <see cref="DoublyLinkedTree"/> node.</returns>
        public static DoublyLinkedTree Create(string name)
        {
            return CreateNode(name, null, null);
        }

        /// <summary>Static factory method. Creates a new tree leaf item with the specified name and value.</summary>
        /// <param name="name">The name of the node.</param>
        /// <param name="value">The value of the node.</param>
        /// <returns>A <see cref="DoublyLinkedTree"/> leaf item.</returns>
        public static DoublyLinkedTree Create<V>(string name, V value)
        {
            return CreateLeaf<V>(name, value, null, null);
        }

        private readonly string _name;
        private readonly DoublyLinkedTree _parent;
        private readonly DoublyLinkedTree _previousSibling;

        protected DoublyLinkedTree(string name) { _name = name; }

        protected DoublyLinkedTree(string name, DoublyLinkedTree parent, DoublyLinkedTree previousSibling) : this(name)
        {
            _parent = parent;
            _previousSibling = previousSibling;
        }

        #region ITree

        /// <summary>The name of the tree item.</summary>
        public string Name { get { return _name; } }

        // <summary>Returns <c>true</c> if the tree item supports the <see cref="IValue"/> interface.</summary>
        // public bool IsValue { get { return this is IValue; } }

        #endregion

        #region ILinkedTree

        /// <summary>Returns a reference to the next sibling tree item.</summary>
        public DoublyLinkedTree NextSibling { get; private set; }

        /// <summary>Returns a reference to the first child tree item.</summary>
        abstract public DoublyLinkedTree FirstChild { get; protected set; }

        /// <summary>Indexer property. Enumerates the child items with the specified name.</summary>
        /// <param name="name">An item name.</param>
        /// <returns>An tree item enumerator.</returns>
        public IEnumerable<DoublyLinkedTree> this[string name] { get { return this.Children(name); } }


        /// <summary>Indexer property. Enumerates the descendant items with the specified path.</summary>
        /// <param name="path">An array of nested item names.</param>
        /// <returns>An tree item enumerator.</returns>
        public IEnumerable<DoublyLinkedTree> this[params string[] path]
        {
            get {
                if (path == null || path.Length == 0) { throw new ArgumentNullException("path"); } // nameof(path)
                var result = this.ToEnumerable();
                foreach (var name in path)
                {
                    result = result.SelectMany(n => n[name]);
                }
                return result;
            }
        }

        /// <summary>Returns <c>true</c> if the instance is an internal tree node, i.e. if the item has at least one child.</summary>
        public bool IsInternal { get { return FirstChild != null; } }

        /// <summary>Returns <c>true</c> if the instance is a tree leaf item, i.e. if the item has no children.</summary>
        public bool IsLeaf { get { return FirstChild == null; } }

        #endregion

        #region DoublyLinkedTree

        /// <summary>Returns a reference to the parent tree item.</summary>
        public DoublyLinkedTree Parent { get { return _parent; } }

        /// <summary>Returns a reference to the previous sibling tree item.</summary>
        public DoublyLinkedTree PreviousSibling { get { return _previousSibling; } }

        /// <summary>Returns <c>true</c> if the item represents a root node, i.e. if the item <see cref="Parent"/> reference equals <c>null</c>.</summary>
        public bool IsRoot { get { return Parent == null; } }

        #endregion

        #region IValue

        abstract public Type ValueType { get; }

        #endregion

        #region ILinkedTreeBuilder

        /// <summary>Add a new sibling node with the specified name.</summary>
        /// <param name="name">The name of the new sibling node.</param>
        /// <returns>A reference to the new sibling node.</returns>
        public DoublyLinkedTree AddLastSibling(string name)
        {
            return AddLastSibling(last => CreateNode(name, Parent, last));
        }

        /// <summary>Add a new sibling leaf item with the specified name and value.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new sibling leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new sibling leaf item.</returns>
        public DoublyLinkedTree AddLastSibling<V>(string name, V value)
        {
            return AddLastSibling(last => CreateLeaf(name, value, Parent, last));
        }

        /// <summary>Add a new child node with the specified name.</summary>
        /// <param name="name">The name of the new child node.</param>
        /// <returns>A reference to the new child node.</returns>
        public DoublyLinkedTree AddLastChild(string name)
        {
            return AddLastChild(last => CreateNode(name, this, last));
        }

        /// <summary>Add a new child node with the specified name and value.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new child leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new child leaf item.</returns>
        public DoublyLinkedTree AddLastChild<V>(string name, V value)
        {
            return AddLastChild(last => CreateLeaf(name, value, this, last));
        }

        #endregion

        #region Private members

        private DoublyLinkedTree AddLastSibling(Func<DoublyLinkedTree, DoublyLinkedTree> factory)
        {
            var last = this.LastSibling();
            var node = factory(last);
            last.NextSibling = node;
            return node;
        }

        private DoublyLinkedTree AddLastChild(Func<DoublyLinkedTree, DoublyLinkedTree> factory)
        {
            DoublyLinkedTree node = null;
            var first = FirstChild;
            if (first == null)
            {
                FirstChild = node = factory(null);
            }
            else
            {
                var last = first.LastSibling();
                node = last.AddLastSibling(factory);
            }
            return node;
        }

        // Following members link the base class to the concrete derived classes below

        // delegate T CreateNodeHandler<T>(string name, T parent, T previousSibling) where T : ILinkedTree<T>;
        // delegate T CreateLeafHandler<T, V>(string name, V value, T parent, T previousSibling) where T : ILinkedTree<T>;

        private static DoublyLinkedTree CreateNode(string name, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
        {
            return new Node(name, parent, previousSibling);
        }

        private static DoublyLinkedTree CreateLeaf<V>(string name, V value, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
        {
            return new Leaf<V>(name, value, parent, previousSibling);
        }

        // Private concrete derived classes for nodes and leaves

        private sealed class Node : DoublyLinkedTree
        {
            public Node(string name, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
                : base(name, parent, previousSibling)
            {
            }

            public override DoublyLinkedTree FirstChild { get; protected set; }

            public override Type ValueType { get { throw new InvalidOperationException("A tree node does not have a value"); } }

            public override string ToString() { return string.Format("({0}) {1}", ReflectionHelper.PrettyTypeName(GetType()), Name); }
        }

        private sealed class Leaf<V> : DoublyLinkedTree, IValue<V>
        {
            private readonly V _value;

            public Leaf(string name, V value, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
                : base(name, parent, previousSibling)
            {
                _value = value;
            }

            public override DoublyLinkedTree FirstChild
            {
                get { return null; }
                protected set { throw new InvalidOperationException("You cannot add children to a tree leaf item."); }
            }

            public override Type ValueType { get { return typeof(V); } }

            #region IValue<T>

            public V Value { get { return _value; } }

            #endregion

            public override string ToString() { return string.Format("({0}) {1} = '{2}'", ReflectionHelper.PrettyTypeName(GetType()), Name, Value); }
        }

        #endregion

    }

}
