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
    //TODO: relative uri's may cause problems, and are maybe better fixed by making them absolute, eventually - when serializing a full profile
    // url's relative to that profile can then be "corrected" back to relative uri's. As well, relative uri's to contained resources will need the
    // contained resource to be copied over to the snapshot.
    internal class StructureExpander
    {
        public StructureExpander(Profile.ProfileStructureComponent structure, StructureLoader loader)
        {
            Structure = structure;
            _loader = loader;
        }

        public Profile.ProfileStructureComponent Structure {get; private set; }

        private StructureLoader _loader;

        public Profile.ProfileStructureComponent Expand(Profile.ProfileStructureComponent differential)
        {
            var baseStructure = _loader.LocateBaseStructure(differential.TypeElement);
            if (baseStructure == null) throw Error.InvalidOperation("Could not locate the base profile for type {0}", differential.TypeElement.ToString());

            var baseUri = StructureLoader.BuildBaseStructureUri(differential.TypeElement).ToString();

            var snapshot = (Profile.ProfileStructureComponent)baseStructure.DeepCopy();
            snapshot.SetStructureForm(StructureForm.Snapshot);
            snapshot.SetStructureBaseUri(baseUri.ToString());

            mergeStructure(snapshot, differential);

            var fullDifferential = new DifferentialTreeConstructor(differential).MakeTree();

            var snapNav = new ElementNavigator(snapshot);
            snapNav.MoveToFirstChild();

            var diffNav = new ElementNavigator(fullDifferential);
            diffNav.MoveToFirstChild();

            merge(snapNav, diffNav);

            //TODO: Merge search params?

            snapNav.CommitChanges();
            return snapshot;
        }


        private static void mergeStructure(Profile.ProfileStructureComponent snapshot, Profile.ProfileStructureComponent differential)
        {
            if (differential.Name != null) snapshot.Name = differential.Name;
            if (differential.Publish != null) snapshot.Publish = differential.Publish;
            if (differential.Purpose != null) snapshot.Purpose = differential.Purpose;
        }


        private void merge(ElementNavigator snap, ElementNavigator diff)
        {
            mergeElementAttributes(snap.Current, diff.Current);

            // If there are children, move into them, and recursively merge them
            if (diff.MoveToFirstChild())
            {
                if (!snap.HasChildren)
                {
                    // The differential moves into an element that has no children in the base.
                    // This is allowable if the base's element has a nameReference or a TypeRef,
                    // in which case needs to be expanded before we can move to the path indicated
                    // by the differential
                    expandBaseElement(snap, diff);
                }

                // Due to how MoveToFirstChild() works, we have to move to the first matching *child*
                // when entering the loop for the first time, after that we can look for the next
                // matching *sibling*.
                bool firstEntry = true;

                do
                {
                    if( (firstEntry && !snap.MoveToChild(diff.PathName)) ||
                        (!firstEntry && !snap.MoveToNext(diff.PathName)) )
                             throw Error.InvalidOperation("Differential has a constraint for path {0}, which does not exist in its base", diff.PathName);                   
                    firstEntry = false;

                    // Child found in both, merge them
                    if (childNameRepeats(diff) || diff.Current.IsExtension())
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

        private void mergeSlice(ElementNavigator snap, ElementNavigator diff)
        {
            // diff is now located at the first repeat of a slice, which is (possibly) the slice entry
            // snap is located at the base definition of the element that will become sliced. But snap is not yet sliced.

            // Before we start, is the base element sliceable?
            if (!snap.Current.IsRepeating() && !isSlicedToOne(diff.Current))
                throw Error.InvalidOperation("The slicing entry in the differential at {0} indicates an unbounded slice, but the base element is not a repeating element",
                   diff.Current.Path);
           
            Profile.ElementComponent slicingEntry;

            // Yes, so, first, add the slicing entry to the snapshot. 
            if (diff.Current.Slicing != null)
            {
                slicingEntry = createSliceEntry(snap.Current, diff.Current);
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
                snap.InsertBefore(slicingEntry);
            }

            snap.MoveToNext();  

            // The differential and the snapshot are now both positioned on the first "real" slicing content element
            // Start by getting an unaltered copy of the current base definition, we need to re-insert a fresh copy
            // of it every time we encounter a new slice in the differential

            var slicingTemplate = (Profile.ElementComponent)snap.Current.DeepCopy();
            var slicingName = snap.PathName;
            var first = true;

            do
            {
                if(first)
                {
                    // The first time, we still have the original base definition available to slice
                    first = false;
                }
                else
                {
                    snap.InsertAfter((Profile.ElementComponent)slicingTemplate.DeepCopy());
                    //snap.MoveToNext();
                }

                merge(snap, diff);
            }
            while (diff.MoveToNext(slicingName));

            //if (slicingEntry.Slicing.Rules != Profile.SlicingRules.Closed)
            //{
            //    // Slices that are open in some form need to repeat the original "base" definition,
            //    // so that the open slices have a place to "fit in"
            //    snap.InsertAfter((Profile.ElementComponent)slicingTemplate.DeepCopy());
            //}

            //TODO: update/check the slice entry's min/max property to match what we've found in the slice group
        }


        private Profile.ElementComponent createExtensionSlicingEntry(string path, Profile.ElementComponent template)
        {
            // Create a pre-fab extension slice, filled with sensible defaults
            // Extensions will repeat their definitions in their slicing entry
            var result = (Profile.ElementComponent)template.DeepCopy();
            //var result = new Profile.ElementComponent();
            //result.Path = path;
            result.Slicing = new Profile.ElementSlicingComponent();
            result.Slicing.Discriminator = "url";
            result.Slicing.Ordered = false;
            result.Slicing.Rules = Profile.SlicingRules.Open;

            return result;
        }

        private void expandBaseElement(ElementNavigator snap, ElementNavigator diff)
        {
            snap.ExpandElement(_loader);

            if (!snap.HasChildren)
            {
                // Snapshot's element turns out not to be expandable, so we can't move to the desired path
                throw Error.InvalidOperation("Differential has nested constraints for node {0}, but this is a leaf node in base", diff.Path);
            }
        }

        private static bool childNameRepeats(ElementNavigator diff)
        {
            var isSliced = false;

            var currentPath = diff.PathName;
            if (diff.MoveToNext())
            {
                // check whether the next sibling in the differential has the same name,
                // that means we're looking at a slice
                isSliced = diff.PathName == currentPath;
                diff.MoveToPrevious();
            }

            return isSliced;
        }

        private void mergeElementAttributes(Profile.ElementComponent dest, Profile.ElementComponent src)
        {
            // Check whether this is an empty parent inserted into the differential form to create a full tree representation
            // without skips
            var isFillerParent = src.Definition == null && src.Slicing == null;
            if (isFillerParent) return;   // nothing to do, parent remains unchanged

            if (src.Name != null) dest.Name = src.Name;

            // You cannot change Element.representation in a derived profile

            if (src.Definition != null)
            {
                if (dest.Definition == null) dest.Definition = new Profile.ElementDefinitionComponent();

                mergeElementDefnAttributes(dest.Definition, src.Definition);
            }

            if(src.Slicing != null) dest.Slicing = (Profile.ElementSlicingComponent)src.Slicing.DeepCopy();
        }


        private Profile.ElementComponent createSliceEntry(Profile.ElementComponent baseDefn, Profile.ElementComponent diff)
        {
            var slicingEntry = new Profile.ElementComponent();

            slicingEntry.PathElement = (FhirString)baseDefn.PathElement.DeepCopy();
            if (diff.Name != null) slicingEntry.NameElement = (FhirString)diff.NameElement.DeepCopy();

            if (diff.Slicing != null) slicingEntry.Slicing = (Profile.ElementSlicingComponent)diff.Slicing.DeepCopy();
            
            slicingEntry.Definition = (Profile.ElementDefinitionComponent)baseDefn.Definition.DeepCopy();

            // If the differential overrides the elementdefn, only some of the fields go into the slicing entry
            if (diff.Definition != null)
            {
                if (diff.Definition.CommentsElement != null) slicingEntry.Definition.CommentsElement = (FhirString)diff.Definition.CommentsElement.DeepCopy();
                if (diff.Definition.ShortElement != null) slicingEntry.Definition.ShortElement = (FhirString)diff.Definition.ShortElement.DeepCopy();
                if (diff.Definition.FormalElement != null) slicingEntry.Definition.FormalElement = (FhirString)diff.Definition.FormalElement.DeepCopy();
                if (diff.Definition.MinElement != null) slicingEntry.Definition.MinElement = (Integer)diff.Definition.MinElement.DeepCopy();
                if (diff.Definition.MaxElement != null) slicingEntry.Definition.MaxElement = (FhirString)diff.Definition.MaxElement.DeepCopy();
                slicingEntry.Definition.IsModifierElement = (FhirBoolean)baseDefn.Definition.IsModifierElement.DeepCopy();
            }

            return slicingEntry;
        }


        private void mergeElementDefnAttributes(Profile.ElementDefinitionComponent snap, Profile.ElementDefinitionComponent diff)
        {
            if (diff.ShortElement != null) snap.ShortElement = (FhirString)diff.ShortElement.DeepCopy();
            if (diff.FormalElement != null) snap.FormalElement = (FhirString)diff.FormalElement.DeepCopy();
            if (diff.CommentsElement != null) snap.CommentsElement = (FhirString)diff.CommentsElement.DeepCopy();
            if (diff.RequirementsElement != null) snap.RequirementsElement = (FhirString)diff.RequirementsElement.DeepCopy();

            if(diff.SynonymElement != null)
            {
                if(snap.SynonymElement == null) snap.SynonymElement = new List<FhirString>();

                // Add new synonyms to the snap, and replace existing ones
                foreach(var dsyn in diff.SynonymElement)
                {
                    snap.SynonymElement.Remove(snap.SynonymElement.FirstOrDefault(ssyn => ssyn.Value == dsyn.Value));
                    snap.SynonymElement.Add((FhirString)dsyn.DeepCopy());
                }
                // Original code from Java leaves snapshot untouched when encountering an existing synonym,but
                // this means differential can not override e.g. extensions in such synonyms.
                //var newSynonyms = from dsyn in diff.SynonymElement
                //                  where !snap.SynonymElement.Any(ssyn => ssyn.Value == dsyn.Value)
                //                  select (FhirString)dsyn.DeepCopy();
                //snap.SynonymElement.AddRange(newSynonyms);
            }

            if (diff.MinElement != null) snap.MinElement =  (Integer)diff.MinElement.DeepCopy();
            if (diff.MaxElement != null) snap.MaxElement =  (FhirString)diff.MaxElement.DeepCopy();

            // ElementDefinition.nameReference cannot be overridden by a derived profile

            if(diff.Value != null) snap.Value = (Element)diff.Value.DeepCopy();
            if(diff.Example != null) snap.Example = (Element)diff.Example.DeepCopy();
            if(diff.MaxLengthElement != null) snap.MaxLengthElement =  (Integer)diff.MaxLengthElement.DeepCopy();

            // todo: [GG] what to do about conditions?  [EK] Since me made ElementDefinition.Constrain cumulative, these need to be cumulative too?
            if (diff.ConditionElement != null && diff.ConditionElement.Any())
            {
                if (snap.ConditionElement == null) snap.ConditionElement = new List<Id>();
                snap.ConditionElement.AddRange(diff.ConditionElement.DeepCopy());
            }

            if(diff.MustSupportElement != null) snap.MustSupportElement = (FhirBoolean)diff.MustSupportElement.DeepCopy();

            // ElementDefinition.isModifier cannot be overridden by a derived profle

            if(diff.Binding != null) snap.Binding = (Profile.ElementDefinitionBindingComponent)diff.Binding.DeepCopy();

            // todo: [GG] is this actually right?  [EK] I think it is, at least this is what Forge expects as well
            if(diff.Type != null && diff.Type.Any())
                snap.Type = new List<Profile.TypeRefComponent>(diff.Type.DeepCopy());
            
            // todo: [GG] mappings are cumulative - or does one replace another?
            if(diff.Mapping != null && diff.Mapping.Any())
            {
                if(snap.Mapping == null) snap.Mapping = new List<Profile.ElementDefinitionMappingComponent>();
                snap.Mapping.AddRange(diff.Mapping.DeepCopy());
            }

            // todo: constraints are cumulative - or does one replace another?
            if(diff.Constraint != null && diff.Constraint.Any())
            {
                if(snap.Constraint == null) snap.Constraint = new List<Profile.ElementDefinitionConstraintComponent>();
                snap.Constraint.AddRange(diff.Constraint.DeepCopy());
            }
        }


        private static bool isSlicedToOne(Profile.ElementComponent element)
        {
            // Note that in DSTU1, Slicing and Definition cannot both be non-null, but this is an error, and we really
            // need an Definition on a Slice entry element as well.
            return element.Slicing != null && 
                   element.Definition != null && 
                   element.Definition.Max == "1";
        }
    }
}
