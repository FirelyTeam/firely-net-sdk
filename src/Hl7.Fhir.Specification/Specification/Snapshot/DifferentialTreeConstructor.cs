/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
 
// #define DUMPOUTPUT

// EXPERIMENTAL
//
// #define CGNAMING
//
// Handle Chris Grenz element naming system
// Name children of slices: [sliceName].[elementName]
// Example:
//   Path                       Name        Description
//   -------------------------------------------------------------
//   Patient.identifier                     slicing intro
//   Patient.identifier         bsn         first slice
//   Patient.identifier.use     bsn.use     child of first slice
//   Patient.identifier         bsn/1       first reslice
//   Patient.identifier.use     bsn/1.use   child of first reslice


// * spec: slices MUST have names - but don't expect this for extensions
//   => Reject non-extension slices without a name; emit OperationOutcome issue
//
// * Reslicing: name = "slice/reslice"
//   => "/" is illegal character in slice name!
//   No other special rules on names, so e.g. "." is allowed and has no special meaning...
//
// * Sibling
//   - If name equals previous element name (also both empty) => error! duplicate constraint => remove, emit OperationOutcome issue
//   - If no match, then this represents a new (re)slice; we already ensured that the previous sibling element has a parent => no action
// * Direct child
//   - Parent already exists, by definition => no action
//   - If the child has a name, then it represents a nested slice => no special action necessary
// * Otherwise we are moving to a grand child or into a separate disjoint subtree
//   - Usual rules apply, cf. direct child
//   - Compare parent path with previous element; if match, then no action; otherwise emit parent
//
// * change exceptions to operation issues...?

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Support;
using System.Diagnostics;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Specification.Snapshot
{
    // [WMR 20161012] Note: internal scope for unit testing

    /// <summary>
    /// Differential structures may contain paths that "skip" over parents. For our profile expansion logic,
    /// it's easier to have the skipped parents present. This class will insert these missing parents.
    /// Notice that these parent are just "stand ins", there's no
    /// slicing or ElementDefn information associated with them, so they should not have any 
    /// influence on the final snapshot form.
    /// </summary>
    public class DifferentialTreeConstructor
    {
        /// <summary>Create a valid tree structure from a sparse differential element list by adding missing parent element definitions.</summary>
        /// <returns>A tree structure representing the differential component.</returns>
        /// <remarks>This method returns a new list of element definitions. The input elements list is not modified.</remarks>
        public List<ElementDefinition> MakeTree(List<ElementDefinition> elements)
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

#if CGNAMING
                    // Handle Chris Grenz naming, e.g.
                    // - Patient.name.use : "officialName.use"
                    // - Patient.name.use : "maidenName.use"
                    // => Belongs to a different slice "maidenName", not present => add parent slice
                    var thisNameComponents = ParsedSliceName.Parse(diff[index]);
                    var prevNameComponents = ParsedSliceName.Parse(index > 0 ? diff[index - 1] : null);
                    if (thisNameComponents.ElementName != null && thisNameComponents.SliceName != prevNameComponents.SliceName)
                    {
                        var parentElement = new ElementDefinition()
                        {
                            Path = ElementDefinitionNavigator.GetParentPath(thisPath),
                            Name = thisNameComponents.SliceName
                        };
                        diff.Insert(index, parentElement);
                    }
                    else
                    {
                        index++;
                    }
#else
                    // So we have already ensured that the parent node exists while processing the previous element
                    // OK, proceed to the next element
                    index++;
#endif
                }
                else if (ElementDefinitionNavigator.IsDirectChildPath(prevPath, thisPath))
                {
#if CGNAMING
                    // Handle Chris Grenz naming, e.g.
                    // - Patient.identifier : "mrn"
                    // - Patient.identifier.use : "mrn/officialMRN.use"
                    // => Belongs to a different (re-)slice "mrn/officialMRN", not present => add parent (re)slice
                    var parentName = getSliceParentName(diff[index].Name);
                    var prevName = index > 0 ? diff[index - 1].Name : null;
                    if (parentName != null && parentName != prevName)
                    {
                        var parentElement = new ElementDefinition()
                        {
                            Path = prevPath, // ElementDefinitionNavigator.GetParentPath(thisPath),
                            Name = parentName
                        };
                        diff.Insert(index, parentElement);
                    }
                    else
                    {
                        index++;
                    }
#else
                    // The previous element is our parent
                    // OK, proceed to the next element
                    index++;
#endif
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

#if CGNAMING
        static string getSliceParentName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var slashPos = name.LastIndexOf("/");
                var dotPos = name.LastIndexOf(".");
                if (dotPos > slashPos)
                {
                    return name.Substring(0, dotPos);
                }
            }
            return null;
        }

        struct ParsedSliceName {
            public ParsedSliceName(string sliceName, string elementName) { this.SliceName = sliceName; this.ElementName = elementName; }
            // public readonly string BaseSliceName; // before "/"
            public readonly string SliceName;
            public readonly string ElementName;
            public static ParsedSliceName Parse(ElementDefinition element)
            {
                var name = element.Name;
                if (!string.IsNullOrEmpty(name))
                {
                    var last = ElementDefinitionNavigator.GetLastPathComponent(element.Path);
                    if (name.EndsWith("." + last))
                    {
                        return new ParsedSliceName(
                            name.Substring(0, name.Length - last.Length - 1),
                            name.Substring(name.Length - last.Length)
                        );
                    }
                }
                return new ParsedSliceName(name, null);
            }
        }

#endif

    }     
}
