/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

#define NEWBUILDER
#define IM_MUTABLE_TEST

using System;

namespace Hl7.Fhir.Navigation
{
    /// <summary>
    /// Common generic interface for a navigation tree of type <typeparamref name="T"/>
    /// that supports node values of varying types.
    /// </summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <example><code>MyTree : IVariantNavigationTree&lt;MyTree&gt; { }</code></example>
    public interface IVariantNavigationTree<T> : INavigationTree<T>, IVariantTreeBuilder<T>, IValueProvider
        where T : IVariantNavigationTree<T> { }

    /// <summary>
    /// Abstract generic base class for a navigation tree of type <typeparamref name="T"/>
    /// that supports node values of varying types.
    /// </summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <example><code>MyTree : VariantNavigationTree&lt;MyTree&gt; { }</code></example>
    public abstract class VariantNavigationTree<T> : NavigationTree<T>, IVariantNavigationTree<T>
        where T : VariantNavigationTree<T>
    {
        protected VariantNavigationTree(T parent, T previousSibling, string name) : base(parent, previousSibling, name) { }

        #region ILinkedValueTreeBuilder<T>

        /// <summary>Add a new node with the specified name and value of type <typeparamref name="V"/> as the first child.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        public T AddFirstChild<V>(string name, V value) { return AddFirstChild(() => CreateNode(Self, null, name, value)); }

        /// <summary>Add a new node with the specified name and value of type <typeparamref name="V"/> as the next sibling.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new node of type <typeparamref name="T"/>.</returns>
        public T AddNextSibling<V>(string name, V value) { return AddNextSibling(() => CreateNode(Parent, Self, name, value)); }

        #endregion

        #region IValueProvider

        /// <summary>Returns the type of the value provided by the current tree node.</summary>
        public abstract Type ValueType { get; }

        /// <summary>Gets the instance value as an <see cref="object"/>.</summary>
        public abstract object ObjectValue { get; }

        #endregion

        #region Protected members

        /// <summary>Creates a new tree node of type <typeparamref name="T"/> with the specified value of type <typeparamref name="V"/>.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The value of the node.</param>
        /// <returns>A new node of type <typeparamref name="T"/>.</returns>
        protected abstract T CreateNode<V>(T parent, T previousSibling, string name, V value);

        #endregion
    }

    #region Mutable / Immutable subclasses - DEMO/TEST


#if DEBUG && IM_MUTABLE_TEST

    /// <summary>
    /// Common generic interface for a navigation tree node of type <typeparamref name="T"/>
    /// with an immutable variant value of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    /// <example><code>MyTree : IImmutableVariantNavigationTreeNode&lt;MyTree&gt; { }</code></example>
    public interface IImmutableVariantNavigationTreeNode<T, V> : IVariantNavigationTree<T>, IValueProvider<V>
        where T : IImmutableVariantNavigationTreeNode<T, V>
    { }

    /// <summary>
    /// Abstract generic base class for a navigation tree node of type <typeparamref name="T"/>
    /// with an immutable variant value of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    /// <example><code>MyTree : ImmutableVariantNavigationTreeNode&lt;MyTree&gt; { }</code></example>
    public abstract class ImmutableVariantNavigationTreeNode<T, V> : VariantNavigationTree<T>, IImmutableVariantNavigationTreeNode<T, V>
        where T : ImmutableVariantNavigationTreeNode<T, V>
    {
        private readonly V _value;

        protected ImmutableVariantNavigationTreeNode(T parent, T previousSibling, string name, V value)
            : base(parent, previousSibling, name)
        {
            _value = value;
        }

        #region IValueProvider

        /// <summary>Returns the type of the value provided by this instance, i.e. the type of <typeparamref name="V"/>.</summary>
        public sealed override Type ValueType { get { return typeof(V); } }

        /// <summary>Gets the instance value as an <see cref="object"/>.</summary>
        public override object ObjectValue { get { return Value; } }

        #endregion

        #region IValueProvider<V>

        /// <summary>Gets the node value of type <typeparamref name="V"/>.</summary>
        public virtual V Value { get { return _value; } }

        #endregion

        public override string ToString() { return string.Format("{0} = '{1}'", base.ToString(), Value); }
    }

    /// <summary>
    /// Common generic interface for a navigation tree node of type <typeparamref name="T"/>
    /// with an mutable variant value of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The type of tree.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    /// <example><code>MyTree : IMutableVariantNavigationTreeNode&lt;MyTree&gt; { }</code></example>
    public interface IMutableVariantNavigationTreeNode<T, V> : IVariantNavigationTree<T>, IMutableValueProvider<V>
        where T : IMutableVariantNavigationTreeNode<T, V>
    { }

    /// <summary>
    /// Abstract generic base class for a navigation tree node of type <typeparamref name="T"/>
    /// with an mutable variant value of type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="T">The tree type.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    /// <example><code>MyTree : MutableVariantNavigationTreeNode&lt;MyTree&gt; { }</code></example>
    public abstract class MutableVariantNavigationTreeNode<T, V> : VariantNavigationTree<T>, IMutableVariantNavigationTreeNode<T, V>
        where T : MutableVariantNavigationTreeNode<T, V>
    {
        protected MutableVariantNavigationTreeNode(T parent, T previousSibling, string name, V value)
            : base(parent, previousSibling, name)
        {
            Value = value;
        }

        #region IValueProvider

        /// <summary>Returns the type of the value provided by this instance, i.e. the type of <typeparamref name="V"/>.</summary>
        public sealed override Type ValueType { get { return typeof(V); } }

        /// <summary>Gets the instance value as an <see cref="object"/>.</summary>
        public override object ObjectValue { get { return Value; } }

        #endregion

        #region IValueProvider<V>

        /// <summary>Gets or sets the node value of type <typeparamref name="V"/>.</summary>
        public virtual V Value { get; set; }

        #endregion

        public override string ToString() { return string.Format("{0} = '{1}'", base.ToString(), Value); }
    }

#endif

    #endregion

}
