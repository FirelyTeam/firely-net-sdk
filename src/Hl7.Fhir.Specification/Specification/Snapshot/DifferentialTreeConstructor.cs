/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>
    /// Differential structures may contain paths that "skip" over parents. For our profile expansion logic,
    /// it's easier to have the skipped parents present. This class will insert these missing parent.
    /// Notice that these parent are just "stand ins", there's no
    /// slicing or ElementDefn information associated with them, so they should not have any 
    /// influence on the final snapshot form.
    /// </summary>
    internal class DifferentialTreeConstructor
    {
        private List<ElementDefinition> _source;

        public DifferentialTreeConstructor(List<ElementDefinition> elements)
        {
            _source = elements;
        }

        /// <summary>
        /// Creates a differential structure with all "skipped" parents filled in.
        /// </summary>
        /// <returns>The full tree structure representing the differential</returns>
        /// <remarks>This operation will not touch the source differential, but instead will return a new structure.</remarks>
        public List<ElementDefinition> MakeTree()
        {
            var diff = new List<ElementDefinition>(_source.DeepCopy());   // We're going to modify the differential

            if (diff.Count == 0 ) return diff;        // nothing to do

            var index = 0;

            while (index < diff.Count)
            {
                var thisPath = diff[index].Path;
                var prevPath = index > 0 ? diff[index - 1].Path : String.Empty;

                if (thisPath.IndexOf('.') == -1)
                {
                    // I am a root node, just one segment of path, I need to be the first element
                    if (index != 0) throw Error.InvalidOperation("Differential has multiple roots at " + thisPath);

                    // Else, I am fine, proceed
                    index++;
                }
                else if (ElementNavigator.IsSibling(thisPath, prevPath) || ElementNavigator.IsDirectChildPath(prevPath, thisPath))
                {
                    // The previous path is a sibling, or my direct parent, so everything is alright, proceed to next node
                    index++;
                }
                else
                {
                    var parentPath = ElementNavigator.GetParentPath(thisPath);

                    if (prevPath == String.Empty || !prevPath.StartsWith(parentPath + "."))
                    {
                        // We're missing a path part, insert an empty parent                    
                        var parentElement = new ElementDefinition() { Path = parentPath };
                        diff.Insert(index, parentElement);

                        // Now, we're not sure this parent has parents, so proceed by checking the parent we have just inserted
                        // so -> index is untouched
                    }
                    else
                    {
                        // So, my predecessor an I share ancestry, of which I am sure it has been inserted by this algorithm
                        // before because of my predecessor, so we're fine.
                        index++;
                    }
                }
            }

            return diff;
        }
    }     
}
