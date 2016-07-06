/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Represents a FHIR navigation tree with variant mutable node values.</summary>
    public class FhirNavigationTree : VariantNavigationTree<FhirNavigationTree>, IMutableValueProvider
    {
        #region Public Factory Method

        /// <summary>Create a new <see cref="FhirNavigationTree"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A new <see cref="FhirNavigationTree"/> node.</returns>
        public static FhirNavigationTree Create(string name) { return new FhirNavigationTree(null, null, name); }

        #endregion

        protected FhirNavigationTree(FhirNavigationTree parent, FhirNavigationTree previousSibling, string name) : base(parent, previousSibling, name) { }

        #region IValueProvider

        /// <summary>Returns the type of the value provided by this instance, or <c>null</c>.</summary>
        public override Type ValueType { get { return null; } }

        /// <summary>Gets the instance value as an <see cref="object"/>.</summary>
        public override object ObjectValue { get { return null; } }

        #endregion

        protected override FhirNavigationTree Self { get { return this; } }

        protected override FhirNavigationTree CreateNode(FhirNavigationTree parent, FhirNavigationTree previousSibling, string name)
        {
            return new FhirNavigationTree(parent, previousSibling, name);
        }

        protected override FhirNavigationTree CreateNode<V>(FhirNavigationTree parent, FhirNavigationTree previousSibling, string name, V value)
        {
            return new Node<V>(name, value, parent, previousSibling);
        }

        /// <summary>Private subclass that implements the variant Value property.</summary>
        /// <typeparam name="V">The value type.</typeparam>
        class Node<V> : FhirNavigationTree, IValueProvider<V>
        {
            public Node(string name, V value, FhirNavigationTree parent, FhirNavigationTree previousSibling) : base(parent, previousSibling, name)
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
    }
}
