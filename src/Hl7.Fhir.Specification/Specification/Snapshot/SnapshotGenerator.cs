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

namespace Hl7.Fhir.Specification.Snapshot
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
            if (!structure.IsConstraint) throw Error.Argument("structure", "structure is not a constraint or extension");
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


        private void merge(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var matches = (new ElementMatcher()).Match(snapNav, diffNav);

            foreach (var match in matches)
            {
                if (!snapNav.ReturnToBookmark(match.BaseBookmark))
                    throw Error.InvalidOperation("Internal merging error: bookmark {0} in snap is no longer available", match.BaseBookmark);
                if (!diffNav.ReturnToBookmark(match.DiffBookmark))
                    throw Error.InvalidOperation("Internal merging error: bookmark {0} in diff is no longer available", match.DiffBookmark);

                if (match.Action == ElementMatcher.MatchAction.Add)
                {
                    // Find last entry in slice to add to the end
                    var current = snapNav.Path;
                    while (snapNav.Current.Path == current && snapNav.MoveToNext()) ;
                    var dest = snapNav.Bookmark();
                    snapNav.ReturnToBookmark(match.BaseBookmark);
                    snapNav.DuplicateAfter(dest);

                    mergeElement(snapNav, diffNav);
                    snapNav.Current.Slicing = null;         // Probably not good enough...
                }
                else if(match.Action == ElementMatcher.MatchAction.Merge)
                {
                    mergeElement(snapNav, diffNav);
                }
                else if(match.Action == ElementMatcher.MatchAction.Slice)
                {

                }
            }

        }


        private void mergeElement(ElementNavigator snap, ElementNavigator diff)
        {
            (new ElementDefnMerger(_markChanges)).Merge(snap.Current, diff.Current);

            if (diff.HasChildren && !snap.HasChildren)
            {
                // The differential moves into an element that has no children in the base.
                // This is allowable if the base's element has a nameReference or a TypeRef,
                // in which case needs to be expanded before we can move to the path indicated
                // by the differential

                // Note that since we merged the parent, a (shorthand) typeslice will already
                // have reduced the numer of types to 1. Still, if you don't do that, we cannot
                // accept constraints on children, need to select a single type first...
                if (snap.Current.Type.Count > 1)
                    throw new NotSupportedException("Differential has a constraint on a choice element {0}, but does so without using a type slice".FormatWith(diff.Path));

                expandBaseElement(snap, diff);
            }

            // Now, recursively merge the children
            merge(snap, diff);
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


        private void makeSlice(ElementNavigator snap, ElementNavigator diff)
        {
            // diff is now located at the first repeat of a slice, which is (possibly) the slice entry
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            if (!snap.Current.IsRepeating() && !snap.Current.IsChoice())
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

            if (slicingEntry.Slicing.Rules != Profile.SlicingRules.Closed)
            {
                // Slices that are open in some form need to repeat the original "base" definition,
                // so that the open slices have a place to "fit in"
                snap.InsertAfter((Profile.ElementComponent)slicingTemplate.DeepCopy());
            }

            //TODO: update / check the slice entry's min/max property to match what we've found in the slice group
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
