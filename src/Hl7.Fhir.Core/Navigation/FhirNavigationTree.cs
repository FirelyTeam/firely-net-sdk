/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>
    /// Concrete implementation of a <see cref="NavigationTree{T}"/> for FHIR resources.
    /// Supports nodes with immutable generic values of any type.
    /// </summary>
    public class FhirNavigationTree : NavigationTree<FhirNavigationTree>, ILinkedTreeBuilderWithValues<FhirNavigationTree>, IValueProvider, IAnnotatable
    {
        /// <summary>Create a new <see cref="FhirNavigationTree"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A new <see cref="FhirNavigationTree"/> node.</returns>
        public static FhirNavigationTree Create(string name) { return new FhirNavigationTree(name); }

        protected FhirNavigationTree(string name) : base(name) { }

        protected FhirNavigationTree(string name, FhirNavigationTree parent, FhirNavigationTree previousSibling) : base(name, parent, previousSibling) { }

        #region ILinkedTreeBuilderWithValues<FhirNavigationTree>

        public FhirNavigationTree AddLastSibling<V>(string name, V value)
        {
            return AddLastSibling(last => CreateNode(name, value, Parent, last));
        }

        public FhirNavigationTree AddLastChild<V>(string name, V value)
        {
            return AddLastChild(last => CreateNode(name, value, this, last));
        }

        #endregion

        #region IValueProvider

        /// <summary>
        /// Returns the type of the value provided by this instance, or <c>null</c>.
        /// The <see cref="IValueProvider{T}"/> interface provides strongly-typed access to the actual value.
        /// </summary>
        public virtual Type ValueType { get { return null; } }

        #endregion

        #region Protected members

        protected override FhirNavigationTree Self { get { return this; } }

        protected override FhirNavigationTree CreateNode(string name, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return new FhirNavigationTree(name, parent, previousSibling);
        }

        /// <summary>Creates a new <see cref="FhirNavigationTree"/> node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The value of the node.</param>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <returns>A new <see cref="FhirNavigationTree"/> node.</returns>
        protected virtual FhirNavigationTree CreateNode<V>(string name, V value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return FhirNavigationTreeWithValue<V>.Create(name, value, parent, previousSibling);
        }

        #endregion

        #region IAnnotatable

        private Lazy<AnnotationList> _annotations = new Lazy<AnnotationList>(() => new AnnotationList());

        public object Annotation(Type type)
        {
            return _annotations.Value.FilterByType(type).FirstOrDefault();
        }

        public T Annotation<T>() where T : class
        {
            return (T)Annotation(typeof(T));
        }

        public IEnumerable<object> Annotations(Type type)
        {
            return _annotations.Value.FilterByType(type).Cast<object>();
        }

        public IEnumerable<T> Annotations<T>() where T : class
        {
            return _annotations.Value.FilterByType(typeof(T)).Cast<T>();
        }

        public void AddAnnotation(object annotation)
        {
            _annotations.Value.AddAnnotation(annotation);
        }

        public void RemoveAnnotations(Type type)
        {
            _annotations.Value.RemoveAnnotation(type);
        }

        public void RemoveAnnotations<T>() where T : class
        {
            RemoveAnnotations(typeof(T));
        }

        #endregion

        public override string ToString()
        {
            return base.ToString() + (_annotations.IsValueCreated ? _annotations.Value.ToString() : "");
        }
    }

    /// <summary>Represents a <see cref="FhirNavigationTree"/> node with a strongly-typed immutable value.</summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class FhirNavigationTreeWithValue<TValue> : FhirNavigationTree, IValueProvider<TValue>
    {
        private readonly TValue _value;

        /// <summary>Create a new <see cref="FhirNavigationTreeWithValue{TValue}"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A new <see cref="FhirNavigationTreeWithValue{TValue}"/> node.</returns>
        public static FhirNavigationTreeWithValue<TValue> Create(string name, TValue value)
        {
            return new FhirNavigationTreeWithValue<TValue>(name, value);
        }

        internal static FhirNavigationTreeWithValue<TValue> Create(string name, TValue value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return new FhirNavigationTreeWithValue<TValue>(name, value, parent, previousSibling);
        }

        protected FhirNavigationTreeWithValue(string name, TValue value) : base(name) { _value = value; }

        protected FhirNavigationTreeWithValue(string name, TValue value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
            : base(name, parent, previousSibling)
        {
            _value = value;
        }

        #region IValueProvider

        public override Type ValueType { get { return typeof(TValue); } }

        #endregion

        #region IValueProvider<V>

        /// <summary>Gets the node value.</summary>
        public TValue Value { get { return _value; } }

        #endregion

        public override string ToString() { return string.Format("{0} = '{1}'", base.ToString(), Value); }
    }

    /// <summary>Represents a <see cref="FhirNavigationTree"/> node with a strongly-typed mutable value.</summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class FhirNavigationTreeWithMutableValue<TValue> : FhirNavigationTree, IMutableValueProvider<TValue>
    {
        /// <summary>Create a new <see cref="FhirNavigationTreeWithMutableValue{TValue}"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A new <see cref="FhirNavigationTreeWithMutableValue{TValue}"/> node.</returns>
        public static FhirNavigationTreeWithMutableValue<TValue> Create(string name, TValue value)
        {
            return new FhirNavigationTreeWithMutableValue<TValue>(name, value);
        }

        internal static FhirNavigationTreeWithMutableValue<TValue> Create(string name, TValue value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return new FhirNavigationTreeWithMutableValue<TValue>(name, value, parent, previousSibling);
        }

        protected FhirNavigationTreeWithMutableValue(string name, TValue value) : base(name) { Value = value; }

        protected FhirNavigationTreeWithMutableValue(string name, TValue value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
            : base(name, parent, previousSibling)
        {
            Value = value;
        }

        #region IValueProvider

        public override Type ValueType { get { return typeof(TValue); } }

        #endregion

        #region IValueProvider<V>

        /// <summary>Gets or sets the node value.</summary>
        public TValue Value { get; set; }

        #endregion
        
        public override string ToString() { return string.Format("{0} = '{1}'", base.ToString(), Value); }
    }
}
