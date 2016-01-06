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
    /// <summary>
    /// Common generic interface for a navigation tree of type <typeparamref name="T"/>
    /// that supports uniformly typed node values.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <example><code>MyTree : IValueNavigationTree&lt;MyTree&gt; { }</code></example>
    public interface IValueNavigationTree<T> : INavigationTree<T>
        where T : IValueNavigationTree<T>
    { }

    /// <summary>
    /// Common generic interface for a navigation tree of type <typeparamref name="T"/>
    /// that supports uniformly typed node values of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    /// <example><code>MyTree : IValueNavigationTree&lt;MyTree, string&gt; { }</code></example>
    public interface IValueNavigationTree<T, V> : IValueNavigationTree<T>, IValueTreeBuilder<T, V>, IValueProvider<V>
        where T : IValueNavigationTree<T, V> { }

    /// <summary>
    /// Abstract generic base class for a navigation tree of type <typeparamref name="T"/>
    /// that supports uniformly typed mutable node values of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    /// <example><code>MyTree : ValueNavigationTree&lt;MyTree, string&gt; { }</code></example>
    public abstract class ValueNavigationTree<T, V> : NavigationTree<T>, IValueNavigationTree<T, V>, IMutableValueProvider<V>
        where T : ValueNavigationTree<T, V>
    {
        protected ValueNavigationTree(T parent, T previousSibling, string name, V value) : base(parent, previousSibling, name)
        {
            Value = value;
        }

        #region ILinkedUniformValueTreeBuilder<T>

        /// <summary>Add a new node with the specified name and value of type <typeparamref name="V"/> as the first child.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        public T AddFirstChild(string name, V value) { return AddFirstChild(() => CreateNode(Self, null, name, value)); }

        /// <summary>Add a new node with the specified name and value of type <typeparamref name="V"/> as the next sibling.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        public T AddNextSibling(string name, V value) { return AddNextSibling(() => CreateNode(Parent, Self, name, value)); }

        #endregion

        #region IValueProvider

        /// <summary>Returns the type of the value provided by this instance, i.e. the type of <typeparamref name="V"/>.</summary>
        public Type ValueType { get { return typeof(V); } }

        /// <summary>Gets the instance value as an <see cref="object"/>.</summary>
        public object ObjectValue { get { return Value; } }

        #endregion

        #region IValueProvider<V>

        /// <summary>Gets or sets the node value of type <typeparamref name="V"/>.</summary>
        public V Value { get; set; }

        #endregion

        #region Protected members

        /// <summary>Creates a new node for the specified generic value type <typeparamref name="T"/>.</summary>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The value of the node.</param>
        /// <returns>A new node of type <typeparamref name="T"/>.</returns>
        protected abstract T CreateNode(T parent, T previousSibling, string name, V value);

        #endregion

        public override string ToString() { return string.Format("{0} = '{1}'", base.ToString(), Value); }
    }
}
