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
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;


namespace Hl7.Fhir.Specification.Snapshot
{
    internal class ElementMatcher
    {
        /// <summary>
        /// Will match up the children of the current element in diffNav to the children of the element in baseNav.
        /// </summary>
        /// <param name="baseNav"></param>
        /// <param name="diffNav"></param>
        /// <returns>Returns a list of Bookmark combinations, the first bookmark pointing to an element in the base,
        /// the second a bookmark in the diff that matches the bookmark in the base.</returns>
        /// <remarks>Will match slices to base elements, re-sliced slices to slices and type-slice shorthands to choie elements.       
        /// </remarks>
        public List<Tuple<Bookmark, Bookmark>> Match(ElementNavigator baseNav, ElementNavigator diffNav)
        {
            if (!baseNav.HasChildren) throw Error.Argument("baseNav", "Cannot match base to diff: base has no children");
            if (!diffNav.HasChildren) throw Error.Argument("diffNav", "Cannot match base to diff: diff has no children");

            var baseStartBM = baseNav.Bookmark();
            var diffStartBM = diffNav.Bookmark();

            baseNav.MoveToFirstChild();
            diffNav.MoveToFirstChild();

            try
            {
                bool foundMatch = false;
                // First, match directly -> try to find the child in base with the same name as the path in the diff
                if(baseNav.PathName == )
                if (baseNav.PathName != diffNav.PathName)
                    foundMatch baseNav.MoveTo(diffNav.PathName);

            }
            finally
            {
                baseNav.ReturnToBookmark(baseStartBM);
                diffNav.ReturnToBookmark(diffStartBM);
            }        
        }


        public List<Tuple<ElementDefinition, ElementDefinition>> Match(IList<ElementDefinition> baseElems, IList<ElementDefinition> diffElems)
        {
            throw new NotImplementedException();
        }
    }
}
