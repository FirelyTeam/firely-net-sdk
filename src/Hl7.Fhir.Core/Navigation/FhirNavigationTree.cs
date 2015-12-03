using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>
    /// Concrete implementation of a <see cref="NavigationTree{T}"/> for FHIR resources.
    /// Each node can be associated with a strongly typed value, via the generic <see cref="IValueProvider{T}"/> interface.
    /// </summary>
    public class FhirNavigationTree : NavigationTree<FhirNavigationTree>, ILinkedTreeBuilderWithValues<FhirNavigationTree>, IValueProvider
    {
        public static FhirNavigationTree Create(string name) { return new FhirNavigationTree(name); }

        protected FhirNavigationTree(string name) : base(name) { }

        protected FhirNavigationTree(string name, FhirNavigationTree parent, FhirNavigationTree previousSibling) : base(name, parent, previousSibling) { }

        protected override FhirNavigationTree Self { get { return this; } }

        protected override FhirNavigationTree CreateNode(string name, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return new FhirNavigationTree(name, parent, previousSibling);
        }

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

        /// <summary>Creates a new <see cref="FhirNavigationTree"/> node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The value of the node.</param>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <returns>A new <see cref="FhirNavigationTree"/> node.</returns>
        protected FhirNavigationTree CreateNode<V>(string name, V value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return FhirNavigationTree<V>.Create(name, value, parent, previousSibling);
        }

        #endregion
    }

    /// <summary>Represents a <see cref="FhirNavigationTree"/> node that supports a strongly-typed value.</summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class FhirNavigationTree<TValue> : FhirNavigationTree, IValueProvider<TValue>
    {
        private readonly TValue _value;

        internal static FhirNavigationTree<TValue> Create(string name, TValue value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return new FhirNavigationTree<TValue>(name, value, parent, previousSibling);
        }

        public FhirNavigationTree(string name) : base(name) { }

        protected FhirNavigationTree(string name, TValue value, FhirNavigationTree parent, FhirNavigationTree previousSibling) 
            : base(name, parent, previousSibling)
        {
            _value = value;
        }

        #region IValueProvider

        public override Type ValueType { get { return typeof(TValue); } }

        #endregion

        #region IValueProvider<V>

        /// <summary>Provides a value of type <typeparamref name="TValue"/>.</summary>
        public TValue Value { get { return _value; } }

        #endregion
    }
}
