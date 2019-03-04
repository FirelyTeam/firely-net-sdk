/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */
 
// #define DUMPOUTPUT

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    /// <summary>
    /// Differential structures may contain paths that "skip" over parents. For our profile expansion logic,
    /// it's easier to have the skipped parents present. This class will insert these missing parents.
    /// Notice that these parent are just "stand ins", there's no
    /// slicing or ElementDefn information associated with them, so they should not have any 
    /// influence on the final snapshot form.
    /// </summary>
    public static class DifferentialTreeConstructor
    {
        // [WMR 20180816] NEW: Expose public extension method on DifferentialComponent
        // Suggested by Brendan Kowitz (https://github.com/brendankowitz)

        /// <summary>Create a valid tree structure from a sparse differential element list by adding missing parent element definitions.</summary>
        /// <returns>A list of <see cref="ElementDefinition"/> instances that represent a tree structure.</returns>
        /// <remarks>This method returns a new list of element definitions. The input elements list is not modified.</remarks>
        public static List<ElementDefinition> MakeTree(this StructureDefinition.DifferentialComponent diff)
        {
            return MakeTree(diff.Element);
        }

        /// <summary>Create a valid tree structure from a sparse differential element list by adding missing parent element definitions.</summary>
        /// <returns>A list of <see cref="ElementDefinition"/> instances that represent a tree structure.</returns>
        /// <remarks>This method returns a new list of element definitions. The input elements list is not modified.</remarks>
        public static List<ElementDefinition> MakeTree(List<ElementDefinition> elements)
        {
            var diff = new List<ElementDefinition>(elements.DeepCopy());   // We're going to modify the differential

            if (diff.Count == 0 ) return diff;        // nothing to do

            var index = 0;

            while (index < diff.Count)
            {
                var thisPath = diff[index].Path;
                var prevPath = index > 0 ? diff[index - 1].Path : String.Empty;

                if (ElementDefinitionNavigator.IsRootPath(thisPath))
                {
                    // Root node must be the first element
                    if (index != 0)
                    {
                        // TODO: Emit OperationOutcome Issue, abort
                        throw Error.InvalidOperation($"Error in snapshot generator. Differential has multiple roots at '{thisPath}'");
                    }
                    // OK, proceed to next element
                    index++;
                }
                else if (ElementDefinitionNavigator.IsSibling(thisPath, prevPath))
                {
                    // The current element represents a sibling of the previous element
                    // Note: don't catch here, let the Snapshot Generator handle this
                    Debug.WriteLineIf(index > 0 && diff[index].Name != null && diff[index].Name == diff[index - 1].Name && diff[index - 1].Slicing == null, $"Warning! Duplicate constraint at index {index}: '{thisPath}'");

                    // So we have already ensured that the parent node exists while processing the previous element
                    // OK, proceed to the next element
                    index++;
                }
                else if (ElementDefinitionNavigator.IsDirectChildPath(prevPath, thisPath))
                {
                    // The previous element is our parent
                    // OK, proceed to the next element
                    index++;
                }
                else
                {
                    // - (Grand) parent element => OK, no action
                    // - Grand child of prev => add parent
                    // - (Grand) child of a sibling => add parent

                    var parentPath = ElementDefinitionNavigator.GetParentPath(thisPath);
                    if (prevPath != null && ElementDefinitionNavigator.IsChildPath(parentPath, prevPath))
                    {
                        // Current element and previous element share common ancestry
                        // Our parent element is also a (grand) parent of the previous element
                        // So we have already ensured that the parent exists.

                        // Verify: if the previous element is a child of the current element, then
                        // we are going up in the hierarchy to a parent path that we have already processed
                        // Note: don't catch here, let the Snapshot Generator handle this
                        // => Must be a slice
                        Debug.WriteLineIf(ElementDefinitionNavigator.IsChildPath(thisPath, prevPath) && diff[index].Name == null, $"Warning: unnamed slice for element {index} : '{thisPath}'");

                        // OK, proceed to next element
                        index++;
                    }
                    else
                    {
                        // Current element is a grand child of the previous element, or a grand child of a sibling element
                        // In both cases we have to add the missing parent element

                        // We're missing a path part, insert an empty parent                    
                        var parentElement = new ElementDefinition() { Path = parentPath };
                        diff.Insert(index, parentElement);
                        // Now process the newly added parent element and ensure it has a parent (=> don't increase index!)
                    }
                }
            }

#if DEBUG && DUMPOUTPUT
            Debug.Print($"[{nameof(DifferentialTreeConstructor)}] results:\r\n" + string.Join(Environment.NewLine, diff.Select(e => $"  {e.Path} : {e.Name}")));
#endif

            return diff;
        }

    }     
}
