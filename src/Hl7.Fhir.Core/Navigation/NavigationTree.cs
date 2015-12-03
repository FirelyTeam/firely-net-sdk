using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Common interface for a <see cref="NavigationTree{T}"/>.</summary>
    /// <typeparam name="T">The type of the implementing class.</typeparam>
    public interface INavigationTree<T> : IDoublyLinkedTree<T> where T : NavigationTree<T> { }

    /// <summary>Abstract base class for navigation trees.</summary>
    /// <typeparam name="T">The type of the derived class.</typeparam>
    public abstract class NavigationTree<T> : INavigationTree<T>, ILinkedTreeBuilder<T> where T : NavigationTree<T>
    {
        private readonly string _name;
        private readonly T _parent;
        private readonly T _previousSibling;

        /// <summary>Create a new tree root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A new <see cref="NavigationTree{T}"/> node.</returns>
        protected NavigationTree(string name) { _name = name; }

        /// <summary>Create a new tree node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        protected NavigationTree(string name, T parent, T previousSibling) : this(name)
        {
            _parent = parent;
            _previousSibling = previousSibling;
        }

        #region ITree

        /// <summary>The name of the tree node.</summary>
        public string Name { get { return _name; } }

        #endregion

        #region ILinkedTree

        /// <summary>Returns a reference to the next sibling node.</summary>
        public T NextSibling { get; private set; }

        /// <summary>Returns a reference to the first child node.</summary>
        public T FirstChild { get; private set; }

        /// <summary>Indexer property. Enumerates the child nodes with the specified name.</summary>
        /// <param name="name">An node name.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        public IEnumerable<T> this[string name] { get { return Self.Children(name); } }

        /// <summary>Indexer property. Enumerates the descendant nodes with the specified path.</summary>
        /// <param name="names">A sequence of path segments, i.e. nested node names.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> sequence.</returns>
        /// <example>
        /// Retrieve descendants of the specified node with path "child.grandchild.greatgrandchild":
        /// <list type="number">
        ///     <item><description>Find child nodes of the specified node with name "child"</description></item>
        ///     <item><description>Find child nodes in the previous result set with name "grandchild"</description></item>
        ///     <item><description>Find child nodes in the previous result set with name "greatgrandchild"</description></item>
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

        /// <summary>Indicates if the instance represents an internal tree node, i.e. if the node has at least one child.</summary>
        public bool IsInternal { get { return FirstChild != null; } }

        /// <summary>Indicates if the instance represents a tree leaf node, i.e. if the node has no children.</summary>
        public bool IsLeaf { get { return FirstChild == null; } }

        #endregion

        #region DoublyLinkedTree

        /// <summary>Returns a reference to the parent tree node.</summary>
        public T Parent { get { return _parent; } }

        /// <summary>Returns a reference to the previous sibling tree node.</summary>
        public T PreviousSibling { get { return _previousSibling; } }

        /// <summary>Indicates if the instance represents a root node, i.e. if the <see cref="Parent"/> reference equals <c>null</c>.</summary>
        public bool IsRoot { get { return Parent == null; } }

        #endregion

        #region ILinkedTreeBuilder

        /// <summary>Add a new node with the specified name as the last sibling.</summary>
        /// <param name="name">The name of the new sibling node.</param>
        /// <returns>A reference to the new sibling node of type <typeparamref name="T"/>.</returns>
        public T AddLastSibling(string name) { return AddLastSibling(last => CreateNode(name, Parent, last)); }

        /// <summary>Add a new node with the specified name as the last child.</summary>
        /// <param name="name">The name of the new child node.</param>
        /// <returns>A reference to the new child node of type <typeparamref name="T"/>.</returns>
        public T AddLastChild(string name) { return AddLastChild(last => CreateNode(name, Self, last)); }

        #endregion

        #region Protected members

        // Following property is for upcasting base reference to derived class

        /// <summary>Returns a self-reference to the current instance of type <typeparamref name="T"/>.</summary>
        /// <example><code>protected override T Self { get { return this; } }</code></example>
        protected abstract T Self { get; } // (T)this;

        /// <summary>Creates a new tree node of type <typeparamref name="T"/>.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <returns>A new node instance of <typeparamref name="T"/>.</returns>
        protected abstract T CreateNode(string name, T parent, T previousSibling);

        /// <summary>Add a new node as the last sibling.</summary>
        /// <param name="factory">A factory delegate to create the new node.</param>
        /// <returns>A new node instance of <typeparamref name="T"/>.</returns>
        protected T AddLastSibling(Func<T, T> factory)
        {
            var last = Self.LastSibling();
            var node = factory(last);
            last.NextSibling = node;
            return node;
        }

        /// <summary>Add a new node as the last child.</summary>
        /// <param name="factory">A factory delegate to create the new node.</param>
        /// <returns>A new node instance of <typeparamref name="T"/>.</returns>
        protected T AddLastChild(Func<T, T> factory)
        {
            T node = null;
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

        #endregion

    }

}
