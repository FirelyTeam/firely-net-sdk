/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath.InstanceTree
{
    /// <summary>Represents a FHIR navigation tree with node values of type <see cref="IElementNavigator"/>.</summary>
    public class FhirInstanceTree : ValueNavigationTree<FhirInstanceTree, IValueProvider>
    {
        #region Public Factory Methods

        /// <summary>Create a new <see cref="FhirInstanceTree"/> root node with the specified name.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <returns>A new <see cref="FhirInstanceTree"/> node.</returns>
        public static FhirInstanceTree Create(string name) { return new FhirInstanceTree(null, null, name, null); }

        /// <summary>Create a new <see cref="FhirInstanceTree"/> node with the specified name and value.</summary>
        /// <param name="name">The name of the new node.</param>
        /// <param name="value">The node value.</param>
        /// <returns>A new <see cref="FhirInstanceTree"/> node.</returns>
        public static FhirInstanceTree Create(string name, IValueProvider value) { return new FhirInstanceTree(null, null, name, value); }

        #endregion

        protected FhirInstanceTree(FhirInstanceTree parent, FhirInstanceTree previousSibling, string name, IValueProvider value) : base(parent, previousSibling, name, value) { }

        protected override FhirInstanceTree Self { get { return this; } }

        protected override FhirInstanceTree CreateNode(FhirInstanceTree parent, FhirInstanceTree previousSibling, string name)
        {
            return new FhirInstanceTree(parent, previousSibling, name, null);
        }

        protected override FhirInstanceTree CreateNode(FhirInstanceTree parent, FhirInstanceTree previousSibling, string name, IValueProvider value)
        {
            return new FhirInstanceTree(parent, previousSibling, name, value);
        }

        //object IValueProvider.Value
        //{
        //    get
        //    {
        //        if (Self.Value != null)
        //            return Self.Value.Value;
        //        else
        //            return null;
        //    }
        //}

        //[Obsolete("This method will be removed from the interface of IFluentPathElement")]
        //IEnumerable<ChildNode> IFluentPathElement.Children()
        //{
        //    return LinkedTreeExtensions.Children(this).Select(c => new ChildNode(c.Name, c));
        //}

        //IEnumerable<string> IElementNavigator.GetChildNames()
        //{
        //    return LinkedTreeExtensions.Children(this).Select(c => c.Name);
        //}

        //IEnumerable<IElementNavigator> IElementNavigator.GetChildrenByName(string name)
        //{
        //    return LinkedTreeExtensions.Children(this).Where(c => c.Name == name);
        //}

        // REFACTORED: Parent is removed.
        //IFluentPathElement IFluentPathElement.Parent
        //{
        //    get { return Parent; }
        //}

    }
}
