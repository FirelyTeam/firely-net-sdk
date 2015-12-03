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
    /// Concrete implementation of a <see cref="NavigationTree{T}"/> for FHIR instances.
    /// Supports nodes with mutable string values.
    /// </summary>
    public class FhirInstanceTree : NavigationTree<FhirInstanceTree>, ILinkedTreeBuilderWithValues<FhirInstanceTree, string>, IValueProvider
    {
        /// <summary>Create a new <see cref="FhirInstanceTree"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A new <see cref="FhirInstanceTree"/> node.</returns>
        public static FhirInstanceTree Create(string name) { return new FhirInstanceTree(name); }

        protected FhirInstanceTree(string name) : base(name) { }

        protected FhirInstanceTree(string name, FhirInstanceTree parent, FhirInstanceTree previousSibling) : base(name, parent, previousSibling) { }

        protected override FhirInstanceTree Self { get { return this; } }

        protected override FhirInstanceTree CreateNode(string name, FhirInstanceTree parent, FhirInstanceTree previousSibling)
        {
            return new FhirInstanceTree(name, parent, previousSibling);
        }

        #region ILinkedTreeBuilderWithValues<FhirInstanceTree, string>

        /// <summary>Add a new node with the specified name and value as the last sibling.</summary>
        /// <param name="name">The name of the new sibling node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new sibling node.</returns>
        public FhirInstanceTree AddLastSibling(string name, string value)
        {
            return AddLastSibling(last => CreateNode(name, value, Parent, last));
        }

        /// <summary>Add a new node with the specified name and value as the last child.</summary>
        /// <param name="name">The name of the new child node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A reference to the new child node.</returns>
        public FhirInstanceTree AddLastChild(string name, string value)
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

        /// <summary>Creates a new <see cref="FhirInstanceTree"/> node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The value of the node.</param>
        /// <param name="parent">A reference to the parent node.</param>
        /// <param name="previousSibling">A reference to the previous sibling node.</param>
        /// <returns>A new <see cref="FhirInstanceTree"/> node.</returns>
        protected FhirInstanceTree CreateNode(string name, string value, FhirInstanceTree parent, FhirInstanceTree previousSibling)
        {
            return FhirInstanceTreeWithValue.Create(name, value, parent, previousSibling);
        }

        #endregion

        /// <summary>Represents a <see cref="FhirInstanceTree"/> node that supports a string value.</summary>
        private class FhirInstanceTreeWithValue : FhirInstanceTree, IMutableValueProvider<string>
        {
            internal static FhirInstanceTreeWithValue Create(string name, string value, FhirInstanceTree parent, FhirInstanceTree previousSibling)
            {
                return new FhirInstanceTreeWithValue(name, value, parent, previousSibling);
            }

            protected FhirInstanceTreeWithValue(string name) : base(name) { }

            protected FhirInstanceTreeWithValue(string name, string value, FhirInstanceTree parent, FhirInstanceTree previousSibling)
                : base(name, parent, previousSibling)
            {
                Value = value;
            }

            #region IValueProvider

            public override Type ValueType { get { return typeof(string); } }

            #endregion

            #region IValueProvider<V>

            /// <summary>Gets or sets the node value.</summary>
            public string Value { get; set; }

            #endregion
        }
    }


}
