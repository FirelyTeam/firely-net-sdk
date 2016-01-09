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
using System.Diagnostics;

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

            var snapshot = (StructureDefinition.SnapshotComponent)baseStructure.Snapshot.DeepCopy();
            generateBaseElements(snapshot.Element);
            var snapNav = new ElementNavigator(snapshot.Element);

            // Fill out the gaps (mostly missing parents) in the differential representation
            var fullDifferential = new DifferentialTreeConstructor(differential.Element).MakeTree();
            var diffNav = new ElementNavigator(fullDifferential);

            merge(snapNav, diffNav);
           
            structure.Snapshot = new StructureDefinition.SnapshotComponent() { Element = snapNav.ToListOfElements() };
        }


        private void merge(ElementNavigator snapNav, ElementNavigator diffNav)
        {
            var snapPos = snapNav.Bookmark();
            var diffPos = diffNav.Bookmark();

            try
            {
                var matches = (new ElementMatcher()).Match(snapNav, diffNav);

                //Debug.WriteLine("Matches for children of {0}".FormatWith(snapNav.Path));
                //matches.DumpMatches(snapNav, diffNav);

                foreach (var match in matches)
                {
                    if (!snapNav.ReturnToBookmark(match.BaseBookmark))
                        throw Error.InvalidOperation("Internal merging error: bookmark {0} in snap is no longer available", match.BaseBookmark);
                    if (!diffNav.ReturnToBookmark(match.DiffBookmark))
                        throw Error.InvalidOperation("Internal merging error: bookmark {0} in diff is no longer available", match.DiffBookmark);

                    if (match.Action == ElementMatcher.MatchAction.Add)
                    {
                        // TODO: move this logic to matcher, the Add should point to the last slice where
                        // the new slice will be added after.

                        // Find last entry in slice to add to the end
                        var current = snapNav.Path;
                        while (snapNav.Current.Path == current && snapNav.MoveToNext()) ;
                        snapNav.MoveToPrevious();       // take one step back...
                        var dest = snapNav.Bookmark();
                        snapNav.ReturnToBookmark(match.BaseBookmark);
                        snapNav.DuplicateAfter(dest);
                        markChange(snapNav.Current);

                        mergeElement(snapNav, diffNav);
                        snapNav.Current.Slicing = null;         // Probably not good enough...
                    }
                    else if (match.Action == ElementMatcher.MatchAction.Merge)
                    {
                        mergeElement(snapNav, diffNav);
                    }
                    else if (match.Action == ElementMatcher.MatchAction.Slice)
                    {
                        makeSlice(snapNav, diffNav);
                    }
                }
            }
            finally
            {
                snapNav.ReturnToBookmark(snapPos);
                diffNav.ReturnToBookmark(diffPos);
            }
        }


        private void mergeElement(ElementNavigator snap, ElementNavigator diff)
        {
            (new ElementDefnMerger(_markChanges)).Merge(snap.Current, diff.Current);

            if (diff.HasChildren)
            {
                if (!snap.HasChildren)
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

                    ExpandElement(snap, _resolver);

                    if (!snap.HasChildren)
                    {
                        // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                        throw Error.InvalidOperation("Differential has nested constraints for node {0}, but this is a leaf node in base", diff.Path);
                    }
                }

                // Now, recursively merge the children
                merge(snap, diff);
            }
        }


        private void makeSlice(ElementNavigator snap, ElementNavigator diff)
        {
            // diff is now located at the first repeat of a slice, which is normally the slice entry (Extension slices need
            // not have a slicing entry)
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            if (!snap.Current.IsRepeating() && !snap.Current.IsChoice())
                throw Error.InvalidOperation("The slicing entry in the differential at {0} indicates an slice, but the base element is not a repeating or choice element",
                   diff.Current.Path);

            ElementDefinition slicingEntry = diff.Current;

            //Mmmm....no slicing entry in the differential. This is only alloweable for extension slices, as a shorthand notation.
            if (slicingEntry.Slicing == null)
            {
                if (slicingEntry.IsExtension())
                {
                    // In this case we create a "prefab" extension slice (with just slincing info)
                    // that's simply merged with the original element in base
                    slicingEntry = createExtensionSlicingEntry();
                }
                else
                {
                    throw Error.InvalidOperation("The slice group at {0} does not start with a slice entry element", diff.Current.Path);
                }
            }

            (new ElementDefnMerger(_markChanges)).Merge(snap.Current, slicingEntry);

            ////TODO: update / check the slice entry's min/max property to match what we've found in the slice group
        }

        private void markChange(Element snap)
        {
            if (_markChanges)
                snap.SetExtension(CHANGED_BY_DIFF_EXT, new FhirBoolean(true));
        }


        private static ElementDefinition createExtensionSlicingEntry()
        {
            // Create a pre-fab extension slice, filled with sensible defaults
            var slicingDiff = new ElementDefinition();
            slicingDiff.Slicing = new ElementDefinition.SlicingComponent();
            slicingDiff.Slicing.Discriminator = new[] { "url" };
            slicingDiff.Slicing.Ordered = false;
            slicingDiff.Slicing.Rules = ElementDefinition.SlicingRules.Open;

            return slicingDiff;
        }


        internal static bool ExpandElement(ElementNavigator nav, ArtifactResolver resolver)
        {
            if (resolver == null) throw Error.ArgumentNull("source");
            if (nav.Current == null) throw Error.ArgumentNull("Navigator is not positioned on an element");

            if (nav.HasChildren) return true;     // already has children, we're not doing anything extra

            var defn = nav.Current;

            if (!String.IsNullOrEmpty(defn.NameReference))
            {
                var sourceNav = new ElementNavigator(nav);
                var success = sourceNav.JumpToNameReference(defn.NameReference);

                if (!success)
                    throw Error.InvalidOperation("Trying to navigate down a node that has a nameReference of '{0}', which cannot be found in the StructureDefinition".FormatWith(defn.NameReference));

                nav.CopyChildren(sourceNav);
            }
            else if (defn.Type != null && defn.Type.Count > 0)
            {
                if (defn.Type.Count > 1)
                    throw new NotSupportedException("Element at path {0} has a choice of types, cannot expand".FormatWith(nav.Path));
                else
                {
                    var coreType = resolver.GetStructureDefinitionForCoreType(defn.Type[0].Code.ToString());

                    if (coreType == null) throw Error.NotSupported("Trying to navigate down a node that has a declared base type of '{0}', which is unknown".FormatWith(defn.Type[0].Code));
                    if (coreType.Snapshot == null) throw Error.NotSupported("Found definition of base type '{0}', but is does not contain a snapshot representation".FormatWith(defn.Type[0].Code));

                    generateBaseElements(coreType.Snapshot.Element);
                    var sourceNav = new ElementNavigator(coreType.Snapshot.Element);
                    sourceNav.MoveToFirstChild();
                    nav.CopyChildren(sourceNav);
                }
            }

            return true;
        }

        private static void generateBaseElements(IEnumerable<ElementDefinition> elements)
        {
            foreach(var element in elements)
            {
                if (element.Base == null)
                {
                    element.Base = new ElementDefinition.BaseComponent()
                    {
                        MaxElement = (FhirString)element.MaxElement.DeepCopy(),
                        MinElement = (Integer)element.MinElement.DeepCopy(),
                        PathElement = (FhirString)element.PathElement.DeepCopy()
                    };
                }
            }
        }
    }
}
