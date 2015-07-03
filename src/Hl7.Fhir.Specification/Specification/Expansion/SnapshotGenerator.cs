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
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Specification.Expansion
{
    public class SnapshotGenerator
    {
        public const string CHANGED_BY_DIFF_EXT = "http://hl7.org/fhir/StructureDefinition/changedByDifferential";
        
        private ArtifactResolver _resolver;
        private bool _markChanges;

        public SnapshotGenerator(ArtifactResolver resolver, bool markChanges=false)
        {
            _resolver = resolver;
            _markChanges = markChanges;
        }
        
        public void Generate(StructureDefinition structure)
        {
            if (structure.Differential == null) throw Error.Argument("structure", "structure does not contain a differential specification");
            if (structure.Type != StructureDefinition.StructureDefinitionType.Constraint && structure.Type != StructureDefinition.StructureDefinitionType.Extension) throw Error.Argument("structure", "structure is not a constraint or extension but an " + structure.Type.ToString());
            if(structure.Base == null) throw Error.Argument("structure", "structure is a constraint, but no base has been specified");

            var differential = structure.Differential;

            var baseStructure = _resolver.GetStructureDefinition(structure.Base);

            if (baseStructure == null) throw Error.InvalidOperation("Could not locate the base StructureDefinition for url " + structure.Base);
            if (baseStructure.Snapshot == null) throw Error.InvalidOperation("Snapshot generator required the base at {0} to have a snapshot representation", structure.Base);

            var snapshot = (StructureDefinition.StructureDefinitionSnapshotComponent)baseStructure.Snapshot.DeepCopy();
            var snapNav = new ElementNavigator(snapshot.Element);
            snapNav.MoveToFirstChild();

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = new DifferentialTreeConstructor(differential.Element).MakeTree();
            var diffNav = new ElementNavigator(fullDifferential);
            diffNav.MoveToFirstChild();

            merge(snapNav, diffNav);
           
            structure.Snapshot = new StructureDefinition.StructureDefinitionSnapshotComponent() { Element = snapNav.ToListOfElements() };
        }


        private void merge(ElementNavigator snap, ElementNavigator diff)
        {
            (new ElementDefnMerger(_markChanges)).Merge(snap.Current, diff.Current);

            // If there are children, move into them, and recursively merge them
            if (diff.MoveToFirstChild())
            {
                if (!snap.HasChildren)
                {
                    // The differential moves into an element that has no children in the base.
                    // This is allowable if the base's element has a nameReference or a TypeRef,
                    // in which case needs to be expanded before we can move to the path indicated
                    // by the differential

                    if (snap.Current.Type.Count > 1)
                        throw new NotSupportedException("Differential has a constraint on a choice element {0}, but does so without using a type slice".FormatWith(diff.Path));

                    expandBaseElement(snap, diff);
                }

                // Due to how MoveToFirstChild() works, we have to move to the first matching *child*
                // when entering the loop for the first time, after that we can look for the next
                // matching *sibling*.
                bool firstEntry = true;

                do
                {
                    if ((firstEntry && !snap.MoveToChild(diff.PathName)) ||
                        (!firstEntry && !snap.MoveTo(diff.PathName)) ) // HACK: I don't think it should be allowed for a diff to list constraints in the wrong order...
                    {
                        throw Error.InvalidOperation("Differential has a constraint for path '{0}', which does not exist in its base", diff.Path);
                    }
                    firstEntry = false;

                    // Child found in both, merge them
                    if (countChildNameRepeats(diff) > 1 || diff.Current.IsExtension())
                    {
                        // The child in the diff repeats or we recognize it as an extension slice -> we're on the first element of a slice!
                        mergeSlice(snap, diff);
                    }
                    else
                        merge(snap, diff);
                }
                while (diff.MoveToNext());

                // After the merge, return the diff and snapho back to their original position
                diff.MoveToParent();
                snap.MoveToParent();
            }
        }


        private void expandBaseElement(ElementNavigator snap, ElementNavigator diff)
        {
            snap.ExpandElement(_resolver);

            if (!snap.HasChildren)
            {
                // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                throw Error.InvalidOperation("Differential has nested constraints for node {0}, but this is a leaf node in base", diff.Path);
            }
        }


        private static int countChildNameRepeats(ElementNavigator diff)
        {
            //TODO: We use this function to determine whether an element is sliced...doing this by counting repeats of elements
            //in the diff. However, when reslicing, the diff doesn't need to have repeating elements, and you have to derive from the
            //base (snapshot) that the element is sliced.

            var repeats = 1;

            var currentPath = diff.PathName;
            var bm = diff.Bookmark();
            while (diff.MoveToNext())
            {
                // check whether the next sibling in the differential has the same name,
                // that means we're looking at a slice
                if (diff.PathName == currentPath)
                    repeats++;
                else
                    break;
            }

            diff.ReturnToBookmark(bm);
            return repeats;
        }

        private void mergeSlice(ElementNavigator snap, ElementNavigator diff)
        {
            // diff is now located at the first repeat of a slice, which is (possibly) the slice entry
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            if(!snap.Current.IsRepeating() && !snap.Current.IsChoice())
                throw Error.InvalidOperation("The slicing entry in the differential at {0} indicates an slice, but the base element is not a repeating or choice element",
                   diff.Current.Path);

            ElementDefinition slicingEntry;

            // Yes, so, first, add the slicing entry to the snapshot. 
            if (diff.Current.Slicing != null)
            {
                slicingEntry = createSliceEntry(snap.Current, diff.Current);
                markChange(slicingEntry);
                snap.InsertBefore(slicingEntry);

                if (!diff.MoveToNext(diff.PathName))
                    throw Error.InvalidOperation("Slicing has no elements beyond the slicing entry");  // currently impossible to happen
            }
            else
            {
                // Mmmm....no slicing entry in the differential. This is only alloweable for extension slices, as a shorthand notation.                 
                if (!snap.Current.IsExtension())
                    throw Error.InvalidOperation("The slice group at {0} does not start with a slice entry element", diff.Current.Path);

                // In this case we insert a "prefab" extension slice.
                slicingEntry = createExtensionSlicingEntry(snap.Path, snap.Current);
                markChange(slicingEntry);
                snap.InsertBefore(slicingEntry);
            }

            snap.MoveToNext();

            // The differential and the snapshot are now both positioned on the first "real" slicing content element
            // Start by duplicating the current unsliced base definition as many times as we have slices, so we can
            // update these copies for each slice.
            var numSlices = countChildNameRepeats(diff);
            for (var count = 0; count < numSlices - 1; count++) snap.Duplicate();

            var slicingName = snap.PathName;

            do
            {
                merge(snap, diff);
            }
            while (diff.MoveToNext(slicingName) && snap.MoveToNext(slicingName));

            //if (slicingEntry.Slicing.Rules != Profile.SlicingRules.Closed)
            //{
            //    // Slices that are open in some form need to repeat the original "base" definition,
            //    // so that the open slices have a place to "fit in"
            //    snap.InsertAfter((Profile.ElementComponent)slicingTemplate.DeepCopy());
            //}

            //TODO: update/check the slice entry's min/max property to match what we've found in the slice group
        }

        private void markChange(Element snap)
        {
            if (_markChanges)
                snap.SetExtension(CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
        }


        private static bool isSlicedToOne(ElementDefinition element)
        {
            return element.Slicing != null && element.Max == "1";
        }


        private ElementDefinition createSliceEntry(ElementDefinition baseDefn, ElementDefinition diff)
        {
            var slicingEntry = (ElementDefinition)baseDefn.DeepCopy();

            (new ElementDefnMerger(_markChanges)).Merge(slicingEntry, diff);

            return slicingEntry;
        }

        private ElementDefinition createExtensionSlicingEntry(string path, ElementDefinition template)
        {
            // Create a pre-fab extension slice, filled with sensible defaults
            var slicingDiff = new ElementDefinition();
            slicingDiff.Slicing = new ElementDefinition.ElementDefinitionSlicingComponent();
            slicingDiff.Slicing.Discriminator = new[] { "url" };
            slicingDiff.Slicing.Ordered = false;
            slicingDiff.Slicing.Rules = ElementDefinition.SlicingRules.Open;

            var result = createSliceEntry(template, slicingDiff);

            return result;
        }
    }
}
