#define RESLICING

// UNSTABLE - Try to handle Chris Grenz's naming strategy
// #define RESLICING_SPARSE

/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20161012] Note: internal scope for unit testing

    /// <summary>
    /// Differential structures may contain paths that "skip" over parents. For our profile expansion logic,
    /// it's easier to have the skipped parents present. This class will insert these missing parent.
    /// Notice that these parent are just "stand ins", there's no
    /// slicing or ElementDefn information associated with them, so they should not have any 
    /// influence on the final snapshot form.
    /// </summary>
    internal class DifferentialTreeConstructor
    {
        /// <summary>Create a valid tree structure from a sparse differential element list by adding missing parent element definitions.</summary>
        /// <returns>A tree structure representing the differential component.</returns>
        /// <remarks>This method returns a new list of element definitions. The input elements list is not modified.</remarks>
        public static List<ElementDefinition> MakeTree(List<ElementDefinition> elements)
        {
            var dtc = new DifferentialTreeConstructor(elements);
            return dtc.makeTree();
        }

        List<ElementDefinition> _source;

        DifferentialTreeConstructor(List<ElementDefinition> elements) { _source = elements; }

        List<ElementDefinition> makeTree()
        {
            var diff = new List<ElementDefinition>(_source.DeepCopy());   // We're going to modify the differential

            if (diff.Count == 0 ) return diff;        // nothing to do

            var index = 0;

            while (index < diff.Count)
            {
                var thisPath = diff[index].Path;
                var prevPath = index > 0 ? diff[index - 1].Path : String.Empty;
#if RESLICING
                // [WMR 20161012] Also handle slicing and reslicing, match element (slice) names
                var thisName = diff[index].Name;
                var prevName = index > 0 ? diff[index - 1].Name : String.Empty;

                var parentName = getParentNameFromSliceName(thisName);      // e.g. "C/2.use" => "C/2"
#if RESLICING_SPARSE
                var baseSliceName = getBaseNamefromResliceName(thisName);   // e.g. "C/2.use" => "C"
#else
                var baseSliceName = parentName == null ? getBaseNamefromResliceName(thisName) : null;   // e.g. "C/2" => "C"
#endif

#endif
                if (thisPath.IndexOf('.') == -1)
                {
                    // I am a root node, just one segment of path, I need to be the first element
                    if (index != 0)
                    {
                        throw Error.InvalidOperation($"Error in snapshot generator. Differential has multiple roots at '{thisPath}'");
                    }

                    // Else, I am fine, proceed
                    index++;
                }
#if RESLICING
                else if (ElementDefinitionNavigator.IsSibling(prevPath, thisPath)
                    && (baseSliceName == null || baseSliceName == prevName)
#if RESLICING_SPARSE
                    && (parentName == null || parentName == getParentNameFromSliceName(prevName))
#endif
                    )
                {
                    // Diff contains both parent and child, continue with next element
                    index++;
                }
                else if (ElementDefinitionNavigator.IsDirectChildPath(prevPath, thisPath))
                {
#if RESLICING_SPARSE
                    if (string.IsNullOrEmpty(thisName)
                        || parentName == prevName
                    )
#endif
                    {
                        // Either the current element is not part of a slice, or it is part of the same slice as the previous element
                        // Note: Chris Grenz also assigns names to child elements of a slice, however, this is not mandated by the spec
                        // If child element has no name, then always assume it belongs to the previous parent element (sliced or not)
                        // Chris Grenz: if the child element and the previous parent element shares a common name prefix, then they belong to the same slice group
                        //
                        // Example:
                        //
                        // Path                     Name
                        // ---------------------------------
                        // Identifier               A
                        // Identifier.system        A.system

                        index++;
                    }
#if RESLICING_SPARSE
                    else
                    {
                        // The current and previous element do not belong to the same slice
                        // Emit a matching parent slice node for the current slice
                        Debug.Assert(prevPath == ElementDefinitionNavigator.GetParentPath(thisPath));

                        var parentElement = new ElementDefinition()
                        {
                            Path = prevPath, // ElementDefinitionNavigator.GetParentPath(thisPath),
                            Name = parentName
                        };
                        diff.Insert(index, parentElement);
                    }
#endif
                }
                else
                {
                    // Not a root, sibling or direct child of the previous element
                    // => Either a grand child or part of a disjoint subtree
                    // If the element is named, then assume that this is defines a new slice
                    // e.g. if the element is resliced  (e.g. "A/C"), then ensure that the parent slice element "A" exists

                    var parentPath = ElementDefinitionNavigator.GetParentPath(thisPath);

#if RESLICING_SPARSE
                    // If this is a named child of a (re)sliced element? e.g. "C/2.use" => parentName = "C/2"
                    // Then ensure that the parent element is introduced
                    if (parentName != null)
                    {
                        var parentElement = new ElementDefinition()
                        {
                            Path = parentPath,
                            Name = parentName
                        };
                        diff.Insert(index, parentElement);
                        // Now, we're not sure this parent has parents, so proceed by checking the parent we have just inserted
                        // so -> index is untouched
                    }
                    
                    // If the element represents a reslice (e.g. "C/2"), then ensure that the base slice is introduced ("C")
                    else
#endif

                    if (baseSliceName != null && !sliceIsIntroduced(diff, index, thisPath, baseSliceName))
                    {
                        var parentElement = new ElementDefinition()
                        {
                            Path = thisPath,
                            Name = baseSliceName
                        };
                        diff.Insert(index, parentElement);
                        // Now, we're not sure this parent has parents, so proceed by checking the parent we have just inserted
                        // so -> index is untouched
                    }
                    else if (string.IsNullOrEmpty(prevPath) || !prevPath.StartsWith(parentPath + "."))
                    {
                        Debug.Assert(parentName == null);
                        var parentElement = new ElementDefinition()
                        {
                            Path = parentPath
                        };
                        diff.Insert(index, parentElement);
                        // Now, we're not sure this parent has parents, so proceed by checking the parent we have just inserted
                        // so -> index is untouched
                    }
                    else
                    {
                        // So, my predecessor and I share ancestry, of which I am sure it has been inserted by this algorithm
                        // before because of my predecessor, so we're fine.
                        index++;
                    }

                }
#else
                else if (ElementDefinitionNavigator.IsSibling(thisPath, prevPath) || ElementDefinitionNavigator.IsDirectChildPath(prevPath, thisPath))
                {
                    // The previous path is a sibling, or my direct parent, so everything is alright, proceed to next node
                    index++;
                }
                else
                {
                    var parentPath = ElementDefinitionNavigator.GetParentPath(thisPath);

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
                        // So, my predecessor and I share ancestry, of which I am sure it has been inserted by this algorithm
                        // before because of my predecessor, so we're fine.
                        index++;
                    }
                }
#endif
            }

            return diff;
        }

        // Returns substring before the last "." character, or null
        // e.g. "SliceName/ResliceName.childElementName" => "SliceName/ResliceName"
        static string getParentNameFromSliceName(string sliceName) => substringBeforeLast(sliceName, ".");

        // Returns substring before the last "/" character, or null
        // e.g. "SliceName/ResliceName.childElementName" => "SliceName"
        static string getBaseNamefromResliceName(string sliceName) => substringBeforeLast(sliceName, "/");

        // Returns all characters upto (but excluding) the last occurance of the specified separator
        static string substringBeforeLast(string value, string separator)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var lastPos = value.LastIndexOf(separator);
                if (lastPos >= 0)
                {
                    return value.Substring(0, lastPos);
                }
            }
            return null;
        }

        // Determine if the element list has already introduced a slice element with the specified path and name
        static bool sliceIsIntroduced(IList<ElementDefinition> elements, int index, string path, string name)
        {
            // Scan all preceding elements with same path prefix
            while (--index >= 0)
            {
                var elem = elements[index];
                if (!elem.Path.StartsWith(path))
                {
                    // Different path prefix, stop scanning
                    break;
                }
                if (elem.Path.Length == path.Length && elem.Name == name)
                {
                    return true; // Match!
                }
            }
            // No match
            return false;
        }
    }     
}
