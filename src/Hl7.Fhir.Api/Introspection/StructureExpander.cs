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
using Hl7.Fhir.Introspection.Source;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;

namespace Hl7.Fhir.Introspection
{
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
            var baseUri = StructureLoader.BuildBaseStructureUri(differential.TypeElement).ToString();

            var snapshot = (Profile.ProfileStructureComponent)baseStructure.DeepCopy();
            snapshot.SetStructureForm(StructureForm.Snapshot);
            snapshot.SetStructureBaseUri(baseUri.ToString());

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

        private void merge(ElementNavigator snapshot, ElementNavigator diff)
        {
            mergeElementAttributes(snapshot.Current, diff.Current);

            // If there are children, move into them, and recursively merge them
            if (diff.MoveToFirstChild())
            {
                if (!snapshot.HasChildren)
                {
                    // The differential moves into an element that has no children in the base.
                    // However, this is possible if the base's element has a nameReference or a TypeRef,
                    // in which case it can be expanded
                    snapshot.ExpandElement(_loader.ArtifactSource);

                    if (!snapshot.HasChildren)
                    {
                        // Snapshot's element is not expandable, so the diff tries to move where it cannot go
                        throw Error.InvalidOperation("Differential has nested constraints for node {0}, but this is a leaf node in base", diff.Path);
                    }
                }

                do
                {
                    if (!snapshot.MoveToChild(diff.PathName))
                        throw Error.InvalidOperation("Differential has a constraint for path {0}, which does not exist in its base", diff.PathName);
                    else 
                    
                        // Child found in both, merge them
                    merge(snapshot, diff);

                    snapshot.MoveToParent();
                }
                while (diff.MoveToNext());

                // After the merge, return the diff and snapshot back to its original position
                diff.MoveToParent();
                
            }
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
    }
}
