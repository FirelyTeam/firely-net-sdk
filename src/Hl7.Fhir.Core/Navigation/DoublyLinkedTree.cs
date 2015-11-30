/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Common interface for a doubly linked tree item.</summary>
    public interface IDoublyLinkedTree : IDoublyLinkedTree<DoublyLinkedTree> { }

    /// <summary>Abstract base class for doubly linked tree items.</summary>
    public abstract class DoublyLinkedTree : IDoublyLinkedTree, ILinkedTreeBuilder<DoublyLinkedTree>
    {
        /// <summary>Static factory method. Creates a new tree root node with the specified name.</summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DoublyLinkedTree Create(string name)
        {
            return DoublyLinkedTreeNode.Create(name, null, null);
        }

        protected DoublyLinkedTree(string name) { Name = name; }

        protected DoublyLinkedTree(string name, DoublyLinkedTree parent, DoublyLinkedTree previousSibling) : this(name)
        {
            Parent = parent;
            PreviousSibling = previousSibling;
        }

        #region ITree

        /// <summary>The name of the tree item.</summary>
        public string Name { get; }

        /// <summary>Returns <c>true</c> if the tree item supports the <see cref="IValue"/> interface.</summary>
        public bool IsValue { get { return this is IValue; } }

        #endregion

        #region ILinkedTree

        /// <summary>Returns a reference to the next sibling tree item.</summary>
        public DoublyLinkedTree NextSibling { get; private set; }

        /// <summary>Returns a reference to the first child tree item.</summary>
        abstract public DoublyLinkedTree FirstChild { get; protected set; }

        /// <summary>Returns <c>true</c> if the instance is an internal tree node, i.e. if the item has at least one child.</summary>
        public bool IsInternal { get { return FirstChild != null; } }

        /// <summary>Returns <c>true</c> if the instance is a tree leaf item, i.e. if the item has no children.</summary>
        public bool IsLeaf { get { return FirstChild == null; } }

        #endregion

        #region DoublyLinkedTree

        /// <summary>Returns a reference to the parent tree item.</summary>
        public DoublyLinkedTree Parent { get; }

        /// <summary>Returns a reference to the previous sibling tree item.</summary>
        public DoublyLinkedTree PreviousSibling { get; }

        /// <summary>Returns <c>true</c> if the item represents a root node, i.e. if the item <see cref="Parent"/> reference equals <c>null</c>.</summary>
        public bool IsRoot { get { return Parent == null; } }

        #endregion

        #region ILinkedTreeBuilder

        /// <summary>Add a new sibling node with the specified name.</summary>
        /// <param name="name">The name of the new sibling node.</param>
        /// <returns>A reference to the new sibling node.</returns>
        public DoublyLinkedTree AddSibling(string name)
        {
            return AddSibling(last => CreateNode(name, Parent, last));
        }

        /// <summary>Add a new sibling leaf item with the specified name and value.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new sibling leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new sibling leaf item.</returns>
        public DoublyLinkedTree AddSibling<V>(string name, V value)
        {
            return AddSibling(last => CreateLeaf(name, value, Parent, last));
        }

        /// <summary>Add a new child node with the specified name.</summary>
        /// <param name="name">The name of the new child node.</param>
        /// <returns>A reference to the new child node.</returns>
        public DoublyLinkedTree AddChild(string name)
        {
            return AddChild(last => CreateNode(name, this, last));
        }

        /// <summary>Add a new child node with the specified name and value.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new child leaf item.</param>
        /// <param name="value">The item value.</param>
        /// <returns>A reference to the new child leaf item.</returns>
        public DoublyLinkedTree AddChild<V>(string name, V value)
        {
            return AddChild(last => CreateLeaf(name, value, this, last));
        }

        #endregion

        #region Private members

        private DoublyLinkedTree AddSibling(Func<DoublyLinkedTree, DoublyLinkedTree> factory)
        {
            var last = this.LastSibling();
            var node = factory(last);
            last.NextSibling = node;
            return node;
        }

        private DoublyLinkedTree AddChild(Func<DoublyLinkedTree, DoublyLinkedTree> factory)
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
                node = last.AddSibling(factory);
            }
            return node;
        }

        // Following members link the base class to the (private) derived classes below

        // delegate T CreateNode<T>(string name, T parent, T previousSibling) where T : ILinkedTree<T>;
        // delegate T CreateLeaf<T, V>(string name, V value, T parent, T previousSibling) where T : ILinkedTree<T>;

        private static DoublyLinkedTree CreateNode(string name, DoublyLinkedTree parent, DoublyLinkedTree last)
        {
            return DoublyLinkedTreeNode.Create(name, parent, last);
        }

        private static DoublyLinkedTree CreateLeaf<V>(string name, V value, DoublyLinkedTree parent, DoublyLinkedTree last)
        {
            return DoublyLinkedTreeLeaf<V>.Create(name, value, parent, last);
        }

        #endregion

    }


    /// <summary>
    /// Represents an internal node in a doubly linked tree structure.
    /// An internal node can have children, but cannot contain a value.
    /// </summary>
    public sealed class DoublyLinkedTreeNode : DoublyLinkedTree
    {
        internal static DoublyLinkedTreeNode Create(string name, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
        {
            return new DoublyLinkedTreeNode(name, parent, previousSibling);
        }

        private DoublyLinkedTreeNode(string name, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
            : base(name, parent, previousSibling)
        {
        }

        /// <summary>Returns a reference to the first child tree item.</summary>
        public override DoublyLinkedTree FirstChild { get; protected set; }

        public override string ToString() { return string.Format("({0}) {1}", ReflectionHelper.PrettyTypeName(GetType()), Name); }
    }

    /// <summary>
    /// Represents an leaf item in a doubly linked tree structure.
    /// A leaf item can contain a value, but cannot reference any children.
    /// </summary>
    public sealed class DoublyLinkedTreeLeaf<V> : DoublyLinkedTree, IValue<V>
    {
        internal static DoublyLinkedTreeLeaf<V> Create(string name, V value, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
        {
            return new DoublyLinkedTreeLeaf<V>(name, value, parent, previousSibling);
        }

        private DoublyLinkedTreeLeaf(string name, V value, DoublyLinkedTree parent, DoublyLinkedTree previousSibling)
            : base(name, parent, previousSibling)
        {
            Value = value;
        }

        /// <summary>This property always returns <c>null</c>, as leaf items have no children.</summary>
        /// <exception cref="InvalidOperationException">Thrown on assignment.</exception>
        public override DoublyLinkedTree FirstChild
        {
            get { return null; }
            protected set { throw new InvalidOperationException("You cannot add children to a tree leaf item."); }
        }

        #region IValue

        /// <summary>
        /// Returns the type of the value.
        /// You can access the value via the <see cref="IValue{V}"/> interface, where V is the returned value type.
        /// </summary>
        public Type ValueType { get { return typeof(V); } }

        /// <summary>Gets a value of type <typeparamref name="V"/>.</summary>
        public V Value { get; }

        #endregion

        public override string ToString() { return string.Format("({0}) {1} = '{2}'", ReflectionHelper.PrettyTypeName(GetType()), Name, Value); }
    }
}
