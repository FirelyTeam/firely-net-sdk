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
    public class FhirInstanceTree : FhirNavigationTreeWithMutableValue<string>
    {
#if false
        /// <summary>Create a new <see cref="FhirInstanceTree"/> root node.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A new <see cref="FhirInstanceTree"/> node.</returns>
        public new static FhirInstanceTree Create(string name, string value) { return new FhirInstanceTree(name, value); }
#endif
        protected FhirInstanceTree(string name, string value) : base(name, value) { }

        protected FhirInstanceTree(string name, string value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
                : base(name, value, parent, previousSibling)
        { }

        protected override FhirNavigationTree CreateNode(string name, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            return new FhirInstanceTree(name, null, parent, previousSibling);
        }

        protected override FhirNavigationTree CreateNode<V>(string name, V value, FhirNavigationTree parent, FhirNavigationTree previousSibling)
        {
            var s = value != null ? value.ToString() : null;
            return new FhirInstanceTree(name, s, parent, previousSibling);
        }
    }
}
