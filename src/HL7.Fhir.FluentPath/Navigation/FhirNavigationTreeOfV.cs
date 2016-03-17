/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.Fhir.Navigation
{
    /// <summary>Represents a FHIR navigation tree with node values of type <typeparamref name="V"/>.</summary>
    /// <typeparam name="V">The value type.</typeparam>
    public class FhirNavigationTree<V> : ValueNavigationTree<FhirNavigationTree<V>, V>
    {
        #region Public Factory Method

        /// <summary>Create a new <see cref="FhirNavigationTree{V}"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A new <see cref="FhirNavigationTree{V}"/> node.</returns>
        public static FhirNavigationTree<V> Create(string name, V value) { return new FhirNavigationTree<V>(null, null, name, value); }

        #endregion

        protected FhirNavigationTree(FhirNavigationTree<V> parent, FhirNavigationTree<V> previousSibling, string name, V value) : base(parent, previousSibling, name, value) { }

        protected override FhirNavigationTree<V> Self { get { return this; } }

        protected override FhirNavigationTree<V> CreateNode(FhirNavigationTree<V> parent, FhirNavigationTree<V> previousSibling, string name)
        {
            return new FhirNavigationTree<V>(parent, previousSibling, name, default(V));
        }

        protected override FhirNavigationTree<V> CreateNode(FhirNavigationTree<V> parent, FhirNavigationTree<V> previousSibling, string name, V value)
        {
            return new FhirNavigationTree<V>(parent, previousSibling, name, value);
        }
    }
}
